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
        private readonly IEmailService _emailService;

        public AuthService(UserManager<Users> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<IdentityResult> RegisterUser(RegisterRequestDTO model)
        {
            var user = new Users { UserName = model.UserName, Email = model.Email, RoleId = 1 };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                await SendEmailConfirmationTokenAsync(user.Email);
            }
            
            return result;
        }

        public async Task SendEmailConfirmationTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Handle the case where the user is not found
                return;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"https://enginar.space/confirm-email?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
            var subject = "Email Confirmation";
            var message = $"Please confirm your email by clicking here: {confirmationLink}";

            await _emailService.SendEmailAsync(email, subject, message);
        }

        public async Task<IdentityResult> ConfirmAccountAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid email or token." });
            }

            // Confirm the email
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }


        public async Task<string> LoginUser(LoginRequestDTO model)
        {

            var user = await _userManager.FindByEmailAsync(model.Identifier) ?? await _userManager.FindByNameAsync(model.Identifier);
            if (user == null) return null;

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            return result ? GenerateJwtToken(user) : null;
        }

        public async Task SendPasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // For security reasons, don't reveal that the email does not exist
                return;
            }

            // Generate a token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Create the reset link (update with your frontend URL)
            var resetLink = $"https://enginar.space/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            // Compose the email
            var subject = "Password Reset Request";
            var message = $"Please reset your password by clicking here: {resetLink}";

            // Send the email
            await _emailService.SendEmailAsync(email, subject, message);
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
