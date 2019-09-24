<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissAccountData.aspx.cs" Inherits="ATOZWEBMCUS.Pages.MissAccountData" %>

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
    <asp:Label ID="lblAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>
    </form>
</body>
</html>
