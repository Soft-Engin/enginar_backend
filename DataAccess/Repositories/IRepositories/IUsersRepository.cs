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
        Task<PaginatedResponseDTO<string>> GetFollowersAsync(string userId, int page, int pageSize);
        Task<PaginatedResponseDTO<string>> GetFollowingAsync(string userId, int page, int pageSize);
        Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId);
        Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId);
    }
}