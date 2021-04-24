using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spotify.Models.Artist
{
    public class CreateArtistRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public IEnumerable<string> CategoriesId { get; set; }

        [Required]
        public string Quote { get; set; }

        [Required]
        public string Image { get; set; }
    }
}