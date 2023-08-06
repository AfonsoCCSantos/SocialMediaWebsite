using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using System.Net;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostInteractionController : ControllerBase
    {

        private readonly PostInteractionService _postInteractionService;

        public PostInteractionController(PostInteractionService postInteractionService)
        {
            _postInteractionService = postInteractionService;
        }

        [HttpPost("like/{postId}"), Authorize]
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

        [HttpDelete("like/{postId}"), Authorize]
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

        [HttpPost("share/{postId}"), Authorize]
        public async Task<ActionResult> SharePost(int postId)
        {
            var result = await _postInteractionService.SharePost(postId, HttpContext);
            return result switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500)
            };
        }

        [HttpDelete("share/{postId}"), Authorize]
        public async Task<ActionResult> UnsharePost(int postId)
        {
            var result = await _postInteractionService.UnsharePost(postId, HttpContext);
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
