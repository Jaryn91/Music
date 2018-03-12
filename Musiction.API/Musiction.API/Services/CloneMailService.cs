using System.Diagnostics;

namespace Musiction.API.Services
{
    public class CloneMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail to {_mailTo} was is send for clone");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Body {message}");
        }
    }
}
