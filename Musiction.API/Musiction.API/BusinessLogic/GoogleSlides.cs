using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Musiction.API.BusinessLogic
{
    public class GoogleSlides
    {
        static string[] Scopes = { SlidesService.Scope.Presentations };
        static string ApplicationName = "Google Slides API .NET Quickstart";

        private readonly UserCredential credential;
        public GoogleSlides()
        {
            var clientSecrets = new ClientSecrets()
            {
                ClientId = Startup.Configuration[Startup.Configuration["env"] + ":GoogleApi:ClientId"],
                ClientSecret = Startup.Configuration[Startup.Configuration["env"] + ":GoogleApi:ClientSecret"]
            };

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                Scopes,
                "user",
                CancellationToken.None).Result;
        }

        public string Create(string title)
        {
            var folderId = "1MILEOahox-bDuu4umu8KRLbvMIbpn4Br";
            var service = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            var fileMetadata = new File()
            {
                Name = title,
                Parents = new List<string>
                {
                    folderId
                }
            };


            var createRequest = service.Presentations.Create(new Presentation()
            {
                Title = title
            });
            var pres = createRequest.Execute();

            DriveService driveService = GetService_v3();
            var getRequest = driveService.Files.Get(pres.PresentationId);
            getRequest.Fields = "parents";
            var file = getRequest.Execute();
            var previousParents = String.Join(",", file.Parents);
            // Move the file to the new folder
            var updateRequest = driveService.Files.Update(new File(), pres.PresentationId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            updateRequest.RemoveParents = previousParents;
            file = updateRequest.Execute();

            return pres.PresentationId;
        }

        public void Download(int id)
        {

        }

        private void InsertPermissions()
        {
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });
            var fileId = "1sTWaJ_j7PkjzaBWtNc3IzovK5hQf21FbOw9yLeeLPNQ";
            var batch = new BatchRequest(driveService);
            BatchRequest.OnResponse<Permission> callback = delegate (
                Permission permission,
                RequestError error,
                int index,
                System.Net.Http.HttpResponseMessage message)
            {
                if (error != null)
                {
                    // Handle error
                    Console.WriteLine(error.Message);
                }
                else
                {
                    Console.WriteLine("Permission ID: " + permission.Id);
                }
            };
            Permission userPermission = new Permission()
            {

                Type = "user",
                Role = "writer",
                EmailAddress = "user@example.com"
            };
            var request = driveService.Permissions.Create(userPermission, fileId);
            request.Fields = "id";
            batch.Queue(request, callback);

            Permission domainPermission = new Permission()
            {
                Type = "domain",
                Role = "reader",
                Domain = "example.com"
            };
            request = driveService.Permissions.Create(domainPermission, fileId);
            request.Fields = "id";
            batch.Queue(request, callback);
            var task = batch.ExecuteAsync();
        }

        public void CreateFolder(string FolderName)
        {
            DriveService service = GetService_v3();

            File FileMetaData = new File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";

            FilesResource.CreateRequest request;

            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();
            //Console.WriteLine("Folder ID: " + file.Id);
        }

        private DriveService GetService_v3()
        {
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });
            return driveService;
        }
    }
}
