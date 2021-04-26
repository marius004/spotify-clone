using System.Collections.Generic;

namespace Spotify.Models.User
{
    public class CreateUserResponse
    {
        public string Email { get; set; }
        
        public string Username { get; set; }
        
        public string JwtToken { get; set; }
        
        public List<string> SongsLiked { get; set; }
        
        public List<string> ArtistsLiked { get; set; }

        public CreateUserResponse(Entities.User user, string token)
        {
            Email = user.Email;
            Username = user.Username;
            JwtToken = token;
            SongsLiked = user.SongsLiked;
            ArtistsLiked = user.ArtistsLiked;
        }
    }
}