using System.Collections.Generic;

namespace MajesticAnalyzer.IO
{
    public interface IPathProvider
    {
        string HomeDirectory { get; }
        
        IEnumerable<string> ChildDirectories { get; }
    }
}