using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Interfaces.Providers;

namespace HolidaySearch.Logic.Provider
{
    public class FlightProvider : IFlightProvider
    {    
        List<Flight> IFlightProvider.GetFlights()
        {
            return null;
        }
    }
}
