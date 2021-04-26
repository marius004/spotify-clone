using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotify.Attributes;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models.User;

namespace Spotify.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("/api/[controller]")]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetAll();
        }
        
        [HttpPost("/api/[controller]")]
        public async Task<IActionResult> Create(CreateUserRequest req)
        {
            try
            {
                var res = await _userService.Create(req);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpPost("/api/[controller]/authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest req)
        {
            var res = await _userService.Authenticate(req);

            if (res == null)
                return BadRequest(new {message = "Username or password incorrect"});

            return Ok(res);
        }

        [Authorize]
        [HttpPut("/api/user")]
        public async Task<UpdateUserResponse> Update(UpdateUserRequest req)
        {
            var user = (User) HttpContext.Items["User"];
            var res = await _userService.Update(user.Id, req);
            return res;
        }
    }
}