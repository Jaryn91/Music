using Musiction.API.BusinessLogic;
using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using Musiction.API.Resources;
using System;
using System.Collections.Generic;

namespace Musiction.API.Models
{
    public class PresentationResponse
    {
        public string Url { get; set; }
        public string Information { get; set; }
        public string AlertMessage { get; set; }
        public string PptxFileId { get; set; }


        public void CreateSuccessResponse(string mergedPresentationId, string presentationType, IEnumerable<Song> songs)
        {
            IOutcomeTextCreator outcomeTextCreator = new OutcomeTextCreator();
            Information = outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);
            PptxFileId = mergedPresentationId;
            if (presentationType == "pptx")
                Url = String.Format(MagicString.PathTFileInGoogleDrive, mergedPresentationId);
            if (presentationType == "zip")
                Url = String.Format(MagicString.PathToDownloadFileFromGoogleDrive, mergedPresentationId);
        }

        public void CreateExceptionResponse(IEnumerable<Song> songs, string exceptionMessage)
        {
            IOutcomeTextCreator outcomeTextCreator = new OutcomeTextCreator();
            Information = outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);

            AlertMessage = exceptionMessage;
        }
    }
}
