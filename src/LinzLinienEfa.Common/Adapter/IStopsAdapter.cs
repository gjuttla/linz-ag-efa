using System.Collections.Generic;
using System.Threading.Tasks;
using LinzLinienEfa.Common.Domain;

namespace LinzLinienEfa.Common.Adapter
{
    public interface IStopsAdapter
    {
        Task<ICollection<Stop>> FindStopsByNameAsync(string name);
    }
}