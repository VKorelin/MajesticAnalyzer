using CsvHelper.Configuration;
using MajesticAnalyzer.Domain;
using System;

namespace MajesticAnalyzer.Majestic
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebsitesInfoMap : ClassMap<UniversityInfo>
    {
        public WebsitesInfoMap()
        {
            Map(m => m.Name).Name("Title");
            Map(m => m.Rank).Name("Ranking");
            Map(m => m.Uri).Name("URL").ConvertUsing(x => new Uri($"http://{x.GetField("URL")}"));
            Map(m => m.ReffDomainsCount).Name("ExtDomains");
            Map(m => m.BacklinksCount).Name("ExtBackLinks");
            Map(m => m.Impact).Name("Impact");
            Map(m => m.Presence).Name("Presence");
            Map(m => m.Country).Name("Country");
            Map(m => m.Region).Name("Region");
        }
    }
}
