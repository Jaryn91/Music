using Musiction.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Musiction.API.Services
{
    public class SongRepository : ISongRepository
    {
        private readonly SongContext _context;

        public SongRepository(SongContext context)
        {
            _context = context;
        }

        public bool AddSong(Song song)
        {
            _context.Songs.Add(song);
            return (_context.SaveChanges() >= 0);
        }

        public Song GetSong(int songId)
        {
            return _context.Songs.FirstOrDefault(s => s.Id == songId);
        }

        public IEnumerable<Song> GetSongsInOrder(List<int> songIds)
        {
            var songs = _context.Songs.Where(s => songIds.Contains(s.Id));
            songs = songs.OrderBy(d => songIds.IndexOf(d.Id));
            return songs.ToList();
        }

        public IEnumerable<Song> GetSongs()
        {
            return _context.Songs.OrderBy(s => s.Name).ToList();
        }

        public void RemoveSong(Song song)
        {
            _context.Remove(song);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
