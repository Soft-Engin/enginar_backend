using Models;

namespace DataAccess.Repositories.IRepositories
{
    public interface IEvents_RequirementsRepository : IRepository<Events_Requirements>
    {
        Task<IEnumerable<Events_Requirements>> FindAllAsync(Func<Events_Requirements, bool> predicate);
        Task AddRangeAsync(IEnumerable<Events_Requirements> eventsRequirements);
    }
}