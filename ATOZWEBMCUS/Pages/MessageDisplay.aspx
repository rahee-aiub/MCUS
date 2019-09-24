<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="MessageDisplay.aspx.cs" Inherits="ATOZWEBMCUS.Pages.MessageDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
    </div>
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Height="458px" TextMode="MultiLine" Width="877px"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
    </div>
    </form>
</body>
</html>
