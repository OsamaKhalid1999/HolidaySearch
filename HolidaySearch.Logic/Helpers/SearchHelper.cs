using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Interfaces.Helpers;

public class SearchHelper : ISearchHelper
{
    private readonly IFilteringHelper _filteringHelper;

    public SearchHelper(IFilteringHelper filteringHelper)
    {
        _filteringHelper = filteringHelper;
    }

    List<HolidayResult> ISearchHelper.MatchFlightsAndHotels(
        SearchHolidayRequest request,
        IEnumerable<Flight> allFlights,
        IEnumerable<Hotel> allHotels)
    {
        if (allFlights == null)
        {
            throw new ArgumentNullException(nameof(allFlights), "Flights collection cannot be null.");
        }

        if (allHotels == null)
        {
            throw new ArgumentNullException(nameof(allHotels), "Hotels collection cannot be null.");
        }

        var requestedDepartureAirport = _filteringHelper.GetAirportSearchKey(request.DepartingFrom);
        var destinationAirport = request.TravelingTo?.Trim().ToUpperInvariant();
        var requestedDepartureDate = request.DepartureDate.Date;
        var numberOfNights = request.Duration;

        var matchingFlights = _filteringHelper.FilterFlights(allFlights, requestedDepartureAirport, destinationAirport, requestedDepartureDate);
        var matchingHotels = _filteringHelper.FilterHotels(allHotels, destinationAirport, requestedDepartureDate, numberOfNights);

        if (matchingFlights?.Count == 0 || matchingHotels?.Count == 0)
        {
            return new List<HolidayResult>();
        }

        var matchingResults = new List<HolidayResult>();

        foreach (var flight in matchingFlights)
        {
            foreach (var hotel in matchingHotels)
            {
                matchingResults.Add(new HolidayResult
                {
                    Flight = flight,
                    Hotel = hotel
                });
            }
        }

        return matchingResults.OrderBy(result => result.TotalPrice).ToList();
    }
}
