using Musiction.API.BusinessLogic;
using Musiction.API.IBusinessLogic;
using System.Collections.Generic;

namespace Musiction.API.Entities
{
    public class PresentationResponse
    {
        public string Url { get; set; }
        public string Information { get; set; }
        public string AlertMessage { get; set; }

        public void CreateSuccessResponse(IEnumerable<Song> songs, string urlToMergedPresentations)
        {
            IOutcomeTextCreator outcomeTextCreator = new OutcomeTextCreator();
            Information = outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);

            Url = urlToMergedPresentations;
        }

        public void CreateExceptionResponse(IEnumerable<Song> songs, string exceptionMessage)
        {
            IOutcomeTextCreator outcomeTextCreator = new OutcomeTextCreator();
            Information = outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);

            AlertMessage = exceptionMessage;
        }
    }
}
