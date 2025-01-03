using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // GetAllAsync with includeProperties
        public async Task<IEnumerable<T>> GetAllAsync(string includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedAsync(
                    Expression<Func<T, bool>> filter = null,
                    int pageNumber = 1,
                    int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedAsync(
            string includeProperties,
            Expression<Func<T, bool>> filter = null,
            int pageNumber = 1,
            int pageSize = 10
            )
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Add eager loading for included properties
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate));
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }


        public async Task AddRangeAsync(IEnumerable<T> eventsRequirements)
        {
            await _dbSet.AddRangeAsync(eventsRequirements);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            // Add eager loading for included properties
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetLastAsync(int count, Expression<Func<T, bool>> predicate, string includeProperties)
        {
            //get last count items that satisfy the predicate and include the properties
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.Where(predicate).OrderByDescending(x => x).Take(count).ToListAsync();
        }
    }
}
