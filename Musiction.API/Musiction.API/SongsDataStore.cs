using Musiction.API.Models;
using System.Collections.Generic;

namespace Musiction.API
{
    public class SongsDataStore
    {
        public static SongsDataStore Current { get; } = new SongsDataStore();
        public List<SongDto> Songs { get; set; }

        public SongsDataStore()
        {
            Songs = new List<SongDto>()
                {
                    new SongDto { Id = 1, Name = "Błogosławieni Miłosierni", Path = @"C:/Blogoslawnie.pptx" },
                    new SongDto {Id = 2, Name = "Barka", Path = @"C:/Blogoslawnie.pptx" }
                };
        }
    }
}
