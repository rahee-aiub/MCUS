<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRMonthlySalaryRegister.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRMonthlySalaryRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Monthly Salary Register
                    </th>
                </tr>
            </thead>

            <tr>
                
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPeriod" runat="server" Text="Month of :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlPeriodMM" runat="server" Height="25px" Width="200px" CssClass="cls text"
                        Font-Size="Large">
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
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        <asp:ListItem Value="2020">2020</asp:ListItem>
                    </asp:DropDownList>


                </td>

            </tr>


            <tr>
                <td>
                    <asp:RadioButton ID="rbtDetail" runat="server" OnCheckedChanged="rbtDetail_CheckedChanged" AutoPostBack="True" Text="Details" />
                    <asp:RadioButton ID="rbtSummary" runat="server" OnCheckedChanged="rbtSummary_CheckedChanged" AutoPostBack="True" Text="Summary" />
                    <asp:RadioButton ID="rbtBrackUpRepColumn" runat="server" AutoPostBack="True" Text="Break Rep.Column" OnCheckedChanged="rbtBrackUpRepColumn_CheckedChanged" />
                    <asp:RadioButton ID="rbtBrackUpDtl" runat="server" AutoPostBack="True" Text="Break Up Details" OnCheckedChanged="rbtBrackUpDtl_CheckedChanged" />
                    <asp:RadioButton ID="rbtBrackUpSum" runat="server" AutoPostBack="True" Text="Break Up Summary" OnCheckedChanged="rbtBrackUpSum_CheckedChanged"/>

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
                <td>
                    <asp:RadioButton ID="rbtAllowance" runat="server" AutoPostBack="True" Text="Allowance" OnCheckedChanged="rbtAllowance_CheckedChanged" />
                    <asp:RadioButton ID="rbtDeduction" runat="server" AutoPostBack="True" Text="Deduction" OnCheckedChanged="rbtDeduction_CheckedChanged" />
                    
                </td>

            </tr>


            <tr>

                <td>
                    <asp:Label ID="lblMode" runat="server" Text="" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlMode" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:Label ID="lblRepColumn" runat="server" Text=" Report Column :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlRepColumn" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                        
                    </asp:DropDownList>

                </td>
            </tr>


            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllZero" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="false" OnCheckedChanged="ChkAllGender_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblZero" runat="server" Text="With Zero Amount" Font-Size="Medium" ForeColor="Red"></asp:Label>

                
                    

                </td>
            </tr>


            <tr>
               
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnView" runat="server" Text="Print/Preview" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" Height="27px" Width="150px" OnClick="BtnView_Click" />
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

    <asp:Label ID="CtrlColumnRec" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="ZeroBal" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

     <asp:Label ID="CtrlProgFlag" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

   

</asp:Content>

