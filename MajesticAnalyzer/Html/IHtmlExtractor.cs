using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajesticAnalyzer.Html
{
    public interface IHtmlExtractor
    {
        string ExtractDescription(string html);
        string ExtractTitle(string html);
    }
}
