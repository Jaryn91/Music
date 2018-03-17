using Musiction.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Musiction.API
{
    public static class SongExtension
    {
        public static void EnsureSeedDataForContext(this SongContext context)
        {
            if (context.Songs.Any())
                return;

            var songs = new List<Song>()
            {

                new Song()
                {
                    Name = "Błogosławieni Miłosierni",
                    Path = @"C:/Blogoslawnie.pptx",
                    YouTubeUrl = @"www.youtube.com/Blogoslawnie"
                },

                new Song()
                {
                    Name = "Barka",
                    Path = @"C:/Barka.pptx",
                    YouTubeUrl = @"www.youtube.com/Barka"
                }
            };

            context.Songs.AddRange(songs);
            context.SaveChanges();
        }
    }
}

