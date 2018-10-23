using Musiction.API.BusinessLogic;
using Musiction.API.IBusinessLogic;
using Musiction.API.Services;
using System.Collections.Generic;

namespace Musiction.API.Entities
{
    public class PresentationResponse
    {
        private IOutcomeTextCreator _outcomeTextCreator;
        private IFileAndFolderPathsCreator _fileAndFolderPath;
        private ISongRepository _songRepository;


        public PresentationResponse(IFileAndFolderPathsCreator fileAndFolderPath,
            IOutcomeTextCreator outcomeTextCreator, ISongRepository songRepository)
        {
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
                //paths.Add(song.Path);
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
                //paths.Add(song.Path);
            }

            var merger = new PowerPointMerger(_fileAndFolderPath);
            var pathToCombinedPptx = merger.Merge(paths);

            var pptxConverter = new PptxToJpgConverter(_fileAndFolderPath);
            var pathToZip = pptxConverter.Convert(pathToCombinedPptx);
            Path = _fileAndFolderPath.GetWebAddressToFile(pathToZip);
            Inforamtion = _outcomeTextCreator.CreateSciprtWithNamesOfSongsAndYouTubeLinks(songs);
            return this;
        }
    }
}
