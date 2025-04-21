using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.ModelRequests.Auth;
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
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
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
