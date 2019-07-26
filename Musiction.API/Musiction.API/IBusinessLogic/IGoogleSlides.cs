using Musiction.API.Models;

namespace Musiction.API.IBusinessLogic
{
    public interface IGoogleSlides
    {
        string Create(string title);
        PresentationOnDrive AddPptxFile(string filePath);
        PresentationOnDrive AddZipFile(string filePath);
        void Remove(string presentationId);
        string DownloadPptx(Entities.Presentation presentation, string googleDriveFileId);
    }
}
