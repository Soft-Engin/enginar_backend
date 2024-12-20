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
    public class Recipe_CommentsRepository : Repository<Recipe_Comments>, IRecipe_CommentsRepository
    {
        private readonly DataContext _db;
        public Recipe_CommentsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
