using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajesticAnalyzer.Html
{
    public interface IHtmlLoader : IDisposable
    {
        IHtmlWrapper Load(string url);
    }
}
