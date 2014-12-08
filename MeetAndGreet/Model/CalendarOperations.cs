using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office365.OutlookServices;
using MeetAndGreet.Helpers;

namespace MeetAndGreet.Model {
  public static class CalendarOperations {

    public static async Task<IEnumerable<IEvent>> GetCalendarEvents()
    {
      var client = await AuthenticationHelper.EnsureOutlookClientCreatedAsync();

      // Obtain calendar event data
      var eventsResults = await (from i in client.Me.Events
                                 where i.End >= DateTimeOffset.UtcNow
                                 select i).ExecuteAsync();

      return eventsResults.CurrentPage;
    }
  
  }
}
