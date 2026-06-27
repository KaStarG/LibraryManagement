using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Models
{
    public class BookCopy
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string InventoryNumber { get; set; }

        public string Status { get; set; }
    }
}