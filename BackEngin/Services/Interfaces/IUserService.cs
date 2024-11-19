using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users?> GetUserByIdAsync(string id);
        Task<Users> UpdateUserAsync(string id, UpdateUserDto userDTO);
        Task<bool> DeleteUserAsync(string id);
    }
}
