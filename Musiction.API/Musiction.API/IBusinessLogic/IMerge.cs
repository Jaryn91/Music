using Musiction.API.Entities;
using System.Collections.Generic;

namespace Musiction.API.IBusinessLogic
{
    public interface IMerge
    {
        string Merge(IEnumerable<Song> songs, bool convertPathToUrl = false);
    }
}
