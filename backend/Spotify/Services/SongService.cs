using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
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
    }
}