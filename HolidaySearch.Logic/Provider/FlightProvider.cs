using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Configuration;
using HolidaySearch.Logic.Interfaces.Providers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HolidaySearch.Logic.Provider
{
    public class FlightProvider : IFlightProvider
    {
        private readonly string _filePath;

        public FlightProvider(IOptionsSnapshot<DataPath> config)
        {
            _filePath = config.Value.FlightsFilePath;
        }

        List<Flight> IFlightProvider.GetFlights()
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, _filePath);
            var json = File.ReadAllText(fullPath);
            return JsonSerializer.Deserialize<List<Flight>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
