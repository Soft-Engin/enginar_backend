using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserListDTO> GetAllUsersAsync();
        Task<GetUserByIdDTO?> GetUserByIdAsync(string id);
        Task<UpdateUserDto> UpdateUserAsync(string id, UpdateUserDto userDTO);
        Task<bool> DeleteUserAsync(string id);

        Task<FollowersDTO> GetFollowersAsync(string userId);
        Task<FollowedUsersDTO> GetFollowingAsync(string userId);
        Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId);
        Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId);
        Task<BookmarkRecipesDTO> GetBookmarkedRecipesAsync(string userId);
        Task<BookmarkBlogsDTO> GetBookmarkedBlogsAsync(string userId);
    }
}
