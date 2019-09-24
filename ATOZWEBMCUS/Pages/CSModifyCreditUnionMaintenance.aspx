<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="CSModifyCreditUnionMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSModifyCreditUnionMaintenance"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate()
        {
            var txtCreUName = document.getElementById('<%=txtCreUName.ClientID%>').value;
           
            var txtGLCashCode = document.getElementById('<%=txtGLCashCode.ClientID%>').value;
            var txtCuOpenDate = document.getElementById('<%=txtCuOpenDate.ClientID%>').value;

            
            if (txtCreUName == '' || txtCreUName.length == 0)
                alert('Please Input Credit Union Name');
            else
                if (txtCuOpenDate == '' || txtCuOpenDate.length == 0)
                    alert('Please Input Open Date');
                 else
                        if (txtGLCashCode == '' || txtGLCashCode.length == 0)
                            alert('Please Input GL Cash Code');
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
            $("#<%=txtCuOpenDate.ClientID %>").dynDateTime({
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
            $("#<%= txtCuOpenDate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtCuOpenDate.ClientID %>").datepicker();

            });

         });
            </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Modify - Credit Union Maintenance
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblCUNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" TabIndex="1"  OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlCreditUNo" runat="server" Height="25px" Width="504px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="1"  OnSelectedIndexChanged="ddlCreditUNo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Width="160px" Height="25px"
                        Font-Size="Large" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCreUName" runat="server" Text="Credit Union Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreUName" runat="server" CssClass="cls text" Width="430px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ToolTip="Enter Name" TabIndex="2" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuOpenDate" runat="server" Text="Open Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuOpenDate" runat="server" CssClass="cls text" Width="199px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" img src="../Images/calender.png"
                        TabIndex="3" AutoPostBack="True" OnTextChanged="txtCuOpenDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="lblCuMemType" runat="server" Text="Depositor Mode:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCuMemberFlag" runat="server" Height="25px" Width="200px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" CssClass="cls text" TabIndex="2">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Single Member</asp:ListItem>
                        <asp:ListItem Value="2">Multiple Member</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="lblCuCertNo" runat="server" Text="Certificate No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuCertificateNo" runat="server" CssClass="cls text" Width="115px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL1" runat="server" Text="Address Line1:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuAddressL1" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL2" runat="server" Text="Address Line2:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuAddressL2" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL3" runat="server" Text="Address Line3:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuAddressL3" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuDivision" runat="server" Text="Division:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" TabIndex="8">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuDistrict" runat="server" Text="District:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" TabIndex="9">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuUpzila" runat="server" Text="Upazila:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlUpzila" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                         TabIndex="10" OnSelectedIndexChanged="ddlUpzila_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuThana" runat="server" Text="Thana:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlThana" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlThana_SelectedIndexChanged" TabIndex="11">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuTelNo" runat="server" Text="Telephone No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuTelNo" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="12"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuMobNo" runat="server" Text="Mobile No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuMobileNo" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="13"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuFax" runat="server" Text="Fax:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuFax" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuEmail" runat="server" Text="E-mail:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuEmail" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="15"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCashCode" runat="server" Text="GL Cash Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtGLCashCode" runat="server" CssClass="cls text" Width="136px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="15" AutoPostBack="True" OnTextChanged="txtGLCashCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlGLCashCode" runat="server" Height="25px" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" OnSelectedIndexChanged="ddlGLCashCode_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>

                    <asp:Button ID="BtnCreUnionUpdate" runat="server" Text="Update" Font-Bold="True"
                        Font-Size="Large" ForeColor="White" OnClientClick="return ValidationBeforeUpdate()"
                        ToolTip="Update Information" CssClass="button green" OnClick="BtnCreUnionUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnCreUniontExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnCreUniontExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>

    <asp:Label ID="lblNewCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewCuTypeName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="GetOpenDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>

    
</asp:Content>
