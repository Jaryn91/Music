namespace Musiction.API.IBusinessLogic
{
    public interface IFileAndFolderPathsCreator
    {
        string GetPathToMergedFiles(string finalFileName);
        string GetPathToZipFiles(string zipName);
        string GetUrlToFile(string path);
    }
}