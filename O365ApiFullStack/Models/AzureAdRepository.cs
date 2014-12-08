using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using O365ApiFullStack.Utils;

namespace O365ApiFullStack.Models {
  public class AzureAdRepository {
    public async Task<IEnumerable<IUser>> GetUsers() {
      var client = await EnsureClientCreated();

      var userResults = await client.DirectoryObjects.OfType<User>().ExecuteAsync();

      List<IUser> allUsers = new List<IUser>();

      do {
        allUsers.AddRange(userResults.CurrentPage);
        userResults = await userResults.GetNextPageAsync();
      } while (userResults != null);

      return allUsers;
    }

    private async Task<ActiveDirectoryClient> EnsureClientCreated() {

      var userObjectId = ClaimsPrincipal.Current.FindFirst(SettingsHelper.ClaimTypeObjectIdentifier).Value;
      var signInUserId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
      var clientCredential = new ClientCredential(SettingsHelper.ClientId, SettingsHelper.ClientSecret);
      var userIdentifier = new UserIdentifier(userObjectId, UserIdentifierType.UniqueId);

      var graphClientResource = new Uri(new Uri(SettingsHelper.AzureAdGraphResourceId), SettingsHelper.AzureAdTenantId);

      AuthenticationContext authContext = new AuthenticationContext(graphClientResource.ToString(), new EFADALTokenCache(signInUserId));

      return new ActiveDirectoryClient(graphClientResource,
        async () => {
          var authResult =
            await
              authContext.AcquireTokenSilentAsync(graphClientResource.ToString(), clientCredential, userIdentifier);

          return authResult.AccessToken;
        });

    }
  }
}