using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models;
using System.ComponentModel.DataAnnotations;


namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    public class AuthControllerV1 : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthControllerV1(UserManager<Users> userManager, SignInManager<Users> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // default creation results in a user with user role (roleId = 1).
            var user = new Users { UserName = model.UserName, Email = model.Email, RoleId = 1 };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            Users? user;
            // Find user by username or email
            user = await _userManager.FindByEmailAsync(model.Identifier);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.Identifier);

                if(user == null)
                {
                    return Unauthorized("Invalid login attempt");
                }
            }

            // Sign in the user (without locking the account)
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid login attempt");
            }

            // Create the JWT token
            var token = GenerateJwtToken(user);

            // Return the token in response
            return Ok(new { Token = token, message = "User logged in successfully!" });
        }

        private string GenerateJwtToken(Users user)
        {
            // Set JWT token expiration time
            var expirationTime = DateTime.UtcNow.AddHours(1);

            // Create claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                // You can add more claims such as roles here
            };

            // Get the JWT key from appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Create the signing credentials
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials
            );

            // Return the token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public class LoginRequest
        {
            public string Identifier { get; set; }
            public string Password { get; set; }
        }
        public class RegisterModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MaxLength(30)]
            public string UserName { get; set; }

            [Required]
            [MinLength(6)]
            public string Password { get; set; }

            [Required]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


    }
}
