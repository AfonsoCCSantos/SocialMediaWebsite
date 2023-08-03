using SocialMedia.Data.Models.Associations;

namespace SocialMedia.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public ICollection<Post> Posts { get; set; } = null!;

        public ICollection<Follow> Followers { get; set; } = null!;

        public ICollection<Follow> Following { get; set; } = null!;

    }
}
