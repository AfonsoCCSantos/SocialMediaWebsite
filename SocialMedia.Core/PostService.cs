using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Data;
using SocialMedia.Data.Data;

namespace SocialMedia.Core
{
    public class PostService
    {
        private readonly SocialMediaContext _context;

        public PostService(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<int> MakePost(PostRequest request)
        {
            User? user = await _context.GetUserById(request.UserId);
            if (user == null)
            {
                return 404;
            }

            Post newPost = new(request.Text, user);
            _context.Posts.Add(newPost);
            _context.SaveChanges();

            return 200;
        }
    }
}
