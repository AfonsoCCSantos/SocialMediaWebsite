using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Abstractions.Models;
using SocialMedia.Data.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SocialMedia.Data;

namespace SocialMedia.Core
{
    public class JwtTokenFunctions
    {

        private readonly IConfiguration _configuration;
        private readonly SocialMediaContext _context;

        public JwtTokenFunctions(IConfiguration configuration, SocialMediaContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string CreateJwtToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<User?> GetUserFromTokenAsync(HttpRequest httpRequest)
        {
            var username = GetUsernameFromToken(httpRequest);
            if (username == null) return null;
            User? user = await _context.GetUserByUsername(username);
            return user;
        }

        public static string? GetUsernameFromToken(HttpRequest httpRequest)
        {
            var token = httpRequest.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            return usernameClaim?.Value;
        }
    }
}
