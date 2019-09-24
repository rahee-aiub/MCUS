<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSIncreaseSanctionAmount.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSIncreaseSanctionAmount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save New Sanction Amount?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />

    <table>
        <tr>
            <td class="auto-style1">
                <asp:Button ID="BtnLoanApplication" runat="server" Text="Increase Sanction Amt" Style="background-color: Silver"
                    Width="212px" OnClick="BtnLoanApplication_Click" Height="31px" />
                <asp:Button ID="BtnDeposit" Style="background-color: InactiveCaption" runat="server"
                    Text="Deposit Guarantor" Width="212px" OnClick="BtnDeposit_Click" Height="30px" />
                <asp:Button ID="BtnShare" runat="server" Text="Share Guarantor" Style="background-color: ActiveCaption"
                    Width="212px" OnClick="BtnShare_Click" Height="30px" />
                <asp:Button ID="BtnProperty" runat="server" Text="Property Guarantor" Style="background-color: linen"
                    Width="212px" OnClick="BtnProperty_Click" Height="30px" />
            </td>
        </tr>
    </table>





    <div align="center">
        <asp:Panel ID="pnlLoanApplication" runat="server" Width="976px">
            <table class="style1">

                <thead>
                    <tr>
                        <th colspan="3">Increase Sanction Amount Maintenance
                        </th>
                    </tr>
                </thead>
                <tr>
                    <td>
                        <asp:Label ID="IlblCUNum" runat="server" Text="Credit Union No:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ItxtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="ItxtCreditUNo_TextChanged"></asp:TextBox>
                        <asp:Label ID="IlblCuName" runat="server" Text=""></asp:Label>&nbsp;
                        <asp:Button ID="BtnHelp" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="BtnHelp_Click" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="IlblMemNo" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ItxtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                            OnTextChanged="ItxtMemNo_TextChanged"></asp:TextBox>

                        <asp:Label ID="IlblMemName" runat="server" Width="400px" Height="25px" Text=""></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="IlblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlIAccType" runat="server" Height="25px" Width="250px"
                            AutoPostBack="True" Font-Size="Large" TabIndex="5" OnSelectedIndexChanged="ddlIAccType_SelectedIndexChanged">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>

                        </asp:DropDownList>

                        <asp:Label ID="IlblAccNo" runat="server" Text="Account No:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                        <asp:TextBox ID="ItxtAccNo" runat="server" CssClass="cls text" Width="170px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium" ToolTip="Enter Code" AutoPostBack="true" OnTextChanged="ItxtAccNo_TextChanged"></asp:TextBox>


                    </td>
                </tr>
            </table>
            <table class="style1">
                <tr>
                    <td>
                        <asp:Label ID="IlblExistSanctionAmount" runat="server" Text="Existing Sanction Amount:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="IlblExSancAmt" runat="server" Font-Size="Large" Width="200px" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="IlblNewSanctionAmount" runat="server" Text="New Sanction Amount:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ItxtNewSanctionAmount" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="190px" Height="25px"
                            Font-Size="Large" AutoPostBack="True" OnTextChanged="ItxtNewSanctionAmount_TextChanged" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="IlblExistIntRate" runat="server" Text="Existing Int. Rate:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="IlblExIntRate" runat="server" Font-Size="Large" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="IlblNewInterestRate" runat="server" Text="New Interest Rate:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                        </td>
                    <td>
                        <asp:TextBox ID="ItxtNewInterestRate" runat="server" CssClass="cls text" Width="100px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Height="25px" Font-Size="Large" OnTextChanged="ItxtNewInterestRate_TextChanged" AutoPostBack="True" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblRenwlDt" runat="server" Text="Renewal Date:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td><asp:Label ID="lblRenewalDate" runat="server" Font-Size="Large" Text=""></asp:Label>
                        </td>
                    <td>
                       
                    <asp:Label ID="lblExpDt" runat="server" Text="New Expiry Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                        </td>
                    <td>
                        <asp:Label ID="lblNewExpiryDate" runat="server" CssClass="cls text" Width="100px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Height="25px" Font-Size="Large"></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="IlblNote" runat="server" Text="Note:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="ItxtNote" runat="server" CssClass="cls text" Width="355px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td></td>
                      <td colspan="3">
                        <asp:Button ID="BtnNewSanction" runat="server" Text="New Sanction" Font-Size="Large"
                            ForeColor="White" Font-Bold="True" CssClass="button green" Height="27px" Width="150px"
                            OnClientClick="return ValidationBeforeSave()" OnClick="BtnNewSanction_Click" />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                        <br />
                    </td>
                </tr>

            </table>
            <asp:TextBox ID="txtHidden" runat="server" Width="115px" Height="25px" Visible="false"
                Font-Size="Large"></asp:TextBox>
            <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>

            <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblSFlag" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblCtrlFlag" runat="server" Text="" Visible="false"></asp:Label>
        </asp:Panel>
    </div>


    <div style="border-style: none; border-color: inherit; border-width: 1px; background-color: InactiveCaption" align="center">
        <asp:Panel ID="pnlDeposit" runat="server" Width="976px" Height="395px">
            <table class="style1">
                <tr>
                    <td>

                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnSearch" runat="server" Text="Help" Width="96px" Height="28px" Font-Size="Large" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="BtnSearch_Click" />
                    </td>

                    <td>
                        <h3>
                            <asp:Label ID="lblCrUNo" runat="server" Text="CuNo" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtCrUNo" runat="server" Width="96px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="1" OnTextChanged="txtCrUNo_TextChanged" AutoPostBack="true"></asp:TextBox>

                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblMemNo" runat="server" Text="Depositor No " ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtDepositMemNo" runat="server" Width="80px" Height="25px" Font-Size="Large"
                            CssClass="cls text" AutoPostBack="true" TabIndex="2" OnTextChanged="DepositMemNo_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblAccType" runat="server" Text="A/c Type" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtAccType" runat="server" Width="70px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="3" AutoPostBack="true" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblAccNo" runat="server" Text="A/c No" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtAccNo" runat="server" Width="160px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="4" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblLionAmt" runat="server" Text="Lien Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtLionAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="5" AutoPostBack="True" OnTextChanged="txtLionAmt_TextChanged" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblTotalLienAmt" runat="server" Text="Total Lien Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtTotalLienAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="6" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        <h3>
                            <asp:Label ID="lblLedgerBalance" runat="server" Text="Ledger Balance" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtLedgerBalance" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="7"></asp:TextBox>
                    </td>
                    <td>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAddDeposit" runat="server" Text="Add" Width="91px" Font-Size="Large"
                            ForeColor="White" Height="25px" Font-Bold="True"
                            CssClass="button green" OnClick="BtnAddDeposit_Click" />
                    </td>

                </tr>
            </table>
            <table style="align-content: center">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Cu Name:" Font-Size="Large" ForeColor="Red"></asp:Label>

                    </td>
                    <td>
                        <asp:Label ID="lblCrName" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Depositor Name:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMemberName" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Account Type:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAccountTypeName" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>

            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader2" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound" EnableModelValidation="false" OnRowDeleting="gvDetailInfo_RowDeleting">
                    <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader2" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="CuNo" HeaderText="CU No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MemNo" HeaderText="Depositor No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccType" HeaderText="A/C Type">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccNo" HeaderText="A/C No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccAmount" DataFormatString="{0:0,0.00}" HeaderText="Amount">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                            <ControlStyle Font-Bold="True" ForeColor="#FF6600" />
                        </asp:CommandField>
                    </Columns>

                </asp:GridView>
            </div>
            <div align="right" style="width: 695px">

                <asp:Label ID="lblTotalAmt" runat="server" Text="Total Lien:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
            <asp:Label ID="lblDepositCuType" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblDepositCuNo" runat="server" Visible="false"></asp:Label>
        </asp:Panel>
    </div>
    <div style="background-color: ActiveCaption; border: 1px" align="center">
        <asp:Panel ID="pnlShare" runat="server" Height="395px">
            <table class="style1">
                <tr>
                    <td>

                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnSSearch" runat="server" Text="Help" Width="96px" Height="28px" Font-Size="Large" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="btnSSearch_Click" />
                    </td>

                    <td>
                        <h3>
                            <asp:Label ID="Label5" runat="server" Text="CuNo" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtShareCuNo" runat="server" Width="96px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="1" AutoPostBack="true" OnTextChanged="txtShareCuNo_TextChanged"></asp:TextBox>

                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label6" runat="server" Text="Cu Name " ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtShareCuName" runat="server" Width="504px" Height="25px" Font-Size="Large" Enabled="false"
                            CssClass="cls text" TabIndex="2" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label7" runat="server" Text="Acc Type" ForeColor="Red" Visible="false"></asp:Label></h3>
                        <asp:TextBox ID="txtShareAccType" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="3" Visible="false"></asp:TextBox>
                    </td>

                    <td>
                        <h3>
                            <asp:Label ID="Label9" runat="server" Text="Share Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtShareAmount" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="4"></asp:TextBox>
                    </td>

                    <td>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAddShare" runat="server" Text="Add" Width="91px" Font-Size="Large"
                            ForeColor="White" Height="25px" Font-Bold="True"
                            CssClass="button green" OnClick="BtnAddShare_Click" />
                    </td>

                </tr>
            </table>

            <br />
            <br />
            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvShareDetails" runat="server" HeaderStyle-CssClass="FixedHeader3" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvShareDetails_RowDataBound" EnableModelValidation="True" OnRowDeleting="gvShareDetails_RowDeleting">
                    <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader3" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CuNo" HeaderText="CU No">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccType" HeaderText="A/C Type">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccNo" HeaderText="Acc No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccAmount" DataFormatString="{0:0,0.00}" HeaderText="Amount">
                            <HeaderStyle Width="180px" />
                            <ItemStyle HorizontalAlign="Right" Width="180px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                            <ControlStyle Font-Bold="True" ForeColor="#FF6600" />
                        </asp:CommandField>
                    </Columns>

                </asp:GridView>
            </div>
            <div align="center" style="width: 600px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbltotalshare" runat="server" Text="Total Share :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                <asp:Label ID="lblShareTotalAmt" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
            <asp:Label ID="lblShareCType" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblShareCNo" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblApplicationNo" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="hdnAccNo" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblShareCu" runat="server" Visible="false"></asp:Label>
        </asp:Panel>
    </div>
    <div style="background-color: linen; border: 1px" align="center">
        <asp:Panel ID="pnlProperty" runat="server" Height="395px">
            <table class="style1">
                <tr>
                    <td>
                        <h3>
                            <asp:Label ID="Label8" runat="server" Text="SL No." ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtSerialNo" runat="server" Width="96px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="1" AutoPostBack="True" OnTextChanged="txtSerialNo_TextChanged"></asp:TextBox>

                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label10" runat="server" Text="Name of Property " ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtNameProperty" runat="server" Width="196px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="2" AutoPostBack="True" OnTextChanged="txtNameProperty_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label11" runat="server" Text="File No" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtFileNo" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="3" AutoPostBack="True" OnTextChanged="txtFileNo_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label12" runat="server" Text="Description" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtDescription" runat="server" Width="227px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="4" AutoPostBack="True" OnTextChanged="txtDescription_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label13" runat="server" Text="Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtProprertyAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="5" onchange="javascript:this.value=Comma(this.value);" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    </td>

                    <td>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAddProperty" runat="server" Text="Add" Width="91px" Font-Size="Large"
                            ForeColor="White" Height="25px" Font-Bold="True"
                            CssClass="button green" OnClick="BtnAddProperty_Click" />
                    </td>

                </tr>
            </table>
            <br />
            <br />
            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvPropertyDetails" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvPropertyDetails_RowDataBound" EnableModelValidation="True" OnRowDeleting="gvPropertyDetails_RowDeleting">
                    <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PrSRL" HeaderText="Srl.No">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrName" HeaderText="Property Name">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileNo" HeaderText="File No">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrDesc" HeaderText="Description">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrAmount" DataFormatString="{0:0,0.00}" HeaderText="Amount">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Right" Width="125px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                            <ControlStyle Font-Bold="True" ForeColor="#FF3300" />
                        </asp:CommandField>
                    </Columns>


                </asp:GridView>
            </div>

            <div align="right" style="width: 685px">

                <asp:Label ID="lblTotalProprty" runat="server" Font-Size="Medium" ForeColor="Red" Text="Total Property :"></asp:Label>
                <asp:Label ID="lblSumProperty" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
        </asp:Panel>
    </div>

    <div align="center" style="width: 685px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblTotalGar" runat="server" Font-Size="X-Large" ForeColor="Red" Text="Total Guaranty :"></asp:Label>
        <asp:Label ID="lblTotalResult" runat="server" Font-Size="X-Large" ForeColor="Red"></asp:Label>
    </div>

    <asp:Label ID="lblAtypeGuaranty" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIAtypeGuaranty" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblIPeriod" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccFlag" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
