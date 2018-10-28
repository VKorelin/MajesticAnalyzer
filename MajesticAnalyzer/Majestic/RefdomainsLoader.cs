using System.Collections.Generic;
using System.IO;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer.Majestic
{
    public class RefdomainsLoader : IRefdomainsLoader
    {
        private readonly ICsvParser<DomainInfo, RefdomainsMap> _csvParser;
        private readonly IPathProvider _pathProvider;
        
        public RefdomainsLoader(ICsvParser<DomainInfo, RefdomainsMap> csvParser, IPathProvider pathProvider)
        {
            _csvParser = csvParser;
            _pathProvider = pathProvider;
        }
        
        public List<DomainInfo> Load(UniversityInfo universityInfo) 
            => _csvParser.Parse(Path.Combine(_pathProvider.HomeDirectory, universityInfo.Uri.Host, "refdomains.csv"));
    }
}