using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Spotify.Models.User;
using Spotify.Util;

namespace Spotify.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }

        public User(CreateUserRequest req)
        {
            Email = req.Email;
            Username = req.Username;
            Password = PasswordHash.Hash(req.Password);
        }
    }
}