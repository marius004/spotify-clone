using System.Collections.Generic;

namespace Spotify.Models.Song
{
    public class UpdateSongRequest
    {
        public string Name { get; set; }
                
        public string ArtistId  { get; set; }

        public string Audio { get; set; }
        
        public IEnumerable<string> CategoriesId { get; set; }
    }
}