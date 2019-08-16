<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TechRequest.aspx.cs" Inherits="TechTicketPOC.TechRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <% foreach (var field in EmailTemplate.Fields)
                    { %>
                        <tr>
                            <td><%= field.DisplayName %></td>
                            <td><asp:TextBox runat="server"></asp:TextBox></td>
                        </tr>
                <%} %>
            </table> 
        </div>
    </form>
</body>
</html>
