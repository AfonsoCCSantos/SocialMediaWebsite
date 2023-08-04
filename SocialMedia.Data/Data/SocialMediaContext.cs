using Microsoft.EntityFrameworkCore;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Models.Associations;

namespace SocialMedia.Data.Data
{
    public class SocialMediaContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Post> Posts { get; set; } = null!;

        public DbSet<Follow> Follows { get; set; } = null!;

        public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options) { }

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

            modelBuilder.Entity<User>()
                .HasIndex(u => new {u.UserName, u.Email})
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RFQKAUE\SQLEXPRESS;Initial Catalog=SocialMedia;Integrated Security=True;TrustServerCertificate=true;");
        }
    }
}
