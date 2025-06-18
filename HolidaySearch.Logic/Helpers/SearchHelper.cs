using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Interfaces.Helpers;

namespace HolidaySearch.Logic.Helpers
{
    internal class SearchHelper : ISearchHelper
    {
        List<HolidayResult> ISearchHelper.MatchFlightsAndHotels(SearchHolidayRequest request, IEnumerable<Flight> flights, IEnumerable<Hotel> hotels)
        {
            return null;
        }
    }
}

