using Microsoft.AspNetCore.Mvc;
using SocialMedia.Abstractions;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Core.Services;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthenticationService _authenticationService;

        public AuthController(AuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterUserRequest request)
        {
            RegistrationMessage message = await _authenticationService.RegisterAsync(request);
            return message switch
            {
                RegistrationMessage.Success => Ok(),
                RegistrationMessage.UsernameAlreadyTaken => Conflict("Username already taken"),
                RegistrationMessage.EmailAlreadyRegistered => Conflict("Email already registered"),
                _ => new StatusCodeResult(500),
            };
        }

    }
}
