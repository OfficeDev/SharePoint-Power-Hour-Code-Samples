using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O365ApiFullStack.Models;

namespace O365ApiFullStack
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
        public static void SignOut(Uri postLogoutRedirect)
        {
            new Authenticator().Logout(postLogoutRedirect);
        }

        public static async Task SendMessage(EmailModel model)
        {
            var client = await EnsureClientCreated();


            var message = new Message()
            {
                Subject = model.Subject,
                Body = new ItemBody() {Content = model.Body, ContentType = BodyType.HTML}
            };
            message.ToRecipients.Add(new Recipient(){ Address = model.Recipient});

            await client.Me.Messages.AddMessageAsync(message);
        }
    }
}
