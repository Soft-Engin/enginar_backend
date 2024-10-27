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
    public class Users_InteractionsRepository : Repository<Users_Interactions>, IUsers_InteractionsRepository
    {
        private readonly DataContext _db;
        public Users_InteractionsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
