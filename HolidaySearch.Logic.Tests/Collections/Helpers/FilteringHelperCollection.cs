using HolidaySearch.Logic.Helpers;
using HolidaySearch.Logic.Interfaces.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace HolidaySearch.Logic.Tests.Collections.Helpers
{
    [ExcludeFromCodeCoverage]
    internal class FilteringHelperCollection
    {
        internal IFilteringHelper Helper;

        private FilteringHelperCollection(IFilteringHelper helper)
        {
            Helper = helper;
        }

        internal static FilteringHelperCollection Create()
        {
            var helper = new FilteringHelper();
            return new FilteringHelperCollection(helper);
        }
    }
}