using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlLoaderFactory : IHtmlLoaderFactory
    {
        private readonly Func<IHtmlLoader> factory;

        public HtmlLoaderFactory(Func<IHtmlLoader> factory)
        {
            this.factory = factory;
        }

        public IHtmlLoader Create()
        {
            return factory();
        }
    }
}