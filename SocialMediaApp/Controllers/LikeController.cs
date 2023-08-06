using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using System.Net;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {

        private readonly LikeService _postInteractionService;

        public LikeController(LikeService postInteractionService)
        {
            _postInteractionService = postInteractionService;
        }

        [HttpPost("{postId}"), Authorize]
        public async Task<ActionResult> LikePost(int postId)
        {
            var result = await _postInteractionService.LikePost(postId, HttpContext);
            return result switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                _ => StatusCode(500)
            };
        }

        [HttpDelete("{postId}"), Authorize]
        public async Task<ActionResult> UnlikePost(int postId)
        {
            var result = await _postInteractionService.UnlikePost(postId, HttpContext);
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
