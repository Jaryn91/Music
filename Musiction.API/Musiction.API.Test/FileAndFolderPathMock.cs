﻿using Musiction.API.IBusinessLogic;
using System;
using System.IO;

namespace Musiction.API.Test
{
    public class FileAndFolderPathMock : IFileAndFolderPathsCreator
    {
        public string GetPathToMergedFiles(string finalFileName)
        {
            string outcomeFileName = "FinaleFile_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pptx";
            string folder = Directory.GetCurrentDirectory();
            string folderPath = Path.Combine(folder, "Merged");
            EnsurePathExists(folderPath);
            string outcomeFilePath = Path.Combine(folderPath, outcomeFileName);
            return outcomeFilePath;
        }

        public string GetPresentationFilePath(string pptxName)
        {
            throw new NotImplementedException();
        }

        public string GetUrlToFile(string pathToCombinedPptx)
        {
            throw new NotImplementedException();
        }

        public string GetPathToZipFiles(string zipName)
        {
            string folder = Directory.GetCurrentDirectory();
            string folderPath = Path.Combine(folder, "Zip");
            EnsurePathExists(folderPath);
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
    }
}
