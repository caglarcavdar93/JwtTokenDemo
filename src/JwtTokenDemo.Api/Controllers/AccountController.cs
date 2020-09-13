using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtTokenDemo.Api.Models;
using JwtTokenDemo.Api.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JwtTokenDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var entity = _userService.GetUserByName(user.UserName);
                if (entity != null)
                {
                    if (entity.Password == user.Password)
                    {
                        return Ok(_userService.GetTokenResponse(entity));
                    }
                }
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
