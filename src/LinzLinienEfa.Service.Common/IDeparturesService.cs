using System.Collections.Generic;
using System.Threading.Tasks;
using LinzLinienEfa.Domain;

namespace LinzLinienEfa.Service.Common
{
    public interface IDeparturesService
    {
        Task<ICollection<Departure>> GetDeparturesForStopAsync(Stop stop, uint limit);
        Task<ICollection<Departure>> GetDeparturesForStopAsync(string stopId, uint limit);
    }
}