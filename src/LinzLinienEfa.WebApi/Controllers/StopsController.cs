using System.Threading.Tasks;
using LinzLinienEfa.Common.Adapter;
using Microsoft.AspNetCore.Mvc;

namespace LinzLinienEfa.WebApi.Controllers
{
    public class StopsController : Controller
    {
        private readonly IStopsAdapter stopsAdapter;

        public StopsController(IStopsAdapter stopsAdapter)
        {
            this.stopsAdapter = stopsAdapter;
        }
        
        [HttpGet("api/stops/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            var stops = await stopsAdapter.FindStopsByNameAsync(name);
            if (stops.Count == 0)
            {
                return NotFound();
            }
            return Ok(stops);
        }
    }
}