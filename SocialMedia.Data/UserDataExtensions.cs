using Microsoft.EntityFrameworkCore;
using SocialMedia.Data.Data;

namespace SocialMedia.Data
{
    public static class UserDataExtensions
    {
        public static async Task<bool> IsEmailAlreadyRegistered(this SocialMediaContext context, 
                                                                string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public static async Task<bool> IsUsernameTaken(this SocialMediaContext context,
                                                       string username)
        {
            return await context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
