using EDUTECH.Models;

namespace EDUTECH.Dto
{
    public class AgentViewModel
    {
        public User User { get; set; }
        public University University { get; set; }
        public List<Event> Events { get; set; }
        public List<College> Colleges { get; set; }
        public List<Section> Sections { get; set; }
        public List<Specialty> Specialties { get; set; }
        public List<Book> Books { get; set; }
        public List<EventsDto> EventsDto { get; set; }
        public List<ScientificDegree> scientificDegrees { get; set; }

        public Event AddEditEvent { get; set; }
        public Book AddEditBook { get; set; }
        public College AddEditCollege { get; set; }
        public Section AddEditSection { get; set; }
        public Specialty AddEditSpecialty { get; set; }

    }
}
