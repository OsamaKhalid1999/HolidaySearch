using HolidaySearch.Logic.Interfaces.Helpers;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace HolidaySearch.Logic.Tests.Collections.Helpers
{
    [ExcludeFromCodeCoverage]
    internal class SearchHelperCollection
    {
        internal ISearchHelper Helper;
        internal Mock<IFilteringHelper> FilteringHelper;

        private SearchHelperCollection(ISearchHelper helper, Mock<IFilteringHelper> filteringHelper)
        {
            Helper = helper;
            FilteringHelper = filteringHelper;
        }

        internal static SearchHelperCollection Create(Mock<IFilteringHelper> mockFilteringHelper = null)
        {
            mockFilteringHelper ??= new Mock<IFilteringHelper>(MockBehavior.Loose);
            var helper = new SearchHelper(mockFilteringHelper.Object);
            return new SearchHelperCollection(helper, mockFilteringHelper);
        }
    }
}
