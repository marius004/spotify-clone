using System.Collections.Generic;

namespace Spotify.Models.Song
{
    public class PlainSongResponse
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string ArtistId { get; set; }
        
        public IEnumerable<string> CategoriesId;

        public string ArtistName { get; set; }
        
        public PlainSongResponse(Entities.Song song, string artistName)
        {
            Id = song.Id;
            Name = song.Name;
            ArtistId = song.ArtistId;
            CategoriesId = song.CategoriesId;
            ArtistName = artistName;
        }
    }
}