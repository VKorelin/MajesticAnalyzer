using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MajesticAnalyzer.Parser
{
    public class CsvParser<TDomain, TMap> : ICsvParser<TDomain, TMap> where TMap : ClassMap<TDomain>
    {
        private readonly string fileName;

        public CsvParser(string fileName)
        {
            this.fileName = fileName;
        }

        public IList<TDomain> Parse()
        {
            using (var streamReader = new StreamReader(fileName))
            {
                var csvReader = new CsvReader(streamReader);

                csvReader.Configuration.RegisterClassMap<TMap>();
                csvReader.Configuration.HasHeaderRecord = true;
                csvReader.Configuration.Delimiter = ";";

                return csvReader.GetRecords<TDomain>().ToList();
            }
        }
    }
}
