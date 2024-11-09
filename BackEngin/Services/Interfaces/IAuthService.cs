using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegisterRequestModel model);
        Task<string> LoginUser(LoginRequestModel model);
        string GenerateJwtToken(Users user);

        Task<string> SendPasswordResetTokenAsync(string email);  // Return the token for testing
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}
