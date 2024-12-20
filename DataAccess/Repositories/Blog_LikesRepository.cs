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
    public class Blog_LikesRepository : Repository<Blog_Likes>, IBlog_LikesRepository
    {
        private readonly DataContext _db;
        public Blog_LikesRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
