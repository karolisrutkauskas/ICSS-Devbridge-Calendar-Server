using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace DevBridgeAPI.UseCases.Integrations.EmailService
{
    public class EmailClient : IDisposable
    {
        private readonly SmtpClient _client;
        public string Sender { get; }
        public string Host { get; }

        public EmailClient(string sender, string senderPassword, string host)
        {
            Sender = sender;
            Host = host;
            _client = new SmtpClient(host)
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(sender, senderPassword)
            };
        }
        public void SendEmailInBackground(string subject, string htmlString, string receiver)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(Sender);
                message.To.Add(new MailAddress(receiver));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlString;

                _client.Send(message);
                //HostingEnvironment.QueueBackgroundWorkItem(x => _client.Send(message));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool cleanManagedResources)
        {
            _client.Dispose();
        }
    }
}