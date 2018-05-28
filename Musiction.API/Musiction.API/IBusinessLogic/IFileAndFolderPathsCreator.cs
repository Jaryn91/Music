﻿namespace Musiction.API.IBusinessLogic
{
    public interface IFileAndFolderPathsCreator
    {
        string GetMergedFilePath();
        string GetZipFilePath(string zipName);
        string GetPresentationFilePath(string pptxName);
        string GetWebAddressToFile(string pathToCombinedPptx);
    }
}