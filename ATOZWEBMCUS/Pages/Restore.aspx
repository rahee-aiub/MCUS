<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="Restore.aspx.cs" Inherits="ATOZWEBMCUS.Pages.Restore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            width: 571px;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />
    <table>
        <tr>
            <td class="auto-style1"></td>
            <td>
                <div style="border-style: none; border-color: inherit; border-width: 1px; background-color: #DDA0DD; width: 270px;" align="left">
                    <asp:Label ID="lblSelectDay" runat="server" Text=" Select a Day:" Font-Size="X-Large"
                        ForeColor="Red"></asp:Label><br />
                    <br />
                    <asp:RadioButton ID="rbtSunday" runat="server" AutoPostBack="True" Text="Sunday" Font-Size="X-Large" ForeColor="Black" OnCheckedChanged="rbtSunday_CheckedChanged" />&nbsp;&nbsp;<br />
                    <asp:RadioButton ID="rbtMonday" runat="server" AutoPostBack="True" Text="Monday" Font-Size="X-Large" OnCheckedChanged="rbtMonday_CheckedChanged" />&nbsp;&nbsp;<br />
                    <asp:RadioButton ID="rbtTuesday" runat="server" AutoPostBack="True" Text="Tuesday" Font-Size="X-Large" OnCheckedChanged="rbtTuesday_CheckedChanged" />&nbsp;&nbsp;<br />
                    <asp:RadioButton ID="rbtWednesday" runat="server" AutoPostBack="True" Text="Wednesday" Font-Size="X-Large" OnCheckedChanged="rbtWednesday_CheckedChanged" />&nbsp;&nbsp;<br />
                    <asp:RadioButton ID="rbtThursday" runat="server" AutoPostBack="True" Text="Thursday" Font-Size="X-Large" OnCheckedChanged="rbtThursday_CheckedChanged" />&nbsp;&nbsp;<br />
                    <asp:RadioButton ID="rbtFriday" runat="server" AutoPostBack="True" Text="Friday" Font-Size="X-Large" OnCheckedChanged="rbtFriday_CheckedChanged" />&nbsp;&nbsp;<br />
                    <asp:RadioButton ID="rbtSaturday" runat="server" AutoPostBack="True" Text="Saturday" Font-Size="X-Large" OnCheckedChanged="rbtSaturday_CheckedChanged" />&nbsp;&nbsp;<br />

                </div>
            </td>
        </tr>

    </table>
    <br />
    <br />
    <div style="border: 1px" align="center">

        <asp:Label ID="lblFrom" runat="server" Text=" From" Font-Size="X-Large"></asp:Label>&nbsp;&nbsp;
                   
                          <asp:TextBox ID="txtFrom" runat="server" Width="403px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                              Font-Size="Large" AutoPostBack="true"></asp:TextBox><br />

        <asp:Label ID="lblTo" runat="server" Text=" To" Font-Size="X-Large"></asp:Label>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                   
                          <asp:TextBox ID="txtTo" runat="server" Width="404px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                              Font-Size="Large" AutoPostBack="true"></asp:TextBox><br />




        <asp:ListBox ID="listDatabase" runat="server" Height="83px" Width="251px" Visible="false">
            <asp:ListItem Value="0">A2ZCSMCUS</asp:ListItem>
            <asp:ListItem Value="1">A2ZGLMCUS</asp:ListItem>
            <asp:ListItem Value="2">A2ZHKMCUS</asp:ListItem>
            <asp:ListItem Value="3">A2ZHRMCUS</asp:ListItem>
            <asp:ListItem Value="4">A2ZBTMCUS</asp:ListItem>
            <asp:ListItem Value="5">A2ZCSMCUST2014</asp:ListItem>
            <asp:ListItem Value="6">A2ZCSMCUST2015</asp:ListItem>
            <asp:ListItem Value="7">A2ZIMAGEMCUS</asp:ListItem>
            <asp:ListItem Value="8">A2ZOBTMCUS</asp:ListItem>
        </asp:ListBox>

        <asp:Label ID="CtrlTo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="CtrlFrom" runat="server" Text="" Visible="false"></asp:Label>



    </div>
    <br />
    <div style="border: 1px" align="center">
        <asp:Button ID="BtnRestore" runat="server" Text="Restore" BackColor="#009900" ForeColor="#FFFF99" Height="34px" Width="94px" OnClick="BtnRestore_Click" />&nbsp;&nbsp;
            <asp:Button ID="BtnExit" runat="server" Text="Exit" BackColor="Red" ForeColor="#FFFF99" Height="34px" Width="79px" OnClick="BtnExit_Click" />
    </div>

    <div id="divRestore" runat="server" align="left" class="grid_scroll" visible="false">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Database" HeaderStyle-Width="60px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDatabase" runat="server" Text='<%#Eval("DatabaseName") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>


            </Columns>

        </asp:GridView>
    </div>

    <asp:Label ID="RestoreFlag" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

