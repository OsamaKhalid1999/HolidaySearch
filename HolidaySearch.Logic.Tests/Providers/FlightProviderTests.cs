using HolidaySearch.Domain.Search;
using HolidaySearch.Tests.Collections.Providers;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace HolidaySearch.Logic.Tests.Providers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
   
    public class FlightProviderTests
    {
        [Test]
        public void GetFlights_ReturnsExpectedData()
        {
            // Arrange
            var directoryPath = "TestData";
            Directory.CreateDirectory(directoryPath); 

            var testFilePath = Path.Combine(directoryPath, "test-flights.json");
            var expectedFlights = new List<Flight>
            {
                new()
                {
                    Id = 1,
                    Airline = "TestAir",
                    From = "MAN",
                    To = "AGP",
                    Price = 200,
                    DepartureDate = new DateTime(2025, 07, 01)
                }
            };
            File.WriteAllText(testFilePath, JsonSerializer.Serialize(expectedFlights));

            var collection = FlightProviderCollection.Create(testFilePath);

            // Act
            var result = collection.Provider.GetFlights();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Airline, Is.EqualTo("TestAir"));

            File.Delete(testFilePath);
        }

    }
}
