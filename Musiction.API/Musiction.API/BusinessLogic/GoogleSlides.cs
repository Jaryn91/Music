using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using System;
using System.Threading;

namespace Musiction.API.BusinessLogic
{
    public class GoogleSlides
    {
        static string ApplicationName = "Google Slides API .NET Quickstart";

        private readonly UserCredential _credential;
        private readonly SlidesService _slidesService;
        private readonly DriveService _driveService;
        private readonly string _folder;
        public GoogleSlides()
        {
            _folder = Startup.Configuration[Startup.Configuration["env"] + ":GoogleApi:Folder"];

            var clientSecrets = new ClientSecrets()
            {
                ClientId = Startup.Configuration[Startup.Configuration["env"] + ":GoogleApi:ClientId"],
                ClientSecret = Startup.Configuration[Startup.Configuration["env"] + ":GoogleApi:ClientSecret"]
            };

            _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new[] { DriveService.Scope.Drive, SlidesService.Scope.Presentations },
                "user",
                CancellationToken.None).Result;

            _slidesService = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = ApplicationName,
            });

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = ApplicationName,
            });


        }

        public string Create(string title)
        {
            var createRequest = _slidesService.Presentations.Create(new Presentation()
            {
                Title = title,
            });
            var pres = createRequest.Execute();
            var fileId = pres.PresentationId;
            MoveFileToFolder(fileId);
            return fileId;
        }

        public void Download(int id)
        {

        }

        private void MoveFileToFolder(string fileId)
        {
            var getRequest = _driveService.Files.Get(fileId);
            getRequest.Fields = "parents";
            var file = getRequest.Execute();
            var previousParents = String.Join(",", file.Parents);

            // Move the file to the new folder
            var updateRequest = _driveService.Files.Update(new File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = _folder;
            updateRequest.RemoveParents = previousParents;
            var response = updateRequest.Execute();
        }
    }
}
