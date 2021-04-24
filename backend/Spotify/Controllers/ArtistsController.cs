using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models;
using Spotify.Models.Artist;

namespace Spotify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly DatabaseSettings _databaseSettings;
        private readonly IArtistService _artistService;

        public ArtistsController(DatabaseSettings databaseSettings, IArtistService artistService)
        {
            _databaseSettings = databaseSettings;
            _artistService = artistService;
        }

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

        [HttpPost]
        public async Task<StatusCodeResult> Create(CreateArtistRequest req)
        {
            await _artistService.Create(req);
            return new StatusCodeResult(201);
        }

    }
}