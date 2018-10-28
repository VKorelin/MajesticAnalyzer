using System.IO;
using System.Linq;

namespace MajesticAnalyzer
{
    public class PathProvider : IPathProvider
    {
        public string HomeDirectory => @"D:\Webometrics\";
        
        public string[] ChildDirectories => Directory.GetDirectories(HomeDirectory).Select(Path.GetFileName).ToArray();
    }
}