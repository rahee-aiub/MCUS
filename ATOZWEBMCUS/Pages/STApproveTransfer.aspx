<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STApproveTransfer.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STApproveTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function ApproveValidation() {
            return confirm('Do you want to Approve Data?');
        }
        function SelectValidation() {
            return confirm('Do you want to Select Data?');
        }
        function RejectValidation() {
            return confirm('Do you want to Reject Data?');
        }
    </script>

    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div id="DivMainHeader" runat="server" align="center">
        <table class="style1">
            <tr>
                <th colspan="4">
                    <p align="center" style="color: blue">
                        Approve Transfer Transaction
                    </p>
                </th>
            </tr>
        </table>
    </div>

    <div id="DivButton" runat="server" align="center">
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="DivGridViewCancle" runat="server" align="center" style="height: 276px; overflow: auto; width: 100%;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        <p align="center" style="color: blue">
                            Approve - Spooler
                        </p>
                        <asp:GridView ID="gvTransferDetails" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="757px">
                            <Columns>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnSelect" runat="server" Text="Select" Height="27px" Width="68px" OnClick="BtnSelect_Click" CssClass="button green" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnRejectSelect" runat="server" Text="Reject" Width="68px" OnClick="BtnRejectSelect_Click" CssClass="button red" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <%--<asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnPrint" runat="server" Text="Print" Width="60px" CssClass="button black size-100" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                                <asp:TemplateField HeaderText="Voucher No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVchNo" runat="server" Text='<%# Eval("VchNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chalan No." Visible="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemNo" runat="server" Text='<%# Eval("ChalanNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rcv Warehouse No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWareHouseNo" runat="server" Text='<%# Eval("RcvWarehouseNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rcv Warehouse Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWarehouseName" runat="server" Text='<%# Eval("RcvWarehouseName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue Warehouse No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueWareHouseNo" runat="server" Text='<%# Eval("IssWarehouseNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue Warehouse Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueWarehouseName" runat="server" Text='<%# Eval("IssWarehouseName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />

                            </Columns>
                        </asp:GridView>
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="Div2" runat="server" align="center" style="overflow: auto; width: 100%;">
        <asp:GridView ID="gvItemDetails" runat="server" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtId" Visible="false" runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("Id")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Group" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGroupNo" runat="server" EnableViewState="true" Width="80px" Height="20" Enabled="false" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ItemGroupNo")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Category" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCategory" runat="server" EnableViewState="true" Enabled="false" Width="80px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ItemCategoryNo")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemCode" runat="server" EnableViewState="true" Enabled="false" Width="80px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ItemCode")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="250px" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemName" runat="server" EnableViewState="true" Enabled="false" Width="250px" Height="20" Style="text-align: left" ForeColor="Blue" Text='<%#Bind("ItemName")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Purchase Qty" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPurchaseQty" runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ItemPurchaseQty")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUnitDesc" runat="server" EnableViewState="true" Enabled="false" Width="80px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ItemUnitDesc")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUnitPrice" runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: right" ForeColor="Blue" Text='<%#Bind("ItemUnitPrice")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Price" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTotalPrice" runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: right" ForeColor="Blue" Text='<%#Bind("ItemTotalPrice")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Mising Qty" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtMissingQty" runat="server" Font-Bold="true" Enabled="true" EnableViewState="true" Width="100px" Height="20" Style="text-align: right" ForeColor="Blue" onkeypress="return IsNumberKey(event)" onFocus="javascript:this.select();" Text='<%#Bind("TrnMissQtyCr")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <br />
    

    <div id="divApprove" runat="server" align="center">
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="Red" Text="Total Amount :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="cls text" Style="text-align: right" Font-Size="Large" Height="25px" Width="150px"></asp:TextBox>
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnApproved" runat="server" Text="Approved" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" ToolTip="Approved Page" CausesValidation="False"
                        CssClass="button red" OnClientClick="return ApproveValidation()" OnClick="btnApproved_Click" />
                    &nbsp;
                    <asp:Button ID="btnCanApproved" runat="server" Text="Cancel" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button blue" OnClick="btnCanApproved_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Approve Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Approve" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>

    <asp:Label ID="lblSelectedVchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcessDate" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

