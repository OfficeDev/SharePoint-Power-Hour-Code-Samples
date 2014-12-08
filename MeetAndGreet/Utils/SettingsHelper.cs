using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetAndGreet.Utils {
  public class SettingsHelper {

    public static string ClientId {
      get { return App.Current.Resources["ida:ClientID"].ToString(); }
    }

    public static string AzureAdTenantId {
      get { return App.Current.Resources["ida:AadTenantId"].ToString(); }
    }

    public static string O365DiscoveryServiceEndpoint {
      get { return "https://api.office.com/discovery/v1.0/me/"; }
    }

    public static string O365DiscoveryResourceId {
      get { return "https://api.office.com/discovery/"; }
    }

    public static string AzureAdGraphResourceId {
      get { return "https://graph.windows.net"; }
    }

    public static string AzureADAuthority {
      get { return string.Format("https://login.windows.net/{0}/", AzureAdTenantId); }
    }

    public static string ClaimTypeObjectIdentifier {
      get { return "http://schemas.microsoft.com/identity/claims/objectidentifier"; }
    }
  }
}