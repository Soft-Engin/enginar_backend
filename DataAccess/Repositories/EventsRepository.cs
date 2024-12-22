using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<(IEnumerable<Events> Items, int TotalCount)> GetPaginatedBySeedAsync(uint multiplier, uint seedValue, uint limiter, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            IQueryable<Events> query = _dbSet;

            // Calculate weighted order dynamically
            var weightedQuery = query
                .Select(entity => new
                {
                    Entity = entity,
                    Weight = (entity.Id * multiplier + seedValue) % limiter //does not work
                })
                .OrderBy(item => item.Weight)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Execute the query
            var items = await weightedQuery
                .Select(item => item.Entity)
                .ToListAsync();

            var totalCount = await query.CountAsync();

            return (items, totalCount);
        }

        public async Task<(IEnumerable<Events> Items, int TotalCount)> GetPaginatedBySeedAsync(uint multiplier, uint seedValue, uint limiter, int pageNumber = 1, int pageSize = 10, string includeProperties = "")
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            IQueryable<Events> query = _dbSet;

            // Add eager loading for included properties
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            // Calculate weighted order dynamically
            var weightedQuery = query
                .Select(entity => new
                {
                    Entity = entity,
                    Weight = (entity.Id * multiplier + seedValue) % limiter //does not work
                })
                .OrderBy(item => item.Weight)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Execute the query
            var items = await weightedQuery
                .Select(item => item.Entity)
                .ToListAsync();

            var totalCount = await query.CountAsync();

            return (items, totalCount);
        }


        public async Task<(IEnumerable<Events> Items, int TotalCount)> GetPaginatedBySeedAsync(uint multiplier, uint seedValue, uint limiter, Expression<Func<Events, bool>> predicate, int pageNumber = 1, int pageSize = 10, string includeProperties = "")
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            IQueryable<Events> query = _dbSet;

            // Add eager loading for included properties
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            // Calculate weighted order dynamically
            var weightedQuery = query
                .Where(predicate)
                .Select(entity => new
                {
                    Entity = entity,
                    Weight = (entity.Id * multiplier + seedValue) % limiter //does not work
                })
                .OrderBy(item => item.Weight)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Execute the query
            var items = await weightedQuery
                .Select(item => item.Entity)
                .ToListAsync();

            var totalCount = await query.CountAsync();

            return (items, totalCount);
        }

    }
}
