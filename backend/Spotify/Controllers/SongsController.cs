using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models.Song;

namespace Spotify.Controllers
{
    [ApiController]
    public class SongsController
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpPost("api/songs")]
        public async Task<StatusCodeResult> Create(CreateSongRequest req)
        {
            await _songService.Create(req);
            return new StatusCodeResult(201);
        } 

        [HttpGet("api/song/{id}")]
        public async Task<IActionResult> GetSongById(string id) 
        {
            var res = await _songService.GetById(id); 
            
            if(res == null) 
               return new NotFoundObjectResult("Song not found");
            
            return new OkObjectResult(res);    
        }
        
        [HttpGet("api/songs")]
        public async Task<IEnumerable<Song>> Get(string categoryId, string artistId)
        {
            if (!string.IsNullOrEmpty(categoryId))
                return await _songService.GetByCategory(categoryId);

            if (!string.IsNullOrEmpty(artistId))
                return await _songService.GetByArtist(artistId);

            return await _songService.GetAll();
        }

        [Authorize]
        [HttpPut("/api/song/{id}")]
        public void Update(string id,UpdateSongRequest req)
        {
            _songService.Update(id, req);
        }

        [Authorize]
        [HttpDelete("/api/song/{id}")]
        public async Task Delete(string id)
        {
            await _songService.DeleteById(id);
        }
    }
}