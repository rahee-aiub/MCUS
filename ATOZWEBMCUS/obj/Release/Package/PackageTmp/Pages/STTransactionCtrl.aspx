<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STTransactionCtrl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STTransactionCtrl" Title="Transaction Control" %>

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
                    <th colspan="3">Stock Transaction Control
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Func No.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFuncNo" runat="server" Height="30px"
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlFuncNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Item Purchase</asp:ListItem>
                        <asp:ListItem Value="11">Item Sale</asp:ListItem>
                        <asp:ListItem Value="13">Item Use</asp:ListItem>
                        <asp:ListItem Value="61">Payment Supplier</asp:ListItem>
                        <asp:ListItem Value="62">Payment VAT Amt.</asp:ListItem>
                        <asp:ListItem Value="63">Payment TAX Amt.</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" Height="30px"
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                         <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Sub Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" Height="30px"
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="316px" Font-Size="Large">
                         <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>


                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Pay Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPayType" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Payment</asp:ListItem>
                        <asp:ListItem Value="2">VAT</asp:ListItem>
                        <asp:ListItem Value="3">TAX</asp:ListItem>
                        <asp:ListItem Value="4">Net Payment</asp:ListItem>
                        <asp:ListItem Value="5">Cost Amount</asp:ListItem>
                        <asp:ListItem Value="6">Profit Amount</asp:ListItem>
                        <asp:ListItem Value="7">Payment Supplier</asp:ListItem>
                        <asp:ListItem Value="8">Payment VAT Amt.</asp:ListItem>
                        <asp:ListItem Value="9">Payment TAX Amt.</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Trn Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnType" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Cash</asp:ListItem>
                        <asp:ListItem Value="3">Transfer</asp:ListItem>
                        <asp:ListItem Value="48">Bank</asp:ListItem>
                       
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Trn Mode :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnMode" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" Font-Size="Large">
                        <asp:ListItem Value="0">Debit</asp:ListItem>
                        <asp:ListItem Value="1">Credit</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Trn. Debit Code :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnDebitCode" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtTrnDebitCode_TextChanged"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblDebitCd" runat="server" Text="" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Trn. Credit Code :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnCreditCode" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtTrnCreditCode_TextChanged"></asp:TextBox>
                      &nbsp;<asp:Label ID="lblCreditCd" runat="server" Text="" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Trn. Rec. Desc:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnRecDesc" runat="server" CssClass="cls text" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
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
                    <asp:Button ID="btnUpdate" runat="server" Text="Update"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="btnUpdate_Click" Height="27px" />
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClick="btnCancel_Click" Height="27px" />
                      &nbsp;
                      <asp:Button ID="btnView" runat="server" Text="View"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClick="btnView_Click" Height="27px" />
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
    <div align="center" class="grid_scroll">

           <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-BackColor="#ffcc00"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblId" Visible="false" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Func No" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblFuncNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("FuncNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Func Desc" HeaderStyle-Width="200px" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblFuncDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("FuncDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Group Code" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupCode" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GroupCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Sub Group Code" HeaderStyle-Width="135px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblSubGroupCode" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("SubGroupCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Pay Type" HeaderStyle-Width="135px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblPayType" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("PayType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Trn Type" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblTrnType" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("TrnType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField HeaderText="Trn Mode" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblTrnMode" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("TrnMode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="GLAccNoDr" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNoDr" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLAccNoDr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="GLAccNoCr" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNoCr" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLAccNoCr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Trn Rec Desc" HeaderStyle-Width="200px" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblTrnRecDesc" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("TrnRecDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="75px" ItemStyle-Width="68px">
                    <ItemTemplate>
                        <asp:Button ID="BtnHdrSelect" runat="server" Font-Bold="True" Text="Select" OnClick="BtnHdrSelect_Click" CssClass="button green" />
                        
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

     </div>

    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label6" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSelectedId" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
