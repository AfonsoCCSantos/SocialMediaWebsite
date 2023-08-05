using SocialMedia.Abstractions;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Data;
using SocialMedia.Data.Data;

namespace SocialMedia.Core.Authorization
{
    public class AuthenticationService
    {

        private readonly SocialMediaContext _context;

        public AuthenticationService(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationMessage> RegisterAsync(RegisterUserRequest request)
        {
            if (await _context.IsEmailAlreadyRegistered(request.Email))
            {
                return AuthenticationMessage.EmailAlreadyRegistered;
            }
            if (await _context.IsUsernameTaken(request.Username))
            {
                return AuthenticationMessage.UsernameAlreadyTaken;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            User newUser = new(request.Name, request.Email, request.Username, passwordHash);
            await _context.Users.AddAsync(newUser);
            _context.SaveChanges();

            return AuthenticationMessage.Success;
        }

        public async Task<User?> LoginAsync(LoginUserRequest request)
        {
            var user = await _context.GetUserByEmail(request.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
