﻿using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEngin.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(UserManager<Users> userManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> RegisterUser(RegisterRequestDTO model)
        {
            var role = await _unitOfWork.Roles.FindAsync(r => r.Name == "User");
            var roleId = role.First().Id;
            var user = new Users { UserName = model.UserName, Email = model.Email, RoleId = roleId, FirstName = model.FirstName, LastName = model.LastName };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<string?> LoginUser(LoginRequestDTO model)
        {

            var user = await _userManager.FindByEmailAsync(model.Identifier) ?? await _userManager.FindByNameAsync(model.Identifier);
            if (user == null) return null;

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            return result ? await GenerateJwtToken(user) : null;
        }

        public async Task<string?> SendPasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // For security reasons, you might want to return a generic response
                return null;
            }

            // Generate a token without sending an email
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Return the token directly (for testing purposes)
            // In a real-world scenario, you would send the token to the user's email
            return token;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid email." });
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Passwords do not match." });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            return result;
        }

        public async Task<string> GenerateJwtToken(Users user)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId);
            var expirationTime = DateTime.UtcNow.AddMinutes(60 * 24); //time is hard coded bcus I'm tired
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role.Name)
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTSecretKey")));
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

        public async Task<IdentityResult> MakeUserAdminAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            var role = await _unitOfWork.Roles.FindAsync(r => r.Name == "Admin");
            var roleId = role.First().Id; // FindAsync returns a list, select first

            if (user.RoleId == roleId)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User is already an Admin." });

            }

            user.RoleId = roleId;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }


    }
}
