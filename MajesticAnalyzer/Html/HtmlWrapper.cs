using System;

namespace MajesticAnalyzer.Html
{
    public class HtmlWrapper : IHtmlWrapper
    {
        private readonly IHtmlExtractor htmlExtractor;

        public HtmlWrapper(Uri url, string html, IHtmlExtractor htmlExtractor)
        {
            this.htmlExtractor = htmlExtractor;
            Url = url;
            Html = html;
        }

        public Uri Url { get; }

        public string Html { get; }

        public string GetDescription() => htmlExtractor.ExtractDescription(Html);

        public string GetTitle() => htmlExtractor.ExtractTitle(Html);
    }
}
