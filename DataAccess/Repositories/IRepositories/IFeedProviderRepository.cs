namespace DataAccess.Repositories.IRepositories
{
    public interface IFeedProviderRepository<T> where T : class
    {
        Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedBySeedAsync(uint multiplier, uint seedValue, uint limiter, int pageNumber = 1, int pageSize = 10);
    }
}