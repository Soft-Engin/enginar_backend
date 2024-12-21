using Models;
using System.Linq.Expressions;

namespace DataAccess.Repositories.IRepositories
{
    public interface IAddressesRepository : IRepository<Addresses>
    {
        Task<Addresses> SingleOrDefaultAsync(Func<Addresses, bool> predicate);

    }
}