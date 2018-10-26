using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlWrapperFactory : IHtmlWrapperFactory
    {
        private readonly Func<Uri, string, IHtmlWrapper> _factory;

        public HtmlWrapperFactory(Func<Uri, string, IHtmlWrapper> factory)
        {
            _factory = factory;
        }

        public IHtmlWrapper Create(Uri uri, string html) => _factory(uri, html);
    }
}
