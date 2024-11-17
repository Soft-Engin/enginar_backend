using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegisterRequestDTO model);
        Task<string> LoginUser(LoginRequestDTO model);
        string GenerateJwtToken(Users user);

        Task SendPasswordResetTokenAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDTO model);

        Task<IdentityResult> ConfirmAccountAsync(string email, string token);
    }
}
