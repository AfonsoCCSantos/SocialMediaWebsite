using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using System.Net;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : ControllerBase
    {

        private readonly ShareService _sharesService;

        public ShareController(ShareService sharesService)
        {
            _sharesService = sharesService;
        }

        [HttpPost("{postId}"), Authorize]
        public async Task<ActionResult> SharePost(int postId)
        {
            var result = await _sharesService.SharePost(postId, HttpContext);
            return result switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500)
            };
        }

        [HttpDelete("{postId}"), Authorize]
        public async Task<ActionResult> UnsharePost(int postId)
        {
            var result = await _sharesService.UnsharePost(postId, HttpContext);
            return result switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500)
            };
        }
    }
}
