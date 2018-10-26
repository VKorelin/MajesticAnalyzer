using System.Collections.Generic;

namespace MajesticAnalyzer.Domain
{
    public class University
    {
        public UniversityInfo Info { get; }
        public List<ReffResource> Backlinks { get; }
        public List<ReffResource> Domains { get; }
    }
}
