using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace DevBridgeAPI.UseCases.Integrations.EmailService
{
    public class EmailClient
    {
        private readonly string _senderPassword;
        public string Sender { get; }
        public string Host { get; }

        public EmailClient(string sender, string senderPassword, string host)
        {
            Sender = sender;
            Host = host;
            _senderPassword = senderPassword;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed in lambda")]
        public void SendEmailInBackground(string subject, string htmlString, string receiver)
        {
            SmtpClient client = new SmtpClient(Host)
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(Sender, _senderPassword)
            };
            var message = new MailMessage
            {
                From = new MailAddress(Sender),
                Subject = subject,
                IsBodyHtml = true,
                Body = htmlString
            };
            message.To.Add(new MailAddress(receiver));

            HostingEnvironment.QueueBackgroundWorkItem(x =>
            {
                try
                {
                    client.Send(message);
                } catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                } finally
                {
                    message.Dispose();
                    client.Dispose();
                }
            });
        }
    }
}