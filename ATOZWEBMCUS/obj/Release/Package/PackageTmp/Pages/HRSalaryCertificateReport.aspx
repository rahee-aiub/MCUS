<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRSalaryCertificateReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRSalaryCertificateReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed???');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 363px;
            margin: 0 auto;
            width: 1650px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            Width: 1600px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Salary Certificate Report
                    </th>
                </tr>
            </thead>


            <tr>
                <td>
                    <asp:Label ID="lblPeriod" runat="server" Text="Month of :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                <%--</td>
                <td>--%>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlPeriodMM" runat="server" Height="25px" Width="200px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">January </asp:ListItem>
                        <asp:ListItem Value="2">February </asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlPeriodYYYY" runat="server" Height="25px" Width="100px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        
                    </asp:DropDownList>


                </td>

            </tr>


            <tr>
                <td>
                   <asp:CheckBox ID="ChkAllEmp" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" OnCheckedChanged="ChkAllEmp_CheckedChanged" />
                <%--</td>

                <td style="background-color: #fce7f9">--%>
                     &nbsp;
                    <asp:Label ID="lblEmp" runat="server" ForeColor="Red" Text="Employee's " Font-Size="Large"></asp:Label>
                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                    <asp:TextBox ID="txtEmpID" runat="server" CssClass="cls text" Width="71px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True" OnTextChanged="txtEmpID_TextChanged"></asp:TextBox>

                    <asp:DropDownList ID="ddlEmp" runat="server" Height="31px" Width="418px" AutoPostBack="True"
                        Font-Size="Large" Style="margin-left: 7px" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
           
                </td>

            </tr>


            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllArea" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllArea_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblArea" runat="server" Text="District/Area:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlArea" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllLocation" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllLocation_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblLocation" runat="server" Text="Posting/Location:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;
                    <asp:DropDownList ID="ddlLocation" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllProject" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllProject_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblProject" runat="server" Text="Project:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlProject" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">CORE</asp:ListItem>
                        <asp:ListItem Value="2">CSI</asp:ListItem>
                        <asp:ListItem Value="3">TEACHERS</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllReligion" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllReligion_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblReligion" runat="server" Text="Religion:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlReligion" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllGender" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllGender_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblGender" runat="server" Text="Gender:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:DropDownList ID="ddlGender" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Male</asp:ListItem>
                        <asp:ListItem Value="2">Female</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>

             <tr>

                <td>
                    <asp:CheckBox ID="ChkAllStatus" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllStatus_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblStatus" runat="server" Text="Service Status :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:DropDownList ID="ddlStatus" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
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
                        <asp:ListItem Value="9">Transfer</asp:ListItem>
                        <asp:ListItem Value="10">Death</asp:ListItem>
                        <asp:ListItem Value="99">Stop Salary Payment</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>

            
            <tr>
                <%--<td></td>--%>
                <td>
                    <asp:Button ID="BtnPrint" runat="server" Text="View/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="130px" CssClass="button green" OnClick="BtnPrint_Click"/>&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>

    

    <asp:HiddenField ID="hdnPeriod" runat="server" />
    <asp:HiddenField ID="hdnDate" runat="server" />
    <asp:HiddenField ID="hdnMonth" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" />
    <asp:HiddenField ID="hdnMsg" runat="server" />
    <asp:HiddenField ID="hdnToDaysDate" runat="server" />

        
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
