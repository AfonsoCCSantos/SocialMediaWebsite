﻿using Microsoft.AspNetCore.Http;
using SocialMedia.Abstractions.Models;
using SocialMedia.Abstractions.Requests;
using SocialMedia.Core.Authorization;
using SocialMedia.Data;
using SocialMedia.Data.Data;
using System.IdentityModel.Tokens.Jwt;

namespace SocialMedia.Core
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

        public async Task<int> MakePost(PostRequest request, HttpContext httpContext)
        {

            var username = _jwtTokenFunctions.GetUsernameFromToken(httpContext.Request);

            if (username == null)
            {
                return 401;
            }

            User? user = await _context.GetUserByUsername(username);
            if (user == null)
            {
                return 404;
            }

            Post newPost = new(request.Text, user);
            await _context.Posts.AddAsync(newPost);
            _context.SaveChanges();
            return 200;
        }

        public async Task<int> EditPost(int postId, PostRequest request, HttpContext httpContext)
        {
            var username = _jwtTokenFunctions.GetUsernameFromToken(httpContext.Request);
            var post = await _context.GetPostById(postId);

            if (post == null)
            {
                return 404;
            }
            await _context.Entry(post).Reference(p => p.User).LoadAsync();
            if (!post.User.UserName.Equals(username))
            {
                return 401;
            }

            post.Text = request.Text;
            _context.Update(post);
            _context.SaveChanges();
            return 200;
        }
    }
}
