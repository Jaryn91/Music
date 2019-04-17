using System.Collections.Generic;

namespace Musiction.API.Models
{
    public class SongResponse
    {
        public IEnumerable<SongDto> Songs { get; set; }
        public string Information { get; set; }
        public string AlertMessage { get; set; }

    }
}
