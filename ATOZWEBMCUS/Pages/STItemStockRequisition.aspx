<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STItemStockRequisition.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STItemStockRequisition" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 400px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />


    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Item Stock Requisition Maintenance
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblBooth" runat="server" Text="Warehouse From :" Font-Size="Large"
                        ForeColor="Red" Width="150px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWareHouse" runat="server" Enabled="false" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblGroupTyper" runat="server" Text="Group Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroupType" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="lblCategory" runat="server" Text="Category :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td>

                    <asp:Label ID="Label1" runat="server" Text="Requsition No. :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtReqNo" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="115px" Height="25px" Font-Size="Large" TabIndex="0" AutoPostBack="true" OnTextChanged="txtReqNo_TextChanged"></asp:TextBox>

                </td>
            </tr>



            <tr>

                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue" Height="27px" Width="100px" OnClick="btnPrint_Click" />
                    &nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" Height="27px" Width="100px" OnClick="btnSearch_Click" />

                    &nbsp;
                       <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Size="Large" ForeColor="White"
                           Font-Bold="True" CssClass="button blue" Height="27px" Width="100px" OnClick="btnUpdate_Click" />
                    &nbsp;
                    <asp:Button ID="btnSend" runat="server" Text="Send" Font-Size="Large" ForeColor="White"
                           Font-Bold="True" CssClass="button green" Height="27px" Width="100px" OnClick="btnSend_Click" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="100px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    <br />
    <br />
    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetails" runat="server" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>
                <asp:TemplateField HeaderText="Item Code"  HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemCode" runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("STKItemCode")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemName" runat="server" Enabled="false" EnableViewState="true" CssClass="cls text" Width="350px" Height="20" Style="text-align: left" ForeColor="Blue" Text='<%#Bind("STKItemName")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Balance Qty." HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBalQty" runat="server" Enabled="false" EnableViewState="true" CssClass="cls text" Width="100px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("STKUnitQty")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Req. Qty." HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtReqQty" runat="server" Font-Bold="true" Enabled="true" EnableViewState="true" CssClass="cls text" Width="100px" Height="20" Style="text-align: right" Font-Size="Large" ForeColor="Blue" onkeydown="return (event.keyCode !=13);" onkeypress="return IsNumberKey(event)" onFocus="javascript:this.select();">
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Note" HeaderStyle-Width="300px" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtNote" runat="server" Font-Bold="true" Enabled="true" EnableViewState="true" CssClass="cls text" Width="300px" Height="20" Style="text-align: left" Font-Size="Large" ForeColor="Blue" onkeydown="return (event.keyCode !=13);" onFocus="javascript:this.select();">
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

    <div runat="server" style="align-content: center">
    </div>

    <asp:Label ID="lblSalDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="lblDesc1" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc2" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc3" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc4" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc5" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc6" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc7" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc8" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc9" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc10" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc11" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc12" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc13" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc14" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="hdnPeriod" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnMonth" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnYear" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnToDaysDate" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="CtrlColumnRec" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="ZeroBal" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="lblRepDate" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="lblCashCode" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

</asp:Content>

