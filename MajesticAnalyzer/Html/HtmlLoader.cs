using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlLoader : IHtmlLoader, IDisposable
    {
        private readonly IHtmlWrapperFactory _htmlFactory;
        private readonly IWebClientWrapper _webClient;

        public HtmlLoader(IHtmlWrapperFactory htmlFactory, IWebClientWrapper webClient)
        {
            _htmlFactory = htmlFactory;
            _webClient = webClient;
        }

        public IHtmlWrapper Load(string url)
        {
            var html = _webClient.Load(url);
            return _htmlFactory.Create(new Uri(url), html);
        }

        public void Dispose() => _webClient.Dispose();
    }
}
