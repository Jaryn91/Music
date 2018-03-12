using System.Diagnostics;

namespace Musiction.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "testmail@mail.com";
        private string _mailFrom = "noreplay@mail.com";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail to {_mailTo} was is send from LocalMailService");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Body {message}");
        }
    }
}
