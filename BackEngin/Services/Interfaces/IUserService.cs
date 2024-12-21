using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedResponseDTO<UserDTO>> GetAllUsersAsync(int page, int pageSize);
        Task<GetUserByIdDTO?> GetUserByIdAsync(string id);
        Task<UpdateUserDto> UpdateUserAsync(string id, UpdateUserDto userDTO);
        Task<bool> DeleteUserAsync(string id);

        Task<PaginatedResponseDTO<string>> GetFollowersAsync(string userId, int page, int pageSize);
        Task<PaginatedResponseDTO<string>> GetFollowingAsync(string userId, int page, int pageSize);
        Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId);
        Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId);
        Task<PaginatedResponseDTO<BookmarkRecipesItemDTO>> GetBookmarkedRecipesAsync(string userId, int page, int pageSize);
        Task<PaginatedResponseDTO<BookmarkBlogsItemDTO>> GetBookmarkedBlogsAsync(string userId, int page, int pageSize);
        Task<PaginatedResponseDTO<UserDTO>> SearchUsersAsync(UserSearchParams searchParams, int pageNumber, int pageSize);
        Task<PaginatedResponseDTO<RecipeDTO>> GetUserRecipesAsync(string userId, int pageNumber, int pageSize);
        Task<PaginatedResponseDTO<BlogDTO>> GetUserBlogsAsync(string userId, int pageNumber, int pageSize);
        Task<PaginatedResponseDTO<LikedBlogsItemDTO>> GetLikedBlogsAsync(string userId, int page, int pageSize);
        Task<PaginatedResponseDTO<EventDTO>> GetUserEventsAsync(string userId, int page, int pageSize);
        Task<PaginatedResponseDTO<LikedRecipesItemDTO>> GetLikedRecipesAsync(string userId, int page, int pageSize);
    }
}
