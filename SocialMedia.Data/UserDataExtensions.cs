using Microsoft.EntityFrameworkCore;
using SocialMedia.Abstractions.Models;
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

        public static async Task<User?> GetUserByEmail(this SocialMediaContext context,
                                                       string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public static async Task<User?> GetUserById(this SocialMediaContext context,
                                                       int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
