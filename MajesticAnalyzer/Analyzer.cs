using System.Collections.Generic;
using MajesticAnalyzer.Html;
using MajesticAnalyzer.Majestic;

namespace MajesticAnalyzer
{
    public class Analyzer : IAnalyzer
    {
        private readonly IHtmlLoaderFactory _htmlLoaderFactory;
        private readonly IWebsitesInfoLoader _websitesInfoLoader;
        private readonly IBacklinksLoader _backlinksLoader;
        private readonly IRefdomainsLoader _refdomainsLoader;
        private readonly IPathProvider _pathProvider;

        public Analyzer(
            IHtmlLoaderFactory htmlLoaderFactory,
            IWebsitesInfoLoader websitesInfoLoader,
            IBacklinksLoader backlinksLoader, 
            IRefdomainsLoader refdomainsLoader,
            IPathProvider pathProvider)
        {
            _htmlLoaderFactory = htmlLoaderFactory;
            _websitesInfoLoader = websitesInfoLoader;
            _backlinksLoader = backlinksLoader;
            _refdomainsLoader = refdomainsLoader;
            _pathProvider = pathProvider;
        }

        public void Start()
        {
            var universities = _websitesInfoLoader.Load();
            var reffDataFolders = new HashSet<string>(_pathProvider.ChildDirectories);

            foreach(var universityInfo in universities)
            {
                if (!reffDataFolders.Contains(universityInfo.Uri.Host))
                {
                    continue;
                }
                
                using (var htmlLoader = _htmlLoaderFactory.Create())
                {
                    var backlinks = _backlinksLoader.Load(universityInfo);
                    var refdomains = _refdomainsLoader.Load(universityInfo);
                }
            }
        }
    }
}
