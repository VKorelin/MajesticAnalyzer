using System;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO.Csv;

namespace MajesticAnalyzer.IO
{
    public class ReffContentOutputService : IReffContentOutputService
    {
        private readonly IPathProvider _pathProvider;
        private readonly ICsvWriter<ReffResource, ReffResourceMap> _csvWriter;

        public ReffContentOutputService(IPathProvider pathProvider, ICsvWriter<ReffResource, ReffResourceMap> csvWriter)
        {
            _pathProvider = pathProvider;
            _csvWriter = csvWriter;
        }
        
        public void WriteContent(ReffResource reffResource)
        {
            if (reffResource == null) throw new ArgumentNullException(nameof(reffResource));
            if(reffResource.UniversityInfo == null) throw new ArgumentException("UniversityInfo of ReffResource should not be null", nameof(reffResource.UniversityInfo));
            
            var path = _pathProvider.GetContentOutputPath(reffResource.UniversityInfo);
            _csvWriter.Write(path, reffResource, true);
        }

        public void WriteContent(University university)
        {
            if (university == null) throw new ArgumentNullException(nameof(university));
            if (university.Info == null) throw new ArgumentException("UniversityInfo of University should not be null", nameof(university.Info));
            
            var path = _pathProvider.GetContentOutputPath(university.Info);
            _csvWriter.Write(path, university.ReffResources, true);
        }
    }
}