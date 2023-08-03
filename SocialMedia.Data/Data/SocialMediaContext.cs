using Microsoft.EntityFrameworkCore;
using SocialMedia.Data.Models.Associations;
using SocialMedia.Data.Models;

namespace SocialMedia.Data.Data
{
    public class SocialMediaContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Post> Posts { get; set; } = null!;

        public DbSet<Follow> Follows { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
        }
    }
}
