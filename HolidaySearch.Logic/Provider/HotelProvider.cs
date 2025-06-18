using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Configuration;
using HolidaySearch.Logic.Interfaces.Providers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HolidaySearch.Logic.Provider
{
    public class HotelProvider : IHotelProvider
    {
        private readonly string _filePath;
        public HotelProvider(IOptionsSnapshot<DataPath> config)
        {
            _filePath = config.Value.HotelsFilePath;
        }

        List<Hotel> IHotelProvider.GetHotels()
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, _filePath);
            var json = File.ReadAllText(fullPath);
            return JsonSerializer.Deserialize<List<Hotel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
