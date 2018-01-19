using System.Collections.Generic;

namespace LinzLinienEfa.Common.Configuration
{
    public interface IAppConfig
    {
        string EfaApiBaseUrl { get; }
        string TripEndpoint { get; }
        string DisplayMontitorEndpoint { get; }
        ICollection<string> StopNameCityPrefixes { get; }
        IDictionary<string, string> Replacements { get; }
    }
}