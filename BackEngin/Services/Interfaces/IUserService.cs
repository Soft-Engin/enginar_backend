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
        Task<GetUserByIdDTO?> GetUserByIdAsync(string id);
        Task<Users> UpdateUserAsync(string id, UpdateUserDto userDTO);
        Task<bool> DeleteUserAsync(string id);

        Task<FollowersDTO> GetFollowersAsync(string userId);
        Task<FollowedUsersDTO> GetFollowingAsync(string userId);
        Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId);
        Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId);
    }
}
