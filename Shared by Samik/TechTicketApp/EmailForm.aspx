<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailForm.aspx.cs" Inherits="TechTicketApp.EmailForm" %>

<%@ Register Src="~/UserControl/EmailTemplate.ascx" TagPrefix="uc1" TagName="EmailTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <uc1:EmailTemplate runat="server" ID="EmailTemplate" />
        </div>
    </form>
</body>
</html>
