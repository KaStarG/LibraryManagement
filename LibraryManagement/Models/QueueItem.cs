using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    [Table("Queue")]
    public class QueueItem
    {
        public int Id { get; set; }

        public int ReaderId { get; set; }

        public int BookId { get; set; }

        public DateTime QueueDate { get; set; }
    }
}