using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
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
            var service = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var createRequest = service.Presentations.Create(new Presentation()
            {
                Title = title
            });
            var pres = createRequest.Execute();
            return pres.PresentationId;
        }

        public void Download(int id)
        {

        }
    }
}
