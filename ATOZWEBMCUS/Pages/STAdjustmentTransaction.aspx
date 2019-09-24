<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STAdjustmentTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STAdjustmentTransaction" Title="Adjastment Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 250px;
            width: 1000px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 483px;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Stock Adjustment
                    </th>
                </tr>

            </thead>

              <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Voucher No.:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="120px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtVchNo_TextChanged" ></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" Height="30px"
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Sub Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" Height="30px" AutoPostBack="true"
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>


                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Item Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemCode" runat="server" Width="120px" Height="25px" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        CssClass="cls text" TabIndex="2" AutoPostBack="True" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Item:" Font-Size="Large" ForeColor="Red"></asp:Label></td>
                <td>
                       <asp:DropDownList ID="ddlItemName" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="316px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                        </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Item Bal. Qty:" Font-Size="Large" ForeColor="Red"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtBalanceQty" runat="server" Width="120px" Height="25px" Style="text-align: Left" Font-Size="Large" ReadOnly="true"
                            CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblBalUnit" runat="server" Font-Size="Large" ForeColor="#400040"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Trn Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnType" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="316px" Font-Size="Large">
                        
                        <asp:ListItem Value="0">Debit</asp:ListItem>
                        <asp:ListItem Value="1">Credit</asp:ListItem>
                        </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Quantity:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="cls text" Width="120px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                     &nbsp;<asp:Label ID="lblBalUnit2" runat="server" Font-Size="Large" ForeColor="#400040"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Trn. Note:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnNote" runat="server" CssClass="cls text" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
                </td>

            </tr>

            <tr>

                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="btnSubmit_Click" Height="27px" />&nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClick="btnCancel_Click" Height="27px" />
                    &nbsp;
                      &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" Height="27px" />
                    <br />

                </td>
            </tr>
        </table>
    </div>



    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label6" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSelectedId" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
