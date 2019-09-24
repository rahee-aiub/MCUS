<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STApproveItemStockRequisition.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STApproveItemStockRequisition" %>

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
                        Approve Item Stock Requisition
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

                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" Width="60px" CssClass="button black size-100" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="ReqDate" HeaderText="Req. Date" DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:TemplateField HeaderText="Ware House" Visible =" false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWareHouseNo" runat="server" Text='<%# Eval("ReqWarehouseNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WareHouse Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWareHouseName" runat="server" Text='<%# Eval("ReqWarehouseName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req. No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqNo" runat="server" Text='<%# Eval("ReqNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Group Type" Visible="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupNo" runat="server" Text='<%# Eval("ReqItemGroupNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Group Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("ReqItemGroupDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Category" Visible="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("ReqItemCategoryNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("ReqItemCategoryDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                

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

               
                <asp:TemplateField HeaderText="Item Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemCode" runat="server" EnableViewState="true" Enabled="false" Width="80px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ReqItemCode")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="250px" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemName" runat="server" EnableViewState="true" Enabled="false" Width="250px" Height="20" Style="text-align: left" ForeColor="Blue" Text='<%#Bind("ReqItemName")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Balance" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItmBal" runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ReqUnitQtyBalance")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Item Require" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItmReq" runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("ReqReqUnitQty")%>'>                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Note" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtNote" runat="server" EnableViewState="true" Enabled="false" Width="300px" Height="20" Style="text-align: left" ForeColor="Blue" Text='<%#Bind("ReqNote")%>'>                        
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

    <asp:Label ID="CtrlWarehouse" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlReqNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGroup" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCategory" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

