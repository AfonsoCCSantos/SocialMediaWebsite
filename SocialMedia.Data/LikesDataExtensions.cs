using Microsoft.EntityFrameworkCore;
using SocialMedia.Abstractions.Models.Associations;
using SocialMedia.Data.Data;

namespace SocialMedia.Data
{
    public static class LikesDataExtensions
    {

        public static async Task<bool> CheckIfUserLikedPost(this SocialMediaContext context, int postId, int userId)
        {
            var entry = await context.Likes.FirstOrDefaultAsync(l => l.User.Id == userId && l.PostId == postId);
            return entry != null;
        }

        public static async Task<Likes> GetLikeByPostAndUser(this SocialMediaContext context, int postId, int userId)
        {
            var entry = await context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
            return entry;
        }
    }
}
