using System;

namespace MajesticAnalyzer.Html
{
    public interface IHtmlWrapperFactory
    {
        IHtmlWrapper Create(Uri url, string html);
    }
}
