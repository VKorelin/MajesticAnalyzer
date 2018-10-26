using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlLoaderFactory : IHtmlLoaderFactory
    {
        private readonly Func<IHtmlLoader> _factory;

        public HtmlLoaderFactory(Func<IHtmlLoader> factory)
        {
            _factory = factory;
        }

        public IHtmlLoader Create() => _factory();
    }
}