using Musiction.API.BusinessLogic;
using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using System.Collections.Generic;

namespace Musiction.API.Models
{
    public class PresentationResponse
    {
        public string Information { get; set; }
        public string AlertMessage { get; set; }
        public PresentationDto PresentationDto;




        public void CreateExceptionResponse(IEnumerable<Song> songs, string exceptionMessage)
        {
            IOutcomeTextCreator outcomeTextCreator = new OutcomeTextCreator();
            Information = outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);

            AlertMessage = exceptionMessage;
        }

        public void CreateSuccessResponse(PresentationDto presentationDto)
        {
            PresentationDto = presentationDto;

        }

    }
}
