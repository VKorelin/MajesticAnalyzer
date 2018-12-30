using CsvHelper.Configuration;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.IO.Csv
{
    public class ReffResourceMap : ClassMap<ReffResource>
    {
        public ReffResourceMap()
        {
            Map(x => x.Domain.Host).Name("Host");
            Map(x => x.Title).Name("Title");
            Map(x => x.Description).Name("Description");
        }
    }
}