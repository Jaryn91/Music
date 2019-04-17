using Musiction.API.Entities;
using System.Collections.Generic;

namespace Musiction.API.Services
{
    public interface ISongRepository
    {
        IEnumerable<Song> Get();
        Song Get(int songId);
        bool Add(Song song);
        bool Save();
        void Remove(Song song);
        IEnumerable<Song> GetSongsInOrder(List<int> ids);
    }
}
