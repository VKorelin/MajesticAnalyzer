using System;

namespace MajesticAnalyzer.Html
{
    public interface IHtmlLoader : IDisposable
    {
        IHtmlWrapper Load(string url);
    }
}
