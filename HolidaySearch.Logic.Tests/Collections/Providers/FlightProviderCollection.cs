using HolidaySearch.Logic.Configuration;
using HolidaySearch.Logic.Interfaces.Providers;
using HolidaySearch.Logic.Provider;
using Microsoft.Extensions.Options;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace HolidaySearch.Logic.Tests.Collections.Providers
{
    [ExcludeFromCodeCoverage]
    internal class FlightProviderCollection
    {
        internal IFlightProvider Provider;
        internal Mock<IOptionsSnapshot<DataPath>> ConfigMock;

        private FlightProviderCollection(IFlightProvider provider, Mock<IOptionsSnapshot<DataPath>> configMock)
        {
            Provider = provider;
            ConfigMock = configMock;
        }

        internal static FlightProviderCollection Create(string flightPath = "Resources/flights.json")
        {
            var config = new DataPath { FlightsFilePath = flightPath };
            var mockOptions = new Mock<IOptionsSnapshot<DataPath>>(MockBehavior.Loose);
            mockOptions.Setup(x => x.Value).Returns(config);

            var provider = new FlightProvider(mockOptions.Object);
            return new FlightProviderCollection(provider, mockOptions);
        }
    }
}
