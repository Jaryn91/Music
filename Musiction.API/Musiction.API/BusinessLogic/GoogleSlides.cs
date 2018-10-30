using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Util.Store;
using Musiction.API.IBusinessLogic;
using Musiction.API.Resources;
using System.Threading;

namespace Musiction.API.BusinessLogic
{
    public class GoogleSlides : IGoogleSlides
    {
        private readonly DriveService _driveService;
        private readonly string _folder;
        private readonly string _presentationTemplate;

        public GoogleSlides(IGetValue valueRetrieval)
        {
            _folder = valueRetrieval.Get(KeyConfig.GoogleFolder);
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
            updateRequest.AddParents = _folder;
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
    }
}
