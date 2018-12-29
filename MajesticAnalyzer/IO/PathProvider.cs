using System.Collections.Generic;
using System.IO;
using System.Linq;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.IO
{
    public class PathProvider : IPathProvider
    {
        private readonly IConfigurationProvider _configurationProvider;
        
        public string HomeDirectory => _configurationProvider.GetHomeDirectory();
        
        public IEnumerable<string> ChildDirectories => Directory.GetDirectories(HomeDirectory).Select(Path.GetFileName);

        public PathProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public string GetContentOutputPath(UniversityInfo universityInfo) => Path.Combine(HomeDirectory, universityInfo.Uri.Host, "content.csv");
    }
}