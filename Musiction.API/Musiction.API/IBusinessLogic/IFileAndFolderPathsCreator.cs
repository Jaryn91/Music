namespace Musiction.API.IBusinessLogic
{
    public interface IFileAndFolderPathsCreator
    {
        string GetPathToMergedFiles();
        string GetPathToZipFiles(string zipName);
        string GetWebAddressToFile(string pathToCombinedPptx);
    }
}