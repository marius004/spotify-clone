using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models;
using Spotify.Models.Artist;

namespace Spotify.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IMongoCollection<Artist> _artists;

        public ArtistService(DatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _artists = database.GetCollection<Artist>(databaseSettings.ArtistsCollectionName);
        }

        public async Task Create(CreateArtistRequest req)
        {
            var query = await _artists.FindAsync(artist => artist.Name == req.Name);
            
            if (query.ToList().FirstOrDefault() != null)
                return;
            
            var artist = new Artist(req);
            await _artists.InsertOneAsync(artist);
        }

        public async Task Delete(string id)
        {
            await _artists.DeleteOneAsync(artist => artist.Id == id);
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return (await _artists.FindAsync(artist => true)).ToList();
        }

        public async Task<Artist> GetById(string id)
        {
            return (await _artists.FindAsync(artist => artist.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<Artist>> GetByCategory(string categoryId)
        {
            var query = await _artists.FindAsync(artist => artist.CategoriesId.Contains(categoryId));
            return await query.ToListAsync();
        }
    }
}