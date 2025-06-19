using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Interfaces.Helpers;
using HolidaySearch.Logic.Interfaces.Providers;
using HolidaySearch.Logic.Interfaces.Services;

namespace HolidaySearch.Logic.Services
{
    public class HolidaySearchService : IHolidaySearchService
    {
        private readonly IFlightProvider _flightProvider;
        private readonly IHotelProvider _hotelProvider;
        private readonly ISearchHelper _searchHelper;

        public HolidaySearchService(
           IFlightProvider flightProvider,
           IHotelProvider hotelProvider,
           ISearchHelper searchHelper)
        {
            _flightProvider = flightProvider;
            _hotelProvider = hotelProvider;
            _searchHelper = searchHelper;
        }

        SearchHolidayResponse IHolidaySearchService.Search(SearchHolidayRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Search request cannot be null.");
            }

            ValidateSearchHolidayRequest(request);

            var flights = _flightProvider.GetFlights();
            if (flights == null)
            {
                throw new InvalidOperationException("Flight data could not be retrieved.");
            }

            var hotels = _hotelProvider.GetHotels();
            if (hotels == null)
            {
                throw new InvalidOperationException("Hotel data could not be retrieved.");
            }

            var filtered = _searchHelper.MatchFlightsAndHotels(request, flights, hotels);

            if (filtered == null)
            {
                throw new InvalidOperationException("Filtered results could not be calculated.");
            }

            var paged = filtered
                .OrderBy(x => x.TotalPrice)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new SearchHolidayResponse
            {
                TotalResults = filtered.Count(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Results = paged
            };
        }

        private void ValidateSearchHolidayRequest(SearchHolidayRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DepartingFrom))
            {
                throw new ArgumentException("DepartingFrom must be provided.", nameof(request.DepartingFrom));
            }

            if (string.IsNullOrWhiteSpace(request.TravelingTo))
            {
                throw new ArgumentException("TravelingTo must be provided.", nameof(request.TravelingTo));
            }

            if (request.DepartureDate == default)
            {
                throw new ArgumentException("Valid DepartureDate must be provided.", nameof(request.DepartureDate));
            }

            if (request.Duration <= 0)
            {
                throw new ArgumentException("Duration must be greater than zero.", nameof(request.Duration));
            }

            if (request.PageNumber <= 0)
            {
                throw new ArgumentException("PageNumber must be greater than zero.", nameof(request.PageNumber));
            }

            if (request.PageSize <= 0)
            {
                throw new ArgumentException("PageSize must be greater than zero.", nameof(request.PageSize));
            }
        }
    }
}
