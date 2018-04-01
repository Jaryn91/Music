using System;
using System.IO;

namespace Musiction.API.BusinessLogic
{
    public class FileAndFolderPath : IFileAndFolderPath
    {
        private string _folder = Directory.GetCurrentDirectory();
        public string GetMergedFilePath()
        {
            string outcomeFileName = "FinaleFile_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pptx";
            string folderPath = Path.Combine(_folder, Startup.Configuration["folderSettings:mergedPath"]);
            EnsurePathExists(folderPath);
            string outcomeFilePath = Path.Combine(folderPath, outcomeFileName);
            return outcomeFilePath;
        }

        public string GetZipFilePath(string zipName)
        {
            string folderPath = Path.Combine(_folder, Startup.Configuration["folderSettings:zipPath"]);
            EnsurePathExists(folderPath);
            string outcomeFilePath = Path.Combine(folderPath, zipName);
            return outcomeFilePath;
        }

        public string GetPresentationFilePath(string pptxName)
        {
            string folderPath = Path.Combine(_folder, Startup.Configuration["folderSettings:presentationPath"]);
            EnsurePathExists(folderPath);
            string outcomeFilePath = Path.Combine(folderPath, pptxName);
            return outcomeFilePath;
        }

        private void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
