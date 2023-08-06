namespace SocialMedia.Abstractions.Models.Associations
{
    public class Shares
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        public Shares(int userId, int postId) 
        {
            UserId = userId;
            PostId = postId;
        }
    }
}
