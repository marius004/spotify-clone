using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spotify.Entities;

namespace Spotify.Models.Artist
{
    public class CreateArtistRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public IEnumerable<string> CategoriesId { get; set; }

        [Required]
        public string Image { get; set; }
    }
}