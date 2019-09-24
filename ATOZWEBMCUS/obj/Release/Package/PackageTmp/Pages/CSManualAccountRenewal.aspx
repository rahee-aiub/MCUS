<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSManualAccountRenewal.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSManualAccountRenewal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtTranDate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtTranDate.ClientID %>").datepicker();

             });

         });


    </script>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed ?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Print or Preview information??');
        }

    </script>


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width: 1200px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 1180px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Manual Renewal Processing
                    </th>
                </tr>
            </thead>

           


            <tr>
                <td style="background-color: #fce7f9">

                    <asp:Label ID="lblAccNo" runat="server" Text="Account No. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td style="background-color: #fce7f9">

                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="1" AutoPostBack="True" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>&nbsp;
                   <asp:Label ID="lblAccTitle" runat="server" Text=""></asp:Label>

                </td>

            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblTranDate" runat="server" Text="Process Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTranDate" runat="server" Enabled="false" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnTextChanged="txtTranDate_TextChanged" AutoPostBack="True"></asp:TextBox>

                </td>
            </tr>

            <%--<tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Text="Voucher No. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnCalculate" runat="server" Text="Calculate" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnCalculate_Click" />&nbsp;
                       &nbsp;
                      <asp:Button ID="BtnPost" runat="server" Text="Post" Font-Size="Large" ForeColor="White"
                          Font-Bold="True" CssClass="button blue" OnClick="BtnPost_Click" OnClientClick="return ValidationBeforeSave()" />&nbsp;
                    <asp:Button ID="BtnReverse" runat="server" Text="Reverse" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnReverse_Click" OnClientClick="return ValidationBeforeSave()" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    <div align="left" class="grid_scroll">
        <asp:GridView ID="gvDetailInfoFDR" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfoFDR_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>

                <asp:BoundField HeaderText="CU No." DataField="CuNumber" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="85px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="AccType" DataField="AccType" HeaderStyle-Width="83px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Name" DataField="MemName" HeaderStyle-Width="425px" ItemStyle-Width="430px" />
                <asp:BoundField HeaderText="FD Amount" DataField="FDAmount" DataFormatString="{0:0,0.00}" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="Rate" DataField="AccIntRate" DataFormatString="{0:0,0.00}" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField HeaderText="Days" DataField="NoDays" DataFormatString="{0:0,0.00}" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField HeaderText="Interest" DataField="CalInterest" DataFormatString="{0:0,0.00}" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />

            </Columns>

        </asp:GridView>
        <asp:GridView ID="gvDetailInfo6YR" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo6YR_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>

                <asp:BoundField HeaderText="CU No." DataField="CuNumber" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="85px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="AccType" DataField="AccType" HeaderStyle-Width="83px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Name" DataField="MemName" HeaderStyle-Width="425px" ItemStyle-Width="430px" />
                <asp:BoundField HeaderText="FD Amount" DataField="FDAmount" DataFormatString="{0:0,0.00}" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="Rate" DataField="NewIntRate" DataFormatString="{0:0,0.00}" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField HeaderText="Days" DataField="NoDays" DataFormatString="{0:0,0.00}" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField HeaderText="Interest" DataField="CalInterest" DataFormatString="{0:0,0.00}" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />

            </Columns>

        </asp:GridView>
    </div>
    <%-- <div align="right" style="width: 1290px; height: 24px;">
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>

             
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="txtTotalFDAmt" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True" Font-Overline="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="txtTotalIntRate" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             
    </div>--%>


    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMsgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblPdate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

