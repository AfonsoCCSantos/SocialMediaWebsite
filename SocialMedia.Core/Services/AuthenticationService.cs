using SocialMedia.Abstractions.Requests;
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

        public int Register(RegisterUserRequest request)
        {





            return 0;
        }

    }
}
