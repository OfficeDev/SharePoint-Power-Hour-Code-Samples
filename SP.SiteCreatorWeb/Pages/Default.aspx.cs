using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace SP.SiteCreatorWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Uri redirectUrl;
            switch (SharePointContextProvider.CheckRedirectionStatus(Context, out redirectUrl))
            {
                case RedirectionStatus.Ok:
                    return;
                case RedirectionStatus.ShouldRedirect:
                    Response.Redirect(redirectUrl.AbsoluteUri, endResponse: true);
                    break;
                case RedirectionStatus.CanNotRedirect:
                    Response.Write("An error occurred while processing your request.");
                    Response.End();
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // The following code gets the client context and Title property by using TokenHelper.
            // To access other properties, the app may need to request permissions on the host web.
            var spContext = SharePointContextProvider.Current.GetSharePointContext(Context);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                clientContext.Load(clientContext.Web, web => web.Title);
                clientContext.ExecuteQuery();
                Response.Write(clientContext.Web.Title);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Uri tenantAdministrationUrl = new Uri("https://dotnetrocks-admin.sharepoint.com/");

            string accessToken = TokenHelper.GetAppOnlyAccessToken(
                TokenHelper.SharePointPrincipal,
                tenantAdministrationUrl.Authority,
                TokenHelper.GetRealmFromTargetUrl(tenantAdministrationUrl)).AccessToken;

            var newSite = new SiteCreationProperties()
            {
                Url = "https://dotnetrocks.sharepoint.com/sites/" + SiteName.Text,
                Owner = SiteOwner.Text,
                Template = "STS#0", // Team Site
                Title = "App Provisioned Site - " + SiteName.Text,
                StorageMaximumLevel = 1000,
                StorageWarningLevel = 500,
                TimeZoneId = 7,
                UserCodeMaximumLevel = 7,
                UserCodeWarningLevel = 1
            };

            using (var clientContext = TokenHelper.GetClientContextWithAccessToken(tenantAdministrationUrl.ToString(), accessToken))
            {
                var tenant = new Tenant(clientContext);
                var spoOperation = tenant.CreateSite(newSite);

                clientContext.Load(spoOperation);
                clientContext.ExecuteQuery();
            }
        }
    }
}