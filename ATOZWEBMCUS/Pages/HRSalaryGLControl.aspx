<%@ Page Title="AllowanceCodeMaintenance" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRSalaryGLControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRSalaryGLControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

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
            width: 483px;
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
                    <th colspan="3">Salary Code Control Maintenance
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblCtrlHead" runat="server" Text="Control Head :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:DropDownList ID="ddlCtrlHead" runat="server" Height="25px"
                        Width="200px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlCtrlHead_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Allowance</asp:ListItem>
                        <asp:ListItem Value="2">Deduction</asp:ListItem>
                        <asp:ListItem Value="99">Stop Salary Payment</asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Allowance Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlCode" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            

            
        </table>
    </div>
    <br />
    <br />
    <div class="grid_scroll">
        <asp:GridView ID="gvGLControlInfo" runat="server" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvGLControlInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>

                <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CtrlHead" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblCtrlHead" runat="server" Text='<%# Eval("SalCtrlHead") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CtrlCode" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblCtrlCode" runat="server" Text='<%# Eval("SalCtrlCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Project" Visible="false" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblProject" runat="server" Text='<%# Eval("SalCtrlPayType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Project" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblProjectDesc" runat="server" Text='<%# Eval("SalCtrlPayTypeDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
               
                <asp:TemplateField HeaderText="GL Code" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGLCode" runat="server" Text='<%# Eval("SalCtrlGLCode") %>' Font-Bold="true"></asp:TextBox>

                    </ItemTemplate>
                </asp:TemplateField>

                


            </Columns>
        </asp:GridView>

    </div>

    <div align="center">
        <table>
            <tr>
                <td></td>
                <td>
                    
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" />&nbsp;
                    
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <br />
    

</asp:Content>

