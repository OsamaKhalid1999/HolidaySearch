using HolidaySearch.Domain.Search;
using HolidaySearch.Logic.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolidaySearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidaySearchService _holidaySearchService;

        public HolidayController(IHolidaySearchService holidaySearchService)
        {
            _holidaySearchService = holidaySearchService;
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(SearchHolidayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Search(SearchHolidayRequest request)
        {          
            var result = _holidaySearchService.Search(request);
            return Ok(result);
        }
    }
}
