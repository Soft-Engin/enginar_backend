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
    public class Event_LikesRepository : Repository<Event_Likes>, IEvent_LikesRepository
    {
        private readonly DataContext _db;
        public Event_LikesRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
