using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostsApiDatabase
{
    public class PostsDbContext : DbContext
    {
        //const string ConnectionString = "User ID=postgres;Password=postsapitestpassword;Host=34.131.51.218;Database=allposts";
        const string ConnectionString = "User ID=postgres;Password=postgres;Host=127.0.0.1;Database=allposts";
        public PostsDbContext() { }

        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasKey(post => post.Id);

            modelBuilder.Entity<Post>()
                .Property(post => post.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>()
                .Property(post => post.Title)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<Post>()
                .Property(post => post.Content)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Post>()
                .Property(post => post.Added)
                .HasDefaultValue(DateTime.Now.ToUniversalTime());
        }
    }
}
