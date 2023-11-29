using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class ScientificDegree
    {
        public ScientificDegree()
        {
            Specialties = new HashSet<Specialty>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Specialty> Specialties { get; set; }
    }
}
