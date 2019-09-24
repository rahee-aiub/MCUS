<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSGroupSummaryStatement.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSGroupSummaryStatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <div align="center">
        
                    <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="6">
                                   Group Summary Statement Report
                                </th>
                            </tr>

                        </thead>
                   
                        
                        <tr>
                            <td>
                                <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" AutoPostBack="true" ToolTip="Enter Code" TabIndex="1" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                                <asp:CheckBox ID="chkOldSearch" runat="server" Font-Size="Medium" ForeColor="Red" Text="Old No. Search"/>
                               <asp:Label ID="lblCuName" runat="server" Text=""></asp:Label>
                                 <asp:Button ID="BtnHelp" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="BtnHelp_Click" />

                            </td>

                             <td class="auto-style4">

                    <asp:Label ID="lblAccStatus" runat="server" Text="Account Status "></asp:Label>
                   </td>
                    <td>
                     :

                    </td>

                    <td class="auto-style4"> 
                    <asp:DropDownList ID="ddlAccStatus" runat="server" Font-Size="Large" Height="25px" Width="120px">
                        <asp:ListItem Value="0" Selected="True"> All  </asp:ListItem>
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="99">Close</asp:ListItem>
                    </asp:DropDownList>
               </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1" >
                                <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True" TabIndex="2"
                                    OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblMemName" runat="server" Height="25px" Text=""></asp:Label>
                                
                            </td>
                         <td colspan ="3" class="auto-style1">

                         </td>
                        </tr>


                        </table>

        <table>
        <tr>
            <td></td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClick="BtnView_Click"/>&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                <br />
            </td>
        </tr>
    </table>
                    </div>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuStatus" runat="server" Text="" Visible="false"></asp:Label>
    <asp:TextBox ID="txtHidden" runat="server"  Visible="false" Font-Size="Large"></asp:TextBox>
    <asp:TextBox ID="CtrlFlag" runat="server"  Visible="false" Font-Size="Large"></asp:TextBox>
     <asp:TextBox ID="hdnCuNumber" runat="server"  Visible="false" Font-Size="Large"></asp:TextBox>
</asp:Content>

