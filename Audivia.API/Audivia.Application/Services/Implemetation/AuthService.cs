using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Auth;
using Audivia.Domain.ModelRequests.Mail;
using Audivia.Domain.ModelResponses.Auth;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;


namespace Audivia.Application.Services.Implemetation
{
    public class AuthService : IAuthService
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IRoleRepository roleRepository, IMailService mailService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            if (await _userRepository.GetByEmail(request.Email) != null)
            {
                throw new HttpRequestException("Email existed!");
            }

            if (await _userRepository.GetByUsername(request.UserName) != null)
            {
                throw new HttpRequestException("Username existed!");
            }

            var customerRole = await _roleRepository.GetByRoleName("customer");
            var user = new User
            {
                Email = request.Email,
                Username = request.UserName,
                Password = PasswordHasher.HashPassword(request.Password),
                RoleId = customerRole.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            user.TokenConfirmEmail = Guid.NewGuid().ToString();

            await _userRepository.Create(user);

            await _mailService.SendEmailAsync(new MailRequest()
            {
                ToEmail = user.Email,
                Body = EmailContent.ConfirmEmail(user.Username, _configuration, user.TokenConfirmEmail),
                Subject = "[Audivia] Confirm Email"
            });

            return new RegisterResponse
            {
                Message = "Registered successfully!",
                Response = ModelMapper.MapUserToDTO(user),
                Success = true
            };
        }

        public async Task<ConfirmEmailResponse> VerifyEmail(ConfirmEmailRequest request)
        {
            var user = await _userRepository.GetByTokenConfirm(request.Token);
            if (user == null)
            {
                throw new KeyNotFoundException("Token is invalid! User not found!");
            }
            user.ConfirmedEmail = true;
            user.TokenConfirmEmail = "";
            await _userRepository.Update(user);
            await _mailService.SendEmailAsync(new MailRequest
            {
                ToEmail = user.Email,
                Subject = "[Audivia] Welcome to Audivia",
                Body = EmailContent.WelcomeEmail(user.Username ?? "New Customer")
            });
            return new ConfirmEmailResponse { StatusCode = 200, IsSuccess = true, Message = "Xác thực thành công!" };
        }

        public async Task<LoginResponse> LoginWithEmailAndPassword(LoginRequest request)
        {
            var user = await _userRepository.GetByEmail(request.Email)
                        ?? throw new KeyNotFoundException("Incorrect email or password!");
            bool isPasswordMatched = PasswordHasher.VerifyPassword(request.Password, user.Password);
            if (!isPasswordMatched)
            {
                throw new KeyNotFoundException("Incorrect email or password!");
            }
            if (!user.ConfirmedEmail)
            {
                throw new HttpRequestException("You must check the mailbox to confirm email before login!");
            }

            // generate access token and response
            var accessToken = await GenerateAccessToken(user);
            var refeshToken = GenerateRefreshToken(request.Email);
            return new LoginResponse()
            {
                Message = "Login successfully!",
                AccessToken = accessToken,
                RefreshToken = refeshToken
            };
        }

        public async Task<UserDTO?> GetCurrentUserAsync(ClaimsPrincipal userClaims)
        {
            var email = userClaims.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return null;

            var user = await _userRepository.FindFirst(u => u.Email == email);
            if (user == null)
                return null;

            var roleName = userClaims.FindFirst(ClaimTypes.Role)?.Value;

            return ModelMapper.MapUserToDTO(user, roleName);
        }

