using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MajesticAnalyzer.IO.Csv
{
    public interface ICsvWriter<in TDomain, TMap> where TMap : ClassMap<TDomain>
    {
        void Write(string fileName, IEnumerable<TDomain> data, bool append = true);
        void Write(string fileName, TDomain data, bool append = true);
    }
}