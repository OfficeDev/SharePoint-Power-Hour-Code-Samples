using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetAndGreet
{
    public static class CalendarAPISample
    {
        const string ExchangeResourceId = "https://outlook.office365.com";
        const string ExchangeServiceRoot = "https://outlook.office365.com/ews/odata";

        public static async Task<IEnumerable<IEvent>> GetCalendarEvents()
        {
            var client = await EnsureClientCreated();

            // Obtain calendar event data
            var eventsResults = await (from i in client.Me.Events
                                      where i.End >= DateTimeOffset.UtcNow
                                      select i).ExecuteAsync();

            return eventsResults.CurrentPage;
        }

        private static async Task<ExchangeClient> EnsureClientCreated()
        {
            Authenticator authenticator = new Authenticator();
            var authInfo = await authenticator.AuthenticateAsync(ExchangeResourceId);

            return new ExchangeClient(new Uri(ExchangeServiceRoot), authInfo.GetAccessToken);
        }
        public static async Task SignOut()
        {
            await new Authenticator().LogoutAsync();
        }
    }
}
