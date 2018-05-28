using Musiction.API.Entities;
using System.Collections.Generic;

namespace Musiction.API.IBusinessLogic
{
    public interface IOutcomeTextCreator
    {
        string CreateSciprtWithNamesOfSongsAndYouTubeLinks(IEnumerable<Song> songs);
    }
}
