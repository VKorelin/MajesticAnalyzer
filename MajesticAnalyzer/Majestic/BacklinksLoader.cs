using System.Collections.Generic;
using System.IO;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer.Majestic
{
    public class BacklinksLoader : IBacklinksLoader
    {
        private readonly ICsvParser<Backlink, BacklinksMap> _csvParser;
        private readonly IPathProvider _pathProvider;
        
        public BacklinksLoader(ICsvParser<Backlink, BacklinksMap> csvParser, IPathProvider pathProvider)
        {
            _csvParser = csvParser;
            _pathProvider = pathProvider;
        }
        
        public List<Backlink> Load(UniversityInfo universityInfo) 
            => _csvParser.Parse(Path.Combine(_pathProvider.HomeDirectory, universityInfo.Uri.Host, "backlinks.csv"));
    }
}