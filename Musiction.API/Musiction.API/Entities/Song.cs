using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musiction.API.Entities
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string YouTubeUrl { get; set; }

        [MaxLength(50)]
        public string PresentationId { get; set; }

        public List<LinkSongToPresentation> LinkSongToPresentation { get; set; } = new List<LinkSongToPresentation>();

    }
}
