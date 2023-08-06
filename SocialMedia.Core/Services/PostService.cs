using Microsoft.AspNetCore.Http;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Data;
using SocialMedia.Data.Data;
using System.Net;

namespace SocialMedia.Core.Services
{
    public class PostService
    {
        private readonly SocialMediaContext _context;
        private readonly JwtTokenFunctions _jwtTokenFunctions;

        public PostService(SocialMediaContext context, JwtTokenFunctions jwtTokenFunctions)
        {
            _context = context;
            _jwtTokenFunctions = jwtTokenFunctions;
        }

        public async Task<HttpStatusCode> MakePost(PostRequest request, HttpContext httpContext)
        {

            var username = _jwtTokenFunctions.GetUsernameFromToken(httpContext.Request);

            if (username == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            User? user = await _context.GetUserByUsername(username);
            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }

            Post newPost = new(request.Text, user);
            await _context.Posts.AddAsync(newPost);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> EditPost(int postId, PostRequest request, HttpContext httpContext)
        {
            var username = _jwtTokenFunctions.GetUsernameFromToken(httpContext.Request);
            var post = await _context.GetPostById(postId);

            if (post == null)
            {
                return HttpStatusCode.NotFound;
            }
            await _context.Entry(post).Reference(p => p.User).LoadAsync();
            if (!post.User.UserName.Equals(username))
            {
                return HttpStatusCode.Unauthorized;
            }

            post.Text = request.Text;
            _context.Update(post);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
    }
}
