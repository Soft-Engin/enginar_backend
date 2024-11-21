using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepositories
{
    public interface IUsers_Recipes_InteractionsRepository :  IRepository<Users_Recipes_Interaction>
    {
        Task<BookmarkRecipesDTO> GetBookmarkedRecipesAsync(string userId);
    }
}
