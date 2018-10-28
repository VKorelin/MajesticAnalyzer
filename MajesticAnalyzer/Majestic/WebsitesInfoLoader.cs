using System.Collections.Generic;
using System.IO;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer.Majestic
{
    public class WebsitesInfoLoader : IWebsitesInfoLoader
    {
        private readonly ICsvParser<UniversityInfo, WebsitesInfoMap> _csvParser;
        private readonly IPathProvider _pathProvider;

        private string FileName => Path.Combine(_pathProvider.HomeDirectory, "WebsitesInfo.csv");
        
        public WebsitesInfoLoader(ICsvParser<UniversityInfo, WebsitesInfoMap> csvParser, IPathProvider pathProvider)
        {
            _csvParser = csvParser;
            _pathProvider = pathProvider;
        }

        public List<UniversityInfo> Load() => _csvParser.Parse(FileName);
    }
}