using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDUTECH.Models
{
    public partial class University
    {
        public University()
        {
            Books = new HashSet<Book>();
            Colleges = new HashSet<College>();
            Events = new HashSet<Event>();
            Requirements = new HashSet<Requirement>();
        }

        public int Id { get; set; }
        public string? Logo { get; set; }
        public string? Name { get; set; }
        public string? OutSideDescription { get; set; }
        public string? InSideDescription { get; set; }
        public int? StudetNumbers { get; set; }
        public string? Location { get; set; }
        public int? UserId { get; set; }
        public string? Link { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<College> Colleges { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
    }
}
