using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Tests.Collections.Helpers;
using Moq;

namespace HolidaySearch.Logic.Tests.Helpers.Search
{
    [TestFixture]
    internal class SearchTests
    {
        [Test]
        public void MatchFlightsAndHotels_ReturnsCombinedAndSortedResults()
        {
            // Arrange
            var collection = SearchHelperCollection.Create();
            var mock = collection.FilteringHelper;

            var request = new SearchHolidayRequest
            {
                DepartingFrom = "London",
                TravelingTo = "CDG",
                DepartureDate = new DateTime(2025, 6, 1),
                Duration = 7
            };

            var flights = new List<Flight>
            {
                new() { From = "LGW", To = "CDG", DepartureDate = request.DepartureDate, Price = 150 },
                new() { From = "LTN", To = "CDG", DepartureDate = request.DepartureDate, Price = 100 }
            };

            var hotels = new List<Hotel>
            {
                new() { Name = "Hotel A", ArrivalDate = request.DepartureDate, Nights = 7, PricePerNight = 80, LocalAirports = new() { "CDG" } },
                new() { Name = "Hotel B", ArrivalDate = request.DepartureDate, Nights = 7, PricePerNight = 120, LocalAirports = new() { "CDG" } }
            };

            mock.Setup(x => x.GetAirportSearchKey("London")).Returns("LONDON");
            mock.Setup(x => x.FilterFlights(flights, "LONDON", "CDG", request.DepartureDate)).Returns(flights);
            mock.Setup(x => x.FilterHotels(hotels, "CDG", request.DepartureDate, 7)).Returns(hotels);

            // Act
            var result = collection.Helper.MatchFlightsAndHotels(request, flights, hotels);

            // Assert
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result, Is.Ordered.By(nameof(HolidayResult.TotalPrice)));
            Assert.That(result.First().Hotel.Name, Is.EqualTo("Hotel A"));
        }

        [Test]
        public void MatchFlightsAndHotels_NoMatches_ReturnsEmpty()
        {
            // Arrange
            var collection = SearchHelperCollection.Create();
            var mock = collection.FilteringHelper;

            var request = new SearchHolidayRequest
            {
                DepartingFrom = "ABC",
                TravelingTo = "XYZ",
                DepartureDate = new DateTime(2025, 6, 10),
                Duration = 5
            };

            mock.Setup(x => x.GetAirportSearchKey("ABC")).Returns("ABC");
            mock.Setup(x => x.FilterFlights(It.IsAny<IEnumerable<Flight>>(), "ABC", "XYZ", request.DepartureDate)).Returns(new List<Flight>());
            mock.Setup(x => x.FilterHotels(It.IsAny<IEnumerable<Hotel>>(), "XYZ", request.DepartureDate, 5)).Returns(new List<Hotel>());

            // Act
            var result = collection.Helper.MatchFlightsAndHotels(request, new List<Flight>(), new List<Hotel>());

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MatchFlightsAndHotels_Calls_NormalizeAirport_Once()
        {
            // Arrange
            var collection = SearchHelperCollection.Create();
            var mock = collection.FilteringHelper;

            var request = new SearchHolidayRequest
            {
                DepartingFrom = "ANY",
                TravelingTo = "CDG",
                DepartureDate = DateTime.Today,
                Duration = 7
            };

            mock.Setup(x => x.GetAirportSearchKey("ANY")).Returns("ANY");
            mock.Setup(x => x.FilterFlights(It.IsAny<IEnumerable<Flight>>(), "ANY", "CDG", It.IsAny<DateTime>())).Returns(new List<Flight>());
            mock.Setup(x => x.FilterHotels(It.IsAny<IEnumerable<Hotel>>(), "CDG", It.IsAny<DateTime>(), 7)).Returns(new List<Hotel>());

            // Act
            collection.Helper.MatchFlightsAndHotels(request, new List<Flight>(), new List<Hotel>());

            // Assert
            mock.Verify(x => x.GetAirportSearchKey("ANY"), Times.Once);
        }

        [Test]
        public void MatchFlightsAndHotels_Calls_FilterFlights_Once()
        {
            // Arrange
            var collection = SearchHelperCollection.Create();
            var mock = collection.FilteringHelper;

            var request = new SearchHolidayRequest
            {
                DepartingFrom = "LGW",
                TravelingTo = "CDG",
                DepartureDate = new DateTime(2025, 7, 1),
                Duration = 10
            };

            mock.Setup(x => x.GetAirportSearchKey("LGW")).Returns("LGW");
            mock.Setup(x => x.FilterFlights(It.IsAny<IEnumerable<Flight>>(), "LGW", "CDG", request.DepartureDate)).Returns(new List<Flight>());
            mock.Setup(x => x.FilterHotels(It.IsAny<IEnumerable<Hotel>>(), "CDG", request.DepartureDate, 10)).Returns(new List<Hotel>());

            // Act
            collection.Helper.MatchFlightsAndHotels(request, new List<Flight>(), new List<Hotel>());

            // Assert
            mock.Verify(x => x.FilterFlights(It.IsAny<IEnumerable<Flight>>(), "LGW", "CDG", request.DepartureDate), Times.Once);
        }

        [Test]
        public void MatchFlightsAndHotels_Calls_FilterHotels_Once()
        {
            // Arrange
            var collection = SearchHelperCollection.Create();
            var mock = collection.FilteringHelper;

            var request = new SearchHolidayRequest
            {
                DepartingFrom = "LTN",
                TravelingTo = "JFK",
                DepartureDate = new DateTime(2025, 5, 20),
                Duration = 3
            };

            mock.Setup(x => x.GetAirportSearchKey("LTN")).Returns("LTN");
            mock.Setup(x => x.FilterFlights(It.IsAny<IEnumerable<Flight>>(), "LTN", "JFK", request.DepartureDate)).Returns(new List<Flight>());
            mock.Setup(x => x.FilterHotels(It.IsAny<IEnumerable<Hotel>>(), "JFK", request.DepartureDate, 3)).Returns(new List<Hotel>());

            // Act
            collection.Helper.MatchFlightsAndHotels(request, new List<Flight>(), new List<Hotel>());

            // Assert
            mock.Verify(x => x.FilterHotels(It.IsAny<IEnumerable<Hotel>>(), "JFK", request.DepartureDate, 3), Times.Once);
        }
    }
}
