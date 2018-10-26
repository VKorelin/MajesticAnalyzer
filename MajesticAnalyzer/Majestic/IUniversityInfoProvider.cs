using MajesticAnalyzer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajesticAnalyzer.Majestic
{
    public interface IUniversityInfoProvider
    {
        List<UniversityInfo> GetUniversities();
    }
}
