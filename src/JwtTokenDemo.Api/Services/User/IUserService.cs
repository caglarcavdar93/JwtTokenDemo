using JwtTokenDemo.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokenDemo.Api.Services.User
{
    public interface IUserService
    {
        public TokenResult GetTokenResponse(UserModel user);
        public UserModel GetUserByName(string userName);
    }
}
