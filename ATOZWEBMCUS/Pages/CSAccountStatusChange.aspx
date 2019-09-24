<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAccountStatusChange.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountStatusChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save New Sanction Amount?');
        }

        function ValidationBeforeUpdate()
        {
            var txtCreditUNo = document.getElementById('<%=txtCreditUNo.ClientID%>').value;
            var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;
            
            var txtAccStatDate = document.getElementById('<%=txtAccStatDate.ClientID%>').value;
            
            
            
            if (txtCreditUNo == '' || txtCreditUNo.length == 0)
                alert('Please Input Credit Union No.');
            else
            if (txtMemNo == '' || txtMemNo.length == 0)
                alert('Please Input Depositor No.');
            else
            
            if (txtAccStatDate == '' || txtAccStatDate.length == 0)
                alert('Please Input Status Date');
            else
                return confirm('Are you sure you want to Update information?');
            return false;
        }


    </script>

     <%-- <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtAccStatDate.ClientID %>").dynDateTime({
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
    </script>--%>


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtAccStatDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtAccStatDate.ClientID %>").datepicker();

            });

        });

        
            </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

      <br />
    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Account Status Change
                    </th>
                </tr>
            </thead>

             <tr>
                <td>
                    <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="1" AutoPostBack="True" ToolTip="Enter Code" onkeypress="return functionx(event)" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblAccTitle" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                <asp:Button ID="BtnSearch" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                                    Font-Bold="True" CssClass="button green" OnClick="BtnSearch_Click" />

                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="2"></asp:TextBox>

                     <asp:Label ID="lblCuName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>
                    

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="3" onkeypress="return functionx(event)"></asp:TextBox>

                     <asp:Label ID="lblMemName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>
                    
                </td>
            </tr>
            

            <tr>
                    <td>
                        <asp:Label ID="lblCurrAccStatus" runat="server" Text="Current Status:" Font-Size="Large" 
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCurrStatus" runat="server" Width="303px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:Label>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" Text="Since :" Font-Size="Large" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblCurrStatDt" runat="server" Width="161px" Height="25px" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"></asp:Label>
                    </td>
                </tr>
            <tr>
                    <td>
                        <asp:Label ID="lblCurrAccRef" runat="server" Text="Current Reference:" Font-Size="Large" 
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCurrRef" runat="server" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:Label>

                        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblBal" runat="server" Text="Balance :" Font-Size="Large" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblBalance" runat="server" Width="161px" Height="25px" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"></asp:Label>
                        
                    </td>
                </tr>

            <tr>
                    <td>
                        <%--<asp:Label ID="Label1" runat="server" Text="Current Reference:" Font-Size="Large" 
                            ForeColor="Red"></asp:Label>--%>
                    </td>
                    <td>
                        <%--<asp:Label ID="Label2" runat="server" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:Label>--%>

                        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                        <asp:Label ID="lblBalLien" runat="server" Text="Total Lien Amt. :" Font-Size="Large" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblBalLienAmount" runat="server" Width="161px" Height="25px" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"></asp:Label>
                        
                    </td>
                </tr>
            </table>

            <table class="style1">
              <thead>
                <tr>
                    <th colspan="3">
                    </th>
                </tr>
            </thead>
             <tr>
                    <td>
                        <asp:Label ID="lblAccStatus" runat="server" Text="Change Status:" Font-Size="Large" 
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccStat" runat="server" Height="24px" Width="322px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Large" TabIndex="5" OnSelectedIndexChanged="ddlAccStat_SelectedIndexChanged"></asp:DropDownList>
                        &nbsp;&nbsp;
                    </td>
                </tr>

                 <tr>
                <td>
                    <asp:Label ID="lblLienAmt" runat="server" Text="Lien Amount :" Font-Size="Large" 
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtLienAmt" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="190px" Height="25px" Font-Size="Large" TabIndex="6"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAccStatdate" runat="server" Text="Status Date :" Font-Size="Large" 
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtAccStatDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="190px" Height="25px" TabIndex="7" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
               
            <tr>
                    <td>
                        <asp:Label ID="lblNote" runat="server" Text="Reference:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewNote" runat="server" CssClass="cls text" Width="556px" Height="25px" TabIndex="8" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
            
            <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Size="Large"
                            ForeColor="White" Font-Bold="True" Height="27px" Width="100px" CssClass="button green"
                            OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click"/>&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                        <br />
                    </td>
                </tr>

        </table>
        <asp:TextBox ID="txtHidden" runat="server"  Width="115px" Height="25px" Visible="false"
                                    Font-Size="Large"></asp:TextBox>
     <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccBalance" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="lblStatP" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblStatC" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblStatPDate" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="hdnCuNumber" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnID" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="CtrlAccType" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblcls" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="lblflag" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="lblModule" runat="server"  Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
        
    </div>

</asp:Content>

