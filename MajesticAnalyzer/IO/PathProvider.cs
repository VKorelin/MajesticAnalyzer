using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MajesticAnalyzer.IO
{
    public class PathProvider : IPathProvider
    {
        public string HomeDirectory => @"D:\Webometrics\";
        
        public IEnumerable<string> ChildDirectories => Directory.GetDirectories(HomeDirectory).Select(Path.GetFileName);
    }
}