#SharePoint Power Hour#

This repository provides samples from the **SharePoint Power Hour Session**.

## SP.SiteCreator

`SP.SiteCreator` and `SP.SiteCreatorWeb` are representing a Provider-Hosted SharePoint App. Which consumes the new Site Provisioning API offered by SharePoint's Client Components.

Depending on your development environment, you've to re-add the reference to **Microsoft.Online.SharePoint.Client.Tenant.dll** which can be found in `C:\Program Files\SharePoint Client Components\16.0\Assemblies`

For providing a consistent UI it's also consuming recent **Office Widgets** (available as Nuget Package) `Microsoft.Office.WebWidgets.Experimental`

## KeepTrack and O365APIFullStack

Both Applications are demonstrating how to use Office 365 APIs from within an WebApplication. 

**If you haven't installed Office 365 API Tools, you've to install them using TOOLS | EXTENSIONS AND UPDATES within Visual Studio**

All samples are created with Version 1.3.41104.1 of the Office 365 API Tools.

## MeetAndGreet

MeetAndGreet shows how to use Office365 API's within a Windows 8.1 Store App.

## Further Readme's

Review Project directories for dedicated README.md files. They provide more information on how to get started with each sample.
