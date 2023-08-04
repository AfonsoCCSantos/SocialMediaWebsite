using SocialMedia.Abstractions;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Data;
using SocialMedia.Data.Data;

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
            if (await _context.IsEmailAlreadyRegistered(request.Email))
            {
                return RegistrationMessage.EmailAlreadyRegistered;
            }
            if (await _context.IsUsernameTaken(request.Username))
            {
                return RegistrationMessage.UsernameAlreadyTaken;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            User newUser = new(request.Name, request.Email, request.Username, passwordHash);
            await _context.AddAsync(newUser);
            _context.SaveChanges();

            return RegistrationMessage.Success;
        }
    }
}
