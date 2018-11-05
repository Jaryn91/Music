using Musiction.API.IBusinessLogic;
using Musiction.API.Resources;
using System.IO;

namespace Musiction.API.BusinessLogic
{
    public class FileAndFolderPathsCreator : IFileAndFolderPathsCreator
    {
        private readonly string _folder;
        private readonly string _webAddress;
        private readonly IGetValue _valueRetrieval;

        public FileAndFolderPathsCreator(IGetValue valueRetrieval)
        {
            _valueRetrieval = valueRetrieval;
            _folder = _valueRetrieval.Get("FileRoot");
            _webAddress = _valueRetrieval.Get("WebAddress");
        }

        public string GetPathToMergedFiles()
        {
            var folderPath = GetCombinedFolderPath("folderSettings:mergedPath");
            var outcomeFileName = MagicString.FinalFileName;
            return Path.Combine(folderPath, outcomeFileName);
        }

        public string GetPathToZipFiles(string zipName)
        {
            var folderPath = GetCombinedFolderPath("folderSettings:zipPath");
            var outcomeFilePath = Path.Combine(folderPath, zipName);
            return outcomeFilePath;
        }

        private void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string GetCombinedFolderPath(string sectionInAppSetting)
        {
            var list = _valueRetrieval.GetSectionToList(sectionInAppSetting);
            list.Insert(0, _folder);
            var folderPath = Path.Combine(list.ToArray());
            EnsurePathExists(folderPath);
            return folderPath;
        }

        public string GetUrlToFile(string path)
        {
            return path.Replace(_folder, _webAddress);
        }
    }
}
