using HolidaySearch.Logic.Tests.Collections.Helpers;

namespace HolidaySearch.Logic.Tests.Helpers.Filtering
{
    [TestFixture]
    public class GetAirportSearchKeyTests
    {
        [Test]
        public void GetAirportSearchKeyt_ReturnsAny_WhenInputIsNullOrWhitespace()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;

            // Act
            var result = helper.GetAirportSearchKey(" ");

            // Assert
            Assert.That(result, Is.EqualTo("ANY"));
        }

        [Test]
        public void GetAirportSearchKey_ReturnsLondon_WhenInputContainsLondon()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;

            // Act
            var result = helper.GetAirportSearchKey("London Heathrow");

            // Assert
            Assert.That(result, Is.EqualTo("LONDON"));
        }

        [Test]
        public void GetAirportSearchKey_ReturnsAny_WhenInputIsAnyAirport()
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;

            // Act
            var result = helper.GetAirportSearchKey("Any Airport");

            // Assert
            Assert.That(result, Is.EqualTo("ANY"));
        }

        [TestCase("  jfk  ", "JFK")]
        [TestCase("MAN", "MAN")]
        public void GetAirportSearchKey_ReturnsTrimmedUppercaseCode_WhenCustomInputProvided(string input, string expected)
        {
            // Arrange
            var helper = FilteringHelperCollection.Create().Helper;

            // Act
            var result = helper.GetAirportSearchKey(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
