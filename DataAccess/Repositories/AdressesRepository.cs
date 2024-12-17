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
    public class AdressesRepository : Repository<Addresses>, IAddressesRepository
    {
        private readonly DataContext _db;
        public AdressesRepository(DataContext db) : base(db)
        {
            _db = db;

        }
    }
}
