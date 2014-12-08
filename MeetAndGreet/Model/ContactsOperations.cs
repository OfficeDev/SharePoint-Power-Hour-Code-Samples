using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office365.OutlookServices;
using MeetAndGreet.Helpers;

namespace MeetAndGreet.Model {
  public static class ContactsOperations {
    public static async Task<IEnumerable<IContact>> GetContacts() {
      var client = await AuthenticationHelper.EnsureOutlookClientCreatedAsync();

      // Obtain first page of contacts
      var contactsResults = await client.Me.Contacts.Select(c => c).ExecuteAsync();

      return contactsResults.CurrentPage;
    }

  }
}
