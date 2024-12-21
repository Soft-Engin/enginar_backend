using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models.InteractionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Recipe_BookmarksRepository : Repository<Recipe_Bookmarks>, IRecipe_BookmarksRepository
    {
        private readonly DataContext _db;
        public Recipe_BookmarksRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
