using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(O365ApiFullStack.Startup))]

namespace O365ApiFullStack {
  public partial class Startup {

    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
    }

  }
}