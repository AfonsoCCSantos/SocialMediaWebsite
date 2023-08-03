using Microsoft.AspNetCore.Mvc;
using SocialMedia.Abstractions.DTOs;
using SocialMedia.Abstractions.Models;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        public ActionResult<User> Register(UserDTO request)
        {
            return null;

        }

    }
}
