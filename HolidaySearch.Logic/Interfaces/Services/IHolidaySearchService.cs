using HolidaySearch.Domain.Search;

namespace HolidaySearch.Logic.Interfaces.Services
{
    public interface IHolidaySearchService
    {
        SearchHolidayResponse Search(SearchHolidayRequest request);
    }
}
