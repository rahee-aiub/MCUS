<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestAccountDataConversion.aspx.cs" Inherits="ATOZWEBMCUS.Pages.RestAccountDataConversion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed???');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>


    <style type="text/css">
        .style1 {
            height: 178px;
            width: 803px;
        }

        .red {
        }
        .green {}
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
                        <asp:Label ID="Label6" runat="server" Text="Abnormal Accounts:" Font-Size="Large"
                            ForeColor="Black"></asp:Label>
                        &nbsp;
                        
 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnGenerateAccNo" runat="server" Text="Generate Account No." Font-Size="Large" ForeColor="White" Height="31px" Width="214px"
                Font-Bold="True" CssClass="button green" BackColor="Red" OnClick="BtnGenerateAccNo_Click" />

                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblDBName" runat="server" Text="Source Database Name:" Font-Size="X-Large"
                            ForeColor="Black"></asp:Label>
                        <asp:Label ID="lblname" runat="server" Text="A2ZCCULB" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label><br />
                        <br />

                        <asp:Label ID="lblTbName" runat="server" Text=" Source Table Name:" Font-Size="X-Large"
                            ForeColor="Black"></asp:Label>
                        <asp:Label ID="lbltname" runat="server" Text="A2ZACC??" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>

                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Convert Database Name:" Font-Size="X-Large"
                        ForeColor="Black"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="A2ZCSMCUS" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label><br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" Text=" Convert Table Name:" Font-Size="X-Large"
                        ForeColor="Black"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text="A2ZACCOUNT" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblAccType" runat="server" Text="Source Table Name :" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccType" runat="server" Height="25px" Width="316px"
                            AutoPostBack="True" Font-Size="Large" CssClass="cls text"
                            OnSelectedIndexChanged="ddlAccTupe_SelectedIndexChanged">
                                                        
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                            <asp:ListItem Value="1">dbo.A2ZACC11</asp:ListItem>
                            <asp:ListItem Value="2">dbo.A2ZACC12</asp:ListItem>
                            <asp:ListItem Value="3">dbo.A2ZACC13</asp:ListItem>
                            <asp:ListItem Value="4">dbo.A2ZACC14</asp:ListItem>
                            <asp:ListItem Value="5">dbo.A2ZACC15</asp:ListItem>
                            <asp:ListItem Value="6">dbo.A2ZACC16</asp:ListItem>
                            <asp:ListItem Value="7">dbo.A2ZACC17</asp:ListItem>
                            <asp:ListItem Value="8">dbo.A2ZACC18</asp:ListItem>
                            <asp:ListItem Value="9">dbo.A2ZACC19</asp:ListItem>
                            <asp:ListItem Value="10">dbo.A2ZACC20</asp:ListItem>
                            <asp:ListItem Value="11">dbo.A2ZACC21</asp:ListItem>                            
                            <asp:ListItem Value="12">dbo.A2ZACC23</asp:ListItem>
                            <asp:ListItem Value="13">dbo.A2ZACC24</asp:ListItem>
                            <asp:ListItem Value="14">dbo.A2ZACC51</asp:ListItem>
                            <asp:ListItem Value="15">dbo.A2ZACC52</asp:ListItem>
                            <asp:ListItem Value="16">dbo.A2ZACC53</asp:ListItem>
                            <asp:ListItem Value="17">dbo.A2ZACC54</asp:ListItem>
                            <asp:ListItem Value="18">dbo.A2ZACC55</asp:ListItem>
                            <asp:ListItem Value="19">dbo.A2ZACC58</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Truncate table dbo.A2ZACCOUNT :" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTrunTable" runat="server" Height="25px" Width="316px"
                            AutoPostBack="True" Font-Size="Large" CssClass="cls text"
                            OnSelectedIndexChanged="ddlTrunTable_SelectedIndexChanged">

                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>


                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblProcess" runat="server" Text="Processing..." Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                     &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblProcessing" runat="server"  Font-Size="Large"
                            ForeColor="Red"></asp:Label>
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

    <asp:Label ID="CtrlOldCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNum" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAcType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="NoRoundingBy" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="RoundingByFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="MatureAmt" runat="server" Text="" Visible="false"></asp:Label>
        
    </form>
</body>
</html>
