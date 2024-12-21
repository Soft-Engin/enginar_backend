using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models.InteractionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Blog_CommentsRepository : Repository<Blog_Comments>, IBlog_CommentsRepository
    {
        private readonly DataContext _db;
        public Blog_CommentsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
