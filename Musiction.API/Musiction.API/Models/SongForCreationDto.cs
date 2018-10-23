﻿using System.ComponentModel.DataAnnotations;

namespace Musiction.API.Models
{
    public class SongForCreationDto
    {
        [Required(ErrorMessage = "Znasz piosenkę bez tytułu? Podaj jakiś tytuł, proszę!")]
        [MaxLength(100)]
        public string Name { get; set; }

        //[MaxLength(200)]
        //public string Path { get; set; }

        [MaxLength(50)]
        public string YouTubeUrl { get; set; }

        //public IFormFile PptxFile { get; set; }

        [MaxLength(50)]
        public string PresentationId { get; set; }
    }
}
