using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<Readers> Readers { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<QueueItem> QueueItems { get; set; }

        public DbSet<BookCopy> BookCopies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
    }