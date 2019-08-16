<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TechRequestExt.aspx.cs" Inherits="TechTicketPOC.TechRequestExt" %>
<%@ Register TagName="Email" TagPrefix="tkt" src="~/UserControls/EmailTicket.ascx" %>

<ext:XScript runat="server" ID="XScript">
</ext:XScript>

<tkt:Email ID="EmailTicket" runat="Server" ></tkt:Email>
