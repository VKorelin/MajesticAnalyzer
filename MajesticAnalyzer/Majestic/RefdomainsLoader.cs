using System.IO;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer.Majestic
{
    public class RefdomainsLoader : DataLoader<DomainInfo, RefdomainsMap>, IRefdomainsLoader
    {
        private readonly IPathProvider _pathProvider;
        
        public RefdomainsLoader(ICsvParser<DomainInfo, RefdomainsMap> csvParser, IPathProvider pathProvider)
            : base(csvParser)
        {
            _pathProvider = pathProvider;
        }

        protected override string GetPath(string universityHost) 
            => Path.Combine(_pathProvider.HomeDirectory, universityHost, "refdomains.csv");
    }
}