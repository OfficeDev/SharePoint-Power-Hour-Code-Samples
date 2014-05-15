using Microsoft.Office365.OAuth;
using Microsoft.Office365.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetAndGreet
{
    static class  SitesApiSample
    {
        //TODO: Replace with your SharePoint Site Name
        const string SharePointResourceId = "https://dotnetrocks.sharepoint.com";
        const string SharePointServiceRoot = "https://dotnetrocks.sharepoint.com/_api/";

        public static async Task<IEnumerable<IFileSystemItem>> GetDefaultDocumentFiles()
        {
            var client = await EnsureClientCreated();

            // Obtain files in default SharePoint folder
            var filesResults = await client.Files.ExecuteAsync();
            var files = filesResults.CurrentPage.OrderBy(e => e.Name);
            return files;
        }
    
        private static async Task<SharePointClient> EnsureClientCreated()
        {
            Authenticator authenticator = new Authenticator();
            var authInfo = await authenticator.AuthenticateAsync(SharePointResourceId, ServiceIdentifierKind.Resource);

            // Create the SharePoint client proxy:
            return new SharePointClient(new Uri(SharePointServiceRoot), authInfo.GetAccessToken);
        }
        public static async Task SignOut()
        {
            await new Authenticator().LogoutAsync();
        }
    }
}
