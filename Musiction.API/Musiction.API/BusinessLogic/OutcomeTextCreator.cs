using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using System.Collections.Generic;
using System.Text;

namespace Musiction.API.BusinessLogic
{
    public class OutcomeTextCreator : IOutcomeTextCreator
    {
        public string CreateSciprtWithNamesOfSongsAndYouTubeLinks(IEnumerable<Song> songs)
        {
            var sb = new StringBuilder();
            foreach (var song in songs)
                sb.AppendLine(string.Format(song.Name + " " + song.YouTubeUrl));

            return sb.ToString();
        }

    }
}
