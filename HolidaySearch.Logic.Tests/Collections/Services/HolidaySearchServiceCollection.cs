using HolidaySearch.Logic.Interfaces.Helpers;
using HolidaySearch.Logic.Interfaces.Providers;
using HolidaySearch.Logic.Interfaces.Services;
using HolidaySearch.Logic.Services;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace HolidaySearch.Logic.Tests.Collections.Services
{
    [ExcludeFromCodeCoverage]
    internal class HolidaySearchServiceCollection
    {
        internal IHolidaySearchService Service;
        internal Mock<IFlightProvider> FlightProvider;
        internal Mock<IHotelProvider> HotelProvider;
        internal Mock<ISearchHelper> SearchHelper;

        internal HolidaySearchServiceCollection(
            IHolidaySearchService service,
            Mock<IFlightProvider> flightProvider,
            Mock<IHotelProvider> hotelProvider,
            Mock<ISearchHelper> searchHelper)
        {
            Service = service;
            FlightProvider = flightProvider;
            HotelProvider = hotelProvider;
            SearchHelper = searchHelper;
        }

        internal static HolidaySearchServiceCollection Create(
            Mock<IFlightProvider> mockFlightProvider = null,
            Mock<IHotelProvider> mockHotelProvider = null,
            Mock<ISearchHelper> mockSearchHelper = null)
        {
            mockFlightProvider ??= new Mock<IFlightProvider>(MockBehavior.Loose);
            mockHotelProvider ??= new Mock<IHotelProvider>(MockBehavior.Loose);
            mockSearchHelper ??= new Mock<ISearchHelper>(MockBehavior.Loose);

            IHolidaySearchService service = new HolidaySearchService(
                mockFlightProvider.Object,
                mockHotelProvider.Object,
                mockSearchHelper.Object);

            return new HolidaySearchServiceCollection(service, mockFlightProvider, mockHotelProvider, mockSearchHelper);
        }
    }
}
