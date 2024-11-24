using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly DataContext _db;
        public UsersRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
