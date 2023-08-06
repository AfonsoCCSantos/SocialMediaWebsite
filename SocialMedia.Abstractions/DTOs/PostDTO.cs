namespace SocialMedia.Abstractions.DTOs
{
    public class PostDTO
    {
        public string Text { get; set; }

        public int LikeCount { get; set; }

        public int ShareCount { get; set; }

        public int UserId { get; set; }

        public PostDTO(string text, int likes, int shares, int userId) 
        {
            Text = text;
            LikeCount = likes;
            ShareCount = shares;
            UserId = userId;
        }
    }
}
