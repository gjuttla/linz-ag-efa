using System.Collections.Generic;

namespace LinzLinienEfa.Common.Configuration
{
    public class AppConfig : IAppConfig
    {
        public string EfaApiBaseUrl { get; set; }
        public string TripEndpoint { get; set; }
        public string DisplayMontitorEndpoint { get; set; }
        public ICollection<string> StopNameCityPrefixes { get; set; }
        public IDictionary<string, string> Replacements { get; set; }
    }
}