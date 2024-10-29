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
    public class Events_RequriementsRepository : Repository<Events_Requirements>, IEvents_RequirementsRepository
    {
        private readonly DataContext _db;
        public Events_RequriementsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
