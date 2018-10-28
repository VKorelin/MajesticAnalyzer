using CsvHelper.Configuration;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.Majestic
{
    public class BacklinksMap : ClassMap<Backlink>
    {
        public BacklinksMap()
        {
            Map(x => x.Url).Name("Source URL");
        }
    }
}