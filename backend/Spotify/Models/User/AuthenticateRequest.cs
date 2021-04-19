using System.ComponentModel.DataAnnotations;

namespace Spotify.Models.User
{
    public class AuthenticateRequest
    {
        [Required] 
        public string Username { get; set; }
        
        [Required] 
        public string Password { get; set; }
    }
}