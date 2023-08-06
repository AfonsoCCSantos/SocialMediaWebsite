using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Core.Services;
using System.Net;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> MakePostAsync(PostRequest request)
        {
            var result = await _postService.MakePost(request, HttpContext);
            return result switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                _ => StatusCode(500)
            };
        }

        [HttpPatch("{postId}"), Authorize]
        public async Task<ActionResult> EditPost(int postId, PostRequest request)
        {
            var result = await (_postService.EditPost(postId, request, HttpContext));
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
