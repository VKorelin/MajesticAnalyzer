using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlWrapperFactory : IHtmlWrapperFactory
    {
        private readonly Func<Uri, string, IHtmlWrapper> factory;

        public HtmlWrapperFactory(Func<Uri, string, IHtmlWrapper> factory)
        {
            this.factory = factory;
        }

        public IHtmlWrapper Create(Uri uri, string html)
        {
            return factory(uri, html);
        }
    }
}
