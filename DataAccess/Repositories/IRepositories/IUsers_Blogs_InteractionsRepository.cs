using Models;
using Models.DTO;

namespace DataAccess.Repositories.IRepositories
{
    public interface IUsers_Blogs_InteractionsRepository : IRepository<Users_Blogs_Interaction>
    {
        Task<BookmarkBlogsDTO> GetBookmarkedBlogsAsync(string userId);
    }
}