using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Musiction.API.Models
{
    public class SongForCreationDto
    {
        [Required(ErrorMessage = "Podaj nazwę piosenki")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Path { get; set; }

        public IFormFile PptxFile { get; set; }
    }
}
