using System.ComponentModel.DataAnnotations;

namespace Musiction.API.Models
{
    public class SongForCreationDto
    {
        [Required(ErrorMessage = "Znasz jakąś pieśń bez tytułu? Podaj <b>tytuł</b> pieśni, proszę!")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string YouTubeUrl { get; set; }
    }
}
