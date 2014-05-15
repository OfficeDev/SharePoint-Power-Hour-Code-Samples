using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetAndGreet
{
    public static class MailApiSample
    {
        const string ExchangeResourceId = "https://outlook.office365.com";
        const string ExchangeServiceRoot = "https://outlook.office365.com/ews/odata";

        public static async Task<IEnumerable<IMessage>> GetMessages()
        {
            var client = await EnsureClientCreated();

            var messageResults = await (from i in client.Me.Inbox.Messages
                                     orderby i.DateTimeSent descending
                                     select i).ExecuteAsync();

            return messageResults.CurrentPage;
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
