using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace DataAccess.Repositories
{
    public class Users_Blogs_InteractionsRepository : Repository<Users_Blogs_Interaction>, IUsers_Blogs_InteractionsRepository
    {
        private readonly DataContext _db;
        public Users_Blogs_InteractionsRepository(DataContext db) : base(db)
        {
            _db = db;

        }

        public async Task<PaginatedResponseDTO<BookmarkBlogsItemDTO>> GetBookmarkedBlogsAsync(string userId, int page, int pageSize)
        {
            var bookmarkInteraction = await _db.Interactions
                .FirstOrDefaultAsync(i => i.Name == "BookmarkBlog");

            if (bookmarkInteraction == null)
                throw new Exception("Bookmark interaction not defined in the database.");

            var bookmarkedBlogsQuery = _db.Users_Blogs_Interactions
                .Where(ubi => ubi.UserId == userId && ubi.InteractionId == bookmarkInteraction.Id)
                .Select(ubi => new { ubi.Blog.User, ubi.Blog.Header, ubi.Blog.BodyText });

            var totalCount = await bookmarkedBlogsQuery.CountAsync();

            var blogs = await bookmarkedBlogsQuery
                .Skip((page - 1) * pageSize) // Skip records of previous pages
                .Take(pageSize)             // Take the records for the current page
                .ToListAsync();

            return new PaginatedResponseDTO<BookmarkBlogsItemDTO>
            {
                Items = blogs.Select(b => new BookmarkBlogsItemDTO
                {
                    UserName = b.User.UserName != null ? b.User.UserName : "Unknown",
                    Header = b.Header,
                    BodyText = b.BodyText
                }).ToList(),
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }
    }
}
