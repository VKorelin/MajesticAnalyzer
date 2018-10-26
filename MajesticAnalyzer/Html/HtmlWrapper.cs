using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlWrapper : IHtmlWrapper
    {
        private readonly IHtmlExtractor _htmlExtractor;

        public HtmlWrapper(Uri url, string html, IHtmlExtractor htmlExtractor)
        {
            _htmlExtractor = htmlExtractor;
            Url = url;
            Html = html;
        }

        public Uri Url { get; }

        public string Html { get; }

        public string GetDescription() => _htmlExtractor.ExtractDescription(Html);

        public string GetTitle() => _htmlExtractor.ExtractTitle(Html);
    }
}
