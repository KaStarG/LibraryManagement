using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Models
{
    public class Readers
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
