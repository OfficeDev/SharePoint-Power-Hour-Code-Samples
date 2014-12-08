using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetAndGreet.Helpers;
using Microsoft.Office365.SharePoint.FileServices;

namespace MeetAndGreet.Model {
  public static class FilesOperations {
    public static async Task<IEnumerable<IItem>> GetMyFiles() {
      var client = await AuthenticationHelper.EnsureSharePointClientCreatedAsync();

      // Obtain files in folder "Shared with Everyone"
      var filesResults = await client.Files.ExecuteAsync();
      var files = filesResults.CurrentPage.OrderBy(e => e.Id);

      return files;
    }

  }
}
