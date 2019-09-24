<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAutoTransactionList.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAutoTransactionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed ?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Print or Preview information ?');
        }

    </script>

     <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width:1250px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
            Width:1230px
     
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
                    <th colspan="3">Auto Transaction List
                    </th>
                </tr>
            </thead>

            <tr>

                <td>
                    <asp:Label ID="lblFuncType" runat="server" Text="Auto Transaction Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFuncType" runat="server" Height="25px" Width="248px" CssClass="cls text"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Auto Provision FDR</asp:ListItem>
                        <asp:ListItem Value="2">Auto Provision 6YR</asp:ListItem>
                        <asp:ListItem Value="3">Auto Anniversary FDR</asp:ListItem>
                        <asp:ListItem Value="4">Auto Anniversary 6YR</asp:ListItem>
                        <asp:ListItem Value="5">Auto Renewal FDR</asp:ListItem>
                        <asp:ListItem Value="6">Auto Renewal 6YR</asp:ListItem>
                        <asp:ListItem Value="7">Auto Renewal MSplus</asp:ListItem>
                        <asp:ListItem Value="8">Auto Benefit MSplus</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
           
            <tr>
                <td></td>
                <td>
                    
                       <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="White" Height="27px" Width="86px"
                           Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnPrint_Click" />&nbsp;
                      
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    
    <br />
    
    
    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblPdate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="txtToDaysDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeName" runat="server" Text="" Visible="false"></asp:Label>
    
    

</asp:Content>

