<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmpPostingMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmpPostingMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/styletext.css" rel="stylesheet" />

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            width: 1183px;
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

        /*.auto-style5 {
            width: 132px;*/
        /*}*/
    </style>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtNewPostingDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtNewPostingDate.ClientID %>").datepicker();

            });

        });

    </script>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeUpdate() {
            var txtNewPostingDate = document.getElementById('<%=txtNewPostingDate.ClientID%>').value;
            var ddlNewArea = document.getElementById('<%=ddlNewArea.ClientID%>');
            var ddlNewSection = document.getElementById('<%=ddlNewSection.ClientID%>');
            var ddlNewDepartment = document.getElementById('<%=ddlNewDepartment.ClientID%>');
            var ddlNewProject = document.getElementById('<%=ddlNewProject.ClientID%>');
            var ddlNewDesignation = document.getElementById('<%=ddlNewDesignation.ClientID%>');
            var ddlNewCashCode = document.getElementById('<%=ddlNewCashCode.ClientID%>');


            if (txtNewPostingDate == '' || txtNewPostingDate.length == 0) {
                document.getElementById('<%=txtNewPostingDate.ClientID%>').focus();
                alert('Please Input Posting Date');
            }
            else if ((ddlNewArea.selectedIndex == 0)) {
                document.getElementById('<%=ddlNewArea.ClientID%>').focus();
                alert('Please Select New Area');
            }
            else if ((ddlNewSection.selectedIndex == 0)) {
                document.getElementById('<%=ddlNewSection.ClientID%>').focus();
                 alert('Please Select New Section');
             }
             else if ((ddlNewDepartment.selectedIndex == 0)) {
                 document.getElementById('<%=ddlNewDepartment.ClientID%>').focus();
                 alert('Please Select New Department');
             }
             else if ((ddlNewProject.selectedIndex == 0)) {
                 document.getElementById('<%=ddlNewProject.ClientID%>').focus();
                 alert('Please Select New Project');
             }
             else if ((ddlNewDesignation.selectedIndex == 0)) {
                 document.getElementById('<%=ddlNewDesignation.ClientID%>').focus();
                 alert('Please Select New Designation');
             }
             else if ((ddlNewCashCode.selectedIndex == 0)) {
                 document.getElementById('<%=ddlNewCashCode.ClientID%>').focus();
                         alert('Please Select New Cash Code');
                     }
                     else
                         return confirm('Are you sure you want to Update information?');
    return false;
}

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />

    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Employee's Posting Maintenance
                    </th>
                </tr>

            </thead>


            <tr>
                <td>
                    <asp:Label ID="lblEmpNo" runat="server" Text="Employee No  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSumbit" runat="server" Text="Search" Font-Size="Medium" ForeColor="#FFFFCC"
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


        </table>
    </div>

    <div align="center">
        <table class="style1">

            <tr>
                <td>
                    <asp:Label ID="lblLastPostingDate" runat="server" Text="Last Posting Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastPostingDate" runat="server" CssClass="cls text"
                        Width="190px" Height="25px" Font-Size="Medium" img src="../Images/calender.png" Enabled="False"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="lblNewPostingDate" runat="server" Text="New Posting Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtNewPostingDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="190px" Height="25px" Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtNewPostingDate_TextChanged"></asp:TextBox>
                </td>

            </tr>


            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLArea" runat="server" Text="Last District/Area:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastArea" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastAreaCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewArea" runat="server" Text="New District/Area:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewArea" runat="server" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLLocation" runat="server" Text="Last Posting/Location:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastLocation" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastLocationCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewLocation" runat="server" Text="New Posting/Location:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewLocation" runat="server" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLSection" runat="server" Text="Last Section :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastSection" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastSectionCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewSection" runat="server" Text="New Section :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewSection" runat="server" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLDepartment" runat="server" Text="Last Department :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastDepartment" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastDepartmentCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewDepartment" runat="server" Text="New Department :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewDepartment" runat="server" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLProject" runat="server" Text="Last Project :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastProject" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastProjectCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewProject" runat="server" Text="New Project :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewProject" runat="server" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">CORE</asp:ListItem>
                        <asp:ListItem Value="2">CSI</asp:ListItem>
                        <asp:ListItem Value="3">TEACHERS</asp:ListItem>

                    </asp:DropDownList>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLDesignation" runat="server" Text="Last Designation :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastDesignation" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastDesignationCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewDesignation" runat="server" Text="New Designation :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewDesignation" runat="server" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLCashCode" runat="server" Text="Last Cash Code :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastCashCodeDesc" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastCashCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewCashCode" runat="server" Text="New Cash Code :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewCashCode" runat="server" Height="25px" Width="347px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>

            </tr>

        </table>
    </div>


    <br />
    <br />
    <div align="center">
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
            Height="25px" OnClick="BtnUpdate_Click" />&nbsp;
        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Cancel Information" CssClass="button Blue"
            Height="25px" OnClick="BtnCancel_Click" />&nbsp;
        <asp:Button ID="BtnView" runat="server" Text="History" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="History Information" CssClass="button green"
            Height="25px" OnClick="BtnView_Click" />&nbsp;
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="25px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>
    <br />

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField HeaderText="Employee" DataField="EmpCode" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField HeaderText="Posting Date" DataField="EmpPostingDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="110px" ItemStyle-Width="110px" />
                <asp:BoundField HeaderText="District/Area" DataField="EmpNewAreaDesc" HeaderStyle-Width="175px" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Posting/Location" DataField="EmpNewLocationDesc" HeaderStyle-Width="175px" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Section" DataField="EmpNewSectionDesc" HeaderStyle-Width="175px" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Department" DataField="EmpNewDepartmentDesc" HeaderStyle-Width="175px" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Project" DataField="EmpNewProjectDesc" HeaderStyle-Width="175px" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Designation" DataField="EmpNewDesigDesc" HeaderStyle-Width="175px" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Cash Code" DataField="EmpNewCashCode" HeaderStyle-Width="175px" ItemStyle-Width="175px" ItemStyle-HorizontalAlign="Left" />


            </Columns>

        </asp:GridView>

    </div>


    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlStatus" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

