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

        public DbSet<Likes> Likes { get; set; } = null!;

        public DbSet<Shares> Shares { get; set; } = null!;


        public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureFollowTable(modelBuilder);
            ConfigureLikesTable(modelBuilder);
            ConfigureSharesTable(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName, u.Email })
                .IsUnique();
        }

        private static void ConfigureSharesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shares>()
                .HasOne(s => s.User)
                .WithMany(u => u.SharedPosts)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Shares>()
                .HasOne(s => s.Post)
                .WithMany(p => p.Shares)
                .HasForeignKey(s => s.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void ConfigureLikesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Likes>()
                .HasOne(l => l.User)
                .WithMany(u => u.LikedPosts)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Likes>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void ConfigureFollowTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Following)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RFQKAUE\SQLEXPRESS;Initial Catalog=SocialMedia;Integrated Security=True;TrustServerCertificate=true;");
        }
    }
}
