using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using Microsoft.Office365.OutlookServices;
using O365ApiFullStack.Utils;

namespace O365ApiFullStack.Models {
  public class MailRepository {

    public async Task<IEnumerable<IMessage>> GetMessages() {
      var client = await EnsureClientCreated();

      var messageResults = await (from i in client.Me.Messages
                                  orderby i.DateTimeSent descending
                                  select i).ExecuteAsync();

      return messageResults.CurrentPage;
    }
    public async Task SendMessage(EmailModel model) {
      var client = await EnsureClientCreated();


      var message = new Message() {
        Subject = model.Subject,
        Body = new ItemBody() { Content = model.Body, ContentType = BodyType.HTML }
      };
      message.ToRecipients.Add(new Recipient() {
        EmailAddress = new EmailAddress { Address = model.Recipient }
      });

      await client.Me.Messages.AddMessageAsync(message);
    }

    private async Task<OutlookServicesClient> EnsureClientCreated() {
      // fetch from stuff user claims
      var signInUserId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
      var userObjectId = ClaimsPrincipal.Current.FindFirst(SettingsHelper.ClaimTypeObjectIdentifier).Value;

      // discover contact endpoint
      var clientCredential = new ClientCredential(SettingsHelper.ClientId, SettingsHelper.ClientSecret);
      var userIdentifier = new UserIdentifier(userObjectId, UserIdentifierType.UniqueId);

      // create auth context
      AuthenticationContext authContext = new AuthenticationContext(SettingsHelper.AzureADAuthority, new EFADALTokenCache(signInUserId));

      // create O365 discovery client 
      DiscoveryClient discovery = new DiscoveryClient(new Uri(SettingsHelper.O365DiscoveryServiceEndpoint),
        async () => {
          var authResult = await authContext.AcquireTokenSilentAsync(SettingsHelper.O365DiscoveryResourceId, clientCredential, userIdentifier);

          return authResult.AccessToken;
        });

      // query discovery service for endpoint for 'calendar' endpoint
      CapabilityDiscoveryResult dcr = await discovery.DiscoverCapabilityAsync("Mail");

      // create an OutlookServicesclient
      return new OutlookServicesClient(dcr.ServiceEndpointUri,
        async () => {
          var authResult =
            await
              authContext.AcquireTokenSilentAsync(dcr.ServiceResourceId, clientCredential, userIdentifier);
          return authResult.AccessToken;
        });
    }
  }
}