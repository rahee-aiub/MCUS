<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayMsg.aspx.cs" Inherits="ATOZWEBMCUS.Pages.DisplayMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notification</title>
    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/A2ZERPStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 27px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <div id="DivMain" runat="server" align="center">
 <%--       <h1 style="color: #15317E">
            AtoZ Micro Credit Union System
        </h1>
        <h2 style="color: #15317E">
            For</h2>
        <h2 style="color: #C35817">
            <span>THE CO-OPERATIVE CREDIT UNION LEAGUE OF <st1:country-region
w:st="on"><st1:place w:st="on">BANGLADESH</st1:place></st1:country-region></span></h2>
   --%>     
        <br />
        <br />
        <table class="style1">
            <tr>
                <%--<marquee>--%>
                <td style="font-size: large" class="style1">
                    <asp:Label ID="lblOne" runat="server"></asp:Label>
                </td>
                <%--</marquee>--%>
            </tr>
            <tr>
                <td style="font-size: large" class="style1">
                    <asp:Label ID="lblTwo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-size: large" class="style1">
                    <asp:Label ID="lblThree" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-size: large" class="style1">
                    <asp:Label ID="lblPreviousPage" runat="server" Visible ="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Back" CssClass="button green size-120"
                        OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
   <%-- <div id="footer">
        <h1 style="color: #15317E">
            Developed By AtoZ Computer Services.Computer Services.</h1>
    </div>--%>
    </form>
</body>
</html>
