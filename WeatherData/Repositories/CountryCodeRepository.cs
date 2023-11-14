using Microsoft.EntityFrameworkCore;
using WeatherData.Entities;
using WeatherData.Repositories.Interfaces;

namespace WeatherData.Repositories
{
    public class CountryCodeRepository : ICountryCodeRepository
    {
        private readonly DataContext _context;

        public CountryCodeRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CountryCode> GetByIdAsync(Guid id)
        {
            return await _context.CountryCodes.FindAsync(id);
        }

        public async Task<IEnumerable<CountryCode>> GetAllAsync()
        {
            return await _context.CountryCodes.ToListAsync();
        }

        public async Task AddAsync(CountryCode entity)
        {
            _context.CountryCodes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CountryCode entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entityToRemove = await _context.CountryCodes.FindAsync(id);
            if (entityToRemove != null)
            {
                _context.CountryCodes.Remove(entityToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}