using System;

namespace MajesticAnalyzer.Html
{
    public interface IHtmlWrapper
    {
        Uri Url { get; }

        string Html { get; }

        string GetTitle();

        string GetDescription();
    }
}