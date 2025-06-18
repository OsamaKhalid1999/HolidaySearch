using HolidaySearch.Domain.Search;

namespace HolidaySearch.Logic.Interfaces.Providers
{
    public interface IHotelProvider
    {      
        List<Hotel> GetHotels();
    }
}
