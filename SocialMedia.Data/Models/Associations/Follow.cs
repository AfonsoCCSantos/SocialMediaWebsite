namespace SocialMedia.Data.Models.Associations
{
    public class Follow
    {
        public int Id { get; set; }

        public User Follower { get; set; } = null!;

        public User Following { get; set; } = null!;

        public DateTime DateFollowed { get; set; }

    }
}
