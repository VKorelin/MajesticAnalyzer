using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Html;
using MajesticAnalyzer.Majestic;

namespace MajesticAnalyzer
{
    public class Analyzer : IAnalyzer
    {
        private readonly IHtmlLoaderFactory htmlLoaderFactory;
        private readonly IOutputService outputService;
        private readonly IUniversityInfoProvider universityInfoProvider;
        private readonly IUniversityLoader universityLoader;

        public Analyzer(
            IHtmlLoaderFactory htmlLoaderFactory,
            IOutputService outputService,
            IUniversityInfoProvider universityInfoProvider,
            IUniversityLoader universityLoader)
        {
            this.htmlLoaderFactory = htmlLoaderFactory;
            this.outputService = outputService;
            this.universityInfoProvider = universityInfoProvider;
            this.universityLoader = universityLoader;
        }

        public void Start()
        {
            var universities = universityInfoProvider.GetUniversities();

            foreach(var universityInfo in universities)
            {
                using (IHtmlLoader htmlLoader = htmlLoaderFactory.Create())
                {
                    University university = universityLoader.LoadUniversity(universityInfo);

                    foreach (var link in university.Domains)
                    {
                        IHtmlWrapper htmlWrapper = htmlLoader.Load(link.Uri.AbsoluteUri);

                    }
                }
            }
        }
    }
}
