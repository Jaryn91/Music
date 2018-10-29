using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Util.Store;
using Musiction.API.IBusinessLogic;
using System.Threading;

namespace Musiction.API.BusinessLogic
{
    public class GoogleSlides : IGoogleSlides
    {
        static string ApplicationName = "Google Slides API .NET Quickstart";

        private readonly DriveService _driveService;
        private readonly IGetValue _valueRetrieval;
        private readonly string _folder;
        private readonly string _presentationTemplate;

        public GoogleSlides(IGetValue valueRetrieval)
        {
            _valueRetrieval = valueRetrieval;
            _folder = _valueRetrieval.Get("GoogleApi:Folder");
            _presentationTemplate = _valueRetrieval.Get("GoogleApi:Template");

            var clientSecrets = new ClientSecrets()
            {
                ClientId = _valueRetrieval.Get("GoogleApi:ClientId"),
                ClientSecret = _valueRetrieval.Get("GoogleApi:ClientSecret")
            };

            string credPath = _valueRetrieval.Get("GoogleApi:Token");

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new[] { DriveService.Scope.Drive, SlidesService.Scope.Presentations },
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
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
