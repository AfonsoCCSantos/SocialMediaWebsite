namespace SocialMedia.Abstractions.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int PostsCount { get; set; }
    }
}
