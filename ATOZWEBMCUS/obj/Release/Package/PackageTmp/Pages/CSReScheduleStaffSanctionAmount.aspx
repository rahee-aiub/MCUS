<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSReScheduleStaffSanctionAmount.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSReScheduleStaffSanctionAmount" %>

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

    
    <div align="center">
        <asp:Panel ID="pnlLoanApplication" runat="server" Width="976px">
            <table class="style1">

                <thead>
                    <tr>
                        <th colspan="3">Staff Loan New Sanction Amount
                        </th>
                    </tr>
                </thead>
                
                <tr>
                    <td>
                        <asp:Label ID="IlblMemNo" runat="server" Text="Staff Code :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ItxtMemNo" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                            OnTextChanged="ItxtMemNo_TextChanged"></asp:TextBox>

                        <asp:DropDownList ID="ddlLoanMemNo" runat="server" Height="25px" Width="504px"
                            Font-Size="Large" CssClass="cls text" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlLoanMemNo_SelectedIndexChanged">
                            </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="IlblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                            OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlIAccType" runat="server" Height="25px" Width="250px"
                            AutoPostBack="True" Font-Size="Large" TabIndex="5" OnSelectedIndexChanged="ddlIAccType_SelectedIndexChanged">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>

                        </asp:DropDownList>

                        <asp:Label ID="IlblAccNo" runat="server" Text="Account No:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                        <asp:TextBox ID="ItxtAccNo" runat="server" CssClass="cls text" Width="170px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium" ToolTip="Enter Code" Enabled="false" AutoPostBack="True" OnTextChanged="ItxtAccNo_TextChanged"></asp:TextBox>

                        <asp:DropDownList ID="ddlAccNo" runat="server" CssClass="cls text" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="200px" Font-Size="Large" TabIndex="0" AutoPostBack="True" OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>


                        &nbsp;&nbsp;&nbsp;


                    </td>
                </tr>
                          </table>
        </asp:Panel>
    </div>
    <div align="center">
        <table class="style1">

                <tr>
                    <td>
                        <asp:Label ID="IlblExDisbAmt" runat="server" Text="Existing Disbursement Amt. :" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    
                        &nbsp;
                    
                        <asp:TextBox ID="lblExDisbAmt" runat="server" Enabled="false" CssClass="cls text" Height="25px" Width="150px" Font-Size="Large" Text="" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>

                        </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="IlblExLoanBal" runat="server" Text="Loan Balance :" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    
                        &nbsp;
                    
                        <asp:TextBox ID="lblExLoanBal" runat="server" Enabled="false" CssClass="cls text" Height="25px" Width="150px" Font-Size="Large" Text="" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>

                        </td>
                    
                    <td>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="IlblNewLoanAmt" runat="server" Text="New Disbursement Amount :" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    
                        &nbsp;<asp:TextBox ID="lblNewLoanAmt" runat="server" Enabled="false" CssClass="cls text" Height="25px" Width="150px" Font-Size="Large" Text="" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>


                    </td>
                </tr>

            </table>
        
    </div>
    <div align="center">
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="IlblExistSanctionAmount" runat="server" Text="Existing Sanction Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="IlblExSancAmt" runat="server" Font-Size="Large" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="IlblNewSanctionAmount" runat="server" Text="New Loan Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ItxtIncreaseSanctionAmount" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="120px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="ItxtIncreaseSanctionAmount_TextChanged" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="IlblExistNoInstl" runat="server" Text="Existing No. Installment:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="IlblExNoInstl" runat="server" Font-Size="Large" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="IlblNewNoInstal" runat="server" Text="New No. Installment:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ItxtNewNoInstal" runat="server" CssClass="cls text" Width="120px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" OnTextChanged="ItxtNewNoInstl_TextChanged" AutoPostBack="True" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="IlblExistInstlAmt" runat="server" Text="Existing Installment Amt.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExistInstlAmt" runat="server" Font-Size="Large" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="IlblNewInstalAmt" runat="server" Text="New Installment Amt.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ItxtNewInstalAmt" runat="server" CssClass="cls text" Width="120px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" OnTextChanged="ItxtNewInstalAmt_TextChanged" AutoPostBack="True" onkeypress="return IsDecimalKey(event)" onFocus="javascript:this.select();"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="IlblExistLastInstlAmt" runat="server" Text="Existing Last Installment Amt.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExistLastInstlAmt" runat="server" Font-Size="Large" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="IlblNewLastInstalAmt" runat="server" Text="New Last Installment Amt.:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ItxtNewLastInstalAmt" runat="server" CssClass="cls text" Enabled="false" Width="120px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
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
                    <asp:TextBox ID="ItxtNewInterestRate" runat="server" CssClass="cls text" Width="120px"  BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" OnTextChanged="ItxtNewInterestRate_TextChanged" AutoPostBack="True" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnNewSanction" runat="server" Text="Update New Sanction" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" CssClass="button green" Height="27px" Width="250px"
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
        

        <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblSFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCtrlFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="MSGFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblNewSanctionAmt" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblDisbAmt" runat="server" Text="" Visible="false"></asp:Label>

    </div>


    
    <asp:Label ID="lblPrevInstlAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
