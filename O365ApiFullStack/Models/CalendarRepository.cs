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
  public class CalendarRepository {
    public async Task<IOrderedEnumerable<IEvent>> GetCalendarEvents() {
      var client = await EnsureClientCreated();

      // Obtain calendar event data
      var eventsResults = await (from i in client.Me.Events
                                 where i.End >= DateTimeOffset.UtcNow
                                 select i).Take(10).ExecuteAsync();

      var events = eventsResults.CurrentPage.OrderBy(e => e.Start);

      return events;
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
      CapabilityDiscoveryResult dcr = await discovery.DiscoverCapabilityAsync("Calendar");

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