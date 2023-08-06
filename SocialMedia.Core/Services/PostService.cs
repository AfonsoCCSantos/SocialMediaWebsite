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
        private const int MAX_TIME_TO_EDIT_POST = 15;

        public PostService(SocialMediaContext context, JwtTokenFunctions jwtTokenFunctions)
        {
            _context = context;
            _jwtTokenFunctions = jwtTokenFunctions;
        }

        public async Task<HttpStatusCode> MakePost(PostRequest request, HttpContext httpContext)
        {

            User? user = await _jwtTokenFunctions.GetUserFromTokenAsync(httpContext.Request);
            if (user == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            Post newPost = new(request.Text, user, DateTime.UtcNow);
            await _context.Posts.AddAsync(newPost);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> EditPost(int postId, PostRequest request, HttpContext httpContext)
        {
            var username = JwtTokenFunctions.GetUsernameFromToken(httpContext.Request);
            var post = await _context.GetPostById(postId);

            if (post == null)
            {
                return HttpStatusCode.NotFound;
            }

            TimeSpan timeDifference = DateTime.UtcNow - post.CreatedAt;
            if (timeDifference.TotalMinutes > MAX_TIME_TO_EDIT_POST)
            {
                return HttpStatusCode.BadRequest;
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

        public async Task<HttpStatusCode> DeletePost(int postId, HttpContext httpContext)
        {
            var user = await _jwtTokenFunctions.GetUserFromTokenAsync(httpContext.Request);
            if (user == null) return HttpStatusCode.Unauthorized;

            var post = await _context.GetPostById(postId);
            if (post == null) return HttpStatusCode.NotFound;

            await _context.Entry(post).Reference(p => p.User).LoadAsync();
            if (user.Id != post.User.Id) return HttpStatusCode.Unauthorized;

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
    }
}
