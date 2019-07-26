using Musiction.API.Entities;
using System.Collections.Generic;

namespace Musiction.API.Services
{
    public interface IPresentationRepository
    {
        bool Add(Presentation presentation);
        Presentation Get(int presentationId);
        IEnumerable<Presentation> Get();
        bool Save();
        Presentation Get(string googleDriveFileId);
    }
}
