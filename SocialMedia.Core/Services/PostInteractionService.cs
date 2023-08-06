﻿using Microsoft.AspNetCore.Http;
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

        public PostInteractionService(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<HttpStatusCode> LikePost(int postId, HttpContext httpContext)
        {
            var username = JwtTokenFunctions.GetUsernameFromToken(httpContext.Request);

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

        public async Task<HttpStatusCode> UnlikePost(int postId, HttpContext httpContext)
        {
            var username = JwtTokenFunctions.GetUsernameFromToken(httpContext.Request);
            if (username == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            User? user = await _context.GetUserByUsername(username);

            if (user == null)
            {
                return HttpStatusCode.NotFound;
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
