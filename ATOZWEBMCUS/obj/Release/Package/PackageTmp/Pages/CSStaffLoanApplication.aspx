<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSStaffLoanApplication.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSStaffLoanApplication" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>
    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />



    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtLoanAppDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
        $(document).ready(function () {
            $("#<%=txtLoanFirstInstlDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
        $(document).ready(function () {
            $("#<%=txtLoanExpDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });


    </script>--%>



    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtLoanAppDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtLoanAppDate.ClientID %>").datepicker();

             });

        });
         $(function () {
             $("#<%= txtLoanExpDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtLoanExpDate.ClientID %>").datepicker();

             });

        });
            </script>


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 136px;
            margin: 0 auto;
            width: 791px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            /*width: 750px;*/
            font-weight: bold;
        }

        .FixedHeader2 {
            position: absolute;
            /*//width: 415px;*/
            font-weight: bold;
        }


        .FixedHeader3 {
            position: absolute;
            /*width: 414px;*/
            font-weight: bold;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    
    <div style="background-color: Silver; border: 1px" align="center">
        <asp:Panel ID="pnlLoanApplication" runat="server" Width="976px">


            <table class="style1">

                <tr>
                    <td>
                        <asp:Label ID="lblLoanAppDate" runat="server" Text=" Application Date:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanAppDate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                            Font-Size="Large" ToolTip="Enter Date" img src="../Images/calender.png"></asp:TextBox><img src="../Images/calender.png" />&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" Font-Size="Large" Width="115px" Height="25px" ForeColor="Red" Text="Last Application No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblLastAppNo" runat="server" Font-Size="Large" Width="115px" Height="25px" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLoanAppType" runat="server" Text="Loan A/C Type:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccType" runat="server" Height="25px"
                            Width="199px" AutoPostBack="True" CssClass="cls text"
                            Font-Size="Large" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Visible="false" Width="115px"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <asp:Label ID="lblCUNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlCreditUNo" runat="server" Height="25px" Width="504px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" OnSelectedIndexChanged="ddlCreditUNo_SelectedIndexChanged">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>


                <tr>
                    <td>
                        <asp:Label ID="lblLoanMemNo" runat="server" Text="Staff Code:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanMemNo" runat="server" CssClass="cls text" Width="115px" AutoPostBack="true"
                            Height="25px" Font-Size="Large" OnTextChanged="txtLoanMemNo_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlLoanMemNo" runat="server" Height="25px" Width="504px"
                            Font-Size="Large" CssClass="cls text" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlLoanMemNo_SelectedIndexChanged">
                            </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLoanAppAmount" runat="server" Text=" Application Amount:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanAppAmount" runat="server" CssClass="cls text" Width="199px"
                            Height="25px" Font-Size="Large" onFocus="javascript:this.select();"" AutoPostBack="True" OnTextChanged="txtLoanAppAmount_TextChanged"
                            ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    </td>
                </tr>
                <tr>
                    <td>
                          <asp:Label ID="lblLoanInterestRate" runat="server" Text=" Interest Rate:" Font-Size="Large"
                        ForeColor="Red" onkeydown="return (event.keyCode !=13);"></asp:Label>
                    </td>
                    <td>

                         <asp:TextBox ID="txtLoanInterestRate" runat="server" CssClass="cls text" Width="113px"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Label ID="lblNoInstallment" runat="server" Text="No Of Installment:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                        &nbsp; &nbsp;<asp:TextBox ID="txtNoInstallment" runat="server" CssClass="cls text"
                            Width="100px" Height="25px"
                            Font-Size="Large" AutoPostBack="True"
                            OnTextChanged="txtNoInstallment_TextChanged"></asp:TextBox>
                        
                        <%--<asp:Label ID="lblLoanGracePeriod" runat="server" Font-Size="Large" ForeColor="Red" Text=" Grace Period:"></asp:Label>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                         <asp:TextBox ID="txtLoanGracePeriod" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px"></asp:TextBox>--%>
                  
                   
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLoanCategory" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Category:"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddlLoanCategory" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="150px">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                            <asp:ListItem Value="1">General</asp:ListItem>
                            <asp:ListItem Value="2">Emergency</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lblLoanInstallmentAmount" runat="server" Font-Size="Large" ForeColor="Red" Text=" Installment Amount:"></asp:Label>&nbsp;<asp:TextBox ID="txtLoanInstallmentAmount" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="170px" AutoPostBack="True" OnTextChanged="txtLoanInstallmentAmount_TextChanged" onFocus="javascript:this.select();"></asp:TextBox>
                    </td>
                </tr>
               


                <tr>
                    <td>
                        <asp:Label ID="lblSuretyMemNo" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Surety Member No:"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:TextBox ID="txtSuretyMemNo" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblLoanLastInstlAmount" runat="server" Font-Size="Large" ForeColor="Red" Text="Last Installment Amount:"></asp:Label>&nbsp;<asp:TextBox ID="txtLoanLastInstlAmount" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="170px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLoanPurpose" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Purpose:"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddlLoanPurpose" runat="server" CssClass="cls text" Font-Size="Large" Height="26px" Width="293px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;

                        <asp:Label ID="lblLoanStatDate" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Expire Date:"></asp:Label>
                        <asp:TextBox ID="txtLoanExpDate" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="115px" img src="../Images/calender.png"></asp:TextBox>
                        <%--<asp:Label ID="lblLoanFirstInstlDate" runat="server" Font-Size="Large" ForeColor="Red" Text="First Installment Date:"></asp:Label>
                        <asp:TextBox ID="txtLoanFirstInstlDate" runat="server" CssClass="cls text" Font-Size="Large" Height="25px"  Width="115px" img src="../Images/calender.png" AutoPostBack="True" OnTextChanged="txtLoanFirstInstlDate_TextChanged"></asp:TextBox>--%>
                        
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <%--<img src="../Images/calender.png" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>


                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large"
                            ForeColor="White" Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                            OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
  
    <br />

     
  <asp:Label ID="lblApplicationNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewAppNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTypeCls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="MsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccountNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccBalance" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblPrevInstlAmount" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
