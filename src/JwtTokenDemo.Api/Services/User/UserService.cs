using JwtTokenDemo.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokenDemo.Api.Services.User
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _context;
        public UserService(UserDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public UserModel GetUserByName(string userName)
        {
            var entity = _context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            return entity;
        }
       
        private string GetToken(UserModel user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_configuration.GetValue<int>("Tokens:Lifetime")),
                audience: _configuration.GetValue<string>("Tokens:Audience"),
                issuer: _configuration.GetValue<string>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public TokenResult GetTokenResponse(UserModel user)
        {
            var token = GetToken(user);
            TokenResult result = new TokenResult
            {
                AccessToken = token,
                ExpireInSeconds = _configuration.GetValue<int>("Tokens:Lifetime"),
                UserId = user.UserId.ToString()
            };
            return result;
        }
    }
}
