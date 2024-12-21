using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class RequirementsRepository : Repository<Requirements>, IRequirementsRepository
    {
        private readonly DataContext _db;
        public RequirementsRepository(DataContext db) : base(db)
        {
            _db = db;

        }
        public async Task<IEnumerable<Requirements>> FindAllAsync(Expression<Func<Requirements, bool>> predicate, Func<IQueryable<Requirements>, IQueryable<Requirements>> queryModifier = null)
        {
            IQueryable<Requirements> query = _db.Requirements.Where(predicate);

            if (queryModifier != null)
            {
                query = queryModifier(query);
            }

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Requirements>> GetRangeByIdsAsync(IEnumerable<int> requirementIds)
        {
            return await _db.Requirements
                .Where(r => requirementIds.Contains(r.Id))
                .ToListAsync();
        }
    }
}
