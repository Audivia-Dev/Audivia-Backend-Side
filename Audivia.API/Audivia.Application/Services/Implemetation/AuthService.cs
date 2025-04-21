using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Auth;
using Audivia.Domain.ModelRequests.Mail;
using Audivia.Domain.ModelResponses.Auth;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Audivia.Application.Services.Implemetation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IRoleRepository roleRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _mailService = mailService;
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
                throw new KeyNotFoundException("User not found!");
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
            return new ConfirmEmailResponse
            {
                Message = "Registered successfully!",
                Success = true
            };
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

        private async Task<string> GenerateAccessToken(User user)
        {
            Role? role = await _roleRepository.FindFirst(role => role.Id == user.RoleId);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email != null ? user.Email.ToString() : ""),
                new Claim(ClaimTypes.Role, role.RoleName)
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
    }
}
