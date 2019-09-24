<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="SYSWeekHolidayMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.SYSWeekHolidayMaintenance" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
	    function ValidationBeforeSave()
	    {    
	     return confirm('Are you sure you want to save information?');	  
	    }
	    
	    function ValidationBeforeUpdate()
	    {    
	     return confirm('Are you sure you want to Update information?');	  
	    }
	   	    
    </script>


     <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 190px;
            width: 600px;
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
    </style>
     <script src="../scripts/validation.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <br />
    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
             <thead>
                <tr>
                    <th colspan="3">
                  Weekly Holiday Maintenance
                    </th>
                </tr>
              
            </thead>
            
            <tr>
                <td>
                    <asp:Label ID="lblWeekDay1" runat="server" Text="Day 1 :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlHolWeekDay1" runat="server" Height="25px" 
                        Width="200px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>             
                        <asp:ListItem Value="1">Sunday</asp:ListItem>
                        <asp:ListItem Value="2">Monday</asp:ListItem>
                        <asp:ListItem Value="3">Tuesday</asp:ListItem>
                        <asp:ListItem Value="4">Wednesday</asp:ListItem>
                        <asp:ListItem Value="5">Thursday</asp:ListItem>
                        <asp:ListItem Value="6">Friday</asp:ListItem>
                        <asp:ListItem Value="7">Saturday</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblWeekDay2" runat="server" Text="Day 2 :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:DropDownList ID="ddlHolWeekDay2" runat="server" Height="25px" 
                        Width="200px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>             
                        <asp:ListItem Value="1">Sunday</asp:ListItem>
                        <asp:ListItem Value="2">Monday</asp:ListItem>
                        <asp:ListItem Value="3">Tuesday</asp:ListItem>
                        <asp:ListItem Value="4">Wednesday</asp:ListItem>
                        <asp:ListItem Value="5">Thursday</asp:ListItem>
                        <asp:ListItem Value="6">Friday</asp:ListItem>
                        <asp:ListItem Value="7">Saturday</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeSave()" onclick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" />&nbsp;
                    
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"/>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <br />
    
<asp:Label ID="lblHolTypeDesc" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>
