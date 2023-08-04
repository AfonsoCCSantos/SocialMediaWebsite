namespace SocialMedia.Abstractions.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public User User { get; set; } = null!;


        public Post() 
        {
            //Needed for EFCore
        }


        public Post(string text, User user)
        {
            Text = text;
            User = user;
        }

    }
}
