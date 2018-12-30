using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace MajesticAnalyzer.IO.Csv
{
    public class CsvHandler<TDomain, TMap> : ICsvWriter<TDomain, TMap>, ICsvParser<TDomain, TMap> where TMap : ClassMap<TDomain>
    {
        public List<TDomain> Parse(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.RegisterClassMap<TMap>();
                    csvReader.Configuration.HasHeaderRecord = true;
                    csvReader.Configuration.Delimiter = ",";

                    return csvReader.GetRecords<TDomain>().ToList();
                }
            }
        }

        public void Write(string fileName, IEnumerable<TDomain> data, bool append = true)
        {
            WriteCsv(fileName, append, x => x.WriteRecords(data));
        }

        public void Write(string fileName, TDomain data, bool append = true)
        {
            WriteCsv(fileName, append, x => x.WriteRecords(new List<TDomain>{data}));
        }

        private static void WriteCsv(string fileName, bool append, Action<CsvWriter> write)
        {
            var addHeaderRecord = AddHeaderRecord(fileName, append);
            
            using (var streamWriter = CreateStreamWriter(fileName, append))
            {
                using (var csvWriter = new CsvWriter(streamWriter))
                {
                    csvWriter.Configuration.RegisterClassMap<TMap>();
                    csvWriter.Configuration.HasHeaderRecord = addHeaderRecord;
                    csvWriter.Configuration.Delimiter = ",";

                    write(csvWriter);
                }
            }
        }

        private static StreamWriter CreateStreamWriter(string fileName, bool append)
            => File.Exists(fileName) && append ? File.AppendText(fileName) : File.CreateText(fileName);

        private static bool AddHeaderRecord(string fileName, bool append) => !(File.Exists(fileName) && append);
    }
}