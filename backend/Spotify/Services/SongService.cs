using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Spotify.Attributes;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models;
using Spotify.Models.Song;

namespace Spotify.Services
{
    public class SongService : ISongService
    {
        private readonly IMongoCollection<Song> _songs;
        
        public SongService(DatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _songs = database.GetCollection<Song>(databaseSettings.SongsCollectionName);
        }

        public async Task Create(CreateSongRequest req)
        {
            var song = new Song(req);
            await _songs.InsertOneAsync(song);
        }

        public async Task<IEnumerable<Song>> GetAll()
        {
            var query = await _songs.FindAsync(song => true);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetByArtist(string artistId)
        {
            var query = await _songs.FindAsync(song => song.ArtistId  == artistId);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetByCategory(string categoryId)
        {
            var query = await _songs.FindAsync(song => song.CategoriesId.Contains(categoryId));
            return await query.ToListAsync();
        }

        public async Task<Song> GetById(string id)
        {
            var res = await _songs.FindAsync(song => song.Id == id);
            return res.ToList().FirstOrDefault();
        }

        public void Update(string id, UpdateSongRequest req)
        {
            var filter = Builders<Song>.Filter.Eq("Id", id);
            List<Task> tasks = new List<Task>();
            
            if (req.Audio != null)
            {
                var update = Builders<Song>.Update.Set("Audio", req.Audio);
                tasks.Add(_songs.UpdateOneAsync(filter, update));
            }

            if (req.Name != null)
            {
                var update = Builders<Song>.Update.Set("Name", req.Name);
                tasks.Add(_songs.UpdateOneAsync(filter, update));
            }

            if (req.CategoriesId != null)
            {
                var update = Builders<Song>.Update.Set("CategoriesId", req.CategoriesId);
                tasks.Add(_songs.UpdateOneAsync(filter, update));
            }

            if (req.ArtistId != null)
            {
                var update = Builders<Song>.Update.Set("ArtistId", req.ArtistId);
                tasks.Add(_songs.UpdateOneAsync(filter, update));
            }
            
            Task.WaitAll(tasks.ToArray());
        }
        
        public async Task DeleteById(string id)
        {
            await _songs.DeleteOneAsync(song => song.Id == id);
        }
    }
}