using Microsoft.AspNetCore.Mvc;
using SocialMedia.Abstractions.Requests;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        [HttpPost]
        public ActionResult MakePost(PostRequest request)
        {
            return null;
        }
    }
}
