<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SP.SiteCreatorWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/Office.Controls.css" rel="stylesheet" />
    <script src="<%=Request.QueryString["SPHostUrl"] %>/_layouts/15/SP.RequestExecutor.js" type="text/javascript"></script>
    <script src="/Scripts/Office.Controls.js" type="text/javascript"></script>
    <script src="/Scripts/Office.Controls.PeoplePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function handlePeoplePickerChange(args) {
            if (args.selectedItems.length > 0) {
                document.getElementById("SiteOwner").value = args.selectedItems[0].email;
            }
        }
    </script>
</head>
<body  onload="Office.Controls.Runtime.initialize({});Office.Controls.Runtime.renderAll();">
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="SiteName" runat="server"></asp:TextBox>
    
    </div>
        <div style="display:none">
            <asp:TextBox ID="SiteOwner" runat="server" ClientIDMode="Static"></asp:TextBox>
        </div>
        
        <div id="PeoplePickerSimple" data-office-control="Office.Controls.PeoplePicker" data-office-options='{ "placeholder" : "Please choose an owner for the site collection..." , "onChange" : handlePeoplePickerChange    }'></div>

        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create Site" />
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
