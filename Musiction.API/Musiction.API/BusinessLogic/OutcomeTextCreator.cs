using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musiction.API.BusinessLogic
{
    public class OutcomeTextCreator : IOutcomeTextCreator
    {
        public string CreateSciprtWithNamesOfSongsAndYouTubeLinks(IEnumerable<Song> songs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var song in songs)
                sb.AppendLine(String.Format(song.Name + " " + song.YouTubeUrl));

            return sb.ToString();
        }

    }
}
