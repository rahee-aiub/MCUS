<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSTransferCreditUnion.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSTransferCreditUnion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 355px;
        }

        .auto-style2 {
            width: 557px;
        }
    </style>

    

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Process Transfer?');
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
                    <th colspan="3">Transfer Credit Union No.
                    </th>
                </tr>
            </thead>


            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblCuNo" runat="server" Text="Credit Union No.:" Font-Size="Large" ForeColor="Red"></asp:Label>


                    <asp:TextBox ID="txtCuNo" runat="server" CssClass="cls text" Width="153px" Height="25px"
                        Font-Size="Large" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="btnSearch_Click" />
                    <asp:Label ID="lblCuName" runat="server" Width="440px" Height="25px" Font-Size="Large"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblTransferMode" runat="server" Width="500px" Height="50px" Font-Size="X-Large" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>

                <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnProcess" Height="27px" Width="120px" runat="server" Text="Process" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnProcess_Click" TabIndex="2" />&nbsp;
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Cancel Page" CausesValidation="False"
                        CssClass="button blue" OnClick="BtnCancel_Click" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    <asp:Label ID="lblAffiCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAffiCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAffiCuTypeName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAssoCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAssoCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAssoCuTypeName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblReguCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblReguCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblReguCuTypeName" runat="server" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="lblNewCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewCuTypeName" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGLCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuOldCuNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAccFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewAccount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>



</asp:Content>

