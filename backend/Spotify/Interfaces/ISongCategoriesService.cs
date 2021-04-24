using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify.Entities;
using Spotify.Models.Category;

namespace Spotify.Interfaces
{
    public interface ISongCategoryService
    {
        Task Create(CreateCategoryRequest category);

        Task<IEnumerable<SongCategory>> GetAll();
        Task DeleteById(string id);
    }
}