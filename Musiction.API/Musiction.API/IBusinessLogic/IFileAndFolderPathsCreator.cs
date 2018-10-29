namespace Musiction.API.IBusinessLogic
{
    public interface IFileAndFolderPathsCreator
    {
        string GetMergedFilePath();
        string GetZipFilePath(string zipName);
        string GetWebAddressToFile(string pathToCombinedPptx);
    }
}