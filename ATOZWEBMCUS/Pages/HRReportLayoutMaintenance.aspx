<%@ Page Title="HRReportLayoutMaintenance" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRReportLayoutMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRReportLayoutMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
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
            $("#<%= txtReportDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtReportDate.ClientID %>").datepicker();

            });

        });
        

    </script>



    

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 186px;
            width: 500px;
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
                    <th colspan="3">Report Layout Maintenance
                    </th>
                </tr>

            </thead>
            <tr>
                <td >
                    <asp:Label ID="lblDate" runat="server" Text="Report Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtReportDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="150px" Height="25px" Font-Size="Medium" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRepColumn" runat="server" Text="Report Column:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRepColumn" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtRepColumn_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlRepColumn" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlRepColumn_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRepColumnName" runat="server" Text="Report Column Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRepColumnName" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblRepColumnFlag" runat="server" Text="Report Column Mode:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblRepColFlag" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large"></asp:Label>
                    
                </td>
            </tr>

        </table>
    </div>

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDescription" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDescription_RowDataBound">
             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Code" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="300px" ItemStyle-Width="300px" />
                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDescription" runat="server" ItemStyle-HorizontalAlign="Center"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Check/Uncheck" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                  </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>

     </div>


    <div align="center">
        <table>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                Font-Bold="True" ToolTip="Insert Information" CssClass="button green" Height="27px" Width="96px"
                OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green" Height="27px" Width="96px"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" />&nbsp;
                    
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
            <br />
        </td>
    </tr>
    </table>
    </div>
    <br />
    

    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
</asp:Content>

