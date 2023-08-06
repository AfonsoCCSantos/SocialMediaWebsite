using Azure.Core;
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
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500)
            };
        }

        [HttpDelete("like/{postId}"), Authorize]
        public async Task<ActionResult> UnlikePost(int postId)
        {
            var result = await _postInteractionService.UnlikePost(postId, HttpContext);
            return result switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500)
            };
        }
    }
}
