using CsvHelper.Configuration;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.Majestic
{
    public class RefdomainsMap : ClassMap<DomainInfo>
    {
        public RefdomainsMap()
        {
            Map(x => x.Host).Name("Domain");
            Map(x => x.CountryCode).Name("GeoCountryCode");
            Map(x => x.TotalBacklinks).Name("TotalBackLinks");
        }
    }
}