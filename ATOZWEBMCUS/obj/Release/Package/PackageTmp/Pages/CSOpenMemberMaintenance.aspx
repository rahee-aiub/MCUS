<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSOpenMemberMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSOpenMemberMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {


            var txtCreditUNo = document.getElementById('<%=txtCreditUNo.ClientID%>').value;

              var txtCULBMemName = document.getElementById('<%=txtCULBMemName.ClientID%>').value;

              var txtOpenDate = document.getElementById('<%=txtOpenDate.ClientID%>').value;

              if (txtCreditUNo == '' || txtCreditUNo.length == 0)
                  alert('Please Input Credit Union No.');

              else
                  if (txtCULBMemName == '')
                      alert('Please Input Depositor Name');

                  else
                      if (txtOpenDate == '' || txtOpenDate.length == 0)
                          alert('Please Input Open Date');
                      else
                          return confirm('Are you sure you want to save information?');
              return false;


              return confirm('Are you sure you want to save information?');
          }

          function ValidationBeforeUpdate() {
              return confirm('Are you sure you want to Update information?');
          }

    </script>

    <style type="text/css">
        .style2 {
            width: 218px;
        }

        .style3 {
            width: 216px;
        }

        .style4 {
            width: 213px;
        }

        .style5 {
            width: 208px;
        }

        .style6 {
            width: 206px;
        }

        .style7 {
            width: 143px;
        }

        .auto-style1 {
            height: 33px;
        }
    </style>

    <%--<link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" />
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/styletext.css" rel="stylesheet" />

    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>--%>


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtOpenDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtOpenDate.ClientID %>").datepicker();

            });

        });
        $(function () {
            $("#<%= txtDateOfBirth.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtDateOfBirth.ClientID %>").datepicker();

            });

        });
        $(function () {
            $("#<%= txtPassportIssueDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtPassportIssueDate.ClientID %>").datepicker();

            });

        });
        $(function () {
            $("#<%= txtPassportExpdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtPassportExpdate.ClientID %>").datepicker();

            });

        });
        $(function () {
            $("#<%= txtLastTaxPdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtLastTaxPdate.ClientID %>").datepicker();

            });

        });
    </script>


    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtOpenDate.ClientID %>").dynDateTime({
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
        $("#<%=txtDateOfBirth.ClientID %>").dynDateTime({
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
        $("#<%=txtPassportIssueDate.ClientID %>").dynDateTime({
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
        $("#<%=txtPassportExpdate.ClientID %>").dynDateTime({
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
        $("#<%=txtLastTaxPdate.ClientID %>").dynDateTime({
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div align="left">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Open Depositor Maintenance
                    </th>
                </tr>

            </thead>


            <tr>
                <td class="style7">
                    <asp:Label ID="lblCUNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblCuName" runat="server" Text=""></asp:Label>
                    <asp:DropDownList ID="ddlCreditUNo" runat="server" Height="25px" Width="504px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlCreditUNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>

                    <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <%--                    <asp:Label ID="lblCULBMemNo" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>--%>
                    <asp:Label ID="lblMemType" runat="server" Text="Depositor Type" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <%-- <asp:TextBox ID="txtCULBMemNo" runat="server" CssClass="cls text" Width="118px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                        OnTextChanged="txtCULBMemNo_TextChanged"></asp:TextBox>
                    
                    <asp:DropDownList ID="ddlCULBMemNo" runat="server" Height="25px" Width="504px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlCULBMemNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;&nbsp;--%>
                    <asp:DropDownList
                        ID="ddlMemType" runat="server" Height="25px" Width="150px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Premium</asp:ListItem>
                        <asp:ListItem Value="2">General</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="lblCULBMemName" runat="server" Text="Depositor Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCULBMemName" runat="server" CssClass="cls text" Width="390px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lblSpouseName" runat="server" Text="Spouse Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtSpouseName" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="lblFatherName" runat="server" Text="Father Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="cls text" Width="390px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lblMotherName" runat="server" Text="Mother Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtMotherName" runat="server" CssClass="cls text" Width="398px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="lblOccupation" runat="server" Text="Occupation:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOccupation" runat="server" Height="25px" Width="250px" CssClass="cls text"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged">
                    </asp:DropDownList>

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Label ID="lblNature" runat="server" Text="Nature:" Font-Size="Large" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlNature" runat="server" Height="25px" Width="150px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlNature_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="lblNationality" runat="server" Text="Nationality:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNationality" runat="server" Height="25px" Width="250px"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlNationality_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblReligion" runat="server" Text="Religion:" Font-Size="Large" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlReligion" runat="server" Height="25px" Width="150px" CssClass="cls text"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="lblGender" runat="server" Text="Gender:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGender" runat="server" Height="25px" Width="250px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Male</asp:ListItem>
                        <asp:ListItem Value="2">Female</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlMaritalStatus" runat="server" Height="25px" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                        CssClass="cls text" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Single</asp:ListItem>
                        <asp:ListItem Value="2">Married</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="lblOpenDate" runat="server" Text="Open Date:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOpenDate" runat="server" CssClass="cls text" Width="140px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnTextChanged="txtOpenDate_TextChanged" AutoPostBack="True"></asp:TextBox>

                    &nbsp;&nbsp;
                    <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;
                  <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="cls text" Width="140px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                      Font-Size="Large" OnTextChanged="txtDateOfBirth_TextChanged" AutoPostBack="True"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblAge" runat="server" Text="Age:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtAge" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>&nbsp; &nbsp;
                    <asp:Label ID="lblPlaceOfBirth" runat="server" Text="Place Of Birth:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtPlaceOfBirth" runat="server" CssClass="cls text" Width="135px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnPresentAddress" runat="server" Text="Present Address" Style="background-color: Silver"
                        Width="212px" OnClick="btnPresentAddress_Click" />
                    <asp:Button ID="btnPerAddress" Style="background-color: InactiveCaption" runat="server"
                        Text="Permanent Address" Width="212px" OnClick="btnPerAddress_Click" />
                    <asp:Button ID="btnOtherInfo" runat="server" Text="Other Information" Style="background-color: ActiveCaption"
                        Width="212px" OnClick="btnOtherInfo_Click" />
                </td>
            </tr>
        </table>
        <div style="background-color: Silver; border: 1px">
            <asp:Panel ID="pnlPreAddress" runat="server" Height="236px" Width="1215px">
                <table>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="lblAddL1" runat="server" Text="Address Line1:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtAddressL1" runat="server" CssClass="cls text" Width="360px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAddL2" runat="server" Text="Address Line2:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressL2" runat="server" CssClass="cls text" Width="360px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAddL3" runat="server" Text="Address Line3:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressL3" runat="server" CssClass="cls text" Width="359px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTelNo" runat="server" Text="Telephone No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelNo" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMobNo" runat="server" Text="Mobile No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="E-mail:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDivision" runat="server" Text="Division:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDivision" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                    <asp:Label ID="lblDistrict" runat="server" Text="District:" Font-Size="Large" ForeColor="Red"></asp:Label>
                            <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                    <asp:Label ID="lblUpzila" runat="server" Text="Upazila:" Font-Size="Large" ForeColor="Red"></asp:Label>
                            <asp:DropDownList ID="ddlUpzila" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlUpzila_SelectedIndexChanged">
                            </asp:DropDownList>

                            &nbsp;
                    <asp:Label ID="lblThana" runat="server" Text="Thana:" Font-Size="Large" ForeColor="Red"></asp:Label>
                            <asp:DropDownList ID="ddlThana" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlThana_SelectedIndexChanged">
                            </asp:DropDownList>

                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="background-color: InactiveCaption; border: 1px">
            <asp:Panel ID="pnlPermanent" runat="server" Height="236px" Width="1215px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblPerAddL1" runat="server" Text="Address Line1:" Font-Size="Large"
                                ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPerAddL1" runat="server" CssClass="cls text" Width="360px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPerAddL2" runat="server" Text="Address Line2:" Font-Size="Large"
                                ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPerAddL2" runat="server" CssClass="cls text" Width="360px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPerAddL3" runat="server" Text="Address Line3:" Font-Size="Large"
                                ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPerAddL3" runat="server" CssClass="cls text" Width="359px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPerTelNo" runat="server" Text="Telephone No:" Font-Size="Large"
                                ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPerTelNo" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPerMobNo" runat="server" Text="Mobile No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPerMobNo" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPerEmail" runat="server" Text="E-mail:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPerEmail" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Division:" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPerDivision" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlPerDivision_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="District:" Font-Size="Large" ForeColor="Red"></asp:Label>
                            <asp:DropDownList ID="ddlPerDistrict" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlPerDistrict_SelectedIndexChanged">
                            </asp:DropDownList>

                            &nbsp;
                    <asp:Label ID="Label4" runat="server" Text="Upazila:" Font-Size="Large" ForeColor="Red"></asp:Label>
                            <asp:DropDownList ID="ddlPerUpzila" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlPerUpzila_SelectedIndexChanged">
                            </asp:DropDownList>

                            &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Thana:" Font-Size="Large" ForeColor="Red"></asp:Label>
                            <asp:DropDownList ID="ddlPerThana" runat="server" Height="25px" Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlPerThana_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="background-color: ActiveCaption; border: 1px">
            <asp:Panel ID="pnlOtherInfo" runat="server" Height="222px" Width="1215px">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNationalID" runat="server" Text="National Id No:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNationalID" runat="server" CssClass="cls text" Width="268px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPassportNo" runat="server" Text="Passport No:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassportNo" runat="server" CssClass="cls text" Width="268px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPassportIssuePlace" runat="server" Text="Passport Issue Place:"
                                            Font-Size="Large" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassportIssuePlace" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTIN" runat="server" Text="TIN:" Font-Size="Large" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTIN" runat="server" CssClass="cls text" Width="185px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPassportIssueDate" runat="server" Text="Passport Issue Date:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassportIssueDate" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large" img src="../Images/calender.png"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPassportExpdate" runat="server" Text="Passport Exp.Date:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassportExpdate" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large" img src="../Images/calender.png"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLastTaxPdate" runat="server" Text="Last Tax Pay date:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastTaxPdate" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large" img src="../Images/calender.png"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmpName" runat="server" Text="Employee Name:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="cls text" Width="226px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmpAddress" runat="server" Text="Address:" Font-Size="Large" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpAddress" runat="server" CssClass="cls text" Width="226px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIntroducerMem1" runat="server" Text="Introducer Member1:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIntroducerMem1" runat="server" CssClass="cls text" Width="225px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIntroducerName1" runat="server" Text="Introducer Name1:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIntroducerName1" runat="server" CssClass="cls text" Width="226px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIntroducerMem2" runat="server" Text="Introducer Member2:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIntroduceMem2" runat="server" CssClass="cls text" Width="226px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIntroducerName2" runat="server" Text="Introducer Name2:" Font-Size="Large"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIntroducerName2" runat="server" CssClass="cls text" Width="226px" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Height="25px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <table>
            <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClick="BtnSubmit_Click" OnClientClick="return ValidationBeforeSave()" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
                        OnClick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCUType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCUNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuTypeName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="ProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="GetOpenDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuOpenDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbllastMemno" runat="server" Text="" Visible="false"></asp:Label>




</asp:Content>

