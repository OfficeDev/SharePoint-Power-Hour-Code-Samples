Office 365 Client Libraries for Web Projects
============================================

This sample demonstrates how to use **Office365 APIs** in combination with an ASP.NET MVC Web Application.

Review the **Models/EventRepository.cs** class. It contains all the code for interacting with the Office 365 APIs.

In order to get such a sample running, you've to use the `EventRepository` class from within a controller, ensure that your controller is configured to do async calls by changing the method signature to:

    public Task<ActionResult> Index(){
       // ...
    }

For creating views based on Office365 Client Objects, you should create a strongly typed view. Either by using internal classes such as `Microsoft.Office365.Exchange.IEvent` or `IEnumerable<Microsoft.Office365.Exchange.IEvent>` for list views.

Alternatively you can create your own models and convert Office 365 result objects to your DTO's and use them within your Web Application. That is how this sample is done, using our own model in **Models/EventModel.cs**.

To Get this Sample Working
---
**This project uses NuGet Packages, you've to enable Nuget Package Downloading during Build to get it working**

Right-click the project and register the app using the **Add -> Connected Service** menu option. Grant the app permissions to login (found under **Users & Groups**) and to read & write to the user's calendar. This will register the app in your Azure AD tenant and add the necessary **ClientID** and **ClientSecret** values to the `web.config`.

In addition, open the `web.config` and update the setting **ida:AadTenantId** to contain the value of your Azure AD tenant. This is found on the Quick Start page for your app... it is a GUID that can be found under **Get Started -> Enable Users to Sign On -> Federation Metadata Document URL**.

