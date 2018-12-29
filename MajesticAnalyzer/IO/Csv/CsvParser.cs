﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace MajesticAnalyzer.IO.Csv
{
    public class CsvParser<TDomain, TMap> : ICsvParser<TDomain, TMap> where TMap : ClassMap<TDomain>
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
    }
}