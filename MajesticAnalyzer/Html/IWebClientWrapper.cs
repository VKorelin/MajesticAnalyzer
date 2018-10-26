using System;

namespace MajesticAnalyzer.Html
{
    public interface IWebClientWrapper : IDisposable
    {
        string Load(string url);
    }
}
