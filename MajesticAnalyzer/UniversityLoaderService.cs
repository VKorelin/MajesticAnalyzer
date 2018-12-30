using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Majestic;

namespace MajesticAnalyzer
{
    public class UniversityLoaderService : IUniversityLoaderService
    {
        private readonly IBacklinksLoader _backlinksLoader;
        private readonly IRefdomainsLoader _refdomainsLoader;

        public UniversityLoaderService(IBacklinksLoader backlinksLoader, IRefdomainsLoader refdomainsLoader)
        {
            _backlinksLoader = backlinksLoader;
            _refdomainsLoader = refdomainsLoader;
        }

        public University Load(UniversityInfo universityInfo)
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
                    UniversityInfo = universityInfo
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