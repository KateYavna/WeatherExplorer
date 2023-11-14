using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WeatherServices.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace WeatherServices.Services
{
    public class WeatherRefreshService: BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly int _refreshInterval;

        public WeatherRefreshService(IServiceProvider services, IConfiguration configuration)
        {
            _services = services;
            _refreshInterval = configuration.GetValue<int>("RefreshIntervalHours") * 60 * 60 * 1000; // Convert hours to milliseconds
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_refreshInterval, stoppingToken);
                await RefreshWeatherData(stoppingToken);
            }
        }

        private async Task RefreshWeatherData(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var openWeatherService = scope.ServiceProvider.GetRequiredService<IOpenWeatherService>();
                    var weather = await openWeatherService.RefreshWeatherDataAsync();
                }
            }
            catch (Exception ex)
            {
                using (var scope = _services.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<WeatherRefreshService>>();
                    logger.LogError(ex, "An error occurred while refreshing weather data.");
                }
            }
        }
    }
}