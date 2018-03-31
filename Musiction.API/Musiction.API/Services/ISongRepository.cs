using Musiction.API.Entities;
using System.Collections.Generic;

namespace Musiction.API.Services
{
    public interface ISongRepository
    {
        IEnumerable<Song> GetSongs();
        Song GetSong(int songId);
        bool AddSong(Song song);

        bool Save();
        void RemoveSong(Song song);
    }
}
