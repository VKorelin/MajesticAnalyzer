using System;

namespace MajesticAnalyzer.Domain
{
    public class DomainInfo
    {
        public string Domain { get; set; }

        public string CountryCode { get; set; }

        public string TotalBacklinks { get; set; }
        
        public Uri MainPage => new Uri(Domain); 
    }
}