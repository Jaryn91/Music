using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Musiction.API.BusinessLogic
{
    public class FileAndFolderPathsCreator : IFileAndFolderPathsCreator
    {
        private string _folder = Directory.GetCurrentDirectory();
        private const string PptxExtension = ".pptx";
        public string GetMergedFilePath()
        {
            string outcomeFileName = "FinaleFile_" + DateTime.Now.ToString("yyyyMMddHHmmss") + PptxExtension;
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

        public string GetPresentationFilePath(string songName)
        {
            string folderPath = Path.Combine(_folder, Startup.Configuration["folderSettings:presentationPath"]);
            EnsurePathExists(folderPath);

            var pptxName = CreatePresentationName(songName);

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

        private string CreatePresentationName(string songName)
        {
            var withoutPolishLetters = RemoveDiacritics(songName);
            var pptxSongName = RemoveSpecialCharacters(withoutPolishLetters);
            return "pptx_" + pptxSongName + PptxExtension;
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
    }
}
