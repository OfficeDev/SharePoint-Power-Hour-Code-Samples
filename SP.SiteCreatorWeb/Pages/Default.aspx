<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SP.SiteCreatorWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SharePoint SiteCreator</title>
    <link rel="stylesheet" href="/Content/site.css" />
    <!-- People Picker (Office Widget references) -->
    <link href="/Scripts/Office.Controls.css" rel="stylesheet" />
    <script src="<%=Request.QueryString["SPHostUrl"] %>/_layouts/15/SP.RequestExecutor.js" type="text/javascript"></script>
    <script src="/Scripts/Office.Controls.js" type="text/javascript"></script>
    <script src="/Scripts/Office.Controls.PeoplePicker.js" type="text/javascript"></script>
    <!-- END People Picker (Office Widget references) -->

    <!-- Chrome Control references -->
    <script src="//ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js" type="text/javascript"></script>
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/ChromeLoader.js"></script>
    <!-- END Chrome Control references -->

    <!-- Handle People Picker Changed -->
    <script type="text/javascript">
        function handlePeoplePickerChange(args) {
            if (args.selectedItems.length > 0) {
                document.getElementById("SiteOwner").value = args.selectedItems[0].email;
            }
        }
    </script>

    <!-- END Handle People Picker Changed -->
</head>
<body style="display: none" onload="Office.Controls.Runtime.initialize({});Office.Controls.Runtime.renderAll();">

    <!-- Chrome control placeholder -->
    <div id="chrome_ctrl_placeholder"></div>

    <form id="form1" class="appForm" runat="server">
        <div class="ms-textLarge">This App uses new SharePoint Site Provisioning API. By providing a Name and a SiteOwner a new SiteCollection will be created on your tenant.<br />
            <br />
            In order to get this sample working review the CodeBehind from Default.aspx and change the tenant urls to match your SharePoint-Online Tenant.</div>
        <div class="creator">
            <div>
                <div class="ms-accentText">Site Name </div>
                <asp:TextBox ID="SiteName" CssClass="siteName" runat="server"></asp:TextBox>

            </div>
            <div style="display: none">
                <asp:TextBox ID="SiteOwner" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="ms-accentText">Site Owner </div>
            <div id="PeoplePickerSimple" data-office-control="Office.Controls.PeoplePicker" data-office-options='{ "placeholder" : "Please choose an owner for the site collection..." , "onChange" : handlePeoplePickerChange    }'></div>

            <br />
            <div class="pull-right">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create SiteCollection in Tenant" />
            </div>
        </div>
    </form>
    <p>
        &nbsp;
    </p>
</body>
</html>
