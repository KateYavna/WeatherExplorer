using Microsoft.EntityFrameworkCore;
using WeatherData.Entities;
using WeatherData.Repositories.Interfaces;
using WeatherData.Repositories;
using WeatherData;

namespace Tests.RepositoryTests
{
    [TestFixture]
    public class CountryCodeRepositoryTests
    {
        private DbContextOptions<DataContext> _options;
        private DataContext _testContext;
        private ICountryCodeRepository _countryCodeRepository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _testContext = new DataContext(_options);
            _countryCodeRepository = new CountryCodeRepository(_testContext);
        }

        [TearDown]
        public void TearDown()
        {
            _testContext.Database.EnsureDeleted();
            _testContext.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ReturnsCorrectCountryCode()
        {
            // Arrange
            var countryCode = new CountryCode { Id = Guid.NewGuid(), Code = "US" };
            await _testContext.CountryCodes.AddAsync(countryCode);
            await _testContext.SaveChangesAsync();

            // Act
            var result = await _countryCodeRepository.GetByIdAsync(countryCode.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(countryCode.Id, result.Id);
            Assert.AreEqual(countryCode.Code, result.Code);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllCountryCodes()
        {
            // Arrange
            var countryCodes = new List<CountryCode>
        {
            new CountryCode { Id = Guid.NewGuid(), Code = "US" },
            new CountryCode { Id = Guid.NewGuid(), Code = "CA" },
        };

            await _testContext.CountryCodes.AddRangeAsync(countryCodes);
            await _testContext.SaveChangesAsync();

            // Act
            var result = await _countryCodeRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(countryCodes.Count, result.Count());
            CollectionAssert.AreEquivalent(countryCodes, result);
        }
        [Test]
        public async Task AddAsync_AddsCountryCodeToDatabase()
        {
            // Arrange
            var countryCode = new CountryCode { Id = Guid.NewGuid(), Code = "US" };

            // Act
            await _countryCodeRepository.AddAsync(countryCode);

            // Assert
            var result = await _testContext.CountryCodes.FindAsync(countryCode.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(countryCode.Id, result.Id);
            Assert.AreEqual(countryCode.Code, result.Code);
        }

        [Test]
        public async Task UpdateAsync_UpdatesCountryCodeInDatabase()
        {
            // Arrange
            var countryCode = new CountryCode { Id = Guid.NewGuid(), Code = "US" };
            await _testContext.CountryCodes.AddAsync(countryCode);
            await _testContext.SaveChangesAsync();

            // Update the country code
            countryCode.Code = "CA";

            // Act
            await _countryCodeRepository.UpdateAsync(countryCode);

            // Assert
            var result = await _testContext.CountryCodes.FindAsync(countryCode.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(countryCode.Code, result.Code);
        }

        [Test]
        public async Task DeleteAsync_DeletesCountryCodeFromDatabase()
        {
            // Arrange
            var countryCode = new CountryCode { Id = Guid.NewGuid(), Code = "US" };
            await _testContext.CountryCodes.AddAsync(countryCode);
            await _testContext.SaveChangesAsync();

            // Act
            await _countryCodeRepository.DeleteAsync(countryCode.Id);

            // Assert
            var result = await _testContext.CountryCodes.FindAsync(countryCode.Id);
            Assert.IsNull(result);
        }
    }
}