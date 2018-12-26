using System.Collections.Generic;

namespace MajesticAnalyzer.Domain
{
    public class ReffResource
    {
        public DomainInfo Domain { get; set; }

        public List<string> Backlinks { get; }

        public UniversityInfo University { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public ReffResource()
        {
            Backlinks = new List<string>();
        }
    }
}
