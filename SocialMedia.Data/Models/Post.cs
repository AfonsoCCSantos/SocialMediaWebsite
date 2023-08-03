namespace SocialMedia.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public User User { get; set; } = null!;

    }
}
