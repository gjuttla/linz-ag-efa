using System.Collections.Generic;
using System.Threading.Tasks;
using LinzLinienEfa.Domain;

namespace LinzLinienEfa.Service.Common
{
    public interface IStopsService
    {
        Task<ICollection<Stop>> FindStopsByNameAsync(string name);
    }
}