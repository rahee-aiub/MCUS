<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmployeeIncomeTaxRegister.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmployeeIncomeTaxRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 363px;
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
                    <th colspan="3" class="auto-style1">Employee's Income Tax Register
                    </th>
                </tr>
            </thead>

            <tr>
                
                <td>
                    
                    <asp:Label ID="lblPeriod" runat="server" Text="As on Month :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;
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

            

            <%-- <tr>

                <td>
                    <asp:CheckBox ID="ChkAllArea" runat="server" Font-Size="Large" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllArea_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblArea" runat="server" Text="District/Area:" Font-Size="Large" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlArea" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkAllLocation" runat="server" Font-Size="Large" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllLocation_CheckedChanged" />

                    &nbsp;
                    <asp:Label ID="lblLocation" runat="server" Text="Posting/Location:" Font-Size="Large" ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;<asp:DropDownList ID="ddlLocation" runat="server" Height="25px" Width="247px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                    </asp:DropDownList>

                </td>
            </tr>--%>


              <br />
              <br />
              <br />
            <tr>
                
                <td class="auto-style1">

                    &nbsp;&nbsp;&nbsp;

                    <asp:Button ID="BtnView" runat="server" Text="Print/Preview" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"  Height="27px" Width="150px" OnClick="BtnView_Click" />
                    &nbsp; <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    
     <asp:Label ID="hdnPeriod" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="hdnMonth" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="hdnYear" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="hdnToDaysDate" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

