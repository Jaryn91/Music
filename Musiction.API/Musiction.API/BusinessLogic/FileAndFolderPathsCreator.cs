using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Musiction.API.BusinessLogic
{
    public class FileAndFolderPathsCreator : IFileAndFolderPathsCreator
    {
        private string _folder;
        private string _webAddress;
        private const string _pptxExtension = ".pptx";
        private const string _pptxPrefix = "pptx_";
        private const string _finaleMergedFilePrefix = "finaleFile_";

        public FileAndFolderPathsCreator()
        {
            var fileRoot = Startup.Configuration["environment"] + "FileRoot";
            _folder = Startup.Configuration[fileRoot];
            var webAddress = Startup.Configuration["environment"] + "WebAddress";
            _webAddress = Startup.Configuration[webAddress];
        }

        public string GetMergedFilePath()
        {
            var folderPath = GetCombinedFolderPath("folderSettings:mergedPath");
            string outcomeFileName = _finaleMergedFilePrefix + DateTime.Now.ToString("yyyyMMddHHmmss") + _pptxExtension;
            return Path.Combine(folderPath, outcomeFileName);
        }

        public string GetZipFilePath(string zipName)
        {
            var folderPath = GetCombinedFolderPath("folderSettings:zipPath");
            string outcomeFilePath = Path.Combine(folderPath, zipName);
            return outcomeFilePath;
        }

        public string GetPresentationFilePath(string songName)
        {
            var folderPath = GetCombinedFolderPath("folderSettings:presentationPath");
            var pptxName = CreatePresentationName(songName);
            return Path.Combine(folderPath, pptxName);
        }

        private void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string CreatePresentationName(string songName)
        {
            var withoutPolishLetters = RemoveDiacritics(songName);
            var pptxSongName = RemoveSpecialCharacters(withoutPolishLetters);
            return _pptxPrefix + pptxSongName + _pptxExtension;
        }


        private string RemoveDiacritics(string songName)
        {
            string asciiEquivalents = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(songName));

            return asciiEquivalents;
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        private string GetCombinedFolderPath(string sectionInAppSetting)
        {
            var list = new List<string>();
            Startup.Configuration.GetSection(sectionInAppSetting).Bind(list);
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
