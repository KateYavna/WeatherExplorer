using Microsoft.EntityFrameworkCore;
using WeatherData.Entities;

namespace WeatherData
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options) 
        {
                
        }
        public DbSet<CountryCode> CountryCodes { get; set; }
        public DbSet<WeatherScreen> WeatherScreens { get; set;}
    }
}