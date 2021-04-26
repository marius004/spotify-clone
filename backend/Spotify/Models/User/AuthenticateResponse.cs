using System.Collections.Generic;

namespace Spotify.Models.User
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
        
        public List<string> SongsLiked { get; set; }
        
        public List<string> ArtistsLiked { get; set; }

        public AuthenticateResponse(Entities.User user, string token)
        {
            Username = user.Username;
            Email = user.Email;
            JwtToken = token;
            Id = user.Id;
            SongsLiked = user.SongsLiked;
            ArtistsLiked = user.ArtistsLiked;
        }
    }
}