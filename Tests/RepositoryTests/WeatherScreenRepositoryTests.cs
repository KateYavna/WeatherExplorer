using Microsoft.EntityFrameworkCore;
using WeatherData.Repositories.Interfaces;
using WeatherData.Repositories;
using WeatherData;
using WeatherData.Entities;

namespace Tests.RepositoryTests
{
    [TestFixture]
    public class WeatherScreenRepositoryTests
    {
        private DbContextOptions<DataContext> _options;
        private DataContext _testContext;
        private IWeatherScreenRepository _weatherScreenRepository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _testContext = new DataContext(_options);
            _weatherScreenRepository = new WeatherScreenRepository(_testContext);
        }

        [TearDown]
        public void TearDown()
        {
            _testContext.Database.EnsureDeleted();
            _testContext.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ReturnsCorrectWeatherScreen()
        {
            // Arrange
            var weatherScreen = new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Sunny" };
            await _testContext.Set<WeatherScreen>().AddAsync(weatherScreen);
            await _testContext.SaveChangesAsync();

            // Act
            var result = await _weatherScreenRepository.GetByIdAsync(weatherScreen.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(weatherScreen.Id, result.Id);
            Assert.AreEqual(weatherScreen.WeatherDescription, result.WeatherDescription);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllWeatherScreens()
        {
            // Arrange
            var weatherScreens = new List<WeatherScreen>
            {
                new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Sunny" },
                new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Cloudy" },
            };

            await _testContext.Set<WeatherScreen>().AddRangeAsync(weatherScreens);
            await _testContext.SaveChangesAsync();

            // Act
            var result = await _weatherScreenRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(weatherScreens.Count, result.Count());
            CollectionAssert.AreEquivalent(weatherScreens, result);
        }

        [Test]
        public async Task AddAsync_AddsWeatherScreenToDatabase()
        {
            // Arrange
            var weatherScreen = new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Sunny" };

            // Act
            await _weatherScreenRepository.AddAsync(weatherScreen);

            // Assert
            var result = await _testContext.Set<WeatherScreen>().FindAsync(weatherScreen.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(weatherScreen.Id, result.Id);
            Assert.AreEqual(weatherScreen.WeatherDescription, result.WeatherDescription);
        }

        [Test]
        public async Task UpdateAsync_UpdatesWeatherScreenInDatabase()
        {
            // Arrange
            var weatherScreen = new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Sunny" };
            await _testContext.Set<WeatherScreen>().AddAsync(weatherScreen);
            await _testContext.SaveChangesAsync();

            // Update the weather screen
            weatherScreen.WeatherDescription = "Rainy";

            // Act
            await _weatherScreenRepository.UpdateAsync(weatherScreen);

            // Assert
            var result = await _testContext.Set<WeatherScreen>().FindAsync(weatherScreen.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(weatherScreen.WeatherDescription, result.WeatherDescription);
        }

        [Test]
        public async Task DeleteAsync_DeletesWeatherScreenFromDatabase()
        {
            // Arrange
            var weatherScreen = new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Sunny" };
            await _testContext.Set<WeatherScreen>().AddAsync(weatherScreen);
            await _testContext.SaveChangesAsync();

            // Act
            await _weatherScreenRepository.DeleteAsync(weatherScreen.Id);

            // Assert
            var result = await _testContext.Set<WeatherScreen>().FindAsync(weatherScreen.Id);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetLastAddedAsync_ReturnsLastAddedWeatherScreen()
        {
            // Arrange
            var weatherScreens = new List<WeatherScreen>
            {
                new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Sunny", ScreenTime = DateTime.UtcNow.AddHours(-2) },
                new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Cloudy", ScreenTime = DateTime.UtcNow.AddHours(-1) },
                new WeatherScreen { Id = Guid.NewGuid(), WeatherDescription = "Rainy", ScreenTime = DateTime.UtcNow },
            };

            await _testContext.Set<WeatherScreen>().AddRangeAsync(weatherScreens);
            await _testContext.SaveChangesAsync();

            // Act
            var result = await _weatherScreenRepository.GetLastAddedAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Rainy", result.WeatherDescription);
        }
    }
}