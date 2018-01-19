using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using LinzLinienEfa.Common.Adapter;
using LinzLinienEfa.Common.Configuration;
using LinzLinienEfa.Common.Domain;

namespace LinzLinienEfa.Adapter
{
    public class DeparturesAdapter : IDeparturesAdapter
    {
        private readonly IAppConfig appConfig;

        public DeparturesAdapter(IAppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
        
        public Task<ICollection<Departure>> GetDeparturesForStopAsync(Stop stop, uint limit)
        {
            return GetDeparturesForStopAsync(stop.Id, limit);
        }

        public async Task<ICollection<Departure>> GetDeparturesForStopAsync(string stopId, uint limit)
        {
            var requestResult = await appConfig.EfaApiBaseUrl
                .AppendPathSegment(appConfig.DisplayMontitorEndpoint)
                .SetQueryParams(new
                {
                    outputFormat = "JSON",
                    stateless = 1,
                    type_dm = "any",
                    mode = "direct",
                    name_dm = stopId,
                    limit = limit
                })
                .GetJsonAsync();
            
            var departures = new List<Departure>();

            if (requestResult.departureList == null)
            {
                return departures;
            }

            var isSingleDeparture = requestResult.departureList is IDictionary<string, object>;

            if (isSingleDeparture)
            {
                departures.Add(CreateDeparture(requestResult.departureList.departure));    
            }
            else
            {
                foreach (var departure in requestResult.departureList)
                {
                    departures.Add(CreateDeparture(departure));
                }
            }
            return departures;
        }

        private static Departure CreateDeparture(dynamic departure)
        {
            return new Departure()
            {
                CountdownInMinutes = uint.Parse(departure.countdown),
                Time = ParseDepartureTime(departure.dateTime),
                Line = CreateLine(departure.servingLine)
            };
        }

        private static DateTime ParseDepartureTime(dynamic dateTime)
        {
            return new DateTime(
                int.Parse(dateTime.year), int.Parse(dateTime.month), int.Parse(dateTime.day), 
                int.Parse(dateTime.hour), int.Parse(dateTime.minute), second: 0);
        }
        
        private static Line CreateLine(dynamic line)
        {
            return new Line()
            {
                Number = uint.Parse(line.number),
                Type = GetLineType(line),
                Direction = line.direction,
                InitialOriginStopName = line.directionFrom
            };
        }

        private static TransportationMean GetLineType(dynamic line)
        {
            var transportationMeanNr = int.Parse(line.code);
            switch (transportationMeanNr)
            {
                case 3:
                    return TransportationMean.Bus;
                case 4:
                    return TransportationMean.Tram;
                default:
                    return TransportationMean.Unknown;
            }
        }
    }
}