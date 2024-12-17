using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Recipes_IngredientsRepository : Repository<Recipes_Ingredients>, IRecipes_IngredientsRepository
    {
        private readonly DataContext _db;
        public Recipes_IngredientsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
