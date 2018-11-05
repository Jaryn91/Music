namespace Musiction.API.IBusinessLogic
{
    public interface IFileAndFolderPathsCreator
    {
        string GetPathToMergedFiles();
        string GetPathToZipFiles(string zipName);
        string GetUrlToFile(string pathToCombinedPptx);
    }
}