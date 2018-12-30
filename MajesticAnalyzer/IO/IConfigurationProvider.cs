namespace MajesticAnalyzer.IO
{
    public interface IConfigurationProvider
    {
        string GetHomeDirectory();

        int GetReffDomainsCountForDownload();
    }
}