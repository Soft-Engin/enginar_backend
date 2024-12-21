using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Users_Recipes_InteractionsRepository : Repository<Users_Recipes_Interaction>, IUsers_Recipes_InteractionsRepository
    {
        private readonly DataContext _db;
        public Users_Recipes_InteractionsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
