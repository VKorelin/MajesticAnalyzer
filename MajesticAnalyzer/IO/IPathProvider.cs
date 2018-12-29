using System.Collections.Generic;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.IO
{
    public interface IPathProvider
    {
        string HomeDirectory { get; }
        
        IEnumerable<string> ChildDirectories { get; }

        string GetContentOutputPath(DomainInfo domainInfo);
    }
}