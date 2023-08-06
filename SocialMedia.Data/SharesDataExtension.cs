using Microsoft.EntityFrameworkCore;
using SocialMedia.Abstractions.Models.Associations;
using SocialMedia.Data.Data;

namespace SocialMedia.Data
{
    public static class SharesDataExtension
    {
        public static async Task<bool> CheckIfUserSharedPost(this SocialMediaContext context, int postId, int userId)
        {
            var entry = await context.Shares.FirstOrDefaultAsync(s => s.User.Id == userId && s.PostId == postId);
            return entry != null;
        }

        public static async Task<Shares> GetShareByPostAndUser(this SocialMediaContext context, int postId, int userId)
        {
            var entry = await context.Shares.FirstOrDefaultAsync(s => s.UserId == userId && s.PostId == postId);
            return entry;
        }

    }
}
