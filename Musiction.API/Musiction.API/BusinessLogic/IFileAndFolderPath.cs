namespace Musiction.API.BusinessLogic
{
    public interface IFileAndFolderPath
    {
        string GetMergedFilePath();
        string GetZipFilePath(string zipName);
        string GetPresentationFilePath(string pptxName);
    }
}