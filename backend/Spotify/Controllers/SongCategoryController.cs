using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models.Category;

namespace Spotify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongCategoryController : ControllerBase
    {
        private readonly ISongCategoryService _categoryService;

        public SongCategoryController(ISongCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<SongCategory>> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return categories;
        }

        [HttpPost]
        public async Task Create(CreateCategoryRequest req)
        {
            await _categoryService.Create(req);
        }

        [HttpDelete]
        public async Task Delete(string id)
        {
            await _categoryService.DeleteById(id);
        }
    }
}