        private async Task<string> GenerateAccessToken(User user)
        {
            Role? role = await _roleRepository.FindFirst(role => role.Id == user.RoleId);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email != null ? user.Email.ToString() : ""),
                new Claim(ClaimTypes.Role, role.RoleName),
                new Claim("fullName", user.FullName ?? ""),
                new Claim("phone", user.Phone ?? ""),
                new Claim("avatarUrl",user.AvatarUrl ?? ""),
                new Claim("coverPhoto", user.CoverPhoto ?? ""),
                new Claim("bio", user.Bio ?? ""),
                new Claim("balanceWallet", user.BalanceWallet.ToString()),
                new Claim("audioCharacterId", user.AudioCharacterId ?? ""),
                new Claim("autoPlayDistance", user.AutoPlayDistance != null ? user.AutoPlayDistance.ToString() : "")
            };


            var accessToken = JWTUtils.CreateToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }


        private string GenerateRefreshToken(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };
            var refreshToken = JWTUtils.CreateRefreshToken(claims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }

        public async Task<UserDTO?> GetCurrentUserAsync()
        {
            var userClaims = _httpContextAccessor.HttpContext?.User;
            if (userClaims == null)
                return null;

            return await GetCurrentUserAsync(userClaims); // gọi lại hàm cũ
        }

        public async Task<LoginResponse> LoginWithGoogle(string token)
        {
            var clientId = _configuration["GoogleAuth:ClientId"];
            GoogleJsonWebSignature.Payload googlePayload;
            try
            {
                googlePayload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                });
            }
            catch (InvalidJwtException)
            {
                throw new HttpRequestException("Invalid Google token.");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Failed to validate Google token.", ex);
            }

            var email = googlePayload.Email;
            if (string.IsNullOrEmpty(email))
            {
                throw new HttpRequestException("Could not get email from Google token.");
            }

            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                // Người dùng chưa tồn tại -> Đăng ký mới
                var newUser = new User 
                {
                    Email = email,
                    Username = googlePayload.Name ?? email,
                    ConfirmedEmail = true,
                    RoleId = (await _roleRepository.GetByRoleName("customer")).Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    AvatarUrl = googlePayload.Picture
                };

                await _userRepository.Create(newUser); 
                user = newUser;
            }

            var accessToken = await GenerateAccessToken(user);
            var refeshToken = GenerateRefreshToken(user.Email);


            return new LoginResponse()
            {
                Message = user == null ? "Registered and logged in successfully!" : "Login successfully!",
                AccessToken = accessToken,
                RefreshToken = refeshToken
            };
        }


        public async Task SendResetPasswordOtpAsync(ForgotPasswordRequest request)
        {
            var email = request.Email;
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                throw new KeyNotFoundException("Email không tồn tại.");
            var otp = new Random().Next(100000, 999999);
            user.EmailOtp = otp;
            user.EmailOtpCreatedAt = DateTime.UtcNow;
            await _userRepository.Update(user);
            await _mailService.SendEmailAsync(new MailRequest
            {
                ToEmail = email,
                Subject = "[Audivia] Mã OTP khôi phục mặt khẩu",
                Body = EmailContent.EmailOTPContent(user.Username ?? "bạn", otp)
            });
        }

        public async Task<OTPConfirmResponse> VerifyResetPasswordOtpAsync(ConfirmEmailOTP request)
        {
            var user = await _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                return new OTPConfirmResponse
                {
                    IsSuccess = false,
                    Message = "User not found!"
                };
            }
            if (user.EmailOtp != request.Otp || (user.EmailOtpCreatedAt == null ||
                  (DateTime.UtcNow - user.EmailOtpCreatedAt.Value).TotalMinutes > 5))
            {
               return new OTPConfirmResponse
                {
                    IsSuccess = false,
                    Message = "OTP not correct or expired!"
                };
            }

            user.EmailOtp = null;
            user.EmailOtpCreatedAt = null;
            user.ConfirmedOtp = true;
            await _userRepository.Update(user);
            return new OTPConfirmResponse
            {
                IsSuccess = true,
                Message = "Confirm OTP successfully!"
            };
        }
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetByEmail(request.Email);
            if (user == null)
                return new ResetPasswordResponse
                {
                    IsSuccess = false,
                    Message = "User not found!"
                };
            if (user.ConfirmedOtp != true)
            {
                return new ResetPasswordResponse
                {
                    IsSuccess = false,
                    Message = "OTP not confirm!"
                };
            }

            user.Password = PasswordHasher.HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            user.ConfirmedOtp = null; //reset
            await _userRepository.Update(user);

            return new ResetPasswordResponse
            {
                IsSuccess = true,
                Message = "Change password successfully!"
            };
        }

    }
}
