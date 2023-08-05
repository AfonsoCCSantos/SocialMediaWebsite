using SocialMedia.Abstractions.Models.Associations;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Abstractions.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string UserName { get; set; } = null!;

        public string Name { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime AccountCreatedAt { get; set; }

        public ICollection<Post> Posts { get; set; } = null!;

        public ICollection<Follow> Followers { get; set; } = null!;

        public ICollection<Follow> Following { get; set; } = null!;

        public User()
        {
            //Required for EFCore
        }

        public User(string name, string email, string username, string password)
        {
            Name = name;
            Email = email;
            UserName = username;
            PasswordHash = password;
        }

    }
}
