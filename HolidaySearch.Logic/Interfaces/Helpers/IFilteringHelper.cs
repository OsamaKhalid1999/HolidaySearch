using HolidaySearch.Domain.Search;

namespace HolidaySearch.Logic.Interfaces.Helpers
{
    public interface IFilteringHelper
    {
        List<Flight> FilterFlights(IEnumerable<Flight> flights, string from, string to, DateTime date);
        List<Hotel> FilterHotels(IEnumerable<Hotel> hotels, string to, DateTime date, int nights);
        string GetAirportSearchKey(string input);       
    }
}
