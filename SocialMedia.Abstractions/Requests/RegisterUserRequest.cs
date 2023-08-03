namespace SocialMedia.Abstractions.Requests
{
    public class RegisterUserRequest
    {
        public required string Name { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required string Email { get; set; }
    }
}
