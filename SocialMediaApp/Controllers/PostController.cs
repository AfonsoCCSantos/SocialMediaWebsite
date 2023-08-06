using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Abstractions.DTOs;
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
            var result = await _postService.EditPost(postId, request, HttpContext);
            return result switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500)
            };
        }

        [HttpDelete("{postId}"), Authorize]
        public async Task<ActionResult> DeletePost(int postId)
        {
            var result = await _postService.DeletePost(postId, HttpContext);
            return result switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Unauthorized => Unauthorized(),
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500)
            };
        }

        [HttpGet("{postId]}"), Authorize]
        public async Task<PostDTO> GetPost(int postId)
        {
            var postDTO = await _postService.GetPost(postId, HttpContext);
            return postDTO;
        }
    }
}
