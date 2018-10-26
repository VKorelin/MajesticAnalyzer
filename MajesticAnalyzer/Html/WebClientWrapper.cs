using System.Net;

namespace MajesticAnalyzer.Html
{
    public class WebClientWrapper : IWebClientWrapper
    {
        private readonly WebClient _webClient;

        public WebClientWrapper()
        {
            _webClient = new WebClient();
        }

        public string Load(string url) => _webClient.DownloadString(url);

        public void Dispose() => _webClient.Dispose();

    }
}
