using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Spotify.Models.Song;

namespace Spotify.Entities
{
    public class Song
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string ArtistId { get; set; }
        
        // base64 encoded audio
        public string Audio { get; set; }
        
        public IEnumerable<string> CategoriesId;

        public Song(CreateSongRequest req)
        {
            Name = req.Name;
            ArtistId = req.ArtistId;
            Audio = req.Audio;
            CategoriesId = req.CategoriesId;
        }
    }
}