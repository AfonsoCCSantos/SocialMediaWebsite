namespace SocialMedia.Abstractions.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int PostsCount { get; set; }
    }
}
