using EDUTECH.Models;

namespace EDUTECH.Dto
{
    public class AdminViewModel
    {
        public User User { get; set; }
        public List<University> Universities { get; set; }
        public List<User> Users { get; set; }
        public List<Event> Events { get; set; }
        public List<Message> Messages { get; set; }
        public List<College> Colleges { get; set; }
        public List<Section> Sections { get; set; }
        public List<Specialty> Specialties { get; set; }
        public List<Book> Books { get; set; }
        public List<EventsDto> EventsDto { get; set; }

    }
}
