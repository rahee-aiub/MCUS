<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STSupplierStatementReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STSupplierStatementReport" Title="Supplier Statement Report" %>

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
                        <asp:Label ID="lblRcvPurchaseReport" runat="server" Text="Supplier Statement Report"></asp:Label>
                    </th>
                </tr>
            </thead>

           


            <tr>
                <td style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="Label1" runat="server" Text="Supplier Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>

                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtSupplierCode" runat="server" Width="100px" Height="25px" Font-Size="Large" MaxLength="7"
                        CssClass="cls text" TabIndex="2" AutoPostBack="True" OnTextChanged="txtSupplierCode_TextChanged"></asp:TextBox>

                    &nbsp;&nbsp;
               
                    <asp:DropDownList ID="ddlSupplierName" runat="server" Height="30px"
                        Width="333px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged">
                    </asp:DropDownList>
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

    <asp:Label ID="lblAddressl1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAddressl2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAddressl3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPreAddressLine" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMobile" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
