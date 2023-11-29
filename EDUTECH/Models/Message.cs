using System;
using System.Collections.Generic;

namespace EDUTECH.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? WriteMessage { get; set; }
    }
}
