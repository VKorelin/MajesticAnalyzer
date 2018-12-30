using System.Configuration;

namespace MajesticAnalyzer.IO
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetHomeDirectory() => ConfigurationManager.AppSettings["HomeDirectory"];
        
        public int GetReffDomainsCountForDownload()
        {
            var reffDomainsCountForDownload = ConfigurationManager.AppSettings["ReffDomainsCountForDownload"];
            return string.IsNullOrEmpty(reffDomainsCountForDownload) ? int.MaxValue : int.Parse(reffDomainsCountForDownload);
        }
    }
}