using System;
using System.Collections.Generic;
using CsvHelper.Configuration;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer.Majestic
{
    public abstract class DataLoader<T, TMap> : IUniversityInfoDataLoader<T> where TMap : ClassMap<T>
    {
        private readonly ICsvParser<T, TMap> _csvParser;

        protected DataLoader(ICsvParser<T, TMap> csvParser)
        {
            _csvParser = csvParser;
        }
        
        public List<T> Load(UniversityInfo universityInfo)
        {
            if (universityInfo == null)
            {
                throw new ArgumentNullException(nameof(universityInfo));
            }
            
            return _csvParser.Parse(GetPath(universityInfo.Uri.Host));
        }

        protected abstract string GetPath(string universityHost);
    }
}