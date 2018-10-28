using CsvHelper.Configuration;
using System.Collections.Generic;

namespace MajesticAnalyzer.Parser
{
    public interface ICsvParser<TDomain, TMap> where TMap : ClassMap<TDomain>
    {
        List<TDomain> Parse(string fileName);
    }
}
