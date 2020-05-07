using DevBridgeAPI.Models.Post;
using DevBridgeAPI.Resources;
using DevBridgeAPI.UseCases.Integrations.EmailService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;

namespace DevBridgeAPI.UseCases.Integrations
{
    public class UserIntegrations : IUserIntegrations
    {
        private readonly string _htmlString =
            "<!DOCTYPE html>" +
            "<html>" +
            "  <head>" +
            "    <title>Invitation</title>" +
            "  </head>" +
            "  <body>" +
            "    <h3>Hi, {0}</h3>" +
            "	 <p>A fellow user has invited you to our DevBridge learning platform </br>" +
            "	   Please click on the following link to complete registration: {1}</p>" +
            "	 <p>We hope to see you soon!</p>" +
            "	 <p>Regards</p>" +
            "  </body>" +
            "</html>";

        public void CreateInvitation(User invitedUser)
        {
            var client = new EmailClient(sender: EmailCredentials.Email,
                                         senderPassword: EmailCredentials.Password,
                                         host: ConfigurationManager.AppSettings["appSettings--emailHost"]);
            var registrationTokenUrlEnc = WebUtility.UrlEncode(invitedUser.RegistrationToken);
            var emailUrlEnc = WebUtility.UrlEncode(invitedUser.Email);
            var invitationUrl = string.Format(provider: CultureInfo.GetCultureInfo("en-US"),
                                                format: Strings.UserInvitationUrl, registrationTokenUrlEnc, emailUrlEnc);

            var emailBody = string.Format(provider: CultureInfo.GetCultureInfo("en-US"),
                                            format: _htmlString, invitedUser.FirstName, invitationUrl);
            var subject = Strings.UserInvitationSubject;
            client.SendEmailInBackground(subject, emailBody, invitedUser.Email);
        }
    }
}