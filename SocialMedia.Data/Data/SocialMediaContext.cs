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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Following)
                .WithMany(u => u.Following)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RFQKAUE\SQLEXPRESS;Initial Catalog=SocialMedia;Integrated Security=True;TrustServerCertificate=true;");
        }
    }
}
