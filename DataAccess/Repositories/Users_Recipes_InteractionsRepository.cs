using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Users_Recipes_InteractionsRepository : Repository<Users_Recipes_Interaction>, IUsers_Recipes_InteractionsRepository
    {
        private readonly DataContext _db;
        public Users_Recipes_InteractionsRepository(DataContext db) : base(db)
        {
            _db = db;

        }

        public async Task<PaginatedResponseDTO<BookmarkRecipesItemDTO>> GetBookmarkedRecipesAsync(string userId, int page, int pageSize)
        {
            var bookmarkInteraction = await _db.Interactions
                .FirstOrDefaultAsync(i => i.Name == "BookmarkRecipe");

            if (bookmarkInteraction == null)
                throw new Exception("Bookmark interaction not defined in the database.");

            var bookmarkedRecipesQuery = _db.Users_Recipes_Interactions
                .Where(uri => uri.UserId == userId && uri.InteractionId == bookmarkInteraction.Id)
                .Select(uri => new { uri.Recipe.User, uri.Recipe.Header, uri.Recipe.BodyText });

            var totalCount = await bookmarkedRecipesQuery.CountAsync();

            var recipes = await bookmarkedRecipesQuery
                .Skip((page - 1) * pageSize) // Skip the records of previous pages
                .Take(pageSize)             // Take the records for the current page
                .ToListAsync();

            return new PaginatedResponseDTO<BookmarkRecipesItemDTO>
            {
                Items = recipes.Select(r => new BookmarkRecipesItemDTO
                {
                    UserName = r.User.UserName != null ? r.User.UserName : "Unknown",
                    Header = r.Header,
                    BodyText = r.BodyText
                }).ToList(),
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

    }
}
