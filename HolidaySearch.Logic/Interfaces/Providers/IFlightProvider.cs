using HolidaySearch.Domain.Search;

namespace HolidaySearch.Logic.Interfaces.Providers
{
    public interface IFlightProvider
    {
        List<Flight> GetFlights();
    }
}
