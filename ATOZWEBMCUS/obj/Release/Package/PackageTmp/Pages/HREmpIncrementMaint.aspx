<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmpIncrementMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmpIncrementMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/styletext.css" rel="stylesheet" />

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            width: 1500px;
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
            $("#<%= txtNewIncrementDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtNewIncrementDate.ClientID %>").datepicker();

            });

        });

          </script>



    <script language="javascript" type="text/javascript">
        function ValidationBeforeUpdate() {
            var txtNewIncrementDate = document.getElementById('<%=txtNewIncrementDate.ClientID%>').value;

            if (txtNewIncrementDate == '' || txtNewIncrementDate.length == 0) {
                document.getElementById('<%=txtNewIncrementDate.ClientID%>').focus();
                alert('Please Input Increment Date');
            }
            else
                return confirm('Are you sure you want to Update information?');
            return false;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Employee's Increment Maintenance
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
                    <asp:Label ID="lblLIncrementDate" runat="server" Text="Last Increment Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastIncrementDate" runat="server" CssClass="cls text"
                        Width="190px" Height="25px" Font-Size="Medium" img src="../Images/calender.png" Enabled="False"></asp:TextBox>


                </td>

                <td>
                    <asp:Label ID="lblNewIncrementDate" runat="server" Text="New Increment Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtNewIncrementDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="190px" Height="25px" Font-Size="Medium" img src="../Images/calender.png"></asp:TextBox>
                </td>

            </tr>

             <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLBaseGrade" runat="server" Text="Last Base Grade :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastBaseGrade" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastBaseGradeCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewBaseGrade" runat="server" Text="New Base Grade :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlNewBaseGrade" runat="server" Height="25px" Width="200px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="true" OnSelectedIndexChanged="ddlNewBaseGrade_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Heade Office</asp:ListItem>
                        <asp:ListItem Value="2">Field Office</asp:ListItem>
                    </asp:DropDownList>

                </td>

            </tr>
                       
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLGrade" runat="server" Text="Last Grade :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastGrade" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastGradeDesc" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewGrade" runat="server" Text="New Grade :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtNewGrade" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="190px" Height="25px" Font-Size="Medium" AutoPostBack="true" OnTextChanged="txtNewGrade_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblNewGradeDesc" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLPayScale" runat="server" Text="Last Pay Scale :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastPayScale" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lblLastPayScaleCode" runat="server" Text="" Font-Size="Large" Visible="false"></asp:Label>


                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewPayScale" runat="server" Text="New Pay Scale :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtNewPayScale" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="190px" Height="25px" Font-Size="Medium"></asp:TextBox>

                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLPayLabel" runat="server" Text="Last Pay Steps :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastPayLabel" runat="server" Text="" Font-Size="Large"></asp:Label>
                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewPayLabel" runat="server" Text="New Pay Steps :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtNewPayLabel" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="190px" Height="25px" Font-Size="Medium" AutoPostBack="true" OnTextChanged="txtNewPayLabel_TextChanged"></asp:TextBox>


                </td>

            </tr>

            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblLBasic" runat="server" Text="Last Basic :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblLastBasic" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>

                <td class="auto-style5">
                    <asp:Label ID="lblNewBasic" runat="server" Text="New Basic :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtNewBasic" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="190px" Height="25px" Font-Size="Medium"></asp:TextBox>


                </td>

            </tr>



        </table>
    </div>


    <br />

    <div align="center">
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
            Height="22px" OnClick="BtnUpdate_Click" />&nbsp;
        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Cancel Information" CssClass="button Blue"
            Height="22px" OnClick="BtnCancel_Click"/>&nbsp;
        <asp:Button ID="BtnView" runat="server" Text="History" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="History Information" CssClass="button green"
            Height="22px" OnClick="BtnView_Click" />&nbsp;
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>
    <br />

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField HeaderText="Employee" DataField="EmpCode" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField HeaderText="Date" DataField="EmpIncrementDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Base Grade" DataField="EmpNewBaseGradeDesc" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Grade" DataField="EmpNewGrade" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Pay Scale" DataField="EmpNewPayScaleDesc" HeaderStyle-Width="200px" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Steps" DataField="EmpNewPayLabel" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Basic" DataField="EmpNewBasic" HeaderStyle-Width="100px" ItemStyle-Width="100px" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />


            </Columns>

        </asp:GridView>

    </div>


    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlStatus" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="lblstbasic" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblbar1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbllabel1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblEndbasic1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblbar2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbllabel2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblEndbasic2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblconsulted" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGrade" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPayScale" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPayLabel" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBasic" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblBaseGrade" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

