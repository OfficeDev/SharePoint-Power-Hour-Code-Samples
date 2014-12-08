using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;
using KeepOnTrack.Utils;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using Microsoft.Office365.OutlookServices;
using TasksWeb.Utils;

namespace KeepOnTrack.Models {
  public class EventRepository {
    public async Task<IOrderedEnumerable<EventModel>> GetCalendarEvents() {
      var client = await EnsureClientCreated();

      // Obtain calendar event data
      var eventsResults = await (from i in client.Me.Events
                                 where i.End >= DateTimeOffset.UtcNow
                                 select i).Take(10).ExecuteAsync();

      var events = eventsResults.CurrentPage.OrderBy(e => e.Start);

      var results = new List<EventModel>();

      foreach (var e in events) {
        results.Add(new EventModel {
          Subject = e.Subject,
          Content = e.Body.Content,
          Location = e.Location.DisplayName
        });
      }

      return results.OrderBy(e => e.Subject);
    }

    public async Task<Event> CreateCalendarEvent(EventModel model) {

      var newEvent = new Event() {
        Subject = model.Subject,
        Body =
            new ItemBody() {
              Content = model.Content + "<b>Brought to you by Office365 APIs!</b> - Enjoy this meeting",
              ContentType = BodyType.HTML
            },
        Location = new Location() { DisplayName = model.Location },
        Start = DateTime.Now.AddHours(2),
        End = DateTime.Now.AddHours(4)
      };

      var client = await EnsureClientCreated();
      await client.Me.Events.AddEventAsync(newEvent);
      return newEvent;
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