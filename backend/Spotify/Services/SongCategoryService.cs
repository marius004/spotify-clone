using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models;
using Spotify.Models.Category;

namespace Spotify.Services
{
    public class SongCategoryService : ISongCategoryService
    {

        private readonly DatabaseSettings _databaseSettings;
        private readonly IMongoCollection<SongCategory> _categories;

        public SongCategoryService(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categories = database.GetCollection<SongCategory>(databaseSettings.SongCategoriesCollectionName);
        }
        
        public async Task Create(CreateCategoryRequest req)
        {
            var query = await _categories.FindAsync(cat => cat.Category == req.Category);

            if (query.ToList().FirstOrDefault() != null)
                return;
            
            var category = new SongCategory() {Category = req.Category};
            await _categories.InsertOneAsync(category);
        }

        public async Task<IEnumerable<SongCategory>> GetAll()
        {
            var res = await _categories.FindAsync(cat => true);
            return res.ToList();
        }

        public async Task DeleteById(string id)
        {
            await _categories.DeleteOneAsync(category => category.Id == id);
        }
    }
}