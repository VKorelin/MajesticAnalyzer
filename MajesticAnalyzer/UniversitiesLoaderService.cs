using System.Collections.Generic;
using System.IO;
using System.Linq;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.Majestic;

namespace MajesticAnalyzer
{
    public class UniversitiesLoaderService : IUniversitiesLoaderService
    {
        private readonly IWebsitesInfoLoader _websitesInfoLoader;
        private readonly IPathProvider _pathProvider;
        private readonly IUniversityLoaderService _universitiesLoaderService;

        public UniversitiesLoaderService(
            IWebsitesInfoLoader websitesInfoLoader,
            IPathProvider pathProvider,
            IUniversityLoaderService universitiesLoaderService)
        {
            _websitesInfoLoader = websitesInfoLoader;
            _pathProvider = pathProvider;
            _universitiesLoaderService = universitiesLoaderService;
        }

        public University LoadUniversity(string universityHost)
        {
            var universitiesFolders = new HashSet<string>(_pathProvider.ChildDirectories);
            if (!universitiesFolders.Contains(universityHost))
            {
                throw new DirectoryNotFoundException($"Cannot find {universityHost} university folder");
            }
            
            var universityInfo = _websitesInfoLoader.Load().FirstOrDefault(x => string.Equals(x.Uri.Host, universityHost));
            return _universitiesLoaderService.Load(universityInfo);
        }

        public List<University> LoadUniversities()
        {
            var universityInfos = _websitesInfoLoader.Load();
            var universitiesFolders = new HashSet<string>(_pathProvider.ChildDirectories);

            return universityInfos
                .Where(x => universitiesFolders.Contains(x.Uri.Host))
                .Select(_universitiesLoaderService.Load)
                .ToList();
        }
    }
}
