using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class IngredientsRepository : Repository<Ingredients>, IIngredientsRepository
    {
        private readonly DataContext _db;

        public IngredientsRepository(DataContext db) : base(db)
        {
            _db = db;
        }
    }
}
