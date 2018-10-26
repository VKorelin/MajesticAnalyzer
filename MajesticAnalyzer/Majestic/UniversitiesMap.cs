using CsvHelper.Configuration;
using MajesticAnalyzer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajesticAnalyzer.Majestic
{
    public class UniversitiesMap : ClassMap<UniversityInfo>
    {
        public UniversitiesMap()
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
