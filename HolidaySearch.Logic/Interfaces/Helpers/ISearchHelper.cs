using HolidaySearch.Domain.Search;

namespace HolidaySearch.Logic.Interfaces.Helpers
{
    public interface ISearchHelper
    {
        List<HolidayResult> MatchFlightsAndHotels(SearchHolidayRequest request, IEnumerable<Flight> flights, IEnumerable<Hotel> hotels);
    }

}
