using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string? BookName { get; set; }
        public int? UniversityId { get; set; }

        public virtual University? University { get; set; }
    }
}
