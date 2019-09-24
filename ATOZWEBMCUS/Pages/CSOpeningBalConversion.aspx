<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSOpeningBalConversion.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSOpeningBalConversion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .text {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
        <br />
        <div align="center">
            <table style="border-style: double; border-color: inherit; border-width: medium; height: 225px;">
                <thead>
                    <tr style="border: double">

                        <th colspan="5">CCULB H/O Data Conversion Process
                        </th>
                    </tr>

                </thead>

                <tr>
                    <td>
                        <asp:Label ID="lblDBName" runat="server" Text="Source Database Name:" Font-Size="X-Large"
                            ForeColor="Black"></asp:Label>
                        <asp:Label ID="lblname" runat="server" Text="A2ZMCUST2014" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label><br />
                        <br />

                        <asp:Label ID="lblTbName" runat="server" Text=" Source Table Name:" Font-Size="X-Large"
                            ForeColor="Black"></asp:Label>
                        <asp:Label ID="lbltname" runat="server" Text="A2ZCSOPBALANCE??" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>

                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Convert Database Name:" Font-Size="X-Large"
                        ForeColor="Black"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="A2ZMCUST2014" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label><br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" Text=" Convert Table Name:" Font-Size="X-Large"
                        ForeColor="Black"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text="A2ZCSOPBALANCE" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblAccType" runat="server" Text="Source Table Name :" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccType" runat="server" Height="36px" Width="316px"
                            AutoPostBack="True" Font-Size="X-Large" CssClass="cls text"
                            OnSelectedIndexChanged="ddlAccTupe_SelectedIndexChanged">
                                                        
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                            <asp:ListItem Value="1">dbo.A2ZCSOPBALANCE11</asp:ListItem>
                            <asp:ListItem Value="2">dbo.A2ZCSOPBALANCE12</asp:ListItem>
                            <asp:ListItem Value="3">dbo.A2ZCSOPBALANCE13</asp:ListItem>
                            <asp:ListItem Value="4">dbo.A2ZCSOPBALANCE14</asp:ListItem>
                            <asp:ListItem Value="5">dbo.A2ZCSOPBALANCE15</asp:ListItem>
                            <asp:ListItem Value="6">dbo.A2ZCSOPBALANCE16</asp:ListItem>
                            <asp:ListItem Value="7">dbo.A2ZCSOPBALANCE17</asp:ListItem>
                            <asp:ListItem Value="8">dbo.A2ZCSOPBALANCE18</asp:ListItem>
                            <asp:ListItem Value="9">dbo.A2ZCSOPBALANCE19</asp:ListItem>
                            <asp:ListItem Value="10">dbo.A2ZCSOPBALANCE20</asp:ListItem>
                            <asp:ListItem Value="11">dbo.A2ZCSOPBALANCE21</asp:ListItem>
                            
                            <asp:ListItem Value="12">dbo.A2ZCSOPBALANCE23</asp:ListItem>
                            <asp:ListItem Value="13">dbo.A2ZCSOPBALANCE24</asp:ListItem>
                            <asp:ListItem Value="14">dbo.A2ZCSOPBALANCE51</asp:ListItem>
                            <asp:ListItem Value="15">dbo.A2ZCSOPBALANCE52</asp:ListItem>
                            <asp:ListItem Value="16">dbo.A2ZCSOPBALANCE53</asp:ListItem>
                            <asp:ListItem Value="17">dbo.A2ZCSOPBALANCE54</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Truncate table dbo.A2ZCSOPBALANCE :" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTrunTable" runat="server" Height="35px" Width="316px"
                            AutoPostBack="True" Font-Size="X-Large" CssClass="cls text"
                            OnSelectedIndexChanged="ddlTrunTable_SelectedIndexChanged">

                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>


                        </asp:DropDownList>
                    </td>
                </tr>

            </table>

        </div>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="BtnProceed" runat="server" Text="Proceed" Font-Size="Large" ForeColor="White" Height="31px" Width="86px"
                Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnProceed_Click" BackColor="#009933" />

            <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                Height="31px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                CssClass="button red" PostBackUrl="~/pages/A2ZERPModule.aspx" BackColor="Red" />



        </div>
    </form>
</body>
</html>
