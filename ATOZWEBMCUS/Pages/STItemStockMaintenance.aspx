<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STItemStockMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STItemStockMaintenance" Title="Item Stock Maintenance" %>

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
            height: 300px;
            width: 500px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 483px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Item Code Maintenance
                    </th>
                </tr>
            </thead>
              <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Group Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGrpCode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="115px" Height="25px" Font-Size="Large" TabIndex="0" AutoPostBack="true"
                        OnTextChanged="txtGrpCode_TextChanged" MaxLength="2"></asp:TextBox>

                    <asp:DropDownList ID="ddlGroup" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="280px" AutoPostBack="True"
                        Font-Size="Large"
                        OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" TabIndex="6">
                    </asp:DropDownList>
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Category Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>

                    <asp:DropDownList ID="ddlItemType" runat="server" Height="30px"
                        Width="280px"  BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="true"
                        Font-Size="Large"
                         TabIndex="7" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                        
                    </asp:DropDownList>

                </td>
            </tr>
                     
            <tr>
                <td>
                    <asp:Label ID="lblItemCode" runat="server" Text="Item Code:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>

                    <asp:DropDownList ID="ddlItemNo" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="280px" AutoPostBack="True"
                        Font-Size="Large"
                        OnSelectedIndexChanged="ddlItemNo_SelectedIndexChanged" TabIndex="6">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblItemDesc" runat="server" Text="Item Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemDesc" runat="server" CssClass="cls text" Width="400px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" TabIndex="3"
                        Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblUnitOfMeasurement" runat="server" Text="Unit of Measurement:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>

                    <asp:DropDownList ID="ddlUnitMeasure" runat="server" CssClass="cls text" Width="120px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" MaxLength="50" TabIndex="8" AutoPostBack="True">
                    </asp:DropDownList>
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblReorderLvl" runat="server" Text="Re-order Level:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReorderLvl" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" TabIndex="4"
                        Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>

            </tr>


            <tr>

                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" TabIndex="5" Height="27px" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" Height="27px" />&nbsp;
                    <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green"
                        OnClick="BtnView_Click" Height="27px" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button blue" OnClick="btnCancel_Click" Height="27px" />
                    &nbsp;<asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" Height="27px" />
                    <br />

                </td>
            </tr>
        </table>
    </div>
    <br />
    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField HeaderText="Item Code" DataField="STKItemCode" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Item Name" DataField="STKItemName" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Left" />
            </Columns>

        </asp:GridView>
    </div>

    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnSubGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNewSTKNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnSubGroupCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnLastRec" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNewRec" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
