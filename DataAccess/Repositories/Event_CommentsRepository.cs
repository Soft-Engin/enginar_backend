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
    public class Event_CommentsRepository : Repository<Event_Comments>, IEvent_CommentsRepository
    {
        private readonly DataContext _db;
        public Event_CommentsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
