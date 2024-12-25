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
    public class User_AllergensRepository : Repository<User_Allergens>, IUser_AllergensRepository
    {
        private readonly DataContext _db;
        public User_AllergensRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
