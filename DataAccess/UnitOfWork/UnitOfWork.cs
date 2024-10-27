using BackEngin.Models;
using DataAccess.Repositories;
using BackEngin.Models;
using Microsoft.EntityFrameworkCore;
using BackEngin.Data;

namespace DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IRepository<WeatherForecast> WeatherForecastRepository { get; }

        public UnitOfWork(DataContext context)
        {
            _context = context;
            WeatherForecastRepository = new Repository<WeatherForecast>(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
