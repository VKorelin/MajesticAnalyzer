using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer
{
    public interface IUniversityLoaderService
    {
        University Load(UniversityInfo universityInfo);
    }
}