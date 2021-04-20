using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify.Entities;
using Spotify.Models.Song;

namespace Spotify.Interfaces
{
    public interface ISongService
    {
        Task Create(CreateSongRequest req);
        
        Task<IEnumerable<Song>> GetAll();
        
        Task<IEnumerable<Song>> GetByCategory(string categoryId);
    }
}