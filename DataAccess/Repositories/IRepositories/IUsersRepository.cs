using Models;
using Models.DTO;

namespace DataAccess.Repositories.IRepositories
{
    public interface IUsersRepository : IRepository<Users>
    {
        /*
         * the update (or other methods) method can be overwritten later according to table's needs
        void Update(Users obj);

        */
        Task<PaginatedResponseDTO<UserCompactDTO>> GetFollowersAsync(string userId, int page, int pageSize);
        Task<PaginatedResponseDTO<UserCompactDTO>> GetFollowingAsync(string userId, int page, int pageSize);
        Task<IEnumerable<UserCompactDTO>> GetAllFollowingAsync(string userId);
        
        Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId);
        Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId);
    }
}