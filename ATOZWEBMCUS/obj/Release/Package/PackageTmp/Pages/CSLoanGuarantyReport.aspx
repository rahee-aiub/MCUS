<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSLoanGuarantyReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSLoanGuarantyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed???');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

         <br />
    <br />
    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Loan Guaranty Information
                    </th>
                </tr>
            </thead>




               <tr>
                <td>
                    <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlCreditUNo" runat="server" Height="24px" Width="472px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlCreditUNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="ChkAllMemNo" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllMemNo_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                    &nbsp;<asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                        OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlMemNo" runat="server" Height="24px" Width="472px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlMemNo_SelectedIndexChanged">
                       <%-- <asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            
             <tr>
                <td>
                    <asp:CheckBox ID="ChkAllAccType" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllAccType_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                    &nbsp;<asp:Label ID="lblAccType" runat="server" Text="Loan Account Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                        OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlAccType" runat="server" Height="24px" Width="472px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:CheckBox ID="ChkAllAccNo" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllAccNo_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                    &nbsp;<asp:Label ID="lblAccNo" runat="server" Text="Loan Account No. :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                        OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlAccNo" runat="server" Height="24px" Width="472px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="150px" CssClass="button green" OnClick="BtnView_Click"  />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
     <asp:TextBox ID="txtHidden" runat="server"  Visible="false"></asp:TextBox>
     <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTypeCls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblChkCuType" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblChkCuNo" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblChkMemNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

