using CsvHelper.Configuration;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.IO.Csv
{
    public class BacklinksMap : ClassMap<Backlink>
    {
        public BacklinksMap()
        {
            Map(x => x.Url).Name("Source URL");
        }
    }
}