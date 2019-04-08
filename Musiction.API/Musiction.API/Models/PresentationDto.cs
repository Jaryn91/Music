using System.Collections.Generic;

namespace Musiction.API.Models
{
    public class PresentationDto
    {
        public string CreatedDate { get; set; }

        public string CreateBy { get; set; }

        public string Type { get; set; }

        public List<string> SongNames { get; set; }
    }
}
