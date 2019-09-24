<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="SYSThanaCodeMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.SYSThanaCodeMaintenance" Title="Thana Maintenance" %>

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
            width: 540px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 514px;

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
                    <th colspan="3">Thana Code Maintenance
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Division Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="115px" Height="25px" Font-Size="Large" AutoPostBack="true"
                        OnTextChanged="txtcode_TextChanged"></asp:TextBox>

                    <asp:DropDownList ID="ddlDivision" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblDistcode" runat="server" Text="District Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDistcode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="118px" Height="25px" Font-Size="Large"
                        onkeypress="return functionx(event)" AutoPostBack="True"
                        OnTextChanged="txtDistcode_TextChanged"></asp:TextBox>

                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>

                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblUpzilacode" runat="server" Text="Upzila Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUpzilacode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="118px" Height="25px" Font-Size="Large"
                        onkeypress="return functionx(event)" AutoPostBack="True"
                        OnTextChanged="txtUpzilacode_TextChanged"></asp:TextBox>

                    <asp:DropDownList ID="ddlUpzila" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlUpzila_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>

                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblThanacode" runat="server" Text="Thana Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlThana" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlThana_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                    </asp:DropDownList>

                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblThanaDescription" runat="server" Text="Description:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtThanaDescription" runat="server" CssClass="cls text" Width="421px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>

            </tr>


            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" />&nbsp;
                     <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green" 
                         onclick="BtnView_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />

                </td>
            </tr>
        </table>
    </div>
    <br />
     <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
 AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                  
                <asp:BoundField HeaderText="Divi Code" DataField="DiviCode" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Dist Code" DataField="DistCode" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Thana Code" DataField="ThanaCode" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Description" DataField="ThanaDescription" HeaderStyle-Width="250px" ItemStyle-Width="250px" />
                
              </Columns>
           
        </asp:GridView>
     </div>

    <asp:Label ID="hdnDiviCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnDistCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnUpzilaCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnThanaCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnDiviOrgCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnDistOrgCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnUpzilaOrgCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnThanaOrgCode" runat="server" Visible="False"></asp:Label>
    

</asp:Content>
