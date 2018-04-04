using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Musiction.API.BusinessLogic
{
    public class FileSaver : IFileSaver
    {
        private IFileAndFolderPathsCreator _fileAndFolderPath;

        public FileSaver(IFileAndFolderPathsCreator fileAndFolderPath)
        {
            _fileAndFolderPath = fileAndFolderPath;
        }

        public void DeleteSong(string path)
        {
            File.Delete(path);
        }

        public async Task<string> SaveSong(IFormFile file, string songName)
        {
            var filePath = _fileAndFolderPath.GetPresentationFilePath(songName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }

        public string UpdateSongPath(string oldPath, string newSongName)
        {
            var nameFileName = _fileAndFolderPath.GetPresentationFilePath(newSongName);
            File.Move(oldPath, nameFileName);
            return nameFileName;
        }
    }
}
