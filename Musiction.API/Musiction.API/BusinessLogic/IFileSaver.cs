using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Musiction.API.BusinessLogic
{
    public interface IFileSaver
    {
        Task<string> SaveSong(IFormFile file, string songName);
        string UpdateSongPath(string oldPath, string newSongName);
        void DeleteSong(string path);
    }
}