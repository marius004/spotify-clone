using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spotify.Models.User
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        
        public string Username { get; set; }    
        
        public string Password { get; set; }
        
        public IEnumerable<string> SongsLiked { get; set; }
        
        public IEnumerable<string> SongsUnliked { get; set; }

        public IEnumerable<string> ArtistsLiked { get; set; }
        
        public IEnumerable<string> ArtistsUnliked { get; set; }
    }
}