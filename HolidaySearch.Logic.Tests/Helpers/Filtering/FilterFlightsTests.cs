using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Tests.Collections.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace HolidaySearch.Logic.Tests.Helpers.Filtering
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class FilterFlightsTests
    {
        [Test]
        public void FilterFlights_ShouldReturnMatchingFlight_WhenExactMatchExists()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;
            var flights = new List<Flight>
            {
                new() { From = "JFK", To = "CDG", DepartureDate = new DateTime(2025, 6, 1) },
                new() { From = "JFK", To = "CDG", DepartureDate = new DateTime(2025, 6, 2) }
            };

            // Act
            var result = helper.FilterFlights(flights, "JFK", "CDG", new DateTime(2025, 6, 1));

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterFlights_ShouldReturnAllLondonMatches_WhenFrom_IsLondon()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;
            var flights = new List<Flight>
            {
                new() { From = "LGW", To = "CDG", DepartureDate = new DateTime(2025, 6, 1) },
                new() { From = "LTN", To = "CDG", DepartureDate = new DateTime(2025, 6, 1) },
                new() { From = "MAN", To = "CDG", DepartureDate = new DateTime(2025, 6, 1) }
            };

            // Act
            var result = helper.FilterFlights(flights, "LONDON", "CDG", new DateTime(2025, 6, 1));

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void FilterFlights_ShouldReturnAll_WhenFrom_IsAny()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;
            var flights = new List<Flight>
            {
                new() { From = "XYZ", To = "CDG", DepartureDate = new DateTime(2025, 6, 1) },
                new() { From = "ABC", To = "CDG", DepartureDate = new DateTime(2025, 6, 1) }
            };

            // Act
            var result = helper.FilterFlights(flights, "ANY", "CDG", new DateTime(2025, 6, 1));

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void FilterFlights_ShouldReturnEmpty_WhenNoMatchFound()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;
            var flights = new List<Flight>
            {
                new() { From = "JFK", To = "LAX", DepartureDate = new DateTime(2025, 6, 1) }
            };

            // Act
            var result = helper.FilterFlights(flights, "LHR", "CDG", new DateTime(2025, 6, 1));

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FilterFlights_ShouldThrow_WhenInputFlightsIsNull()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                helper.FilterFlights(null, "JFK", "CDG", new DateTime(2025, 6, 1)));
        }

        [Test]
        public void FilterFlights_ShouldHandle_NullFrom_Or_To_ValuesInFlight()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;
            var flights = new List<Flight>
            {
                new() { From = null, To = "CDG", DepartureDate = new DateTime(2025, 6, 1) },
                new() { From = "JFK", To = null, DepartureDate = new DateTime(2025, 6, 1) },
                new() { From = "JFK", To = "CDG", DepartureDate = new DateTime(2025, 6, 1) }
            };

            // Act
            var result = helper.FilterFlights(flights, "JFK", "CDG", new DateTime(2025, 6, 1));

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
