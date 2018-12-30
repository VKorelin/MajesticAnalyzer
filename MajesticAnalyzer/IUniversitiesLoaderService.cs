using System.Collections.Generic;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer
{
    public interface IUniversitiesLoaderService
    {
        University LoadUniversity(string universityHost);
        
        List<University> LoadUniversities();
    }
}
