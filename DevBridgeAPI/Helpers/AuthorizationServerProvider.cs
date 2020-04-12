using DevBridgeAPI.Models;
using DevBridgeAPI.Repository.Selector;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DevBridgeAPI.Helpers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var userSelector = new UsersSelector();

            var userData = (User) userSelector.SelectOneRow(context.UserName, context.Password);
            if (userData != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, userData.Role));
                identity.AddClaim(new Claim(ClaimTypes.Name, userData.Email));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                context.Rejected();
            }
        }
    }
}