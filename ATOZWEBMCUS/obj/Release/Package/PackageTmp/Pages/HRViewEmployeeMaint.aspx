<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRViewEmployeeMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRViewEmployeeMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 25px;
        }

        .auto-style6 {
            height: 30px;
        }

        .auto-style9 {
            height: 250px;
        }

        .auto-style15 {
            height: 18px;
        }
    </style>
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 190px;
            margin: 0 auto;
            width: 1160px;
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
        .auto-style16 {
            width: 523px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            var txtEmpID = document.getElementById('<%=txtEmpID.ClientID%>').value;
             var txtEmpName = document.getElementById('<%=txtEmpName.ClientID%>').value;
             var ddlDesignation = document.getElementById('<%=ddlDesignation.ClientID%>');
             var ddlServiceType = document.getElementById('<%=ddlServiceType.ClientID%>');
             var ddlArea = document.getElementById('<%=ddlArea.ClientID%>');
             var ddlSection = document.getElementById('<%=ddlSection.ClientID%>');
             var ddlDepartment = document.getElementById('<%=ddlDepartment.ClientID%>');
             var txtGrade = document.getElementById('<%=txtGrade.ClientID%>').value;
             var txtStep = document.getElementById('<%=txtStep.ClientID%>').value;

             if (txtEmpID == '' || txtEmpID.length == 0) {
                 document.getElementById('<%=txtEmpID.ClientID%>').focus();
                 alert('Please Input Employee Id.');
             }
             else if (txtEmpName == '' || txtEmpName.length == 0) {
                 document.getElementById('<%=txtEmpName.ClientID%>').focus();
                 alert('Please Input Employee Name.');
             }
             else if ((ddlDesignation.selectedIndex == 0)) {
                 document.getElementById('<%=ddlDesignation.ClientID%>').focus();
                 alert('Please Select Designation.');
             }
             else if ((ddlServiceType.selectedIndex == 0)) {
                 document.getElementById('<%=ddlServiceType.ClientID%>').focus();
                 alert('Please Select Service Type.');
             }
             else if ((ddlArea.selectedIndex == 0)) {
                 document.getElementById('<%=ddlArea.ClientID%>').focus();
                 alert('Please Select location/Area.');
             }
             else if ((ddlSection.selectedIndex == 0)) {
                 document.getElementById('<%=ddlSection.ClientID%>').focus();
                 alert('Please Select Section.');
             }
             else if ((ddlDepartment.selectedIndex == 0)) {
                 document.getElementById('<%=ddlDepartment.ClientID%>').focus();
                 alert('Please Select Department.');
             }

             else if (txtGrade == '' || txtGrade.length == 0) {
                 document.getElementById('<%=txtGrade.ClientID%>').focus();
                 alert('Please Input Grade.');
             }
             else if (txtStep == '' || txtStep.length == 0) {
                 document.getElementById('<%=txtStep.ClientID%>').focus();
	              alert('Please Input Step.');
	          }

	          else
	              return confirm('Are you sure you want to save information?');
    return false;
}

    </script>

    <script type="text/javascript">
        function closechildwindow() {
            window.opener.document.location.href = 'HREnquireEmpMasterFile.aspx';
            window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body>
        <div align="left">

            <table>
                <tr>
                    <td class="auto-style16">&nbsp;</td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="View Employee Master File Informations" Font-Size="X-Large" ForeColor="#336600" BorderColor="#FF99CC" Font-Bold="True" Font-Italic="True" Font-Underline="True"></asp:Label>

                    </td>

                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label8" runat="server" Text="Status :" Font-Size="X-Large" ForeColor="#336600" BorderColor="#FF99CC" Font-Bold="True" Font-Italic="True" Font-Underline="True"></asp:Label>
                        <asp:Label ID="lblEmpStatusDesc" runat="server" Text="Status :" Font-Size="X-Large" ForeColor="#336600" BorderColor="#FF99CC" Font-Bold="True" Font-Italic="True" Font-Underline="True"></asp:Label>
                        <asp:Label ID="lblEmpStatusDate" runat="server" Text="Status :" Font-Size="X-Large" ForeColor="#336600" BorderColor="#FF99CC" Font-Bold="True" Font-Italic="True" Font-Underline="True"></asp:Label>

                    </td>

                </tr>
            </table>
            <table style="border: groove">
                <tr>
                    <td class="auto-style9">
                        <table style="width: 546px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEmpID" runat="server" Text="Employee ID:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmpID" runat="server" CssClass="cls text" Width="71px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" Enabled="False"></asp:TextBox>

                                    <asp:DropDownList ID="ddlEmpID" runat="server" Height="25px" Width="317px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" Enabled="False">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEmpName" runat="server" Text="Employee Name:" Font-Size="Medium"
                                        ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmpName" runat="server" CssClass="cls text" Width="390px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Height="20px" Font-Size="Medium" ToolTip="Enter Name" MaxLength="25" Enabled="False"></asp:TextBox>
                                    &nbsp;
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblJoiningDate" runat="server" Text="Joining Date:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJoiningDate" runat="server" CssClass="cls text" Width="140px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" AutoPostBack="True" Enabled="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    
                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPerDate" runat="server" Text="Permanent Date:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPerDate" runat="server" CssClass="cls text" Width="140px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" AutoPostBack="True" Enabled="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                            <td>
                                <asp:Label ID="lblLastPostingDt" runat="server" Text="Last Posting Date:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>

                                <asp:TextBox ID="txtLastPostingDt" runat="server" CssClass="cls text" Width="140px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="20px" Font-Size="Medium" AutoPostBack="True"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLastPromotionDt" runat="server" Width="140px" Text="Last Promotion Date:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>

                                <asp:TextBox ID="txtLastPromotionDt" runat="server" CssClass="cls text" Width="140px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="20px" Font-Size="Medium" AutoPostBack="True"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLastIncrtementDt" runat="server" Text="Last Increment Date:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>

                                <asp:TextBox ID="txtLastIncrementDt" runat="server" CssClass="cls text" Width="140px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="20px" Font-Size="Medium" AutoPostBack="True"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;</td>
                        </tr>
                            <tr>
                                <td>

                                    <asp:Label ID="lblBank" runat="server" Text="Bank:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                </td>
                                <td>

                                    <asp:DropDownList ID="ddlBank" runat="server" Height="25px" Width="247px"
                                        CssClass="cls text" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Enabled="False">
                                    </asp:DropDownList>


                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="137px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True" MaxLength="20" Enabled="False"></asp:TextBox>


                                </td>
                            </tr>

                        </table>
                    </td>
                    <td class="auto-style9">
                        <table style="height: 254px">

                            <tr>
                                <td>

                                    <asp:Label ID="lblDesignation" runat="server" Text="Designation:" Font-Size="Medium"
                                        ForeColor="Red"></asp:Label>

                                </td>
                                <td>

                                    <asp:DropDownList ID="ddlDesignation" runat="server" Height="25px" Width="247px"
                                        CssClass="cls text" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Enabled="False">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>

                                    <asp:Label ID="lblServiceType" runat="server" Text="Service Type:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                </td>
                                <td>

                                    <asp:DropDownList ID="ddlServiceType" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" Enabled="False">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style6">

                                    <asp:Label ID="lblArea" runat="server" Text="District/Area:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                </td>
                                <td>

                                    <asp:DropDownList ID="ddlArea" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" Enabled="False">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                            <td class="auto-style6">

                                <asp:Label ID="lblLocation" runat="server" Text="Posting/Location:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>
                            <td>

                                <asp:DropDownList ID="ddlLocation" runat="server" Height="25px" Width="247px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    CssClass="cls text" Font-Size="Medium" Enabled="False">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                            <tr>
                                <td class="auto-style6">

                                    <asp:Label ID="lblSection" runat="server" Text="Section:" Font-Size="Medium"
                                        ForeColor="Red"></asp:Label>

                                </td>
                                <td>

                                    <asp:DropDownList ID="ddlSection" runat="server" Height="25px" Width="247px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        CssClass="cls text" Font-Size="Medium" Enabled="False">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style6">

                                    <asp:Label ID="lblDepartment" runat="server" Text="Department:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                </td>
                                <td>

                                    <asp:DropDownList ID="ddlDepartment" runat="server" Height="25px" Width="247px" CssClass="cls text"
                                        Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Enabled="False">
                                    </asp:DropDownList>

                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lblProject" runat="server" Text="Project:" Font-Size="Medium"
                                        ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProject" runat="server" Height="25px" Width="170px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                                            Font-Size="Medium" Enabled="False">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">CORE</asp:ListItem>
                                            <asp:ListItem Value="2">CSI</asp:ListItem>
                                            <asp:ListItem Value="3">TEACHERS</asp:ListItem>
                                        </asp:DropDownList>

                                   

                                </td>
                            </tr>
                            <tr>
                            <td>
                                <asp:Label ID="lblBaseGrade" runat="server" Text="Base Grade:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBaseGrade" runat="server" Height="25px" Width="170px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" Enabled="False">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem Value="1">Heade Office</asp:ListItem>
                                    <asp:ListItem Value="2">Field Office</asp:ListItem>
                                    <asp:ListItem Value="3">Consolidated</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGrade" runat="server" Text="Grade:" Font-Size="Medium"
                                        ForeColor="Red"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtGrade" runat="server" CssClass="cls text" Width="95px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" Enabled="False"></asp:TextBox>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblSteps" runat="server" Text="Steps:" Font-Size="Medium"
                                        ForeColor="Red"></asp:Label>
                                    &nbsp;
                                    <asp:TextBox ID="txtStep" runat="server" CssClass="cls text" Width="60px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Font-Size="Medium" Enabled="False"></asp:TextBox>

                                </td>
                            </tr>
                            <%-- <tr>
                                  <td class="auto-style2">

                                  </td>
                                  <td class="auto-style2">

                                  </td>
                              </tr>
                              <tr>
                                  <td>

                                  </td>
                                  <td>

                                  </td>
                              </tr>--%>
                        </table>
                    </td>
                    <td>
                        <table style="width: 385px">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlImage" runat="server" Height="223px" Width="321px">
                                        <asp:Image ID="ImgPicture" runat="server" Height="224px" ImageUrl="~/Images/index.jpg" Width="327px" Style="margin-top: 0px" /><br />

                                    </asp:Panel>
                                </td>

                            </tr>

                        </table>

                    </td>
                </tr>
            </table>




            <table>
                <tr>
                    <td class="auto-style2">

                        <asp:Button ID="btnPersonal" runat="server" Text="Personal" Style="background-color: Silver"
                            Width="212px" BorderColor="#333300" BorderStyle="Groove" OnClick="btnPersonal_Click" />
                        <asp:Button ID="btnAddress" Style="background-color: MediumAquamarine" runat="server"
                            Text="Address" Width="212px" BorderStyle="Groove" OnClick="btnAddress_Click" />
                        <asp:Button ID="BtnEducation" runat="server" Text="Education" Style="background-color: ActiveCaption"
                            Width="212px" BorderStyle="Groove" OnClick="BtnEducation_Click" />
                        <asp:Button ID="BtnNominee" runat="server" Text="Nominee" Style="background-color: Khaki"
                            Width="212px" BorderStyle="Groove" OnClick="BtnNominee_Click" />
                    </td>
                </tr>
            </table>

            <div style="background-color: Silver; border: 1px">
                <asp:Panel ID="pnlPersonal" runat="server" Height="210px" Width="1215px">
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFName" runat="server" Text="Father Name:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFName" runat="server" CssClass="cls text" Width="358px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="20" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMName" runat="server" Text="Mother Name:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMName" runat="server" CssClass="cls text" Width="358px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="20" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSpouseName" runat="server" Text="Spouse Name:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSpouseName" runat="server" CssClass="cls text" Width="358px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="20" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="cls text" Width="140px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Height="20px" Font-Size="Medium" AutoPostBack="True" Enabled="False"></asp:TextBox>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBloodGrp" runat="server" Text="Blood Group:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBloodGrp" runat="server" CssClass="cls text" Width="140px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNationalId" runat="server" Text="National Id:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNationalId" runat="server" CssClass="cls text" Width="358px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="16" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>


                                </table>
                            </td>

                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNationality" runat="server" Text="Nationality:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlNationality" runat="server" Height="25px" Width="170px"
                                                CssClass="cls text" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblReligion" runat="server" Text="Religion:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlReligion" runat="server" Height="25px" Width="170px" CssClass="cls text"
                                                Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Enabled="False">
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblGender" runat="server" Text="Gender:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlGender" runat="server" Height="25px" Width="170px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" Enabled="False">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">Male</asp:ListItem>
                                                <asp:ListItem Value="2">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" Height="25px" Width="170px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                CssClass="cls text" Font-Size="Medium" Enabled="False">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">Single</asp:ListItem>
                                                <asp:ListItem Value="2">Married</asp:ListItem>
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblHeight" runat="server" Font-Size="Medium" ForeColor="Red" Text="Height:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHeight" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="20px" Width="138px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTin" runat="server" Font-Size="Medium" ForeColor="Red" Text="TIN:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTin" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="20px" Width="138px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPassportNo" runat="server" Text="Passport No:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassportNo" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Height="20px" Font-Size="Large" Enabled="False"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPassportIssueDate" runat="server" Text="Passport Issue Date:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassportIssueDate" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Height="20px" Font-Size="Large" img src="../Images/calender.png" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPassportExpdate" runat="server" Text="Passport Exp.Date:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassportExpdate" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Height="20px" Font-Size="Large" img src="../Images/calender.png" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPassportIssuePlace" runat="server" Text="Passport Issue Place:"
                                                Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassportIssuePlace" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Height="20px" Font-Size="Large" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLicenseNo" runat="server" Text="License No:"
                                                Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLicenseNo" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Height="20px" Font-Size="Large" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLicenseExpDate" runat="server" Text="License Exp.Date:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLicenseExpDate" runat="server" CssClass="cls text" Width="185px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Height="20px" Font-Size="Large" img src="../Images/calender.png" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                </asp:Panel>
            </div>

            <div style="background-color: MediumAquamarine; border: 1px">
                <asp:Panel ID="pnlAddress" runat="server" Height="210px" Width="1280px">
                    <table style="height: 187px">
                        <tr>
                            <td>
                                <table style="width: 645px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPreAdd" runat="server" Text="Present Address:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPreAdd" runat="server" CssClass="cls text" Width="360px" Height="50px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" TextMode="MultiLine" MaxLength="30" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPreTelNo" runat="server" Text="Telephone No:" Font-Size="Medium"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPreTelNo" runat="server" CssClass="cls text" Width="200px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="11" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Red" Text="Division:"></asp:Label>
                                            <asp:DropDownList ID="ddlPreDivision" runat="server" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px" OnSelectedIndexChanged="ddlPreDivision_SelectedIndexChanged" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPreMobNo" runat="server" Text="Mobile No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPreMobNo" runat="server" CssClass="cls text" Width="200px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="11" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Red" Text="District:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlPreDistrict" runat="server" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px" OnSelectedIndexChanged="ddlPreDistrict_SelectedIndexChanged" Enabled="False">
                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPreEmail" runat="server" Text="E-mail:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPreEmail" runat="server" CssClass="cls text" Width="200px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="15" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Red" Text="Upazil:"></asp:Label>&nbsp; &nbsp;
                            <asp:DropDownList ID="ddlPreUpzila" runat="server" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px" Enabled="False">
                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                    <td></td>


                                    <td>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                        &nbsp; &nbsp;&nbsp; &nbsp;
                                        <asp:Label ID="Label9" runat="server" Font-Size="Medium" ForeColor="Red" Text="Thana:"></asp:Label>&nbsp; &nbsp;
                            <asp:DropDownList ID="ddlPreThana" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px">
                            </asp:DropDownList>
                                    </td>
                                </tr>

                                </table>
                            </td>
                            <td style="background-color: darkseagreen" class="auto-style15"></td>
                            <td>
                                <table style="width: 625px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPerAdd" runat="server" Text="Permanent Address:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPerAdd" runat="server" CssClass="cls text" Width="361px" Height="50px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" TextMode="MultiLine" MaxLength="30" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTelNo" runat="server" Text="Telephone No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelNo" runat="server" CssClass="cls text" Width="200px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="11" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Red" Text="Division:"></asp:Label>
                                            <asp:DropDownList ID="ddlPerDivision" runat="server" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px" OnSelectedIndexChanged="ddlPerDivision_SelectedIndexChanged" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMobNo" runat="server" Text="Mobile No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="cls text" Width="200px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="11" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Red" Text="District:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlPerDistrict" runat="server" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px" OnSelectedIndexChanged="ddlPerDistrict_SelectedIndexChanged" Enabled="False">
                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEmail" runat="server" Text="E-mail:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="cls text" Width="200px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                                Font-Size="Medium" MaxLength="15" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="Label6" runat="server" Font-Size="Medium" ForeColor="Red" Text="Upazil:"></asp:Label>&nbsp; &nbsp;
                            <asp:DropDownList ID="ddlPerUpzila" runat="server" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px" Enabled="False">
                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                     <tr>
                                    <td></td>
                                    <td>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                                        <asp:Label ID="Label10" runat="server" Font-Size="Medium" ForeColor="Red" Text="Thana:"></asp:Label>&nbsp; &nbsp;
                            <asp:DropDownList ID="ddlPerThana" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" Font-Size="Medium" Height="25px" Width="190px">
                            </asp:DropDownList>
                                    </td>
                                </tr>



                                </table>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
            </div>

            <div style="background-color: ActiveCaption; border: 1px" align="center">
                <asp:Panel ID="pnlEducation" runat="server" Height="211px" Width="1215px">
                    <%--<table class="style1">
                    <tr>
                        <td>
                            <h3>
                                <asp:Label ID="lblDegree" runat="server" Text="Degree" ForeColor="Red" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtDegree" runat="server" Width="230px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="1"></asp:TextBox>

                        </td>
                        <td>
                            <h3>
                                <asp:Label ID="lblResult" runat="server" Text="Result " ForeColor="Red" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtResult" runat="server" Width="68px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="2"></asp:TextBox>
                        </td>
                        <td>
                            <h3>
                                <asp:Label ID="lblPassYr" runat="server" Text="Pass Year" ForeColor="Red" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtPassYr" runat="server" Width="105px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="3"></asp:TextBox>
                        </td>

                        <td>
                            <h3>
                                <asp:Label ID="lblBoard" runat="server" Text="Borad/University" ForeColor="Red" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtBoard" runat="server" Width="354px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="4"></asp:TextBox>
                            <asp:Label ID="lblEduId" runat="server" Text="" Visible="false"></asp:Label>

                        </td>

                        <td>
                            <br />
                            <br />
                        </td>

                    </tr>
                </table>--%>

                    <br />
                    <div align="center" class="grid_scroll">
                        <asp:GridView ID="gvEducation" runat="server" HeaderStyle-CssClass="FixedHeader3" HeaderStyle-BackColor="YellowGreen"
                            AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" EnableModelValidation="True" OnRowDataBound="gvEducation_RowDataBound">
                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader3" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                            <Columns>
                                <asp:TemplateField Visible="false" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Degree" HeaderText="Degree">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Result" HeaderText="Result">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PassYear" HeaderText="Pass Year">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Board" HeaderText="Borad/Uni">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:BoundField>

                            </Columns>

                        </asp:GridView>
                    </div>


                </asp:Panel>
            </div>

            <div style="background-color: Khaki; border: 1px" align="left">
                <asp:Panel ID="pnlNominee" runat="server" Height="211px" Width="1215px">
                    <%--<table class="style1">
                    <tr>
                        <td>
                            <h3>
                                <asp:Label ID="lblNomName" runat="server" Text="Nominee Name" ForeColor="#CC3300" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtNomName" runat="server" Width="230px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="1"></asp:TextBox>

                        </td>
                        <td>
                            <h3>
                                <asp:Label ID="lblNomRelation" runat="server" Text="Relation " ForeColor="Red" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtNomRelation" runat="server" Width="128px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="2"></asp:TextBox>
                        </td>
                        <td>
                            <h3>
                                <asp:Label ID="lblNomAddress" runat="server" Text="Address" ForeColor="Red" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtNomAdress" runat="server" Width="352px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="3"></asp:TextBox>
                        </td>

                        <td>
                            <h3>
                                <asp:Label ID="lblNomConctNo" runat="server" Text="Contact No" ForeColor="Red" Font-Size="Medium"></asp:Label></h3>
                            <asp:TextBox ID="txtNomContactNo" runat="server" Width="181px" Height="25px" Font-Size="Medium"
                                CssClass="cls text" TabIndex="4" MaxLength="11"></asp:TextBox>
                            <asp:Label ID="lblNomId" runat="server" Text="" Visible="false"></asp:Label>
                        </td>

                        <td>
                            <br />
                            <br />
                        </td>

                    </tr>
                </table>--%>

                    <br />
                    <div align="center" class="grid_scroll">
                        <asp:GridView ID="gvNominee" runat="server" HeaderStyle-CssClass="FixedHeader3" HeaderStyle-BackColor="YellowGreen"
                            AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" EnableModelValidation="True" Height="33px" OnRowDataBound="gvNominee_RowDataBound">
                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader3" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                            <Columns>
                                <asp:TemplateField Visible="false" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NomineeName" HeaderText="Name">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="left" Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Relation" HeaderText="Relation">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="left" Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="Address">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="left" Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No">
                                    <HeaderStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="left" Width="180px" />
                                </asp:BoundField>


                            </Columns>

                        </asp:GridView>
                    </div>


                </asp:Panel>
            </div>
            <table>
                <tr>
                    <td></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp;
                 &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="21px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                        <br />
                    </td>
                </tr>
            </table>
        </div>

        <asp:Label ID="CtrlAddRec" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
    </body>
</asp:Content>

