using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Interfaces.Helpers;

namespace HolidaySearch.Logic.Helpers
{
    public class FilteringHelper : IFilteringHelper
    {
        List<Flight> IFilteringHelper.FilterFlights(IEnumerable<Flight> flights, string from, string to, DateTime date)
        {
            if (flights == null)
            {
                throw new ArgumentNullException(nameof(flights));
            }

            if (string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("Departure airport cannot be null or empty.", nameof(from));
            }

            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("Destination airport cannot be null or empty.", nameof(to));
            }

            var requestedDeparture = from.Trim().ToUpperInvariant();
            var requestedDestination = to.Trim().ToUpperInvariant();
            var requestedDate = date.Date;

            var matchingFlights = new List<Flight>();

            foreach (var flight in flights)
            {
                if (flight?.From != null && flight.To != null)
                {
                    var actualDeparture = flight.From.Trim().ToUpperInvariant();
                    var actualDestination = flight.To.Trim().ToUpperInvariant();
                    var flightDate = flight.DepartureDate.Date;

                    var isDepartureMatch = IsMatchingDepartureAirport(actualDeparture, requestedDeparture);
                    var isDestinationMatch = actualDestination == requestedDestination;
                    var isDateMatch = flightDate == requestedDate;

                    if (isDepartureMatch && isDestinationMatch && isDateMatch)
                    {
                        matchingFlights.Add(flight);
                    }
                }
            }

            return matchingFlights;
        }

        List<Hotel> IFilteringHelper.FilterHotels(IEnumerable<Hotel> hotels, string to, DateTime date, int nights)
        {
            if (hotels == null)
            {
                throw new ArgumentNullException(nameof(hotels));
            }

            return hotels.Where(h =>
             h.ArrivalDate.Date == date &&
             h.Nights == nights &&
             h.LocalAirports != null &&
             h.LocalAirports
                 .Select(a => a.Trim().ToUpperInvariant())
                 .Contains(to)
            ).ToList();

        }

        string IFilteringHelper.GetAirportSearchKey(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "ANY";
            }

            var trimmedInput = input.Trim().ToUpperInvariant();
            if (trimmedInput.Contains("LONDON"))
            {
                return "LONDON";
            }

            if (trimmedInput == "ANY AIRPORT")
            {
                return "ANY";
            }

            return trimmedInput;
        }

        private bool IsMatchingDepartureAirport(string actualAirportCode, string requestedAirportGroup)
        {
            var normalizedActualCode = actualAirportCode?.Trim().ToUpperInvariant();

            if (requestedAirportGroup == "ANY")
            {
                return true;
            }

            if (requestedAirportGroup == "LONDON")
            {
                return normalizedActualCode == "LGW" || normalizedActualCode == "LTN" || normalizedActualCode == "STN";
            }

            return normalizedActualCode == requestedAirportGroup;
        }

    }
}
