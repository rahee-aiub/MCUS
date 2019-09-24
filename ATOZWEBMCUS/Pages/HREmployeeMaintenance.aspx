<%@ Page Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true"
    CodeBehind="HREmployeeMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmployeeMaintenance"
    Title="Employee Maintenance" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../scripts/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>

    <script src="../scripts/validation.js" type="text/javascript"></script>

    <script src="../scripts/ui/1.9.1/jquery-ui.js" type="text/javascript"></script>

    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtDOB.ClientID %>").datepicker();
        });
    </script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtJoinDate.ClientID %>").datepicker();
        });
    </script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtConfrimDate.ClientID %>").datepicker();
        });
    </script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtPromotionDate.ClientID %>").datepicker();
        });
    </script>

    <script language="javascript" type="text/javascript">
        function ValidationEmployee() {

            var txtEmpName = document.getElementById('<%=txtEmpName.ClientID%>').value;
            var txtEmpFileNo = document.getElementById('<%=txtEmpFileNo.ClientID%>').value;
            var txtJoinDate = document.getElementById('<%=txtJoinDate.ClientID%>').value;
            var txtDob = document.getElementById('<%=txtDOB.ClientID%>').value;

            var ddlDesignation = document.getElementById('<%=ddlDesignation.ClientID %>');
            var ddlLocation = document.getElementById('<%=ddlLocation.ClientID %>');
            var ddlDepartment = document.getElementById('<%=ddlDepartment.ClientID %>');

            var ddlSection = document.getElementById('<%=ddlSection.ClientID %>');
            var ddlServiceType = document.getElementById('<%=ddlServiceType.ClientID %>');


            if (txtEmpName == '' || txtEmpName.length == 0)
                alert('Please Input Employee Name.');

            else if (txtEmpFileNo == '' || txtEmpFileNo.length == 0)
                alert('Please Input Employee ID.');

            else if (txtJoinDate == '' || txtJoinDate.length == 0)
                alert('Please Input Join Date.');
            else if (txtDob == '' || txtDob.length == 0)
                alert('Please Input Date of Birth.');

            else if (ddlDesignation.selectedIndex == 0)
                alert('Please Select Designation.');

            else if (ddlLocation.selectedIndex == 0)
                alert('Please Select Location.');

            else if (ddlDepartment.selectedIndex == 0)
                alert('Please Select Department.');

            else if (ddlSection.selectedIndex == 0)
                alert('Please Select Section.');

            else if (ddlServiceType.selectedIndex == 0)
                alert('Please Select ServiceType.');

            else
                return confirm('Are you sure you want save the data');
            return false;
        }

        function ValidationBeforeUpdate() {

            var txtEmployeMody = document.getElementById('<%=txtEmployeMody.ClientID%>').value;
            var txtEmpName = document.getElementById('<%=txtEmpName.ClientID%>').value;
            var txtEmpFileNo = document.getElementById('<%=txtEmpFileNo.ClientID%>').value;
            var txtJoinDate = document.getElementById('<%=txtJoinDate.ClientID%>').value;
            var txtDob = document.getElementById('<%=txtDOB.ClientID%>').value;

            var ddlDesignation = document.getElementById('<%=ddlDesignation.ClientID %>');
            var ddlLocation = document.getElementById('<%=ddlLocation.ClientID %>');
            var ddlDepartment = document.getElementById('<%=ddlDepartment.ClientID %>');

            var ddlSection = document.getElementById('<%=ddlSection.ClientID %>');
            var ddlServiceType = document.getElementById('<%=ddlServiceType.ClientID %>');


            if (txtEmployeMody == '' || txtEmployeMody.length == 0)
                alert('Please Input Employee Code.');

            else if (txtEmpName == '' || txtEmpName.length == 0)
                alert('Please Input Employee Name.');

            else if (txtEmpFileNo == '' || txtEmpFileNo.length == 0)
                alert('Please Input Employee ID.');

            else if (txtJoinDate == '' || txtJoinDate.length == 0)
                alert('Please Input Join Date.');
            else if (txtDob == '' || txtDob.length == 0)
                alert('Please Input Date of Birth.');

            else if (ddlDesignation.selectedIndex == 0)
                alert('Please Select Designation.');

            else if (ddlLocation.selectedIndex == 0)
                alert('Please Select Location.');

            else if (ddlDepartment.selectedIndex == 0)
                alert('Please Select Department.');

            else if (ddlSection.selectedIndex == 0)
                alert('Please Select Section.');

            else if (ddlServiceType.selectedIndex == 0)
                alert('Please Select ServiceType.');

            else

                return confirm('Are you sure you want update data?');
            return false;

        }


    </script>

    <style type="text/css">
        .style5 {
            width: 258px;
        }

        .style6 {
            width: 263px;
        }

        .style7 {
            width: 386px;
        }

        .text {
            height: 22px;
        }

        .auto-style1 {
            height: 384px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />

    <div id="DivMain" runat="server" align="center">

        <table>
            <tr>
                <td>
                    <table class="style1">
                        <thead>
                            <tr>
                                <th colspan="6" style="color: Black">Employee Maintenance
                                </th>
                            </tr>
                        </thead>
                        <tr>
                            <td colspan="3">


                                <asp:RadioButton ID="rbSave" runat="server" Checked="True" GroupName="EmpMaintenance"
                                    Text="Add" AutoPostBack="true" OnCheckedChanged="rbSave_CheckedChanged" />
                                <asp:RadioButton ID="rbUpdate" runat="server" GroupName="EmpMaintenance" Text="Modify"
                                    AutoPostBack="true" OnCheckedChanged="rbUpdate_CheckedChanged" />
                                <asp:CheckBox ID="chkAddress" runat="server" Text="Address" AutoPostBack="True" OnCheckedChanged="chkAddress_CheckedChanged" />

                            </td>
                            <th>National ID
                            </th>
                            <th>:
                            </th>
                            <td>

                                <asp:TextBox ID="txtNid" runat="server" MaxLength="14" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Columns="38">
                                </asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <th>System Code
                            </th>
                            <td>:
                            </td>
                            <td>

                                <asp:TextBox ID="txtEmpCode" Enabled="False" runat="server" MaxLength="4" BorderColor="#1293D1"
                                    BorderStyle="Ridge" onkeypress="return IsNumberKey(event)" AutoPostBack="True"
                                    Columns="13">
                                </asp:TextBox>
                                <asp:TextBox ID="txtEmployeMody" runat="server" AutoPostBack="True" CssClass="cls text"
                                    BorderColor="#1293D1" BorderStyle="Ridge" MaxLength="5" Width="50px" OnTextChanged="txtEmployeMody_TextChanged">
                                </asp:TextBox>
                                <asp:DropDownList ID="ddlEmpcode" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlEmpcode_SelectedIndexChanged">
                                </asp:DropDownList>

                            </td>
                            <th>Blood Group
                            </th>
                            <th>:
                            </th>
                            <td>

                                <asp:TextBox ID="txtBloodGroup" runat="server" MaxLength="14" BorderColor="#1293D1"
                                    BorderStyle="Ridge" Columns="38">
                                </asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <th>Employee Name
                            </th>
                            <td>:
                            </td>
                            <td>

                                <asp:TextBox ID="txtEmpName" runat="server" MaxLength="36" BorderColor="#1293D1"
                                    BorderStyle="Ridge" Columns="38">
                                </asp:TextBox>

                            </td>
                            <th>Grade
                            </th>
                            <th>:
                            </th>
                            <td>

                                <asp:DropDownList ID="ddlGrade" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtGrade" runat="server" Enabled="False" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Columns="38">
                                </asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <th>Employee ID
                            </th>
                            <td>:
                            </td>
                            <td>

                                <asp:TextBox ID="txtEmpFileNo" runat="server" BorderColor="#1293D1" Columns="38"
                                    BorderStyle="Ridge" MaxLength="20"
                                    OnTextChanged="txtEmpFileNo_TextChanged" AutoPostBack="true">
                                </asp:TextBox>

                            </td>

                            <th>Date of Birth
                            </th>
                            <th>:
                            </th>
                            <td>

                                <asp:TextBox ID="txtDOB" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>

                            </td>


                        </tr>
                        <tr>
                            <th>Father's Name
                            </th>
                            <td>:
                            </td>
                            <td>

                                <asp:TextBox ID="txtFatherName" runat="server" MaxLength="36" BorderColor="#1293D1"
                                    BorderStyle="Ridge" Columns="38">
                                </asp:TextBox>

                            </td>

                            <th>Religion
                            </th>
                            <th>:
                            </th>
                            <td>

                                <asp:DropDownList ID="ddlReligion" runat="server" Style="width: 250px; height: 22px"
                                    BorderColor="#1293D1" BorderStyle="Ridge" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
                                </asp:DropDownList>

                            </td>

                        </tr>
                        <tr>
                            <th>Mother&#39;s Name
                            </th>
                            <td>:
                            </td>
                            <td>

                                <asp:TextBox ID="txtMotherName" runat="server" MaxLength="36" BorderColor="#1293D1"
                                    BorderStyle="Ridge" Columns="38">
                                </asp:TextBox>

                            </td>

                            <th>Job Location/Posting
                            </th>
                            <th>:
                            </th>
                            <td>

                                <asp:DropDownList ID="ddlLocation" runat="server" Style="width: 250px" BorderColor="#1293D1"
                                    AutoPostBack="True" BorderStyle="Ridge" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                </asp:DropDownList>

                            </td>

                        </tr>
                        <tr>
                            <th>Spouse Name
                            </th>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSpouseName" runat="server" MaxLength="36" BorderColor="#1293D1"
                                    BorderStyle="Ridge" Columns="38" AutoPostBack="True"
                                    OnTextChanged="txtSpouseName_TextChanged">
                                </asp:TextBox>
                            </td>

                            <th>Department
                            </th>
                            <th>:
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDepartment" runat="server" Style="width: 250px; height: 22px"
                                    BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                    Height="16px">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <th>Qualification
                            </th>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtQualification" runat="server" MaxLength="36" BorderColor="#1293D1"
                                    BorderStyle="Ridge" Columns="38">
                                </asp:TextBox>
                            </td>

                            <th>Section
                            </th>
                            <th>:
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlSection" runat="server" Style="width: 250px; height: 22px"
                                    BorderColor="#1293D1" BorderStyle="Ridge"
                                    OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <th>Date of Joining
                            </th>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtJoinDate" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                            </td>

                            <th>Service Type
                            </th>
                            <th>:
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlServiceType" runat="server" Style="width: 250px; height: 22px"
                                    BorderColor="#1293D1" BorderStyle="Ridge"
                                    OnSelectedIndexChanged="ddlServiceType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>



                        </tr>
                        <tr>
                            <th>Designation
                            </th>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation" runat="server" BorderColor="#1293D1"
                                    BorderStyle="Ridge"
                                    OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"
                                    Style="height: 22px">
                                </asp:DropDownList>
                            </td>

                            <th>Date of Confirmation
                            </th>
                            <th>:
                            </th>
                            <td>
                                <asp:TextBox ID="txtConfrimDate" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <th>Gender
                            </th>
                            <td>:
                            </td>
                            <td class="style7">
                                <asp:DropDownList ID="ddlGender" runat="server" BorderColor="#1293D1" BorderStyle="Ridge">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem Value="1">Male</asp:ListItem>
                                    <asp:ListItem Value="2">Female</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <th>Sales Area
                            </th>
                            <th>:
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlArea" runat="server" Style="width: 250px; height: 22px"
                                    BorderColor="#1293D1" BorderStyle="Ridge">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <th>Marital Status
                            </th>
                            <td>:
                            </td>
                            <td class="style7">
                                <asp:DropDownList ID="ddlMarried" runat="server" BorderColor="#1293D1"
                                    BorderStyle="Ridge" Enabled="False">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem Value="1">Married</asp:ListItem>
                                    <asp:ListItem Value="2">Unmarried</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <th>Print Sl No. (For Salary Sheet)
                            </th>
                            <th>:
                            </th>
                            <td>
                                <asp:TextBox ID="txtPrintSlNo" runat="server" BorderColor="#1293D1" Columns="38"
                                    BorderStyle="Ridge">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Last Promotion Date
                            </th>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPromotionDate" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                            </td>

                            <th>Employee Photo
                            </th>
                            <td>:
                            </td>
                            <td class="style7">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:ImageButton ID="ibtnUpload" runat="server" ImageUrl="~/Profile_Pic/Upload.png" Width="98px" Height="54px" OnClick="ibtnUpload_Click" />
                            </td>
                        </tr>

                    </table>
                </td>
                <td style="width: 0px;">
                    <table class="image">
                        <thead>
                            <tr>
                                <th style="color: Black">Employee Photo
                                </th>
                            </tr>
                        </thead>
                        <tr>
                            <td class="auto-style1">
                                <asp:Image ID="ImgPicture" runat="server" Height="338px" ImageUrl="~/Profile_Pic/index.jpg" Width="264px" />
                                DELETE PHOTO<asp:ImageButton ID="ibtnDelete" runat="server" Height="26px" ImageUrl="~/Profile_Pic/delete_user.png" Style="position: relative; top: 2px; left: 8px;" OnClick="ibtnDelete_Click" Width="41px" />
                            </td>
                        </tr>
                    </table>
                    <p style="color: red">Picture Size Should Be 1 Mb Or Less</p>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivMessage" runat="server" align="center" visible="false">
        <table class="style1">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td class="style5">
                    <asp:Button ID="btnHideMessageDiv" runat="server" Text="OK" CssClass="button blue size-100"
                        OnClick="btnHideMessageDiv_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DivAddress" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="9" style="color: Black">Employee Address Information
                    </th>
                </tr>
            </thead>
            <tr>
                <th colspan="3" style="background-color: pink">Permanent Address :
                </th>
                <th colspan="3" style="background-color: #CCCC00">Present Address :
                </th>
            </tr>
            <tr>
                <th>Address Line1
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtPermanentAdd1" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="38">
                    </asp:TextBox>
                </td>
                <th>Address Line1
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtPresentAdd1" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="38">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Address Line2
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtPermanentAdd2" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="38">
                    </asp:TextBox>
                </td>
                <th>Address Line2
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtPresentAdd2" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="38">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Division
                </th>
                <th>:
                </th>
                <td>
                    <asp:DropDownList ID="ddlPermanentDivision" runat="server" BorderColor="#1293D1"
                        BorderStyle="Ridge" AutoPostBack="True" OnSelectedIndexChanged="ddlPermanentDivision_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <th>Division
                </th>
                <th>:
                </th>
                <td>
                    <asp:DropDownList ID="ddlPresentDivision" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPresentDivision_SelectedIndexChanged"
                        Style="height: 22px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>District
                </th>
                <th>:
                </th>
                <td>
                    <asp:DropDownList ID="ddlPermanentDistrict" runat="server" BorderColor="#1293D1"
                        BorderStyle="Ridge" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPermanentDistrict_SelectedIndexChanged"
                        Style="height: 22px">
                    </asp:DropDownList>
                </td>
                <th>District
                </th>
                <th>:
                </th>
                <td>
                    <asp:DropDownList ID="ddlPresentDistrict" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlPresentDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Thana
                </th>
                <th>:
                </th>
                <td>
                    <asp:DropDownList ID="ddlPermanentThana" runat="server" BorderColor="#1293D1"
                        BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlPermanentThana_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <th>Thana
                </th>
                <th>:
                </th>
                <td>
                    <asp:DropDownList ID="ddlPresentThana" runat="server" BorderColor="#1293D1"
                        BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlPresentThana_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Post Code
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpPermanentPostCode" runat="server" BorderColor="#1293D1" MaxLength="4"
                        BorderStyle="Ridge" Columns="38" onkeypress="return IsDecimalKey(event)">
                    </asp:TextBox>
                </td>
                <th>Post Code
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpPresentPostCode" runat="server" BorderColor="#1293D1" MaxLength="4"
                        BorderStyle="Ridge" Columns="38" onkeypress="return IsDecimalKey(event)">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>E-mail
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" BorderColor="#1293D1" BorderStyle="Ridge"
                        Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
                <th>Mobile Phone
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="15" BorderColor="#1293D1" BorderStyle="Ridge"
                        Columns="28" onkeypress="return IsDecimalKey(event)">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Land Phone
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="15" BorderColor="#1293D1"
                        BorderStyle="Ridge">
                    </asp:TextBox>
                </td>
                <th></th>
                <th></th>
                <th></th>
            </tr>
            <tr>
                <th>Employee Reference Name 1
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference1Name" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
                <th>Employee Reference Name 2
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference2Name" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Employee Reference Address 1
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference1AddL1" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
                <th>Employee Reference Address 1
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference2AddL1" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Employee Reference Address 2
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference1AddL2" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
                <th>Employee Reference Address 2
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference2AddL2" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Phone
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference1Phone" runat="server" MaxLength="15" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
                <th>Phone
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference2Phone" runat="server" MaxLength="15" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Relation
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference1Relation" runat="server" MaxLength="20" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
                <th>Relation
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpReference2Relation" runat="server" MaxLength="20" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Confirm With the Relation
                </th>
                <th>:
                </th>
                <td>
                    <asp:CheckBox ID="chkYes" runat="server" Text="Yes" OnCheckedChanged="chkYes_CheckedChanged"
                        AutoPostBack="True" />
                    <asp:CheckBox ID="chkNo" runat="server" Text="No" AutoPostBack="True" OnCheckedChanged="chkNo_CheckedChanged" />
                </td>

                <th style="color: #800000"></th>

                <th></th>
                <th></th>
            </tr>
            <tr>
                <th>Confirmed By (Employee ID)
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtConfirmByEmpID" runat="server" BorderColor="#1293D1" Columns="38"
                        BorderStyle="Ridge" MaxLength="20" Visible="False"
                        OnTextChanged="txtConfirmByEmpID_TextChanged" AutoPostBack="True">
                    </asp:TextBox>

                </td>

                <th>In Case of Emergency :
                </th>
                <th>:
                </th>
                <th></th>


            </tr>
            <tr>

                <th>Confirmed By (Employee Name)
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtConfirmByEmpIDName" runat="server" BorderColor="#1293D1" Columns="38"
                        BorderStyle="Ridge" MaxLength="50" ReadOnly="True" Visible="False">
                    </asp:TextBox>


                </td>

                <th>Name 
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpEmergencyName" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>


            </tr>
            <tr>
                <th>Mode Of Confirmation
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpConfirmMode" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" EnableTheming="True" Visible="False">
                    </asp:TextBox>
                </td>

                <th>Contact No. 
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtEmpEmergencyContactNo" runat="server" MaxLength="15" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35" onkeypress="return IsDecimalKey(event)">
                    </asp:TextBox>
                </td>

            </tr>
            <tr>
                <th colspan="3" style="background-color: pink">Guarantor Information :
                </th>
                <th colspan="3" style="background-color: #CCCC00">Holding Company Assets :
                </th>
            </tr>
            <tr>
                <th>Name
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtGuarName" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
                <th>Mobile
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldMobile" runat="server" MaxLength="15" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="28" onkeypress="return IsDecimalKey(event)">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Father's Name
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtGuarFatherName" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
                <th>Vehicle
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldVehicle" runat="server" MaxLength="11" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>National ID
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtGuarNationalID" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
                <th>Laptop/PC
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldComputer" runat="server" MaxLength="20" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Present Address
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtGuarPresentAdd" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
                <th>Others
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldOther" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Permanent Address
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtGuarPermanentAdd" runat="server" MaxLength="36" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
                <th>Employee Security Deposits Information
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldSecurityDeposit" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Professional Information
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtGuarProfessInfo" runat="server" MaxLength="40" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
                <th>Certificate
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldCertificate" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Mobile Phone
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtGuarMobile" runat="server" MaxLength="15" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
                <th>Cheque
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldCheque" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th></th>
                <th></th>
                <th></th>

                <th>Gurantor Deed
                </th>
                <th>:
                </th>
                <td>
                    <asp:TextBox ID="txtHoldDeed" runat="server" MaxLength="50" BorderColor="#1293D1"
                        BorderStyle="Ridge" Columns="35">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivButton" runat="server" align="center">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ValidationEmployee()"
                        CssClass="button green size-100" OnClick="btnSave_Click" Height="27" />

                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button blue size-80"
                        Height="27" OnClientClick="return ValidationBeforeUpdate()"
                        OnClick="btnUpdate_Click" />


                    <asp:Button ID="btnReport" runat="server" Text="View Report" CssClass="button DarkMagenta size-100"
                        Height="27" OnClick="btnReport_Click" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-100"
                        Height="27" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
