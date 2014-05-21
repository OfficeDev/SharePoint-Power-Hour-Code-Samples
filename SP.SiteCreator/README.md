## SP.SiteCreator##

SP.SiteCreator demonstrates how to use SharePoint's Site Provisioning API and how to use Office Widgets in order to render a PeoplePicker in ProviderHosted Apps using plain HTML5, CSS and JavaScript.

Depending on your development environment, you've to re-add the reference to **Microsoft.Online.SharePoint.Client.Tenant.dll** which can be found in `C:\Program Files\SharePoint Client Components\16.0\Assemblies`

For providing a consistent UI it's also consuming recent **Office Widgets** (available as Nuget Package) `Microsoft.Office.WebWidgets.Experimental`

### Creating the SiteCollection

The process of creating a new SiteCollection is part of the `ClickHandler` within `Default.aspx.cs`, review the method in order to see how the API should be used. For this sample, we've hardcoded the Template, but you can of course provide any other template existing in your SharePoint Farm.

### Office Widgets

PeoplePicker Widget is defined and configured within `Default.aspx` all required components (CSS,JS) are part of the nuget-package and will be installed to your web project automatically. 

**It's important that you enforce the AppWeb creation by adding a simple Empty Element to your App Project as we did during our session. After the Feature and Package nodes are created by VS you can of course delete the Empty Element**

As for any SharePoint Provider Hosted Apps, we've also included the ChromeControl in order to provide a consistent Look 'n Feel for SharePoint users.