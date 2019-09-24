<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLAccountStatement.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLAccountStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../scripts/validation.js" type="text/javascript"></script>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtfdate.ClientID %>").dynDateTime({
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
            $("#<%=txttdate.ClientID %>").dynDateTime({
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
            $("#<%= txtfdate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtfdate.ClientID %>").datepicker();

            });

         });
        $(function () {
            $("#<%= txttdate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txttdate.ClientID %>").datepicker();

             });

         });

    </script>

    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }

        .auto-style3 {
            height: 37px;
        }

        .auto-style4 {
            height: 31px;
        }

        .auto-style5 {
            height: 36px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">GL Account Statement Report
                    </th>
                </tr>

            </thead>

            <tr>

                <td style="background-color: #fce7f9" class="auto-style3">
                    <asp:Label ID="lblGLCode" runat="server" Text="GL Account Code " Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9" class="auto-style3">:
                </td>

                <td style="background-color: #fce7f9" class="auto-style3">
                    <asp:TextBox ID="txtGLCode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="120px" onkeypress="return IsNumberKey(event)"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name" AutoPostBack="True" OnTextChanged="txtGLCode_TextChanged"></asp:TextBox>

                    &nbsp;<asp:DropDownList ID="ddlGLCode" runat="server" Height="28px" BorderColor="#1293D1" BorderStyle="Ridge" Width="396px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGLCode_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack2" runat="server" Text="<<" Font-Size="Medium" ForeColor="Red"
                        Font-Bold="True" CssClass="button green" OnClick="btnBack2_Click" />

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    
                    &nbsp;&nbsp;</td>

            </tr>



            <tr>

                <td style="background-color: #fce7f9" class="auto-style5">
                    <asp:Label ID="lblfdate" runat="server" Text="From Date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>


                </td>
                <td style="background-color: #fce7f9" class="auto-style5">:
                </td>

                <td style="background-color: #fce7f9" class="auto-style5">
                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px" onkeypress="return IsNumberKey(event)"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td style="background-color: #fce7f9" class="auto-style3">
                    <asp:Label ID="lbltdate" runat="server" Text="To Date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>


                </td>
                <td style="background-color: #fce7f9" class="auto-style3">:

                </td>
                <td style="background-color: #fce7f9" class="auto-style3">

                    <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px" onkeypress="return IsNumberKey(event)"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>
                </td>


            </tr>

            <tr>
                <td style="background-color: #fce7f9" class="auto-style3"></td>
                <td style="background-color: #fce7f9" class="auto-style3"></td>
                <td style="background-color: #fce7f9" class="auto-style3"></td>
            </tr>







        </table>
    </div>

    <br />

    <div id="DivReportOption" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th class="auto-style4">Reports Name  Option
                    </th>
                </tr>

            </thead>

            <tr>

                <td>
                    <asp:RadioButton ID="rbtGLAccStatement" runat="server" GroupName="GLRptGrp" Text=" GL Account Statement Report [Detail]" AutoPostBack="True" Checked="True" />
                </td>

            </tr>
            <tr>

                <td>
                    <asp:RadioButton ID="rbtGLAccStVchConsolated" runat="server" GroupName="GLRptGrp" Text=" GL Account Statement Report ( Vch. wise Consolidated )" AutoPostBack="True" Checked="false" />
                </td>

            </tr>

            <tr>

                <td>
                    <asp:RadioButton ID="rbtGLAccStDailyConsolated" runat="server" GroupName="GLRptGrp" Text=" GL Account Statement Report ( Day wise Consolidated )" AutoPostBack="True" Checked="false" />
                </td>

            </tr>
            <tr>

                <td colspan="3" class="auto-style4"></td>

            </tr>
            <tr>



                <td class="auto-style1">


                    <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Height="36px" Width="160px" Font-Bold="True" CssClass="button green" OnClick="BtnView_Click" />&nbsp;
                                      &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="36px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>




            </tr>



        </table>
    </div>



    <asp:Label ID="CtrlRecType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnTranCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead4" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="HdnModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnPrmValue" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="glMainHead" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="glSubHead" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
