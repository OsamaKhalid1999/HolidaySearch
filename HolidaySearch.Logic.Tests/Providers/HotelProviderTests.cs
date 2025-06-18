using HolidaySearch.Domain.Search;
using HolidaySearch.Tests.Collections.Providers;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace HolidaySearch.Logic.Tests.Providers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class HotelProviderTests
    {
        [Test]
        public void GetHotels_ReturnsExpectedData()
        {
            // Arrange
            var directoryPath = "TestData";
            Directory.CreateDirectory(directoryPath); 

            var testFilePath = Path.Combine(directoryPath, "test-hotels.json");

            var expectedHotels = new List<Hotel>
            {
                new Hotel
                {
                    Id = 1,
                    Name = "TestHotel",
                    ArrivalDate = new DateTime(2025, 07, 01),
                    Nights = 7,
                    PricePerNight = 100,
                    LocalAirports = new List<string> { "MAN" }
                }
            };

            File.WriteAllText(testFilePath, JsonSerializer.Serialize(expectedHotels));

            var collection = HotelProviderCollection.Create(testFilePath);

            // Act
            var result = collection.Provider.GetHotels();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("TestHotel"));

            // Cleanup
            File.Delete(testFilePath);
        }
    }
}
