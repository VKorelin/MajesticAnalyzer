using System;

namespace MajesticAnalyzer.Domain
{
    public class UniversityInfo
    {
        public string Name { get; set; }

        public Uri Uri { get; set; }

        public int Rank { get; set; }

        public int BacklinksCount { get; set; }

        public int ReffDomainsCount { get; set; }

        public int Impact { get; set; }

        public int Presence { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }
    }
}
