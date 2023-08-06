using SocialMedia.Abstractions.Models;
using SocialMedia.Data.Data;

namespace SocialMedia.Data
{
    public static class PostDataExtensions
    {
        public static async Task<Post?> GetPostById(this SocialMediaContext context, int postId)
        {
            return await context.Posts.FindAsync(postId);
        }   
    }
}
