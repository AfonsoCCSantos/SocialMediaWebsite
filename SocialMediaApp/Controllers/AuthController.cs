using Microsoft.AspNetCore.Mvc;
using SocialMedia.Abstractions;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Core.Authorization;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthenticationService _authenticationService;
        private readonly JwtTokenCreator _jwtTokenCreator;

        public AuthController(AuthenticationService authenticationService,
                              JwtTokenCreator jwtTokenCreator) 
        {
            _authenticationService = authenticationService;
            _jwtTokenCreator = jwtTokenCreator;
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterUserRequest request)
        {
            AuthenticationMessage message = await _authenticationService.RegisterAsync(request);
            return message switch
            {
                AuthenticationMessage.Success => Ok(),
                AuthenticationMessage.UsernameAlreadyTaken => Conflict("Username already taken"),
                AuthenticationMessage.EmailAlreadyRegistered => Conflict("Email already registered"),
                _ => new StatusCodeResult(500),
            };
        }

        [HttpPost("login")] 
        public async Task<ActionResult> LoginAsync(LoginUserRequest request)
        {
            User? user = await _authenticationService.LoginAsync(request);
            if (user != null)
            {
                string token = _jwtTokenCreator.CreateJwtToken(user);
                return Ok(token);
            }
            return BadRequest();
        }
    }
}
