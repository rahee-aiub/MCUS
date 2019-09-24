<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STItemStockList.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STItemStockList" Title="Item Stock List" %>

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
            height: 412px;
            width: 850px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 833px;
        }
         .FixedHeader2 {
            position: absolute;
            font-weight: bold;
            width: 849px;
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
                    <th colspan="3">Item Stock List
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:CheckBox ID="chkWarehouse" runat="server" Font-Size="Large" ForeColor="Red" Text="All " Checked="True" AutoPostBack="True" OnCheckedChanged="chkWarehouse_CheckedChanged" />
                    &nbsp;
                    <asp:Label ID="Label16" runat="server" Text="Warehouse :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWarehouse" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="300px" Font-Size="Large">
                    </asp:DropDownList>
                </td>
            </tr>
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
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>

                    <asp:DropDownList ID="ddlItemType" runat="server" Height="30px"
                        Width="280px"  BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"
                         TabIndex="7">
                        
                    </asp:DropDownList>

                </td>
            </tr>
                     
          


            <tr>

                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green"
                        OnClick="BtnPrint_Click" Width="86px" Height="27px" />&nbsp;
                    <asp:Button ID="BtnBack" runat="server" Text="Back" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Back Page" CausesValidation="False"
                        CssClass="button green" OnClick="BtnBack_Click" />
                    &nbsp;<asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" Width="86px" Height="27px" />
                    <br />

                </td>
            </tr>
        </table>
    </div>


   
        
      

  
     <div id="DivGV" runat="server" align="Center" class="grid_scroll">


         <asp:GridView ID="gvHead1" runat="server" HeaderStyle-BackColor="#00ffff"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" >
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:TemplateField HeaderText="Item Code" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblHItemCode" runat="server" Font-Bold="True" Enabled="false" Width="90px" Text='<%# Eval("STKItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Item Name"  HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblHItemName" runat="server" Font-Bold="True" Enabled="false" Width="300px" Text='<%# Eval("STKItemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Qty." HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblHQty" runat="server" Font-Bold="True" Enabled="false" Width="70px" Text='<%# Eval("STKUnitQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TP. Qty."  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblHTPQty" runat="server" Font-Bold="True" Enabled="false" Width="70px" Text='<%# Eval("STKTPUnitQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Qty." HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblHTotQty" runat="server" Font-Bold="True" Enabled="false" Width="90px" Text='<%# Eval("TotalQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>               

                 <asp:TemplateField HeaderText="Avg. Cost" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblHAvgCost" runat="server" Font-Bold="True" Enabled="false" Width="90px" Style="color: blue" Text='<%#Eval("STKUnitAvgCost","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Price"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblHTotPrice" runat="server" Font-Bold="True" Enabled="false" Width="100px" Style="color: darkgreen" Text='<%#Eval("CalUnitCost","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>

        </asp:GridView>

        

        <asp:GridView ID="gvHeaderInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvHeaderInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:TemplateField HeaderText="Item Code" HeaderStyle-Width="134px" ItemStyle-Width="137px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("STKItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="500px" ItemStyle-Width="494px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("STKItemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Qty." HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("STKUnitQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TP. Qty." HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTPQty" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("STKTPUnitQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="Total Qty." HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQty" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("TotalQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Avg. Cost" HeaderStyle-Width="135px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblAvgCost" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("STKUnitAvgCost","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Price" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblTotPrice" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("CalUnitCost","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

               
                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="75px" ItemStyle-Width="68px">
                    <ItemTemplate>
                        <asp:Button ID="BtnHdrSelect" runat="server" Font-Bold="True" Text="Detail" OnClick="BtnHdrSelect_Click" CssClass="button green" />
                        
                    </ItemTemplate>
                </asp:TemplateField>

               


            </Columns>

        </asp:GridView>


         <asp:GridView ID="gvSubHeaderInfo" runat="server"  HeaderStyle-BackColor="#ffcc00"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" >
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:TemplateField HeaderText="Ware House" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblWarehouse" runat="server" Font-Bold="True" Width="90px" Enabled="false" Text='<%# Eval("STKWareHouse") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ware House Name" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblWarehouseName" runat="server" Font-Bold="True" Width="300px" Enabled="false" Text='<%# Eval("STKWareHouseName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Qty."  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblWQty" runat="server" Font-Bold="True" Enabled="false" Width="70px" Text='<%# Eval("STKUnitQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TP. Qty."  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblWTPQty" runat="server" Font-Bold="True" Enabled="false" Width="70px" Text='<%# Eval("STKTPUnitQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="Total Qty." HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblWTotalQty" runat="server" Font-Bold="True" Enabled="false" Width="90px" Text='<%# Eval("TotalQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Avg. Cost"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblWAvgCost" runat="server" Font-Bold="True" Enabled="false" Width="90px" Style="color: blue" Text='<%#Eval("STKUnitAvgCost","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblWTotPrice" runat="server" Font-Bold="True" Enabled="false" Width="100px" Style="color: darkgreen" Text='<%#Eval("STKUnitCost","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>



         </div>


    <br />
   
    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnSubGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNewSTKNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnSubGroupCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnLastRec" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNewRec" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCWarehouseflag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlItemCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlItemCodeName" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblRepFlag" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
