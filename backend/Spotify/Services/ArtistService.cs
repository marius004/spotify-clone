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

        public void Update(string id, UpdateArtistRequest req)
        {
            var filter = Builders<Artist>.Filter.Eq("Id", id);
            List<Task> tasks = new List<Task>();
            
            if (!string.IsNullOrEmpty(req.Image) && !string.IsNullOrWhiteSpace(req.Image))
            {
                var update = Builders<Artist>.Update.Set("Image", req.Image);
                tasks.Add(_artists.UpdateOneAsync(filter, update));
            }

            if (!string.IsNullOrEmpty(req.Name) && !string.IsNullOrWhiteSpace(req.Name))
            {
                var update = Builders<Artist>.Update.Set("Name", req.Name);
                tasks.Add(_artists.UpdateOneAsync(filter, update)); 
            }

            if (!string.IsNullOrEmpty(req.Quote) && !string.IsNullOrWhiteSpace(req.Quote))
            {
                var update = Builders<Artist>.Update.Set("Quote", req.Quote);
                tasks.Add(_artists.UpdateOneAsync(filter, update)); 
            }

            if (req.Rating is > 0 and <= 5)
            {
                var update = Builders<Artist>.Update.Set("Rating", req.Rating);
                tasks.Add(_artists.UpdateOneAsync(filter, update));
            }

            if (req.CategoriesId != null)
            {
                var update = Builders<Artist>.Update.Set("CategoriesId", req.CategoriesId);
                tasks.Add(_artists.UpdateOneAsync(filter, update));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task<IEnumerable<PlainArtistResponse>> GetPlainArtists()
        {
            var query = (await _artists.FindAsync(art => true)).ToList();

            List<PlainArtistResponse> res = new List<PlainArtistResponse>();

            foreach (var artist in query)
                res.Add(new PlainArtistResponse(artist));

            return res;
        }
    }
}