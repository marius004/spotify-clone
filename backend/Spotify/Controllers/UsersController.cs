using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models.User;

namespace Spotify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetAll();
        }

        [HttpPost]
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest req)
        {
            var res = await _userService.Authenticate(req);

            if (res == null)
                return BadRequest(new {message = "Username or password incorrect"});

            return Ok(res);
        }
    }
}