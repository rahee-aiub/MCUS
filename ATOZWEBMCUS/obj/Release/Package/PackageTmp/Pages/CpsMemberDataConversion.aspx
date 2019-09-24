 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CpsMemberDataConversion.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CpsMemberDataConversion" %>

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
                        CCULB H/O Cps Data Conversion Process
                    </th>
                </tr>

            </thead>

           
            

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
