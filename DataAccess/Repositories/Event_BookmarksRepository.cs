using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models.InteractionModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Event_BookmarksRepository : Repository<Event_Bookmarks>, IEvent_BookmarksRepository
    {
        private readonly DataContext _db;
        public Event_BookmarksRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
