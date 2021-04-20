using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Spotify.Models.Artist;

namespace Spotify.Entities
{
    public class Artist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> CategoriesId { get; set; }

        public int Rating { get; set; }
        
        ///  base 64 image
        public string Image { get; set; }

        public Artist(CreateArtistRequest req)
        {
            Name = req.Name;
            CategoriesId = req.CategoriesId;
            Image = req.Image;
            Rating = -1;
        }
    }
}