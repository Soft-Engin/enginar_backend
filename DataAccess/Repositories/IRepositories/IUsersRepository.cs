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
        Task<FollowersDTO> GetFollowersAsync(string userId);
        Task<FollowedUsersDTO> GetFollowingAsync(string userId);
        Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId);
        Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId);
    }
}