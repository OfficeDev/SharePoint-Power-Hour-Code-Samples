using KeepOnTrack.Models;
using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepOnTrack
{
    public static class CalendarAPISample
    {
        const string ExchangeResourceId = "https://outlook.office365.com";
        const string ExchangeServiceRoot = "https://outlook.office365.com/ews/odata";

        public static async Task<IOrderedEnumerable<IEvent>> GetCalendarEvents()
        {
            var client = await EnsureClientCreated();

            // Obtain calendar event data
            var eventsResults = await (from i in client.Me.Events
                                      where i.End >= DateTimeOffset.UtcNow
                                      select i).Take(10).ExecuteAsync();

            var events = eventsResults.CurrentPage.OrderBy(e => e.Start);

            return events;
        }

        public static async Task<Event> CreateCalendarEvent(EventModel model)
        {
           
            var newEvent = new Event()
            {
                Subject = model.Subject,
                Body =
                    new ItemBody()
                    {
                        Content = model.Content + "<b>Brought to you by Office365 APIs!</b> - Enjoy this meeting",
                        ContentType = BodyType.HTML
                    },
                Location = new Location() {DisplayName = model.Location},
                Start = DateTime.Now.AddHours(2),
                End = DateTime.Now.AddHours(4)
            };

            var client = await EnsureClientCreated();
            await client.Me.Events.AddEventAsync(newEvent);
            return newEvent;
        }

        private static async Task<ExchangeClient> EnsureClientCreated()
        {
            Authenticator authenticator = new Authenticator();
            var authInfo = await authenticator.AuthenticateAsync(ExchangeResourceId);

            return new ExchangeClient(new Uri(ExchangeServiceRoot), authInfo.GetAccessToken);
        }
        public static void SignOut(Uri postLogoutRedirect)
        {
            new Authenticator().Logout(postLogoutRedirect);
        }
    }
}
