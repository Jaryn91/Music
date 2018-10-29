using Musiction.API.IBusinessLogic;
using System;
using System.IO;

namespace Musiction.API.BusinessLogic
{
    public class FileAndFolderPathsCreator : IFileAndFolderPathsCreator
    {
        private readonly string _folder;
        private readonly string _webAddress;
        private const string _pptxExtension = ".pptx";
        private const string _finaleMergedFilePrefix = "finaleFile_";
        private readonly IGetValue _valueRetrieval;

        public FileAndFolderPathsCreator(IGetValue valueRetrieval)
        {
            _valueRetrieval = valueRetrieval;
            _folder = _valueRetrieval.Get("FileRoot");
            _webAddress = _valueRetrieval.Get("FilWebAddresseRoot");
        }

        public string GetMergedFilePath()
        {
            var folderPath = GetCombinedFolderPath("folderSettings:mergedPath");
            string outcomeFileName = _finaleMergedFilePrefix + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + _pptxExtension;
            return Path.Combine(folderPath, outcomeFileName);
        }

        public string GetZipFilePath(string zipName)
        {
            var folderPath = GetCombinedFolderPath("folderSettings:zipPath");
            string outcomeFilePath = Path.Combine(folderPath, zipName);
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

        public string GetWebAddressToFile(string path)
        {
            return path.Replace(_folder, _webAddress);
        }
    }
}
