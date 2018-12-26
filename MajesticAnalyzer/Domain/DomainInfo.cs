using System;

namespace MajesticAnalyzer.Domain
{
    public class DomainInfo
    {
        public string Host { get; set; }

        public string CountryCode { get; set; }

        public string TotalBacklinks { get; set; }
        
        public Uri MainPage => new Uri($"http://{Host}");

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DomainInfo domainInfo))
            {
                return false;
            }

            return string.Equals(Host, domainInfo.Host);
        }

        public override int GetHashCode() => Host != null ? Host.GetHashCode() : 0;
    }
}