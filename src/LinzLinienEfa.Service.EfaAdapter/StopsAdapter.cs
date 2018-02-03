using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using LinzLinienEfa.Common.Configuration;
using LinzLinienEfa.Common.Extensions;
using LinzLinienEfa.Domain;
using LinzLinienEfa.Service.Common;
using Microsoft.Extensions.Options;

namespace LinzLinienEfa.Service.EfaAdapter
{
    public class StopsAdapter : IStopsService
    {
        private readonly IAppConfig appConfig;
        private readonly IEnumerable<string> stopNamePrefixesSorted;
        
        public StopsAdapter(IOptions<AppConfig> options)
        {
            this.appConfig = options.Value;
            stopNamePrefixesSorted = from prefix in appConfig.StopNameCityPrefixes orderby prefix.Length descending select prefix;
        }
        
        public async Task<ICollection<Stop>> FindStopsByNameAsync(string name)
        {
            var requestResult = await appConfig.EfaApiBaseUrl
                .AppendPathSegment(appConfig.TripEndpoint)
                .SetQueryParams(new
                {
                    outputFormat = "JSON",
                    locationServerActive = 1,
                    stateless = 1,
                    anyObjFilter_dm = 2,
                    type_origin = "stopID",
                    name_origin = name
                })
                .GetJsonAsync();
            
            var stops = new List<Stop>();

            if (requestResult.origin.points == null)
            {
                return stops;
            }
            
            var isSinglePoint = requestResult.origin.points is IDictionary<string, object>;
            
            if (isSinglePoint)
            {
                stops.Add(CreateStop(requestResult.origin.points.point));
            }
            else
            {
                foreach (var point in requestResult.origin.points)
                {
                    stops.Add(CreateStop(point));
                }
            }
            return stops;
        }

        private Stop CreateStop(dynamic point)
        {
            return new Stop()
            {
                Id = point.stateless,
                Name = (point.name as string).RemoveAnyPrefixes(stopNamePrefixesSorted).ReplaceAll(appConfig.Replacements)
            };
        }
    }
}