using System.Collections.Generic;

namespace Musiction.API.Models
{
    public class PresentationDto
    {
        public int Id { get; set; }
        public string CreatedDate { get; set; }

        public string CreateBy { get; set; }

        public string Type { get; set; }

        public List<string> SongNames { get; set; }

        public string Url { get; set; }
    }
}
