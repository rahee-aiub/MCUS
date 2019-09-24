<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSLoanDefaulterReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSLoanDefaulterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            var ddlAccType = document.getElementById('<%=ddlAccType.ClientID%>');
            var txtDueMthFrom = document.getElementById('<%=txtDueMthFrom.ClientID%>').value;
            var txtDueMthTill = document.getElementById('<%=txtDueMthTill.ClientID%>').value;


            if ((ddlAccType.selectedIndex == 0)) {
                document.getElementById('<%=ddlAccType.ClientID%>').focus();
                alert('Please Select Account Type');
            }
            else
                if (txtDueMthFrom == '' || txtDueMthFrom.length == 0) {
                    document.getElementById('<%=txtDueMthFrom.ClientID%>').focus();
                    alert('Please Input Due No.Month');
                }
                else
                if (txtDueMthTill == '' || txtDueMthTill.length == 0) {
                        document.getElementById('<%=txtDueMthFrom.ClientID%>').focus();
                        alert('Please Input Due Upto Month');
                }
                else
                    return confirm('Are you sure you want to Proceed?');
            return false;


        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 363px;
            margin: 0 auto;
            width: 1787px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            Width: 1717px;
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
                    <th colspan="3">Loan Defaulter Report
                    </th>
                </tr>
            </thead>


            <tr>
                <td>
                    <asp:Label ID="lblPeriod" runat="server" Text="Month of :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPeriodMM" runat="server" Height="25px" Width="200px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">January </asp:ListItem>
                        <asp:ListItem Value="2">February </asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlPeriodYYYY" runat="server" Height="25px" Width="100px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                    </asp:DropDownList>


                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAccType" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">
                    </asp:DropDownList>

                </td>
            </tr>



            <tr>
                <td>
                    <asp:Label ID="lblDueMonth" runat="server" Text="Due No.Month :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDueMthFrom" runat="server" CssClass="cls text" Width="75px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                     <asp:Label ID="lblUptoMth" runat="server" Text="Upto :" Font-Size="Medium"
                         ForeColor="Red"></asp:Label>

                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtDueMthTill" runat="server" CssClass="cls text" Width="75px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"></asp:TextBox>


                </td>
            </tr>




            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnView" runat="server" Text="View" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnView_Click" />&nbsp;
                    <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnPrint_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>

    <div align="left" class="grid_scroll">
        <asp:GridView ID="gvLoanDefaulter" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvLoanDefaulter_RowDataBound" OnSelectedIndexChanged="gvLoanDefaulter_SelectedIndexChanged">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="65px">
                    <ItemTemplate>
                        <asp:Button ID="BtnSelect" runat="server" Text="Detail" Width="68px" CssClass="button green" OnClick="BtnSelect_Click" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="CuType" HeaderStyle-Width="68px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCuType" runat="server" Width="68px" Text='<%# Eval("CuType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CuNo" HeaderStyle-Width="68px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCuNo" runat="server" Width="68px" Text='<%# Eval("CuNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CuU No." HeaderStyle-Width="68px">
                    <ItemTemplate>
                        <asp:Label ID="lblCuNumber" runat="server" Width="68px" Text='<%# Eval("CuNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Depositor" HeaderStyle-Width="68px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblMemNo" runat="server" Width="68px" Text='<%# Eval("MemNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AccType" HeaderStyle-Width="68px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblAccType" runat="server" Width="68px" Text='<%# Eval("AccType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AccNo" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <asp:Label ID="lblAccNo" runat="server" Width="130px" Text='<%# Eval("AccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="380px">
                    <ItemTemplate>
                        <asp:Label ID="lblMemName" runat="server" Width="380px" Text='<%# Eval("MemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Due No.Mth." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblDueMonth" runat="server" Width="100px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DueNoInstl"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Due Princ.Amt." HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblDuePrinc" runat="server" Width="120px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DuePrincAmt"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Due Int.Amt." HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblDueInt" runat="server" Width="120px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DueIntAmt"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Open Date" DataField="AccOpenDate" HeaderStyle-Width="95px" ItemStyle-Width="95px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField HeaderText="Exp. Date" DataField="AccLoanExpiryDate" HeaderStyle-Width="75px" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:dd/MM/yyyy}" />

                <asp:TemplateField HeaderText="Sanc.Amt." HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblSancAmt" runat="server" Width="120px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("AccLoanSancAmt"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Int.Rate" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblIntRate" runat="server" Width="70px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("AccIntRate"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="No.Instl." HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblNoInstl" runat="server" Width="70px" Text='<%# Eval("AccNoInstl") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Outs.Bal." HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblOutsBal" runat="server" Width="120px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("AccBalance"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>

        </asp:GridView>

    </div>

   
    <asp:Label ID="hdnPeriod" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMonth" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnYear" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMsg" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnToDaysDate" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="CtrlFlag" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>
