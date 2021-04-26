namespace Spotify.Models.User
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Entities.User user, string token)
        {
            Username = user.Username;
            Email = user.Email;
            Token = token;
            Id = user.Id;
        }
    }
}