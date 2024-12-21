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
    public class Recipe_LikesRepository : Repository<Recipe_Likes>, IRecipe_LikesRepository
    {
        private readonly DataContext _db;
        public Recipe_LikesRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
