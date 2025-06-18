using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Interfaces.Providers;

namespace HolidaySearch.Logic.Provider
{
    public class HotelProvider : IHotelProvider
    {
        private const string HotelsFilePath = "Resources/hotels.json";

        List<Hotel> IHotelProvider.GetHotels()
        {
            return null;
        }
    }
}
