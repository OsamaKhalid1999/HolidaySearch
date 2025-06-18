using HolidaySearch.Logic.Helpers;
using HolidaySearch.Logic.Interfaces.Helpers;
using HolidaySearch.Logic.Interfaces.Providers;
using HolidaySearch.Logic.Interfaces.Services;
using HolidaySearch.Logic.Provider;
using HolidaySearch.Logic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HolidaySearch.Logic.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHolidaySearchServices(this IServiceCollection services)
        {
            // Core search service
            services.AddTransient<IHolidaySearchService, HolidaySearchService>();

            // Data providers
            services.AddTransient<IFlightProvider, FlightProvider>();
            services.AddTransient<IHotelProvider, HotelProvider>();

            // Helpers
            services.AddTransient<ISearchHelper, SearchHelper>();

            return services;
        }
    }
}
