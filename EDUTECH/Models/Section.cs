using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class Section
    {
        public Section()
        {
            Specialties = new HashSet<Specialty>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CollegeId { get; set; }

        public virtual College? College { get; set; }
        public virtual ICollection<Specialty> Specialties { get; set; }
    }
}
