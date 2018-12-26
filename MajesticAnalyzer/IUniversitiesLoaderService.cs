using System.Collections.Generic;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer
{
    interface IUniversitiesLoaderService
    {
        List<University> LoadUniversities();
    }
}
