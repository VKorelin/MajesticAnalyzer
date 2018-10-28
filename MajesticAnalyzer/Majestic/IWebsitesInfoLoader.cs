using MajesticAnalyzer.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajesticAnalyzer.Majestic
{
    public interface IWebsitesInfoLoader
    {
        List<UniversityInfo> Load();
    }
}
