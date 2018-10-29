using Microsoft.Extensions.Configuration;
using Musiction.API.IBusinessLogic;
using System.Collections.Generic;

namespace Musiction.API.BusinessLogic
{
    public class ValueRetrieval : IGetValue
    {
        public string Get(string key)
        {
            var fullKeyName = Startup.Configuration["env"] + ":" + key;
            return Startup.Configuration[fullKeyName];
        }

        public List<string> GetSectionToList(string sectionName)
        {
            var list = new List<string>();
            Startup.Configuration.GetSection(sectionName).Bind(list);
            return list;
        }
    }
}
