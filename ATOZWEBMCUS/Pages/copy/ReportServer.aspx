<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportServer.aspx.cs" Inherits="A2ZERPWEB.Pages.ReportServer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 70px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnExit" runat="server" Text="Exit" Width="67px" CssClass="button red size-80 fl"
                        OnClick="btnExit_Click" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        ReuseParameterValuesOnRefresh="True" DisplayGroupTree="False" />
    </form>
</body>
</html>
