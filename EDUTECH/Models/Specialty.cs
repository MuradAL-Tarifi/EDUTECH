using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class Specialty
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SectionId { get; set; }
        public int? ScientificDegreeId { get; set; }

        public virtual ScientificDegree? ScientificDegree { get; set; }
        public virtual Section? Section { get; set; }
    }
}
