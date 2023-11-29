using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class College
    {
        public College()
        {
            Sections = new HashSet<Section>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? UniversityId { get; set; }

        public virtual University? University { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
