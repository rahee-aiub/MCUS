<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STNonSaleableToSaleable.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STNonSaleableToSaleable" Title="Transfer Item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Non Saleable Item (Item From)
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                     <asp:Label ID="Label4" runat="server" Text="Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:DropDownList ID="ddlGroup" runat="server" Height="30px" 
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Item Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="150px" Height="25px" Font-Size="Large" onkeypress="return IsDecimalKey(event)"
                        MaxLength="7" AutoPostBack="True" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>


                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblItem" runat="server" Text="Item :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNonSaleableItem" runat="server" Height="30px" 
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlNonSaleableItem_SelectedIndexChanged">
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBalQty" runat="server" Text="Balance Qty:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNonSBalQty" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)" ReadOnly="True"></asp:TextBox>
                </td>

            </tr>

             <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Transfer Qty:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTransferQty" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtTransferQty_TextChanged"></asp:TextBox>
                </td>

            </tr>


        </table>

        <br />
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Saleable Item (Item To)
                    </th>
                </tr>

            </thead>
              <tr>
                <td>
                     <asp:Label ID="Label5" runat="server" Text="Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:DropDownList ID="ddlGroup2" runat="server" Height="30px" 
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup2_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Item Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSItemCode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="150px" Height="25px" Font-Size="Large" onkeypress="return IsDecimalKey(event)"
                        MaxLength="7" AutoPostBack="True" OnTextChanged="txtSItemCode_TextChanged"></asp:TextBox>


                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Item :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSaleableItem" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlSaleableItem_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Balance Qty:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSItemBal" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)" ReadOnly="True"></asp:TextBox>
                </td>

            </tr>
                <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Voucher No.:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtVchNo_TextChanged" ></asp:TextBox>
                </td>

            </tr>


            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="btnTransfer_Click" Height="27px" />&nbsp;
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClick="btnCancel_Click" Height="27px" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" Height="27px" />
                    <br />

                </td>
            </tr>
        </table>
    </div>
    <br />

    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label6" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    
    
</asp:Content>
