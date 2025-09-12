using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExlaqiNasiri.Persistence.Services.Helper
{
    public class GenerateJWTTokenService : IGenerateJWTTokenService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly IConfiguration _configuration;

        public GenerateJWTTokenService(UserManager<BaseUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public Result<string> GenerateJwtToken(BaseUser user)
        {
            var issuer = _configuration["JWT:issuer"];
            var audience = _configuration["JWT:audience"];
            var expiresIn = int.Parse(_configuration["JWT:datetime"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secret_key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var access_token = new JwtSecurityToken
                (
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddHours(4).AddDays(expiresIn)
                );
            return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(access_token));
        }

        public Result<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Result<string>.Success(Convert.ToBase64String(randomNumber));
        }
    }
}
