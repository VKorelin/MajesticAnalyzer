using System.Collections.Generic;

namespace MajesticAnalyzer.Domain
{
    public class University
    {
        UniversityInfo Info { get; }
        List<ReffResource> Backlinks { get; }
        List<ReffResource> Domains { get; }
    }
}
