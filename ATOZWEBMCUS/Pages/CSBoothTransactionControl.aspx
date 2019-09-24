<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSBoothTransactionControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSBoothTransactionControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtProcessDate.ClientID %>").dynDateTime({
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
            $("#<%= txtProcessDate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtProcessDate.ClientID %>").datepicker();

             });

         });

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 320px;
            margin: 0 auto;
            width: 1000px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />
    <div align="center">
        <table class="style1">
            <tr>
                <th>Booth Transaction Control
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProcessDate" runat="server" Text="Process Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProcessDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="150px" Height="25px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblCtrlMode" runat="server" Text="Control Mode :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList
                        ID="ddlCtrlMode" runat="server" Height="25px" Width="280px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        <asp:ListItem Value="1">All</asp:ListItem>
                        <asp:ListItem Value="2">Only Transaction</asp:ListItem>
                        <asp:ListItem Value="3">No Transaction</asp:ListItem>
                        <asp:ListItem Value="4">No Process Done</asp:ListItem>
                        <asp:ListItem Value="5">Process Done</asp:ListItem>
                        <asp:ListItem Value="6">Process Done With Transaction</asp:ListItem>
                        <asp:ListItem Value="7">Process Done With No Transaction</asp:ListItem>
                    </asp:DropDownList>



                </td>
            </tr>



        </table>
    </div>
    <br />
    <div align="center">


        <asp:Button ID="BtnProcess" runat="server" Text="Print/Preview" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green"
            Height="24px" OnClick="BtnProcess_Click" />&nbsp;
        <asp:Button ID="BtnSearch" runat="server" Text="Process" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green"
            Height="24px" OnClick="BtnSearch_Click" />
        &nbsp;
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>
    <br />
    <div align="center">


        <table class="style1">
            <thead>
                <tr>
                    <th>

                        <asp:Label ID="Label1" runat="server" Text="Total Booth :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblTotalBooth" runat="server" Font-Size="large"
                            ForeColor="Black"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Transaction Booth :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblTotTrnBooth" runat="server" Font-Size="large"
                            ForeColor="Black"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" Text="No Transaction Booth :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblTotNoTrnBooth" runat="server" Font-Size="large"
                            ForeColor="Black"></asp:Label>

                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <%--<p align="center" style="color: blue">
                            Booth Transaction Control - Spooler
                        </p>--%>

    <div id="DivGridViewCancle" runat="server" align="center" class="grid_scroll">

        <asp:GridView ID="gvBoothControlInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvBoothControlInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:BoundField DataField="ProcessDate" HeaderText="Process Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="CashCodeNo" HeaderText="Booth No" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CashCodeName" HeaderText="Booth Name" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="TrnDebit" HeaderText="Debit Amount" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150px" ItemStyle-Width="150px" />
                <asp:BoundField DataField="TrnCredit" HeaderText="Credit Amount" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="150px" ItemStyle-Width="150px" />
                <asp:BoundField DataField="StatusName" HeaderText="Status" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Blue" />
                
            </Columns>
        </asp:GridView>


    </div>

      <asp:Label ID="lblTransFlag" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

