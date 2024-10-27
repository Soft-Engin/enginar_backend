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
    public class Ingredients_PreferencesRepository : Repository<Ingredients_Preferences>, IIngredients_PreferencesRepository
    {
        private readonly DataContext _db;
        public Ingredients_PreferencesRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
