<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmpStatusChangeMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmpStatusChangeMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/styletext.css" rel="stylesheet" />

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 190px;
            width: 440px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }

        .auto-style5 {
            width: 132px;
        }
    </style>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />--%>


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtEmpStatDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtEmpStatDate.ClientID %>").datepicker();

            });

        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />

    <div align="center">
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="lblEmpNo" runat="server" Text="Employee No  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSumbit" runat="server" Text="Submit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="24px" Width="86px" Font-Bold="True" ToolTip="Submit" CausesValidation="False"
                        CssClass="button green" OnClick="btnSumbit_Click" />


                </td>
                <%--<td>
                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="textbox" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" AutoPostBack="true" OnTextChanged="txtEmpNo_TextChanged"></asp:TextBox>

                </td>--%>
            </tr>
        </table>

        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Name  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblName" runat="server" Text="" Font-Size="Large"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="lblName" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <br />

                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Designation  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblDesign" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%--<td>
                    <asp:Label ID="lblDesign" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <br />

                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Grade  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%-- <td>
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>--%>
            </tr>
        </table>
    </div>
  
    <div align="center">
        <table class="style1" >

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblExsitStatus" runat="server" Text="Existing Status:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlExsitStatus" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="2">Resigned</asp:ListItem>
                        <asp:ListItem Value="3">Retired</asp:ListItem>
                        <asp:ListItem Value="4">LPR</asp:ListItem>
                        <asp:ListItem Value="5">Terminated</asp:ListItem>
                        <asp:ListItem Value="6">Dismissed</asp:ListItem>
                        <asp:ListItem Value="7">Discharged</asp:ListItem>
                        <asp:ListItem Value="8">Suspended</asp:ListItem>
                        <asp:ListItem Value="9">FinalSettlement</asp:ListItem>
                        <asp:ListItem Value="10">Death</asp:ListItem>
                        <asp:ListItem Value="99">Stop Salary Payment</asp:ListItem>

                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td >
                    <asp:Label ID="lblExistDate" runat="server" Text="Existing Status Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtExistDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="190px" Height="25px" Font-Size="Medium"  img src="../Images/calender.png" Enabled="False" ></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblNewStatus" runat="server" Text="New Status:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewStatus" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="2">Resigned</asp:ListItem>
                        <asp:ListItem Value="3">Retired</asp:ListItem>
                        <asp:ListItem Value="4">LPR</asp:ListItem>
                        <asp:ListItem Value="5">Terminated</asp:ListItem>
                        <asp:ListItem Value="6">Dismissed</asp:ListItem>
                        <asp:ListItem Value="7">Discharged</asp:ListItem>
                        <asp:ListItem Value="8">Suspended</asp:ListItem>
                        <asp:ListItem Value="9">FinalSettlement</asp:ListItem>
                        <asp:ListItem Value="10">Death</asp:ListItem>
                        <asp:ListItem Value="99">Stop Salary Payment</asp:ListItem>

                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td >
                    <asp:Label ID="lblEmpStatdate" runat="server" Text="New Status Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtEmpStatDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="190px" Height="25px" Font-Size="Medium"  img src="../Images/calender.png" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>


    <br />
    <br />
    <div align="center">
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
            Height="22px" OnClick="BtnUpdate_Click" />&nbsp;
        <asp:Button ID="BtnView" runat="server" Text="History" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="History Information" CssClass="button green" 
            Height="22px" OnClick="BtnView_Click" />&nbsp;
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField HeaderText="EmpCode" DataField="EmpCode" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="EffectDate" HeaderText="Effect Date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField HeaderText="Status" DataField="StatusDesc" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                

            </Columns>

        </asp:GridView>

    </div>


    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlStatus" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

