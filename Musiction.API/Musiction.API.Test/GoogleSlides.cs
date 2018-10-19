﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xunit;

namespace Musiction.API.Test
{

    public class GoogleSlides
    {
        static string[] Scopes = { SlidesService.Scope.Presentations };
        static string ApplicationName = "Google Slides API .NET Quickstart";

        [Fact]
        public void Test()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
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

            // Create Google Slides API service.
            var service = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var createRequest = service.Presentations.Create(new Presentation()
            {
                Title = "Abba Ojcze " + DateTime.Now
            });

            var pres = createRequest.Execute();
            // Define request parameters.
            String presentationId = "1EAYk18WDjIG-zp_0vLm3CsfQh_i8eXc67Jo2O9C6Vuc";
            PresentationsResource.GetRequest request = service.Presentations.Get(presentationId);

            // Prints the number of slides and elements in a sample presentation:
            // https://docs.google.com/presentation/d/1EAYk18WDjIG-zp_0vLm3CsfQh_i8eXc67Jo2O9C6Vuc/edit
            Presentation presentation = request.Execute();
            IList<Page> slides = presentation.Slides;
            Console.WriteLine("The presentation contains {0} slides:", slides.Count);
            for (var i = 0; i < slides.Count; i++)
            {
                var slide = slides[i];
                Console.WriteLine("- Slide #{0} contains {1} elements.", i + 1, slide.PageElements.Count);
            }

        }
    }
}
