using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace DataAccess.Repositories
{
    public class Users_Blogs_InteractionsRepository : Repository<Users_Blogs_Interaction>, IUsers_Blogs_InteractionsRepository
    {
        private readonly DataContext _db;
        public Users_Blogs_InteractionsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
