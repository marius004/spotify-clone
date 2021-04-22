using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify.Entities;
using Spotify.Models.Artist;

namespace Spotify.Interfaces
{
    public interface IArtistService
    {
        Task Create(CreateArtistRequest req);
 
        Task Delete(string id);

        Task<IEnumerable<Artist>> GetAll();

        Task<Artist> GetById(string id);

        Task<IEnumerable<Artist>> GetByCategory(string category);
    }
}