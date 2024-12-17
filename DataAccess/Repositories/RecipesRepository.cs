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
    public class RecipesRepository : Repository<Recipes>, IRecipesRepository
    {
        private readonly DataContext _db;
        public RecipesRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
