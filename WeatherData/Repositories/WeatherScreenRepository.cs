using Microsoft.EntityFrameworkCore;
using WeatherData.Entities;
using WeatherData.Repositories.Interfaces;

namespace WeatherData.Repositories
{
    public class WeatherScreenRepository: IWeatherScreenRepository
    {
        private readonly DataContext _context;

        public WeatherScreenRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<WeatherScreen> GetByIdAsync(Guid id)
        {
            return await _context.Set<WeatherScreen>().FindAsync(id);
        }

        public async Task<IEnumerable<WeatherScreen>> GetAllAsync()
        {
            return await _context.Set<WeatherScreen>().ToListAsync();
        }

        public async Task AddAsync(WeatherScreen entity)
        {
            await _context.Set<WeatherScreen>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WeatherScreen entity)
        {
            _context.Set<WeatherScreen>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<WeatherScreen>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<WeatherScreen> GetLastAddedAsync()
        {
            return await _context.Set<WeatherScreen>()
                .OrderByDescending(w => w.ScreenTime)
                .FirstOrDefaultAsync();
        }
    }
}