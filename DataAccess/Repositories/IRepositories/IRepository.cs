using Models;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        // include properties will allow us to lazy load the properties of the entity
        // such as the navigation properties
        Task<IEnumerable<T>> GetAllAsync(string includeProperties);
        Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string includeProperties);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedAsync(Expression<Func<T, bool>> filter = null, int pageNumber = 1, int pageSize = 10);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedAsync(string includeProperties, Expression<Func<T, bool>> filter = null, int pageNumber = 1, int pageSize = 10);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> eventsRequirements);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable();
    }
}