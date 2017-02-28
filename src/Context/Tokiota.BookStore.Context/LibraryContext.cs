
namespace Tokiota.BookStore.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Tokiota.BookStore.Entities;
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Serie> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .ToTable("Author");

            modelBuilder.Entity<Book>()
                .ToTable("Book")
                .HasOne(b => b.Serie)
                .WithMany(s => s.Books).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Serie>()
                .ToTable("Serie");
        }
    }
}
