using System.Threading.Tasks;
using LinzLinienEfa.Common.Adapter;
using Microsoft.AspNetCore.Mvc;

namespace LinzLinienEfa.WebApi.Controllers
{
    public class DeparturesController : Controller
    {
        // Default limit according to EFA API documentation.
        private const uint DefaultLimit = 40;
        private readonly IDeparturesAdapter departuresAdapter;

        public DeparturesController(IDeparturesAdapter departuresAdapter)
        {
            this.departuresAdapter = departuresAdapter;
        }
        
        [HttpGet("api/departures/{stopId}")]
        public async Task<IActionResult> Get(string stopId)
        {
            if (string.IsNullOrWhiteSpace(stopId))
            {
                return BadRequest();
            }
            var departures = await departuresAdapter.GetDeparturesForStopAsync(stopId, DefaultLimit);
            if (departures.Count == 0)
            {
                return NotFound();
            }
            return Ok(departures);
        }
    }
}