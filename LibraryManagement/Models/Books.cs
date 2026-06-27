using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Models
{
    public class Books
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PublishYear { get; set; }

        public int Pages { get; set; }

        public bool IsReadingRoomOnly { get; set; }

        public int AuthorId { get; set; }

        public int PublisherId { get; set; }

        public string? Author { get; set; }

        public int? Year { get; set; }
    }
}
