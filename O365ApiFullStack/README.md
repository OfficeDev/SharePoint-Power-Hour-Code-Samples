##Office 365 Client Libraries for Web Projects##

This sample demonstrates how to use **Office365 APIs** in combination with an ASP.NET MVC Web Application.

This sample is also using the Graph API to receive information from Azure AD at runtime. Your account requires Tenant Administrative permissions to execute this sample successfully.

In Order to get such a sample running, you've to call CalendarAPISample from within a Controller, ensure that your controller is configured to do async calls by changing the method signature to

    public Task<ActionResult> Index(){
       // ...
    }

For creating views based on Office365 Client Objects, you should create a strongly typed view. Either by using internal classes such as `Microsoft.Office365.Exchange.IEvent` or `IEnumerable<Microsoft.Office365.Exchange.IEvent>` for list views. 

Alternatively you can create your own Models and Convert Office 365 result objects to your DTO's and use them within your Web Application.

**This project uses NuGet Packages, you've to enable Nuget Package Downloading during Build to get it working**