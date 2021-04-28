using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using Spotify.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models;
using Spotify.Models.Artist;

namespace Spotify.Controllers
{
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly DatabaseSettings _databaseSettings;
        private readonly IArtistService _artistService;
        private readonly IUserService _userService;
        public ArtistsController(DatabaseSettings databaseSettings, IArtistService artistService, IUserService userService)
        {
            _databaseSettings = databaseSettings;
            _artistService = artistService;
            _userService = userService;
        }
        
        [Route("/api/[controller]/name/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetArtistName(string id)
        {
            var res = await _artistService.GetById(id);

            if (res == null)
                return NotFound();

            return Ok(new {name = res.Name});
        }

        [HttpGet("/api/[controller]/plain")]
        public async Task<IActionResult> GetPlainArtists()
        {
            var res = await _artistService.GetPlainArtists();
            return Ok(res);
        }

        [Route("/api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> Get(string categoryId, string singerId)
        {

            if (!string.IsNullOrEmpty(singerId))
            {
                var res = await _artistService.GetById(singerId);

                // no artist with the given id
                if (res == null)
                    return NotFound();
                
                return Ok(res);
            }
            
            if (!string.IsNullOrEmpty(categoryId))
            {
                var res = await _artistService.GetByCategory(categoryId);
                return Ok(res);
            }

            var response = await _artistService.GetAll();
            return Ok(response);
        }

        [Route("/api/[controller]")]
        [HttpPost]
        public async Task<StatusCodeResult> Create(CreateArtistRequest req)
        {
            await _artistService.Create(req);
            return new StatusCodeResult(201);
        }

        [HttpGet("/api/artist/likes/{id}")]
        public async Task<int> GetLikes(string id)
        {
            var users = (await _userService.GetAll()).ToList();
            return users.FindAll(usr => usr.ArtistsLiked != null && usr.ArtistsLiked.Contains(id)).Count;
        }

        [Admin]
        [HttpPut("/api/artist/{id}")]
        public void Update(string id, UpdateArtistRequest req)
        {
            _artistService.Update(id, req);
        }
    }
}