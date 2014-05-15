using Microsoft.Office365.OAuth;
using Microsoft.Office365.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetAndGreet
{
    static class  MyFilesApiSample
    {
        const string MyFilesCapability = "MyFiles";

        public static async Task<IEnumerable<IFileSystemItem>> GetMyFiles()
        {
            var client = await EnsureClientCreated();

            // Obtain files in folder "Shared with Everyone"
            var filesResults = await client.Files.ExecuteAsync();
            var files = filesResults.CurrentPage.OrderBy(e => e.Id);

            return files;
        }
    
        private static async Task<SharePointClient> EnsureClientCreated()
        {
            Authenticator authenticator = new Authenticator();
            var authInfo = await authenticator.AuthenticateAsync(MyFilesCapability, ServiceIdentifierKind.Capability);

            // Create the MyFiles client proxy:
            return new SharePointClient(authInfo.ServiceUri, authInfo.GetAccessToken);
        }
        public static async Task SignOut()
        {
            await new Authenticator().LogoutAsync();
        }
    }
}
