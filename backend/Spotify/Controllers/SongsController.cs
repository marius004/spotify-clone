using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models.Song;

namespace Spotify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Create(CreateSongRequest req)
        {
            await _songService.Create(req);
            return new StatusCodeResult(201);
        }  
        
        [HttpGet]
        public async Task<IEnumerable<Song>> Get(string categoryId, string artistId)
        {
            if (!string.IsNullOrEmpty(categoryId))
                return await _songService.GetByCategory(categoryId);

            if (!string.IsNullOrEmpty(artistId))
                return await _songService.GetByArtist(artistId);

            return await _songService.GetAll();
        }
    }
}