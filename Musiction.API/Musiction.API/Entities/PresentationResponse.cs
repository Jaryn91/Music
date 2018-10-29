using Musiction.API.BusinessLogic;
using Musiction.API.IBusinessLogic;
using Musiction.API.Services;
using System.Collections.Generic;

namespace Musiction.API.Entities
{
    public class PresentationResponse : ICreatePresentationResponse
    {
        private readonly IOutcomeTextCreator _outcomeTextCreator;
        private readonly IFileAndFolderPathsCreator _fileAndFolderPath;
        private readonly ISongRepository _songRepository;
        private readonly IConvertPresentation _presentationConverter;


        public PresentationResponse(IFileAndFolderPathsCreator fileAndFolderPath,
            IOutcomeTextCreator outcomeTextCreator, ISongRepository songRepository,
            IConvertPresentation presentationConverter)
        {
            _presentationConverter = presentationConverter;
            _outcomeTextCreator = outcomeTextCreator;
            _fileAndFolderPath = fileAndFolderPath;
            _songRepository = songRepository;
        }

        public string Path { get; set; }
        public string Inforamtion { get; set; }

        public PresentationResponse CreatePptxResponse(List<int> ids)
        {
            var songs = _songRepository.GetSongsInOrder(ids);
            var paths = new List<string>();
            foreach (var song in songs)
            {
                paths.Add($"https://docs.google.com/presentation/d/{song.PresentationId}/export/pptx");
            }

            var merger = new PowerPointMerger(_fileAndFolderPath);
            var pathToCombinedPptx = merger.Merge(paths);
            Path = _fileAndFolderPath.GetWebAddressToFile(pathToCombinedPptx);
            Inforamtion = _outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);
            return this;
        }

        public PresentationResponse CreateZipResponse(List<int> ids)
        {
            var songs = _songRepository.GetSongsInOrder(ids);
            var paths = new List<string>();
            foreach (var song in songs)
            {
                paths.Add($"https://docs.google.com/presentation/d/{song.PresentationId}/export/pptx");
            }

            var merger = new PowerPointMerger(_fileAndFolderPath);
            var pathToCombinedPptx = merger.Merge(paths);

            var pathToZip = _presentationConverter.Convert(pathToCombinedPptx);
            Path = _fileAndFolderPath.GetWebAddressToFile(pathToZip);
            Inforamtion = _outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);
            return this;
        }
    }
}
