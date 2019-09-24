<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STTransaction" Title="Purchase Transaction" %>

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
        body {
            background-color: linen;
        }
    </style>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            /*width: 900px;*/
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 875px;*/
        }
        .auto-style1 {
            width: 672px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />


    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">Stock Purchase Transaction
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Warehouse:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:DropDownList ID="ddlWarehouse" runat="server" Enabled="false" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="250px" Font-Size="Large">
                    </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                    <td>
                    <asp:Label ID="Label14" runat="server" Text="Supplier No.:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSupplier" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="250px" Font-Size="Large">
                    </asp:DropDownList>

                </td>
            </tr>


            <tr>
               
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Group Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="250px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

           
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="Order No.:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                  <td>
                      
                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
               

            </tr>

            <tr>
                  <td>
                    <asp:Label ID="Label9" runat="server" Text="Voucher No.:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50" AutoPostBack="True" OnTextChanged="txtVchNo_TextChanged"></asp:TextBox>
                </td>
               
                   <td>
                    <asp:Label ID="Label7" runat="server" Text="Chalan No.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChalanNo" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>

                </td>
            
            </tr>
            

        </table>
    </div>


    <div align="center">
        <asp:Panel ID="pnlProperty" runat="server" Height="395px">
            <table class="style1">
                <tr>
                    <td>
                        <h3>
                            <asp:Label ID="Label8" runat="server" Text="Category" ForeColor="Red"></asp:Label></h3>
                        <asp:DropDownList ID="ddlCategory" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="120px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        </asp:DropDownList>

                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label10" runat="server" Text="Item Code" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtItemCode" runat="server" Width="100px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="2" AutoPostBack="True" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label11" runat="server" Text="Item Name" ForeColor="Red"></asp:Label></h3>
                        <asp:DropDownList ID="ddlItemName" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="333px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label12" runat="server" Text="Unit" ForeColor="Red"></asp:Label></h3>
                        <asp:DropDownList ID="ddlUnit" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="113px" Font-Size="Large" Enabled="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label13" runat="server" Text="Quantity" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtQuantity" runat="server" Width="87px" Height="25px" Font-Size="Large" onkeypress="return IsDecimalKey(event)"
                            CssClass="cls text" TabIndex="5" AutoPostBack="True" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label1" runat="server" Text="Unit Price" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtUnitPrice" runat="server" Width="108px" Height="25px" Font-Size="Large" onkeypress="return IsDecimalKey(event)"
                            CssClass="cls text" TabIndex="5" AutoPostBack="True" OnTextChanged="txtUnitPrice_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label2" runat="server" Text="Total Price" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtTotalPrice" runat="server" Width="113px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="5" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAddItem" runat="server" Text="Add" Width="80px" Font-Size="Large"
                            ForeColor="White" Height="27px" Font-Bold="True"
                            CssClass="button green" OnClick="BtnAddItem_Click" />
                    </td>
                </tr>
            </table>
          
            <br />
            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvItemDetails" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvItemDetails_RowDataBound" EnableModelValidation="True" OnRowDeleting="gvItemDetails_RowDeleting">
                    <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemGroupNo" HeaderText="Group">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemCategoryNo" HeaderText="Category">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                            <HeaderStyle Width="325px" />
                            <ItemStyle HorizontalAlign="Center" Width="325px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemUnit" HeaderText="Unit">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemQty" HeaderText="Quantity">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemUnitPrice" DataFormatString="{0:0,0.00}" HeaderText="Unit Price">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Right" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemTotalPrice" DataFormatString="{0:0,0.00}" HeaderText="Total Price">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Right" Width="125px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                            <ControlStyle Font-Bold="True" ForeColor="#FF3300" />
                        </asp:CommandField>
                    </Columns>


                </asp:GridView>
            </div>


        </asp:Panel>
    </div>


    <div align="center">

        <table class="style1">
          
            <tr>
                <td>
                     <asp:Label ID="Label5" runat="server" Text="Trn Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                     <asp:DropDownList ID="ddlTrnType" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="250px" Font-Size="Large">
                         <asp:ListItem Value="0">-Select-</asp:ListItem>
                         <asp:ListItem Value="1">Cash</asp:ListItem>
                          <asp:ListItem Value="48">Bank</asp:ListItem>
                    </asp:DropDownList>
                </td>
                  <td>
                    <asp:Label ID="Label4" runat="server" Text="Total Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Trn Note:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtTrnNote" runat="server" CssClass="cls text" Width="600px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>

               
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Update" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" Height="27px" />&nbsp;
                    &nbsp;
                   <asp:Button ID="BtnView" runat="server" Text="View" Visible="false" Font-Bold="True" Font-Size="Large"
                       ForeColor="White" ToolTip="View Information" CssClass="button green"
                       OnClick="BtnView_Click" Height="27px" />&nbsp;
                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                         Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                         CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>


    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcessDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcessDateTime" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblTotalAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
