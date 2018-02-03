using System.Threading.Tasks;
using LinzLinienEfa.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinzLinienEfa.WebApi.Controllers
{
    public class StopsController : Controller
    {
        private readonly IStopsService stopsService;

        public StopsController(IStopsService stopsService)
        {
            this.stopsService = stopsService;
        }
        
        [HttpGet("api/stops/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            var stops = await stopsService.FindStopsByNameAsync(name);
            if (stops.Count == 0)
            {
                return NotFound();
            }
            return Ok(stops);
        }
    }
}