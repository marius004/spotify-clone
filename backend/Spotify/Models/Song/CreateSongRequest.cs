using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spotify.Models.Song
{
    public class CreateSongRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string ArtistId  { get; set; }
        
        [Required]
        public string Audio { get; set; }
        
        [Required]
        public IEnumerable<string> CategoriesId { get; set; }
    }
}