<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STIssUsedReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STIssUsedReport" Title="Issue Used Report" %>

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

        <script language="javascript" type="text/javascript">
            $(function () {
                $("#<%= txtfdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtfdate.ClientID %>").datepicker();

            });

        });

        $(function () {
            $("#<%= txttdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txttdate.ClientID %>").datepicker();

            });

        });


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
                        <asp:Label ID="lblRcvPurchaseReport" runat="server" Text="Issue Used Report"></asp:Label>
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
                        Font-Size="Large" Style="margin-left: 11px" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="chkGroup" runat="server" Font-Size="Large" ForeColor="Red" Text="   All" Checked="True" AutoPostBack="True" OnCheckedChanged="chkGroup_CheckedChanged" />
                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblGroup" runat="server" Text="Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:DropDownList ID="ddlGroup" runat="server" Height="25px" Width="465px" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="chkCategory" runat="server" Font-Size="Large" ForeColor="Red" Text="   All" Checked="True" AutoPostBack="True" OnCheckedChanged="chkCategory_CheckedChanged" />


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
                <td style="background-color: #fce7f9;">
                    <asp:CheckBox ID="chkTrnType" runat="server" Font-Size="Large" ForeColor="Red" Text="   All" Checked="True" AutoPostBack="True" OnCheckedChanged="chkTrnType_CheckedChanged" />
                </td>
                <td style="background-color: #fce7f9;  ">
                    <asp:Label ID="Label3" runat="server" Text="Trn Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td style="background-color: #fce7f9;  ">
                    <asp:DropDownList ID="ddlTrnType" runat="server" Font-Size="Large" Height="25px" Width="465px">

                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Cash</asp:ListItem>
                        <asp:ListItem Value="48">Bank</asp:ListItem>
                    </asp:DropDownList>


                </td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9;  ">
                    <asp:CheckBox ID="chkVchNo" runat="server" Font-Size="Large" ForeColor="Red" Text="   All" Checked="True" AutoPostBack="True" OnCheckedChanged="chkVchNo_CheckedChanged" />
                </td>
                <td style="background-color: #fce7f9;  ">
                    <asp:Label ID="Label4" runat="server" Text="Vch No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td style="background-color: #fce7f9;  ">
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large"></asp:TextBox>


                </td>
            </tr>
            <tr>
                <td style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblfdate" runat="server" Text="From date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large"></asp:TextBox>

                    &nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:Label ID="lbltdate" runat="server" Text="  To date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large"></asp:TextBox>

                    &nbsp; &nbsp; &nbsp; 

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
    <asp:Label ID="lblRecType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSubHead" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
