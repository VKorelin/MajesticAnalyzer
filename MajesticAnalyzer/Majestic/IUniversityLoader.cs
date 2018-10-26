using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.Majestic
{
    public interface IUniversityLoader
    {
        University LoadUniversity(UniversityInfo universityInfo);
    }
}
