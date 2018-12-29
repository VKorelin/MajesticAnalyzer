using System.IO;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.IO.Csv;

namespace MajesticAnalyzer.Majestic
{
    public class BacklinksLoader : DataLoader<Backlink, BacklinksMap>, IBacklinksLoader
    {
        private readonly IPathProvider _pathProvider;
        
        public BacklinksLoader(ICsvParser<Backlink, BacklinksMap> csvParser, IPathProvider pathProvider) 
            : base(csvParser)
        {
            _pathProvider = pathProvider;
        }

        protected override string GetPath(string universityHost) 
            => Path.Combine(_pathProvider.HomeDirectory, universityHost, "backlinks.csv");
    }
}