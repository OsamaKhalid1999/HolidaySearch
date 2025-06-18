using HolidaySearch.Logic.Configuration;
using HolidaySearch.Logic.Interfaces.Providers;
using HolidaySearch.Logic.Provider;
using Microsoft.Extensions.Options;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace HolidaySearch.Tests.Collections.Providers
{
    [ExcludeFromCodeCoverage]
    internal class HotelProviderCollection
    {
        internal IHotelProvider Provider;
        internal Mock<IOptionsSnapshot<DataPath>> ConfigMock;

        private HotelProviderCollection(IHotelProvider provider, Mock<IOptionsSnapshot<DataPath>> configMock)
        {
            Provider = provider;
            ConfigMock = configMock;
        }

        internal static HotelProviderCollection Create(string hotelPath = "Resources/hotels.json")
        {
            var config = new DataPath { HotelsFilePath = hotelPath };
            var mockOptions = new Mock<IOptionsSnapshot<DataPath>>(MockBehavior.Strict);
            mockOptions.Setup(x => x.Value).Returns(config);

            var provider = new HotelProvider(mockOptions.Object);
            return new HotelProviderCollection(provider, mockOptions);
        }
    }
}
