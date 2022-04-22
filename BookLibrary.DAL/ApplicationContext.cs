using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Username).IsUnique(); });

            modelBuilder.Entity<Comment>()
            .HasOne(p => p.User)
            .WithMany(b => b.Comments)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Comment>()
            //    .HasOne(p => p.Book)
            //    .WithMany(b => b.Comments)
            //    .HasForeignKey(s => s.BookId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
