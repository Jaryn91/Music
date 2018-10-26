using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using Google.Apis.Util.Store;
using Musiction.API.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Musiction.API.BusinessLogic
{
    public class GoogleSlides : IGoogleSlides
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

            string credPath = Startup.Configuration[Startup.Configuration["env"] + ":GoogleApi:Token"];

            _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new[] { DriveService.Scope.Drive, SlidesService.Scope.Presentations },
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

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

        public void BulkUpdate()
        {
            string lineOfText;
            var textFilePath = @"piosenki.txt";
            var filestream = new System.IO.FileStream(textFilePath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            StreamWriter writetext = new StreamWriter("write.txt");
            var posypane = new List<string>();


            while ((lineOfText = file.ReadLine()) != null)
            {
                var info = lineOfText.Split('\t');
                var id = info[0];
                var nameOfSong = info[1];
                var path = info[2];

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = nameOfSong,
                    Parents = new List<string>
                    {
                        _folder
                    }
                };

                FilesResource.CreateMediaUpload request;
                try
                {
                    using (var stream = new System.IO.FileStream(path,
                    System.IO.FileMode.Open))
                    {
                        request = _driveService.Files.Create(
                            fileMetadata, stream, "application/vnd.openxmlformats-officedocument.presentationml.presentation");
                        request.Fields = "id";
                        request.Upload();
                    }
                    var file1 = request.ResponseBody;

                    var slide = _slidesService.Presentations.Get(file1.Id);

                    writetext.WriteLine($"INSERT INTO `Songs`(`Name`, `PresentationId`) VALUES ('{nameOfSong}','{file1.Id}')");
                }
                catch
               (Exception ex)
                { posypane.Add(path); }
            }
            writetext.Close();
        }

        public void GetFiles()
        {
            string pageToken = null;
            List<Temp> dictionary = new List<Temp>();

            do
            {
                var request = _driveService.Files.List();
                request.Spaces = "drive";
                request.Fields = "nextPageToken, files(id, name)";
                request.PageSize = 1000;
                request.PageToken = pageToken;
                var result = request.Execute();
                foreach (var file1 in result.Files)
                {
                    dictionary.Add(new Temp(file1.Id, file1.Name));
                }
                pageToken = result.NextPageToken;
            } while (pageToken != null);
            var ilosc = dictionary.Count;




            //--------------------
            string lineOfText;
            var textFilePath = @"piosenki.txt";
            var filestream = new System.IO.FileStream(textFilePath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            StreamWriter writetext = new StreamWriter("write.txt");
            var posypane = new List<string>();


            while ((lineOfText = file.ReadLine()) != null)
            {
                var info = lineOfText.Split('\t');
                var id = info[0];
                var nameOfSong = info[1];
                var path = info[2];
                var answer = dictionary.Where(d => d.title == nameOfSong).ToList();
                if (answer.Count > 1)
                {
                    writetext.WriteLine($"Duplikat: {nameOfSong}");
                }
                if (answer.Count == 0)
                {
                    writetext.WriteLine($"Nie ma: {nameOfSong}");
                }
                else
                    writetext.WriteLine($"INSERT INTO Songs (Name, PresentationId) VALUES ('{nameOfSong}','{answer.First().filedId}')");
            }

            writetext.Close();
        }


        public void MoveAllFilesToAnotherFolder()
        {
            string pageToken = null;
            List<Temp> dictionary = new List<Temp>();

            do
            {
                var request = _driveService.Files.List();
                request.Spaces = "drive";
                request.Fields = "nextPageToken, files(id, name)";
                request.PageSize = 1000;
                request.PageToken = pageToken;
                var result = request.Execute();
                foreach (var file1 in result.Files)
                {
                    dictionary.Add(new Temp(file1.Id, file1.Name));
                }
                pageToken = result.NextPageToken;
            } while (pageToken != null);
            var ilosc = dictionary.Count;




            //--------------------
            string lineOfText;
            var textFilePath = @"piosenki.txt";
            var filestream = new System.IO.FileStream(textFilePath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            StreamWriter writetext = new StreamWriter("write.txt");
            var posypane = new List<string>();


            while ((lineOfText = file.ReadLine()) != null)
            {
                var info = lineOfText.Split('\t');
                var id = info[0];
                var nameOfSong = "Copy of " + info[1];
                var path = info[2];
                var answer = dictionary.Where(d => d.title == nameOfSong).ToList();

                var getRequest = _driveService.Files.Get(answer.First().filedId);
                getRequest.Fields = "parents";
                var filee = getRequest.Execute();
                var previousParents = String.Join(",", filee.Parents);

                // Move the file to the new folder
                var updateRequest = _driveService.Files.Update(new Google.Apis.Drive.v3.Data.File(), answer.First().filedId);
                updateRequest.Fields = "id, parents";
                updateRequest.AddParents = "_folder7";
                updateRequest.RemoveParents = previousParents;
                var response = updateRequest.Execute();
            }

            writetext.Close();
        }

        public void ChangeFileName()
        {
            string pageToken = null;
            List<Temp> dictionary = new List<Temp>();

            do
            {
                var request = _driveService.Files.List();
                request.Spaces = "drive";
                request.Fields = "nextPageToken, files(id, name)";
                request.PageSize = 1000;
                request.PageToken = pageToken;
                var result = request.Execute();
                foreach (var file1 in result.Files)
                {
                    dictionary.Add(new Temp(file1.Id, file1.Name));
                }
                pageToken = result.NextPageToken;
            } while (pageToken != null);
            var ilosc = dictionary.Count;


            string lineOfText;
            var textFilePath = @"piosenki.txt";
            var filestream = new System.IO.FileStream(textFilePath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            StreamWriter writetext = new StreamWriter("write2.txt");
            var posypane = new List<string>();


            while ((lineOfText = file.ReadLine()) != null)
            {
                var info = lineOfText.Split('\t');
                var id = info[0];
                var nameOfSong = "Copy of " + info[1];
                var path = info[2];
                var answer = dictionary.Where(d => d.title == nameOfSong).ToList();

                var getRequest = _driveService.Files.Get(answer.First().filedId);
                getRequest.Fields = "parents";
                var filee = getRequest.Execute();
                //var previousParents = String.Join(",", filee.T);

                //// Move the file to the new folder
                var updateRequest = _driveService.Files.Update(new Google.Apis.Drive.v3.Data.File(), answer.First().filedId);
                //updateRequest.Fields = "id, parents";
                //updateRequest.AddParents = "_folder7";
                //updateRequest.RemoveParents = previousParents;
                //var response = updateRequest.Execute();
                Google.Apis.Drive.v3.Data.File file3 = new Google.Apis.Drive.v3.Data.File();
                file3.Name = info[1];
                // Rename the file.
                var request = _driveService.Files.Update(file3, answer.First().filedId);
                Google.Apis.Drive.v3.Data.File updatedFile = request.Execute();

                if (answer.Count > 1)
                {
                    writetext.WriteLine($"Duplikat: {nameOfSong}");
                }
                if (answer.Count == 0)
                {
                    writetext.WriteLine($"Nie ma: {nameOfSong}");
                }
                else
                    writetext.WriteLine($"INSERT INTO Songs (Name, PresentationId) VALUES ('{info[1]}','{answer.First().filedId}')");
            }



            writetext.Close();
        }

        private void MoveFileToFolder(string fileId)
        {
            var getRequest = _driveService.Files.Get(fileId);
            getRequest.Fields = "parents";
            var file = getRequest.Execute();
            var previousParents = String.Join(",", file.Parents);

            // Move the file to the new folder
            var updateRequest = _driveService.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
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

    public class Temp
    {
        public Temp(string filedId, string title)
        {
            this.filedId = filedId;
            this.title = title;

        }
        public string filedId { get; set; }
        public string title { get; set; }
    }
}
