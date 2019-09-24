 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberDataConversion.aspx.cs" Inherits="ATOZWEBMCUS.Pages.MemberDataConversion" %>

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
        .red {}
    </style>


</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <div align="center">
        <table style="border: double">
            <thead>
                <tr style="border:double">
                    
                    <th colspan="5">
                        CCULB H/O Data Conversion Process
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblDBName" runat="server" Text="Source Database Name:" Font-Size="X-Large"
                        ForeColor="Black"></asp:Label>
                    <asp:Label ID="lblname" runat="server" Text="A2ZCCULB" Font-Size="X-Large"
                        ForeColor="Red"></asp:Label><br />
                    <br />

                    <asp:Label ID="lblTbName" runat="server" Text=" Source Table Name:" Font-Size="X-Large"
                        ForeColor="Black"></asp:Label>
                    <asp:Label ID="lbltname" runat="server" Text="A2ZMEMBER" Font-Size="X-Large"
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
                    <asp:Label ID="Label4" runat="server" Text="A2ZMEMBER" Font-Size="X-Large"
                        ForeColor="Red"></asp:Label>
                </td>
            </tr>
            

        </table>

    </div>
        <br />
        <br />
    <div align="center">
        <asp:Button ID="BtnProceed" runat="server" Text="Proceed" Font-Size="Large" ForeColor="White"  Height="31px" Width="86px"
                    Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnProceed_Click" BackColor="#009933" />

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="31px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" BackColor="Red" OnClick="BtnExit_Click" />
   
        
        
         </div>



    </form>
</body>
</html>
