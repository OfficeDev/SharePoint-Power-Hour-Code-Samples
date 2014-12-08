using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using KeepOnTrack.Data;

namespace KeepOnTrack {
  public class MvcApplication : System.Web.HttpApplication {
    protected void Application_Start() {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      // set the database initializer
      Database.SetInitializer<KeepOnTrackContext>(new KeepOnTrackInitializer());

      // configure the antiforgery token to use a claim we know exists
      AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
    }
  }
}
