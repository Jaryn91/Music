using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Util.Store;
using Musiction.API.IBusinessLogic;
using Musiction.API.Models;
using Musiction.API.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using File = Google.Apis.Drive.v3.Data.File;

namespace Musiction.API.BusinessLogic
{
    public class GoogleSlides : IGoogleSlides
    {
        private readonly DriveService _driveService;
        private readonly string _folderForSongs;
        private readonly string _folderForPptx;
        private readonly string _folderForZip;
        private readonly string _presentationTemplate;
        private IFileAndFolderPathsCreator _fileAndFolderPathsCreator;

        public GoogleSlides(IGetValue valueRetrieval, IFileAndFolderPathsCreator fileAndFolderPathsCreator)
        {
            _fileAndFolderPathsCreator = fileAndFolderPathsCreator;
            _folderForSongs = valueRetrieval.Get(KeyConfig.FolderForSongs);
            _folderForPptx = valueRetrieval.Get(KeyConfig.FolderForPptx);
            _folderForZip = valueRetrieval.Get(KeyConfig.FolderForZip);
            _presentationTemplate = valueRetrieval.Get(KeyConfig.PresentationTemplate);

            var clientSecrets = new ClientSecrets()
            {
                ClientId = valueRetrieval.Get(KeyConfig.GoogleApiClient),
                ClientSecret = valueRetrieval.Get(KeyConfig.GoogleApiSecret)
            };

            var credPath = valueRetrieval.Get(KeyConfig.GoogleApiToken);

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new[] { DriveService.Scope.Drive, SlidesService.Scope.Presentations },
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
        }

        public PresentationOnDrive AddPptxFile(string filePath)
        {
            var fileMetadata = new File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string>
                {
                    _folderForPptx
                }
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = _driveService.Files.Create(
                    fileMetadata, stream, "application/vnd.openxmlformats-officedocument.presentationml.presentation");
                request.Fields = "id";
                request.Upload();
            }
            var file = request.ResponseBody;
            var presentationOnDrive = new PresentationOnDrive()
            { Name = Path.GetFileName(filePath), Extension = "Pptx", FileId = file.Id };
            return presentationOnDrive;
        }

        public PresentationOnDrive AddZipFile(string filePath)
        {
            var fileMetadata = new File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string>
                {
                    _folderForZip
                }
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(filePath,
                System.IO.FileMode.Open))
            {
                request = _driveService.Files.Create(
                    fileMetadata, stream, "application/zip");
                request.Fields = "id";
                request.Upload();
            }
            var file = request.ResponseBody;
            var presentationOnDrive = new PresentationOnDrive()
            { Name = Path.GetFileName(filePath), Extension = "Zip", FileId = file.Id };
            return presentationOnDrive;
        }

        public string Create(string title)
        {
            var file = new File { Name = title };
            var copyRequest = _driveService.Files.Copy(file, _presentationTemplate);
            var pres = copyRequest.Execute();

            var fileId = pres.Id;
            MoveFileToFolder(fileId);
            return fileId;
        }

        private void MoveFileToFolder(string fileId)
        {
            var getRequest = _driveService.Files.Get(fileId);
            getRequest.Fields = "parents";
            var file = getRequest.Execute();
            var previousParents = string.Join(",", file.Parents);


            var updateRequest = _driveService.Files.Update(new File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = _folderForSongs;
            updateRequest.RemoveParents = previousParents;
            var response = updateRequest.Execute();
        }

        public void Remove(string presentationId)
        {
            if (presentationId == "")
                return;
            if (_driveService.Files.Get(presentationId).FileId != null)
                _driveService.Files.Delete(presentationId).Execute();
        }

        public string DownloadPptx(Entities.Presentation presentation, string googleDriveFileId)
        {
            var presentationName = GetFileName(googleDriveFileId);
            var filePath = _fileAndFolderPathsCreator.GetPathToMergedFiles($"{Path.GetFileNameWithoutExtension(presentationName)}.pptx");
            using (var client = new WebClient())
            {
                client.DownloadFile(String.Format(MagicString.PathToFileInGoogleDrive, googleDriveFileId), filePath);
            }

            return filePath;
        }

        private string GetFileName(string googleDriveFileId)
        {
            return _driveService.Files.Get(googleDriveFileId).Execute().Name;
        }
    }
}
