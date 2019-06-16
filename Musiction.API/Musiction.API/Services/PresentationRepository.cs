using Microsoft.EntityFrameworkCore;
using Musiction.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Musiction.API.Services
{
    public class PresentationRepository : IPresentationRepository
    {
        private readonly SongContext _context;

        public PresentationRepository(SongContext context)
        {
            _context = context;
        }

        public bool Add(Presentation presentation)
        {
            _context.Presentations.Add(presentation);
            return (_context.SaveChanges() >= 0);
        }

        public Presentation Get(int presentationId)
        {
            return _context.Presentations.FirstOrDefault(s => s.Id == presentationId);
        }

        public IEnumerable<Presentation> Get()
        {
            return _context.Presentations.Include(s => s.LinkSongToPresentation).ThenInclude(x => x.Song);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Presentation Get(string googleDriveFileId)
        {
            return _context.Presentations.FirstOrDefault(s => s.GoogleDrivePptxFileId.Contains(googleDriveFileId));
        }
    }
}
