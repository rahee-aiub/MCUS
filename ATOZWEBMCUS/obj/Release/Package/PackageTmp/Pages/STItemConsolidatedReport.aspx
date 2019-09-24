<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STItemConsolidatedReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STItemConsolidatedReport" Title="Item Consolidated Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

        function ValidationBeforeDelete() {
            return confirm('Are you sure you want to Delete information?');
        }


    </script>

        

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 350px;
            width: 650px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 485px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />



    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">
                        <asp:Label ID="lblRcvPurchaseReport" runat="server" Text="Item Consolidated Report"></asp:Label>
                    </th>
                </tr>

            </thead>


            <tr>
                <td style="background-color: #fce7f9">


                    <asp:CheckBox ID="chkWarehouse" runat="server" Font-Size="Large" ForeColor="Red" Text="   All" Checked="True" AutoPostBack="True" OnCheckedChanged="chkWarehouse_CheckedChanged" />

                </td>

                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblcode" runat="server" Text="Warehouse :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td style="background-color: #fce7f9">

                    <asp:TextBox ID="txtWarehouse" runat="server" CssClass="cls text" Width="100px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" OnTextChanged="txtWarehouse_TextChanged"></asp:TextBox>

                    <asp:DropDownList ID="ddlWarehouse" runat="server" Height="25px" Width="350px"
                        Font-Size="Large" Style="margin-left: 11px" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblGroup" runat="server" Text="Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:DropDownList ID="ddlGroup" runat="server" Height="25px" Width="465px" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9">

                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td style="background-color: #fce7f9">
                    <asp:DropDownList ID="ddlCategory" runat="server" Height="25px" Width="465px" AutoPostBack="True"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
           
             <tr>
                <td style="background-color: #fce7f9">

                </td>
                <td style="background-color: #fce7f9">
                   
                    <asp:Label ID="lblPeriod" runat="server" Text="Month of :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    </td>
                 <td style="background-color: #fce7f9">
                    
                    <asp:DropDownList ID="ddlPeriodMM" runat="server" Height="25px" Width="200px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">January </asp:ListItem>
                        <asp:ListItem Value="2">February </asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
               
                    <asp:DropDownList ID="ddlPeriodYYYY" runat="server" Height="25px" Width="100px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        <asp:ListItem Value="2020">2020</asp:ListItem>
                    </asp:DropDownList>


                </td>

            </tr>
            

            

            <tr>
                <td colspan="2" style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9">&nbsp;
                     <asp:Button ID="BtnView" runat="server" Text="View" Font-Size="Large" ForeColor="White"
                         Font-Bold="True" CssClass="button blue" Height="27px" Width="100px" OnClick="BtnView_Click" />&nbsp;
                      &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>

    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCWarehouseflag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnToDaysDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblRecType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSubHead" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
