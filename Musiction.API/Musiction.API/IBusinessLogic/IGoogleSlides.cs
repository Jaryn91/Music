namespace Musiction.API.IBusinessLogic
{
    public interface IGoogleSlides
    {
        string Create(string title);
        string AddPptxFile(string filePath);
        string AddZipFile(string filePath);
        void Remove(string presentationId);
        string DownloadPptx(string googleDriveFileId);
    }
}
