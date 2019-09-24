<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STPaymentTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STPaymentTransaction" Title="Supplier Payment" %>

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
                    <th colspan="4">Supplier Payment
                    </th>
                </tr>

            </thead>
        

              <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Supplier No.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSupplier" runat="server" CssClass="cls text" Width="80px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50" AutoPostBack="True" OnTextChanged="txtSupplier_TextChanged"></asp:TextBox>
                   &nbsp;<asp:DropDownList ID="ddlSupplier" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="250px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                  <td>
                    <asp:Label ID="lblLedgerBalance" runat="server" Text="Ledger Balance :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPayableAmt" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="False"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Payment Type:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPaymentOptn" runat="server" Height="30px"
                        BorderColor="#1293D1" BorderStyle="Ridge" Width="250px" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="61">Supplier Payment</asp:ListItem>
                        <asp:ListItem Value="62">VAT Payment</asp:ListItem>
                        <asp:ListItem Value="63">TAX Payment</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblVatBalance" runat="server" Text="VAT Amt. Balance :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPayableVATAmt" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="False"></asp:TextBox>
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
                    <asp:Label ID="lblTaxBalance" runat="server" Text="TAX Amt. Balance :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPayableTAXAmt" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="False"></asp:TextBox>
                </td>
         </tr>

             <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Tran. Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnType" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="250px" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlTrnType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Cash</asp:ListItem>
                        <asp:ListItem Value="48">Bank</asp:ListItem>
                       
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>


           

              <tr>
                <td>
                    <asp:Label ID="lblGLBankCode" runat="server" Text="Bank Code :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGLBankCode" runat="server" TabIndex="7" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtGLBankCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlGLBankCode" runat="server" Height="28px" Width="300px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGLBankCode_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td>
                    <asp:Label ID="lblChqNo" runat="server" Text="Cheque No. :" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                  <td>
                    <asp:TextBox ID="txtChqNo" runat="server" TabIndex="6" CssClass="cls text" MaxLength="15" Width="140px" Height="25px" onkeydown="return (event.keyCode !=13);" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"></asp:TextBox>&nbsp;
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Amount :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtAmt" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                       onkeypress="return IsDecimalKey(event)"  Font-Size="Large"></asp:TextBox>

                </td>
               
            </tr>

           
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Tran. Note:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtTrnNote" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>


            </tr>
         

          
            <tr>
               
                <td colspan="2">
                    &nbsp;
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

           <%--<asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-BackColor="#ffcc00"
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
        </asp:GridView>--%>

     </div>

    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label6" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSelectedId" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnGLSubHead" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCodeName" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
