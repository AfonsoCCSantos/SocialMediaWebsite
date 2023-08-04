namespace SocialMedia.Abstractions.Requests
{
    public class PostRequest
    {
        public int UserId { get; set; }

        public required string Text { get; set; }
    }
}
