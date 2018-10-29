using System.Collections.Generic;

namespace Musiction.API.IBusinessLogic
{
    public interface IGetValue
    {
        string Get(string key);
        List<string> GetSectionToList(string sectionName);
    }
}
