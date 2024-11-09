using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEngin.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<Users> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(RegisterRequestModel model)
        {
            var user = new Users { UserName = model.UserName, Email = model.Email, RoleId = 1 };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<string> LoginUser(LoginRequestModel model)
        {

            var user = await _userManager.FindByEmailAsync(model.Identifier) ?? await _userManager.FindByNameAsync(model.Identifier);
            if (user == null) return null;

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            return result ? GenerateJwtToken(user) : null;
        }

        public string GenerateJwtToken(Users user)
        {
            var expirationTime = DateTime.UtcNow.AddMinutes(30); //time is hard coded bcus I'm tired
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),

            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
