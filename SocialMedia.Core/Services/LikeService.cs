using Microsoft.AspNetCore.Http;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Models.Associations;
using SocialMedia.Data;
using SocialMedia.Data.Data;
using System.Net;

namespace SocialMedia.Core.Services
{
    public class LikeService
    {

        private readonly SocialMediaContext _context;
        private readonly JwtTokenFunctions _jwtTokenFunctions;

        public LikeService(SocialMediaContext context, JwtTokenFunctions jwtTokenFunctions)
        {
            _context = context;
            _jwtTokenFunctions = jwtTokenFunctions;
        }

        public async Task<HttpStatusCode> LikePost(int postId, HttpContext httpContext)
        {
            User? user = await _jwtTokenFunctions.GetUserFromTokenAsync(httpContext.Request);
            if (user == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            bool userLikedPost = await _context.CheckIfUserLikedPost(postId, user.Id);
            if (userLikedPost)
            {
                return HttpStatusCode.OK;
            }

            Likes newLike = new(user.Id, postId);
            _context.Add(newLike);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> UnlikePost(int postId, HttpContext httpContext)
        {
            User? user = await _jwtTokenFunctions.GetUserFromTokenAsync(httpContext.Request);

            if (user == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            var post = await _context.GetLikeByPostAndUser(postId, user.Id);
            if (post == null)
            {
                return HttpStatusCode.BadRequest;
            }
            _context.Remove(post);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
    }
}
