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
    public class User_Event_ParticipationsRepository : Repository<User_Event_Participations>, IUser_Event_ParticipationsRepository
    {
        private readonly DataContext _db;
        public User_Event_ParticipationsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
