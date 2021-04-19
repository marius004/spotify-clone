using System.ComponentModel.DataAnnotations;

namespace Spotify.Models.Category
{
    public class CreateCategoryRequest
    {
        [Required] 
        public string Category { get; set; }
    }
}