using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Output { get; set; }
        public string? Location { get; set; }
        public string? OrganizerEmail { get; set; }
        public string? OrganizerPhone { get; set; }
        public string? Topic { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Cost { get; set; }
        public int? UniversityId { get; set; }
        public string? OrganizerDescription { get; set; }
        public string? Photo { get; set; }

        public virtual University? University { get; set; }
    }
}
