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
    public class BlogsRepository : Repository<Blogs>, IBlogsRepository
    {
        private readonly DataContext _db;
        public BlogsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
