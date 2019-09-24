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
                        Approve Stock Transaction
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

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnRejectSelect" runat="server" Text="Reject" Width="68px" OnClick="BtnRejectSelect_Click" CssClass="button red" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnPrint" runat="server" Text="Print" Width="60px" CssClass="button black size-100" />
                                    </ItemTemplate>
                                </asp:TemplateField>


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

      <asp:GridView ID="gvItemDetails" runat="server" HeaderStyle-BackColor="YellowGreen" Width="1000px"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px"  EnableModelValidation="True">
                    <HeaderStyle BackColor="YellowGreen" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemGroupNo" HeaderText="Group">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                         <asp:BoundField DataField="ItemPurchaseQty" HeaderText="Quantity">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemUnitDesc" HeaderText="Unit">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>                       
                        <asp:BoundField DataField="ItemUnitPrice" DataFormatString="{0:0,0.00}" HeaderText="Unit Price">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Right" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemTotalPrice" DataFormatString="{0:0,0.00}" HeaderText="Total Price">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Right" Width="125px" />
                        </asp:BoundField>
                                             
                    </Columns>


                </asp:GridView>


    </div>
    <br />
    <div id="DivReject" runat="server" align="center" >
        <table class="style1">
            <tr>
               
                <td>
                    <asp:Label ID="lblNote" runat="server" Font-Size="Large" ForeColor="Red"
                        Text=" Reject Note :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRejectNote"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="1095px"></asp:TextBox>
                </td>
            </tr>
          
        </table>
           <asp:Button ID="BtnReject" runat="server" Text="Reject" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClientClick="return RejectValidation()" OnClick="BtnReject_Click" />
                    &nbsp;
                    <asp:Button ID="BtnCanReject" runat="server" Text="Cancel" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button blue" OnClick="BtnCanReject_Click" />
    </div>

    <div id="DivSelect" runat="server" align="center" >
        <table class="style1" >

            <tr>

                <td>
                    <asp:Label ID="lblTotalItem" runat="server" Font-Size="Large" ForeColor="Red"
                        Text="Total Item Qty. :"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtTotalItemQty" Style="text-align: Center"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px" ReadOnly="true"></asp:TextBox>
                </td>


                <td>
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="Red"
                        Text="Missing Item Qty. :"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtMissingItemQty" Style="text-align: Center"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px" AutoPostBack="true" OnTextChanged="txtMissingItemQty_TextChanged"></asp:TextBox>
                </td>


                <td>
                    <asp:Label ID="lblRcvItmQty" runat="server" Font-Size="Large" ForeColor="Red"
                        Text="Received Item Qty. :"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtRcvItmQty" Style="text-align: Center"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px" ReadOnly="true"></asp:TextBox>
                </td>

               
            </tr>

        </table>
    </div>

    <div id="divApprove" runat="server" align="center">
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
    


</asp:Content>

