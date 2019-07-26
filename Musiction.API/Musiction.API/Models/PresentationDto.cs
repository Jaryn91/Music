using Musiction.API.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Musiction.API.Models
{
    public class PresentationDto
    {
        public int Id { get; set; }
        public string CreatedDate { get; set; }

        public string CreateBy { get; set; }

        public List<string> SongNames { get; set; }

        public string UrlToPptx { get; set; }
        public string UrlToZip { get; set; }
        public string GoogleDriveZipFileId { get; set; }
        public string GoogleDrivePptxFileId { get; set; }
        public string Information
        {
            get
            {
                var sb = new StringBuilder();
                var songs = LinkSongToPresentation.Select(s => s.Song);
                foreach (var song in songs)
                    sb.AppendLine(string.Format(song.Name + " " + song.YouTubeUrl));

                return sb.ToString();
            }
        }

        public List<LinkSongToPresentation> LinkSongToPresentation { get; set; }

    }
}
