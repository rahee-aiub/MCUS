<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmpLeaveApplication.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmpLeaveApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/styletext.css" rel="stylesheet" />


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 363px;
            margin: 0 auto;
            width: 750px;
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

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {

            var txtEmpNo = document.getElementById('<%=txtEmpNo.ClientID%>').value;
            var ddllType = document.getElementById('<%=ddllType.ClientID%>');
            var txtStartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
            var txtEndDate = document.getElementById('<%=txtEndDate.ClientID%>').value;
            if (txtEmpNo == '' || txtEmpNo.length == 0) {
                document.getElementById('<%=txtEmpNo.ClientID%>').focus();
                alert('Please Input Employee No.');
            }
            else if ((ddllType.selectedIndex == 0)) {
                document.getElementById('<%=ddllType.ClientID%>').focus();
                alert('Please Select Leave Type.');
            }
            else if (txtStartDate == '' || txtStartDate.length == 0) {
                document.getElementById('<%=txtStartDate.ClientID%>').focus();
	            alert('Please Input Leave Start Date.');
	        }
	        else if (txtEndDate == '' || txtEndDate.length == 0) {
	            document.getElementById('<%=txtEndDate.ClientID%>').focus();
                          alert('Please Input Leave End Date.');
                      }
                      else
                          return confirm('Are you sure you want to save information?');
          return false;

      }

      function ValidationBeforeUpdate() {
          return confirm('Are you sure you want to Update information?');
      }

    </script>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />--%>


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtStartDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtStartDate.ClientID %>").datepicker();

            });

        });
        $(function () {
            $("#<%= txtEndDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtEndDate.ClientID %>").datepicker();

            });

        });

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />

    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Leave Application Maintenance
                    </th>
                </tr>

            </thead>


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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%-- <td>
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>--%>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="District/Area  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblArea" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%-- <td>
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>--%>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Posting/Location  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblLocation" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%-- <td>
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>--%>
            </tr>
        </table>
    </div>

    <div align="center">
        <table class="style1">

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lbllType" runat="server" Text="Leave Type:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddllType" runat="server" Height="25px" Width="240px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" OnSelectedIndexChanged="ddllType_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTotDay" runat="server" Text="Total Days :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtTotDay" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="50px" Height="25px" Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td></td>

                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:Label ID="lblBalDay" runat="server" Text="Balance Days :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtBalDay" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="50px" Height="25px" Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>


                </td>


            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="120px" Height="25px" Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblEnddate" runat="server" Text="End Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="txtEndDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                         Width="120px" Height="25px" Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>


                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                    <asp:Label ID="lblDay" runat="server" Text="Days :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                       <asp:TextBox ID="txtDay" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                           Width="50px" Height="25px" Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>

                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Leave Purpose :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLPurpose" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="500px" Height="25px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Note :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLNote" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="500px" Height="25px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             </td>
            </tr>

        </table>
    </div>
    <br />
    <br />
    <div align="center">
        <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Save Information" CssClass="button green" OnClientClick="return ValidationBeforeSave()"
            Height="24px" Width="86px" OnClick="BtnSave_Click" />&nbsp;
        <asp:Button ID="BtnView" runat="server" Text="History" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="History Information" CssClass="button green"
            Height="24px" Width="86px" OnClick="BtnView_Click" />&nbsp;

        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                 <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="139px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Enabled="false" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField HeaderText="Start Date" DataField="LStartDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="150px" ItemStyle-Width="150px" />
                <asp:BoundField HeaderText="End Date" DataField="LEndDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="150px" ItemStyle-Width="150px" />
                <asp:BoundField HeaderText="No.Days" DataField="LApplyDays" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField HeaderText="Balance Days" DataField="LBalance" HeaderStyle-Width="200px" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="center" />

                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_Click" CssClass="button green" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>



    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlStatus" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblprocYear" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBalDays" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUsedDays" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblEffectSalary" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

