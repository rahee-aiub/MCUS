<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRReportEmpMasterFile.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRReportEmpMasterFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Employee's Master Information Report
                    </th>
                </tr>
            </thead>

           

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllArea" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllArea_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblArea" runat="server" Text="District/Area:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;
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

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

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
                    <asp:CheckBox ID="ChkAllDesig" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllDesig_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblDesig" runat="server" Text="Designation:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDesig" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllServType" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllServType_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblServType" runat="server" Text="Service Type:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlServType" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllStatus" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllStatus_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblStatus" runat="server" Text="Status :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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

                <td>
                    <asp:CheckBox ID="ChkAllBaseGrade" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllBaseGrade_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblBaseGrade" runat="server" Text="Base Grade :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlBaseGrade" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddlBaseGrade_SelectedIndexChanged">

                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Heade Office</asp:ListItem>
                        <asp:ListItem Value="2">Field Office</asp:ListItem>
                        <asp:ListItem Value="3">Consolidated</asp:ListItem>

                    </asp:DropDownList>

                </td>
            </tr>

            

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllGrade" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllGrade_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblGrade" runat="server" Text="Grade :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:TextBox ID="txtGrade" runat="server" CssClass="cls text" Width="94px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllSteps" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllSteps_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblSteps" runat="server" Text="Steps :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:TextBox ID="txtSteps" runat="server" CssClass="cls text" Width="94px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>

                </td>
            </tr>


            <tr>

                <td>

                    <asp:Button ID="BtnView" runat="server" Text="Print/Preview" Height="27px" Width="150px" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnView_Click" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    <asp:Label ID="lblDesc1" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc2" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc3" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc4" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc5" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc6" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc7" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc8" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc9" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc10" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc11" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc12" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc13" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc14" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="hdnPeriod" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnMonth" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnYear" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnToDaysDate" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

   

</asp:Content>

