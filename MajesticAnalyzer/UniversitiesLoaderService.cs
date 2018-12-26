using System;
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
        private readonly IBacklinksLoader _backlinksLoader;
        private readonly IRefdomainsLoader _refdomainsLoader;
        private readonly IPathProvider _pathProvider;

        public UniversitiesLoaderService(
            IWebsitesInfoLoader websitesInfoLoader,
            IBacklinksLoader backlinksLoader, 
            IRefdomainsLoader refdomainsLoader,
            IPathProvider pathProvider)
        {
            _websitesInfoLoader = websitesInfoLoader;
            _backlinksLoader = backlinksLoader;
            _refdomainsLoader = refdomainsLoader; 
            _pathProvider = pathProvider;
        }

        public List<University> LoadUniversities()
        {
            var universityInfos = _websitesInfoLoader.Load();
            var reffDataFolders = new HashSet<string>(_pathProvider.ChildDirectories);

            return universityInfos
                .Where(x => reffDataFolders.Contains(x.Uri.Host))
                .Select(LoadUniversity)
                .ToList();
        }

        private University LoadUniversity(UniversityInfo universityInfo)
        {
            var reffDomains = _refdomainsLoader.Load(universityInfo);
            if (!reffDomains.Any())
            {
                throw new InvalidDataException($"No reff domains for {universityInfo} university found");
            }
            
            var backlinks = _backlinksLoader.Load(universityInfo);

            var reffResources = new List<ReffResource>();
            reffDomains.ForEach(x => reffResources.Add(
                new ReffResource
                {
                    Domain = x,
                    University = universityInfo
                }));
                
            var reffResourcesMap = reffResources.ToDictionary(x => x.Domain.Host);

            foreach (var backlink in backlinks)
            {
                var backlinkUri = new Uri(backlink.Url);

                if (reffResourcesMap.TryGetValue(backlinkUri.Host, out var resource))
                {
                    resource.Backlinks.Add(backlink.Url);
                }
            }

            return new University
            {
                Info = universityInfo,
                ReffResources = reffResources
            };
        }
    }
}
