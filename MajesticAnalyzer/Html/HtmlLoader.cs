using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlLoader : IHtmlLoader, IDisposable
    {
        private readonly IHtmlWrapperFactory htmlFactory;
        private readonly IWebClientWrapper webClient;

        public HtmlLoader(IHtmlWrapperFactory htmlFactory, IWebClientWrapper webClient)
        {
            this.htmlFactory = htmlFactory;
            this.webClient = webClient;
        }

        public IHtmlWrapper Load(string url)
        {
            string html = webClient.Load(url);
            return htmlFactory.Create(new Uri(url), html);
        }

        public void Dispose()
        {
            webClient.Dispose();
        }
    }
}
