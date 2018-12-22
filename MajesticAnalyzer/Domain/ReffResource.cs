﻿using System.Collections.Generic;

namespace MajesticAnalyzer.Domain
{
    public class ReffResource
    {
        public DomainInfo Domain { get; }

        public List<Backlink> Pages { get; }

        public UniversityInfo University { get; }

        public string Description { get; set; }

        public string Title { get; set; }
    }
}
