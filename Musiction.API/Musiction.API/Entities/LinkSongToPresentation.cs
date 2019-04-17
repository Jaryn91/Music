using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Musiction.API.Entities
{
    public class LinkSongToPresentation
    {
        [Key]
        public int PresentationId { get; set; }
        public Presentation Presentation { get; set; }

        [Key]
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
