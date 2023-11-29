using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class User
    {
        public User()
        {
            Universities = new HashSet<University>();
        }

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsSuperAgent { get; set; }

        public virtual ICollection<University> Universities { get; set; }
    }
}
