using Models;

namespace DataAccess.Repositories.IRepositories
{
    public interface IIngredientsRepository : IRepository<Ingredients>
    {
        // include properties will allow us to lazy load the properties of the entity
        // such as the navigation properties
        Task<IEnumerable<Ingredients>> GetAllAsync(string includeProperties = "");
    }
}
