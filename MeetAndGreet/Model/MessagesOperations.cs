using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetAndGreet.Helpers;
using Microsoft.Office365.OutlookServices;

namespace MeetAndGreet.Model {
  public static class MessagesOperations {
    public static async Task<IEnumerable<IMessage>> GetMessages()
    {
      var client = await AuthenticationHelper.EnsureOutlookClientCreatedAsync();

      var messageResults = await (from i in client.Me.Messages
                                  orderby i.DateTimeSent descending
                                  select i).ExecuteAsync();

      return messageResults.CurrentPage;
    }
  }
}
