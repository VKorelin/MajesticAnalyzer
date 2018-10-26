using System.Net;

namespace MajesticAnalyzer.Html
{
    public class WebClientWrapper : IWebClientWrapper
    {
        private readonly WebClient webClient;

        public WebClientWrapper() => webClient = new WebClient();

        public string Load(string url) => webClient.DownloadString(url);

        public void Dispose() => webClient.Dispose();

    }
}
