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
    public class IngredientTypesRepository : Repository<IngredientTypes>, IIngredientTypesRepository
    {
        private readonly DataContext _db;
        public IngredientTypesRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
