using Bookss.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookss.Models.Entities;
namespace Bookss.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<MyFavoriteBook> MyFavoriteBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BooksRating> BooksRating { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MyFavoriteBook>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User) 
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Book)
                      .WithMany()
                      .HasForeignKey(e => e.BookId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
        
    }
}
