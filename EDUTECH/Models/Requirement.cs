using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class Requirement
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int? UniversityId { get; set; }

        public virtual University? University { get; set; }
    }
}
