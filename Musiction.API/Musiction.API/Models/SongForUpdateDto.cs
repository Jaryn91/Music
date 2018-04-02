using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Musiction.API.Models
{
    public class SongForUpdateDto
    {
        [Required(ErrorMessage = "Podaj nazwę piosenki")]
        [MaxLength(100)]
        public string Name { get; set; }

        public IFormFile PptxFile { get; set; }
    }
}
