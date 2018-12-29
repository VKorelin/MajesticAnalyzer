using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MajesticAnalyzer.IO.Csv
{
    public interface ICsvParser<TDomain, TMap> where TMap : ClassMap<TDomain>
    {
        List<TDomain> Parse(string fileName);
    }
}
