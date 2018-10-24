using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xunit;
using File = Google.Apis.Drive.v3.Data.File;

namespace Musiction.API.Test
{
    public class DriveQuickstart
    {
        static string[] Scopes = { DriveService.Scope.Drive, SlidesService.Scope.Presentations };
        static string ApplicationName = "Drive API .NET Quickstart";

        [Fact]
        public void Main()
        {
            UserCredential credential;


            var folderId = "";
            var fileId = "";

            //string credPath = "token.json";
            //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //    credentialdriveSecrets,
            //    Scopes,
            //    "user",
            //    CancellationToken.None,
            //    new FileDataStore(credPath, true)).Result;
            //Console.WriteLine("Credential file saved to: " + credPath);

            using (var stream =
                new FileStream(@"D:\Downloads\credentials (5).json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var slidesService = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            var fileMetadata = new File()
            {
                Name = "Tytul1",
                Parents = new List<string>
                {
                    folderId
                }
            };


            var createRequest = slidesService.Presentations.Create(new Presentation()
            {
                Title = "Tytul1",
            });
            var pres = createRequest.Execute();
            fileId = pres.PresentationId;


            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            // Retrieve the existing parents to remove
            var getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            var file = getRequest.Execute();
            var previousParents = String.Join(",", file.Parents);
            // Move the file to the new folder
            var updateRequest = service.Files.Update(new File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            updateRequest.RemoveParents = previousParents;
            file = updateRequest.Execute();

            fileMetadata = new File()
            {
                Name = "photo.pptx",
                Parents = new List<string>
                {
                    folderId
                }
            };
            FilesResource.CreateRequest request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file1 = request.Execute();
            //= request.ResponseBody;

            // Define parameters of request.
            //FilesResource.ListRequest listRequest = service.Files.List();
            //listRequest.PageSize = 10;
            //listRequest.Fields = "nextPageToken, files(id, name)";

            //// List files.
            //IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
            //    .Files;
            //Console.WriteLine("Files:");
            //if (files != null && files.Count > 0)
            //{
            //    foreach (var file in files)
            //    {
            //        Console.WriteLine("{0} ({1})", file.Name, file.Id);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No files found.");
            //}
            //Console.Read();

        }

        [Fact]
        public void test3()
        {
            var clientSlidesSecrets = new ClientSecrets()
            {
            };

            UserCredential credential;

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSlidesSecrets,
                Scopes,
                "user",
                CancellationToken.None).Result;


            var slidesService = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var createRequest = slidesService.Presentations.Create(new Presentation()
            {
                Title = "Tytul23",
            });
            var pres = createRequest.Execute();
            var fileId = pres.PresentationId;

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var folderId = "";

            var getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            var file = getRequest.Execute();
            var previousParents = String.Join(",", file.Parents);
            // Move the file to the new folder
            var updateRequest = service.Files.Update(new File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            updateRequest.RemoveParents = previousParents;
            file = updateRequest.Execute();
        }


        [Fact]
        public void ytemp()
        {
            UserCredential credential;
            using (var stream =
                new FileStream(@"D:\Downloads\credentials (4).json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            var fileMetadata1 = new File()
            {
                Name = "Invoices",
                MimeType = "application/vnd.google-apps.folder"
            };
            var request1 = driveService.Files.Create(fileMetadata1);
            request1.Fields = "id";
            //var file1 = request1.Execute();
            //Console.WriteLine("Folder ID: " + file1.Id);

            var folderId = "";
            var fileMetadata = new File()
            {
                Name = "photo.jpg",
                Parents = new List<string>
                {
                    folderId
                }
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(@"D:\Downloads\293.jpg",
                System.IO.FileMode.Open))
            {
                request = driveService.Files.Create(
                    fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                request.Upload();
            }
            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);
        }
    }
}
