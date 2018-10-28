using System.Collections.Generic;
using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.Majestic
{
    public interface IUniversityInfoDataLoader<T>
    {
        List<T> Load(UniversityInfo universityInfo);
    }
}