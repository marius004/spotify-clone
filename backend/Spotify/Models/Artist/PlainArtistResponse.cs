namespace Spotify.Models.Artist
{
    public class PlainArtistResponse
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public PlainArtistResponse(Entities.Artist artist)
        {
            Id = artist.Id;
            Name = artist.Name;
        }
        
    }
}