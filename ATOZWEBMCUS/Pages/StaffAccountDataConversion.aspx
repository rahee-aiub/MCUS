<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffAccountDataConversion.aspx.cs" Inherits="ATOZWEBMCUS.Pages.StaffAccountDataConversion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />



    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed???');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

     <script type="text/javascript">
         $(document).ready(function () {
             $("#<%=txtOpBalDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
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
                            <asp:ListItem Value="1">dbo.A2ZACC55</asp:ListItem>
                            <asp:ListItem Value="2">dbo.A2ZACC58</asp:ListItem>
                            <asp:ListItem Value="3">dbo.A2ZACC56</asp:ListItem>
                            <asp:ListItem Value="4">dbo.A2ZACC57</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>

               
                <tr>
                <td>
                    <asp:Label ID="lblOpBalDate" runat="server" Text="Opening Balance Date.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOpBalDate" runat="server" CssClass="cls text" Width="153px" Height="25px"
                        Font-Size="Large" img src="../Images/calender.png"></asp:TextBox>
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
    <asp:Label ID="lblOpDate" runat="server" Text="" Visible="false"></asp:Label>
        
    </form>
</body>
</html>
