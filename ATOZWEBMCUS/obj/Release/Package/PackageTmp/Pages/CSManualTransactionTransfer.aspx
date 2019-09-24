<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSManualTransactionTransfer.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSManualTransactionTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save New Sanction Amount?');
        }

        function ValidationBeforeUpdate() {
            var txtCreditUNo = document.getElementById('<%=txtCreditUNo.ClientID%>').value;
            var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;

            var txtAccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
          <%--  var txtTrnCuNo = document.getElementById('<%=txtTrnCuNo.ClientID%>').value;
            var txtTrnMemNo = document.getElementById('<%=txtTrnMemNo.ClientID%>').value;--%>

            if (txtCreditUNo == '' || txtCreditUNo.length == 0)
                alert('Please Input From Credit Union No.');
            else
                if (txtMemNo == '' || txtMemNo.length == 0)
                    alert('Please Input From Depositor No.');

                else
                    if (txtAccNo == '')
                        alert('Please Input Account No.');
                    else
                        if (txtTrnCuNo == '' || txtTrnCuNo.length == 0)
                            alert('Please Input To Credit Union No.');
                        else
                            if (txtTrnMemNo == '' || txtTrnMemNo.length == 0)
                                alert('Please Input To Depositor No.');
                            else
                                return confirm('Are you sure you want to Transfer Account Balance?');
            return false;

        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Multi Account Transaction From..........
                    </th>
                </tr>
            </thead>

           
        

            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Transaction Mode :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTranMode" runat="server" Enabled="false" Height="25px" Width="115px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">

                        <asp:ListItem Value="1">Credit</asp:ListItem>
                        <asp:ListItem Value="0">Debit</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="GL Contra Code :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGLContraCode" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="12" onkeydown="return (event.keyCode !=13);" AutoPostBack="true" OnTextChanged="txtGLContraCode_TextChanged"></asp:TextBox>

                    <asp:Label ID="lblGLCodeDesc" runat="server" Text=""></asp:Label>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Description :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="12" onkeydown="return (event.keyCode !=13);"></asp:TextBox>

                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Font-Size="Large" Text="Vch.No. :" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="115px" Height="25px" TabIndex="8" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>


        </table>
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Transaction Transfer To..............
                    </th>
                </tr>
            </thead>

                <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Account Type:" Font-Size="Large" Width="180px"
                        ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>


                    <asp:DropDownList ID="ddlAcType" runat="server" Height="25px" Width="350px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAcType_SelectedIndexChanged" Style="margin-left: 11px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTrnAccNo" runat="server" Text="Account No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="5" AutoPostBack="True"  ToolTip="Enter Code" onkeypress="return functionx(event)"
                        OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblAccTitle" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                     

                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblTrnCuNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="6" AutoPostBack="true" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                    <asp:CheckBox ID="chkOldSearch" runat="server" Font-Size="Medium" ForeColor="Red" Text="Old No. Search" />
                    <asp:Label ID="lblCuName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrnmemno" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="7" AutoPostBack="true" OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblMemName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>


                </td>
            </tr>



            <tr>
                <td>
                    <asp:Label ID="lblTrnAmount" runat="server" Text="Transaction Amount :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="11" AutoPostBack="True" onkeydown="return (event.keyCode !=13);" onkeypress="return IsDecimalKey(event)" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>

                </td>
            </tr>



            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" CssClass="button green" Height="27px" Width="140px"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnAdd_Click" />&nbsp;

                    <asp:Button ID="btnPrint" runat="server" Text="Print/Preview" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="148px" Font-Bold="True" CausesValidation="False" CssClass="button blue" OnClick="btnPrint_Click" />&nbsp;

                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" CssClass="button green" Height="27px" Width="140px"
                         OnClick="BtnUpdate_Click" />&nbsp;

                      <asp:Button ID="btnCancel" runat="server" Text="Refresh" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="102px" Font-Bold="True" CausesValidation="False" CssClass="button blue" OnClick="btnCancel_Click" />


                    &nbsp;


                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                    <br />
                </td>
            </tr>

        </table>

        <div>
            <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-BackColor="YellowGreen"
                AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" Width="706px">
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <Columns>

                    <asp:TemplateField HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblId" Visible="false" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="Account No." DataField="AccNo" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Credit Union No." DataField="CreditUnionNo" HeaderStyle-Width="150px" ItemStyle-Width="150px" />

                    <asp:BoundField HeaderText="Depositor No." DataField="DepositorNo" HeaderStyle-Width="150px" ItemStyle-Width="150px" />
                    <asp:BoundField HeaderText="Amount" DataField="TrnAmount" HeaderStyle-Width="150px" ItemStyle-Width="150px" />

                  <%--  <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="BtnSelect" runat="server" Text="Select" OnClick="BtnSelect_Click" Height="25px" Width="68px" CssClass="button green" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_Click" Height="25px" Width="68px" CssClass="button red" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>

        <asp:TextBox ID="txtHidden" runat="server" Visible="false"></asp:TextBox>
        <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnferCuType" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnferCuNo" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="txtTrnHidden" runat="server" Visible="false"></asp:TextBox>


        <asp:Label ID="hdnID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlProcDate" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblId1" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblId2" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="hdnPrmValue" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnFuncOpt" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnModule" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnUserId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnCashCode" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlVoucherNo" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlProcStat" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblUnPostDataCr" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblUnPostDataDr" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlBalance" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlAvailBalance" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnCuNumber" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="CtrlAccStatus" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlAccType" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlTrnAccType" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnCode" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblcls" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblflag" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="Searchflag" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlLadgerBalance" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlLienAmt" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlLoanSancAmt" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblAtyClass" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblTrnAtyClass" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="ValidityFlag" runat="server" Visible="false"></asp:Label>


        <asp:Label ID="lblTrnTypeTitle" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblFuncTitle" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblBoothNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblBoothName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblProcdate" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="lblPayType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnPayType" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="CtrlRecType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="hdnGLSubHead2" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblDuplicateVch" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblAccMode" runat="server" Text="" Visible="false"></asp:Label>

    </div>

</asp:Content>

