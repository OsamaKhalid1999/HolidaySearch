using HolidaySearch.Domain.Search;
using HolidaySearch.Tests.Collections;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace HolidaySearch.Tests.Services
{
    [TestFixture]
    [ExcludeFromCodeCoverage]   
    public class HolidaySearchServiceTests
    {
        [Test]
        public void Throw_ArgumentNullException_When_Request_Is_Null()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();

            // Act
            TestDelegate method = () => services.Service.Search(null);

            // Assert
            Assert.That(method, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Throw_ArgumentException_When_DepartingFrom_Is_Null()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            var request = CreateValidRequest();
            request.DepartingFrom = null;

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.ArgumentException.With.Property("ParamName").EqualTo("DepartingFrom"));
        }

        [Test]
        public void Throw_ArgumentException_When_TravelingTo_Is_Null()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            var request = CreateValidRequest();
            request.TravelingTo = null;

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.ArgumentException.With.Property("ParamName").EqualTo("TravelingTo"));
        }

        [Test]
        public void Throw_ArgumentException_When_DepartureDate_Is_Default()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            var request = CreateValidRequest();
            request.DepartureDate = default;

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.ArgumentException.With.Property("ParamName").EqualTo("DepartureDate"));
        }

        [Test]
        public void Throw_ArgumentException_When_Duration_Is_Less_Than_1()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            var request = CreateValidRequest();
            request.Duration = 0;

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.ArgumentException.With.Property("ParamName").EqualTo("Duration"));
        }

        [Test]
        public void Throw_ArgumentException_When_PageNumber_Is_Less_Than_1()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            var request = CreateValidRequest();
            request.PageNumber = 0;

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.ArgumentException.With.Property("ParamName").EqualTo("PageNumber"));
        }

        [Test]
        public void Throw_ArgumentException_When_PageSize_Is_Less_Than_1()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            var request = CreateValidRequest();
            request.PageSize = 0;

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.ArgumentException.With.Property("ParamName").EqualTo("PageSize"));
        }

        [Test]
        public void Throw_InvalidOperationException_When_GetFlights_Returns_Null()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(() => null);
            var request = CreateValidRequest();

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.InvalidOperationException);
        }

        [Test]
        public void Throw_InvalidOperationException_When_GetHotels_Returns_Null()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(new List<Flight>());
            services.HotelProvider.Setup(x => x.GetHotels()).Returns(() => null);
            var request = CreateValidRequest();

            // Act
            TestDelegate method = () => services.Service.Search(request);

            // Assert
            Assert.That(method, Throws.InvalidOperationException);
        }

        [Test]
        public void Verify_GetFlights_Called_Once()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(new List<Flight>());
            services.HotelProvider.Setup(x => x.GetHotels()).Returns(new List<Hotel>());
            services.SearchHelper.Setup(x => x.MatchFlightsAndHotels(It.IsAny<SearchHolidayRequest>(), It.IsAny<List<Flight>>(), It.IsAny<List<Hotel>>())).Returns(new List<HolidayResult>());
            var request = CreateValidRequest();

            // Act
            services.Service.Search(request);

            // Assert
            services.FlightProvider.Verify(x => x.GetFlights(), Times.Once);
        }

        [Test]
        public void Verify_GetHotels_Called_Once()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(new List<Flight>());
            services.HotelProvider.Setup(x => x.GetHotels()).Returns(new List<Hotel>());
            services.SearchHelper.Setup(x => x.MatchFlightsAndHotels(It.IsAny<SearchHolidayRequest>(), It.IsAny<List<Flight>>(), It.IsAny<List<Hotel>>())).Returns(new List<HolidayResult>());
            var request = CreateValidRequest();

            // Act
            services.Service.Search(request);

            // Assert
            services.HotelProvider.Verify(x => x.GetHotels(), Times.Once);
        }

        [Test]
        public void Verify_MatchFlightsAndHotels_Called_Once()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(new List<Flight>());
            services.HotelProvider.Setup(x => x.GetHotels()).Returns(new List<Hotel>());
            services.SearchHelper.Setup(x => x.MatchFlightsAndHotels(It.IsAny<SearchHolidayRequest>(), It.IsAny<List<Flight>>(), It.IsAny<List<Hotel>>())).Returns(new List<HolidayResult>());
            var request = CreateValidRequest();

            // Act
            services.Service.Search(request);

            // Assert
            services.SearchHelper.Verify(x => x.MatchFlightsAndHotels(It.IsAny<SearchHolidayRequest>(), It.IsAny<List<Flight>>(), It.IsAny<List<Hotel>>()), Times.Once);
        }

        [Test]
        public void Return_Empty_Result_When_MatchReturns_Empty()
        {
            // Arrange
            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(new List<Flight>());
            services.HotelProvider.Setup(x => x.GetHotels()).Returns(new List<Hotel>());
            services.SearchHelper.Setup(x => x.MatchFlightsAndHotels(It.IsAny<SearchHolidayRequest>(), It.IsAny<List<Flight>>(), It.IsAny<List<Hotel>>())).Returns(new List<HolidayResult>());
            var request = CreateValidRequest();

            // Act
            var result = services.Service.Search(request);

            // Assert
            Assert.That(result.Results, Is.Empty);
            Assert.That(result.TotalResults, Is.EqualTo(0));
        }

        [Test]
        public void Return_PagedResults_Correctly()
        {
            // Arrange
            var results = new List<HolidayResult>
            {
                new HolidayResult { Flight = new Flight { Price = 100 }, Hotel = new Hotel { PricePerNight = 100, Nights = 2 } }, // TotalPrice = 200
                new HolidayResult { Flight = new Flight { Price = 110 }, Hotel = new Hotel { PricePerNight = 105, Nights = 2 } }, // TotalPrice = 210
                new HolidayResult { Flight = new Flight { Price = 120 }, Hotel = new Hotel { PricePerNight = 110, Nights = 2 } }, // TotalPrice = 220
            };

            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(new List<Flight>());
            services.HotelProvider.Setup(x => x.GetHotels()).Returns(new List<Hotel>());
            services.SearchHelper.Setup(x => x.MatchFlightsAndHotels(It.IsAny<SearchHolidayRequest>(), It.IsAny<List<Flight>>(), It.IsAny<List<Hotel>>())).Returns(results);

            var request = CreateValidRequest();
            request.PageNumber = 2;
            request.PageSize = 1;

            // Act
            var result = services.Service.Search(request);

            // Assert
            Assert.That(result.Results.Count(), Is.EqualTo(1));
            Assert.That(result.TotalResults, Is.EqualTo(3));
        }


        [Test]
        public void Return_TotalResults_And_Respects_Sorting()
        {
            // Arrange
            var results = new List<HolidayResult>
            {
                new HolidayResult { Flight = new Flight { Price = 300 }, Hotel = new Hotel { PricePerNight = 50, Nights = 2 } },  // Total = 300 + 100 = 400
                new HolidayResult { Flight = new Flight { Price = 100 }, Hotel = new Hotel { PricePerNight = 150, Nights = 2 } }, // Total = 100 + 300 = 400
                new HolidayResult { Flight = new Flight { Price = 200 }, Hotel = new Hotel { PricePerNight = 100, Nights = 2 } }, // Total = 200 + 200 = 400
            };

            var services = HolidaySearchServiceCollection.Create();
            services.FlightProvider.Setup(x => x.GetFlights()).Returns(new List<Flight>());
            services.HotelProvider.Setup(x => x.GetHotels()).Returns(new List<Hotel>());
            services.SearchHelper.Setup(x => x.MatchFlightsAndHotels(It.IsAny<SearchHolidayRequest>(), It.IsAny<List<Flight>>(), It.IsAny<List<Hotel>>())).Returns(results);

            var request = CreateValidRequest();
            request.PageSize = 3;

            // Act
            var result = services.Service.Search(request);

            // Assert
            var sortedPrices = result.Results.Select(r => r.TotalPrice).ToList();
            Assert.That(sortedPrices, Is.Ordered);
        }


        private SearchHolidayRequest CreateValidRequest()
        {
            return new SearchHolidayRequest
            {
                DepartingFrom = "MAN",
                TravelingTo = "AGP",
                DepartureDate = new DateTime(2025, 07, 01),
                Duration = 7,
                PageNumber = 1,
                PageSize = 10
            };
        }
    }
}
