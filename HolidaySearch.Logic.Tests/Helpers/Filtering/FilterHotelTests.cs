using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Tests.Collections.Helpers;

namespace HolidaySearch.Logic.Tests.Helpers.Filtering
{
    [TestFixture]
    internal class FilterHotelTests
    {
        [Test]
        public void FilterHotels_ReturnsHotel_WhenCriteriaMatches()
        {
            var helper = FilteringHelperCollection.Create().Helper;
            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    ArrivalDate = new DateTime(2025, 6, 1),
                    Nights = 7,
                    LocalAirports = new List<string> { "CDG", "ORY" }
                }
            };

            var result = helper.FilterHotels(hotels, "CDG", new DateTime(2025, 6, 1), 7);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterHotels_ReturnsEmpty_WhenDateDoesNotMatch()
        {
            var helper = FilteringHelperCollection.Create().Helper;
            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    ArrivalDate = new DateTime(2025, 6, 2),
                    Nights = 7,
                    LocalAirports = new List<string> { "CDG" }
                }
            };

            var result = helper.FilterHotels(hotels, "CDG", new DateTime(2025, 6, 1), 7);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FilterHotels_ReturnsEmpty_WhenNightsDoNotMatch()
        {
            var helper = FilteringHelperCollection.Create().Helper;
            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    ArrivalDate = new DateTime(2025, 6, 1),
                    Nights = 5,
                    LocalAirports = new List<string> { "CDG" }
                }
            };

            var result = helper.FilterHotels(hotels, "CDG", new DateTime(2025, 6, 1), 7);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FilterHotels_ThrowsArgumentNullException_WhenHotelsIsNull()
        {
            var helper = FilteringHelperCollection.Create().Helper;
            Assert.Throws<ArgumentNullException>(() => helper.FilterHotels(null, "CDG", DateTime.Today, 5));
        }

        [Test]
        public void FilterHotels_IgnoresHotel_WhenLocalAirportsIsNull()
        {
            var helper = FilteringHelperCollection.Create().Helper;
            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    ArrivalDate = DateTime.Today,
                    Nights = 5,
                    LocalAirports = null
                }
            };

            var result = helper.FilterHotels(hotels, "CDG", DateTime.Today, 5);

            Assert.That(result, Is.Empty);
        }
    }
}
