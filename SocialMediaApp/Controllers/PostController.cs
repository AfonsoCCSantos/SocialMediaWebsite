using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Core;

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
            int result = await _postService.MakePost(request, HttpContext);
            return result switch
            {
                404 => BadRequest(),
                401 => Unauthorized(),
                _ => Ok(),
            };
        }

        [HttpPatch("{postId}"), Authorize]
        public async Task<ActionResult> EditPost(int postId, PostRequest request)
        {
            int result = await (_postService.EditPost(postId, request, HttpContext));
            return result switch
            {
                404 => BadRequest(),
                401 => Unauthorized(),
                _ => Ok(),
            };
        }


    }
}
