using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public int ReaderId { get; set; }

        public int BookCopyId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime? ActualReturnDate { get; set; }
    }
}