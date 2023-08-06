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

        public async Task<HttpStatusCode> SharePost(int postId, HttpContext httpContext)
        {
            User? user = await _jwtTokenFunctions.GetUserFromTokenAsync(httpContext.Request);
            if (user == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            bool userSharedPost = await _context.CheckIfUserSharedPost(postId, user.Id);
            if (userSharedPost)
            {
                return HttpStatusCode.OK;
            }

            Shares newShare = new(user.Id, postId);
            _context.Shares.Add(newShare);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> UnsharePost(int postId, HttpContext httpContext)
        {
            User? user = await _jwtTokenFunctions.GetUserFromTokenAsync(httpContext.Request);

            if (user == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            var share = await _context.GetShareByPostAndUser(postId, user.Id);
            if (share == null)
            {
                return HttpStatusCode.BadRequest;
            }
            _context.Shares.Remove(share);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }
    }
}
