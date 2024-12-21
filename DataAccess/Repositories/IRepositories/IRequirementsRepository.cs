using Models;
using System.Linq.Expressions;

namespace DataAccess.Repositories.IRepositories
{
    public interface IRequirementsRepository : IRepository<Requirements>
    {
        Task<IEnumerable<Requirements>> FindAllAsync(Expression<Func<Requirements, bool>> predicate, Func<IQueryable<Requirements>, IQueryable<Requirements>> queryModifier = null);
        Task<IEnumerable<Requirements>> GetRangeByIdsAsync(IEnumerable<int> requirementIds);
        Task<int> CountAsync(Expression<Func<Requirements, bool>> predicate = null);
    }
}