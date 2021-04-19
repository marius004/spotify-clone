using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spotify.Entities
{
    public class SongCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Category { get; set; }
    }
}