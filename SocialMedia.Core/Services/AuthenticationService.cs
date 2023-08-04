using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Abstractions;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Data.Data;
using System.Net;

namespace SocialMedia.Core.Services
{
    public class AuthenticationService
    {

        private readonly SocialMediaContext _context;

        public AuthenticationService(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<RegistrationMessage> RegisterAsync(RegisterUserRequest request)
        {
            if (await IsEmailAlreadyRegistered(request.Email))
            {
                return RegistrationMessage.EmailAlreadyRegistered;
            }
            if (await IsUsernameTaken(request.Username))
            {
                return RegistrationMessage.UsernameAlreadyTaken;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            User newUser = new(request.Name, request.Email, request.Username, passwordHash);
            await _context.AddAsync(newUser);
            _context.SaveChanges();

            return RegistrationMessage.Success;
        }

        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameTaken(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
