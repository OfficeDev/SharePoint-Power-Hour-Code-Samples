using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetAndGreet.Helpers;
using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace MeetAndGreet.Model {
  public static class AzureAdOperations {
    public static async Task<IEnumerable<IUser>> GetUsers() {
      var client = await AuthenticationHelper.EnsureGraphClientCreatedAsync();

      var userResults = await client.DirectoryObjects.OfType<User>().ExecuteAsync();

      List<IUser> allUsers = new List<IUser>();

      do {
        allUsers.AddRange(userResults.CurrentPage);
        userResults = await userResults.GetNextPageAsync();
      } while (userResults != null);

      return allUsers;
    }
  }
}
