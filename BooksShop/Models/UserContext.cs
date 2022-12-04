using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksShop.Models
{
    public class UserContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Book>().Property(e => e.Category).HasConversion(v => v.ToString(),
            //    v => (Category)Enum.Parse(typeof(Category), v));
           // modelBuilder.Entity<Book>().Property(e => e.Category).HasConversion<string>();
        }
    }
}