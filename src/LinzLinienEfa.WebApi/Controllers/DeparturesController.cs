using System.Threading.Tasks;
using LinzLinienEfa.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinzLinienEfa.WebApi.Controllers
{
    public class DeparturesController : Controller
    {
        // Default limit according to EFA API documentation.
        private const uint DefaultLimit = 40;
        private readonly IDeparturesService departuresService;

        public DeparturesController(IDeparturesService departuresService)
        {
            this.departuresService = departuresService;
        }
        
        [HttpGet("api/departures/{stopId}")]
        public async Task<IActionResult> Get(string stopId)
        {
            if (string.IsNullOrWhiteSpace(stopId))
            {
                return BadRequest();
            }
            var departures = await departuresService.GetDeparturesForStopAsync(stopId, DefaultLimit);
            if (departures.Count == 0)
            {
                return NotFound();
            }
            return Ok(departures);
        }
    }
}