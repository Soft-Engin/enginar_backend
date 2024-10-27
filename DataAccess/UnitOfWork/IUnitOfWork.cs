using BackEngin.Models;
using DataAccess.Repositories;

namespace DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<WeatherForecast> WeatherForecastRepository { get; }
        Task<int> CompleteAsync();
    }
}