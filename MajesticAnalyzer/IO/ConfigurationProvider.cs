using System.Configuration;

namespace MajesticAnalyzer.IO
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetHomeDirectory() => ConfigurationManager.AppSettings["HomeDirectory"];
    }
}