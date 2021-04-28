using System.Collections.Generic;

namespace Spotify.Models.Artist
{
    public class UpdateArtistRequest
    {
        public string Name { get; set; }
        
        public IEnumerable<string> CategoriesId { get; set; }
                
        public int Rating { get; set; }
        
        public string Quote { get; set; }
        
        ///  base 64 image
        public string Image { get; set; }
    }
}