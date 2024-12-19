using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Addresses> SingleOrDefaultAsync(Func<Addresses, bool> predicate)
        {
            return await Task.Run(() => _db.Addresses.SingleOrDefault(predicate));
        }
    }
}
