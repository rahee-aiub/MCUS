<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSBalanceCheckReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSBalanceCheckReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 34px;
        }

        .auto-style5 {
            height: 34px;
            width: 557px;
        }

        .auto-style7 {
            height: 28px;
        }

        .auto-style8 {
            width: 60px;
        }

        .auto-style9 {
            height: 28px;
            width: 60px;
        }

        .auto-style11 {
            width: 557px;
        }

        .auto-style12 {
            height: 28px;
            width: 557px;
        }
    </style>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 350px;
            margin: 0 auto;
            width: 800px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            Width: 783px;
        }
    </style>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDate.ClientID %>").dynDateTime({
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
            $("#<%= txtDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtDate.ClientID %>").datepicker();

             });

        });
         
            </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />
    <br />

    <%--<div id = "DivSelectOption" align="center">
        <table class="style1">
                <thead>
                <tr>
                    <th colspan="5" class="auto-style1">
                       Ledger Balance Reports - Select Option
                    </th>
                </tr>

            </thead>
            <tr>

                <td>

                    <asp:RadioButton ID="rbtLedgerBalance" runat="server" Text="  Upto Ledger Balance Report " Checked="True" GroupName="GRN" />
                &nbsp;</td>
               
               

            </tr>
             <tr>

                <td>

                    
                </td>

            </tr>


               

    
            </table>
        </div>


    
     <br />--%>


    <div id="DivParameter" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="7">Balance Check Reports
                    </th>
                </tr>

            </thead>

            <tr>
                <td colspan="7" style="background-color: #fce7f9" class="auto-style8">
                    <%-- <td colspan ="7">--%>

                </td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9" class="auto-style8"></td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblDate" runat="server" ForeColor="Red" Text="Balance Date  "></asp:Label>
                </td>
                <%--<td class="auto-style11">--%>
                <td style="background-color: #fce7f9" class="auto-style8">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9" class="auto-style8"></td>
                <td style="background-color: #fce7f9">

                    <asp:Label ID="lblAccType" runat="server" ForeColor="Red" Text="A/C Type"></asp:Label>

                </td>

                <td class="auto-style5">
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlAcType" runat="server" Height="31px" Width="441px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAcType_SelectedIndexChanged" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>


            </tr>


           
            <tr>

                <%--<td colspan ="2" class="auto-style10">--%>


                <td colspan="5" style="background-color: #fce7f9" class="auto-style8">

                    <%--<td colspan ="5" class="auto-style3">--%>
                    <%--<td colspan="5" style="background-color: #fce7f9" class="auto-style8">--%>
                        <asp:Button ID="BtnView" runat="server" Text="Preview" Font-Size="Large" ForeColor="White"
                            Font-Bold="True" Height="27px" Width="100px" CssClass="button green" OnClick="BtnView_Click" />&nbsp;

                     &nbsp;
                    <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="White"
                            Font-Bold="True" Height="27px" Width="100px" CssClass="button blue" OnClick="BtnPrint_Click" />&nbsp;

                     &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="100px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />




                        <br />
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
            </tr>


        </table>
    </div>


     <div align="left" class="grid_scroll">
        <asp:GridView ID="gvBalanceCheck" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvBalanceCheck_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

              
                <asp:TemplateField HeaderText="A/c Type" HeaderStyle-Width="68px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblAccType" runat="server" Width="68px" Text='<%# Eval("AccType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="A/c Title" HeaderStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="lblTrnDes" runat="server" Width="250px" Text='<%# Eval("TrnDes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ledger Balance" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblLedgerBalance" runat="server" Width="150px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("LedgerBalance"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Journal Balance" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblJournalBalance" runat="server" Width="150px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("JournalBalance"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Deffer Balance" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblDefferlBalance" runat="server" Width="150px" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("DefferlBalance"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </div>

     <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
