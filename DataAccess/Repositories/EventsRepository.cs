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

    public class EventsRepository : Repository<Events>, IEventsRepository
    {
        private readonly DataContext _db;
        public EventsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
