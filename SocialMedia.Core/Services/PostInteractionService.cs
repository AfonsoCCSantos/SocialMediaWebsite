using Microsoft.AspNetCore.Http;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Models.Associations;
using SocialMedia.Data;
using SocialMedia.Data.Data;
using System.Net;

namespace SocialMedia.Core.Services
{
    public class PostInteractionService
    {

        private readonly SocialMediaContext _context;
        private readonly JwtTokenFunctions _jwtTokenFunctions;

        public PostInteractionService(SocialMediaContext context, JwtTokenFunctions jwtTokenFunctions)
        {
            _context = context;
            _jwtTokenFunctions = jwtTokenFunctions;
        }

        public async Task<HttpStatusCode> LikePost(int postId, HttpContext httpContext)
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

            bool userLikedPost = await _context.CheckIfUserLikedPost(postId, user.Id);
            if (userLikedPost)
            {
                return HttpStatusCode.BadRequest;
            }

            Likes newLike = new(user.Id, postId);
            _context.Add(newLike);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
    }
}
