<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRPayScaleMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRPayScaleMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
           <%-- var txtBase = document.getElementById('<%=txtBase.ClientID%>').value;--%>
             var txtPayScale = document.getElementById('<%=txtPayscale.ClientID%>').value;
             <%--if (txtBase == '' || txtBase.length == 0) {
                 document.getElementById('<%=txtBase.ClientID%>').focus();
                 alert('Please Input Base No.');
             }
             else --%>if (txtPayScale == '' || txtPayScale.length == 0) {
                 document.getElementById('<%=txtPayscale.ClientID%>').focus();
                         alert('Please Input PayScale');
                     }
                     else
                         return confirm('Are you sure you want to save information?');
                 return false;

             }

             function ValidationBeforeUpdate() {
                 return confirm('Are you sure you want to Update information?');
             }
             function ValidationBeforeDelete() {
                 return confirm('Are you sure you want to Delete information?');
             }
    </script>


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 170px;
            margin: 0 auto;
            width: 900px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }

        .auto-style2 {
            width: 363px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">PayScale Maintenance
                    </th>
                </tr>
            </thead>
        </table>
        <br />
        <table class="style1">
            <tr>

                <td>
                    <asp:Label ID="lblBase" runat="server" Text="Base:" Font-Size="Large"
                        ForeColor="Black" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBase" runat="server" Height="25px" Width="170px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Head Office</asp:ListItem>
                        <asp:ListItem Value="2">Field Office</asp:ListItem>
                        
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPayscale" runat="server" Text="Grade:" Font-Size="Large"
                        ForeColor="Black" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPayscale" runat="server" CssClass="cls text" Width="163px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtPayscale_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblShow" runat="server" Font-Size="Large"
                        ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                </td>
            </tr>

        </table>
    </div>


    <div align="center">
        <table class="style1">
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="lblStartBasic" runat="server" Text="Start Basic"></asp:Label></h3>

                    <asp:TextBox ID="txtStartBasic" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" TabIndex="3"></asp:TextBox>


                </td>
                <td>&nbsp;</td>
                <td>

                    <h3>
                        <asp:Label ID="lblBar1" runat="server" Text="Bar1"></asp:Label></h3>
                    <asp:TextBox ID="txtBar1" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblNolabel1" runat="server" Text="No Label1 "></asp:Label></h3>
                    <asp:TextBox ID="txtNolabel1" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" AutoPostBack="True" OnTextChanged="txtNolabel1_TextChanged"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>
                    <h3>
                        <asp:Label ID="lblEndBasic1" runat="server" Text="End Basic1"></asp:Label></h3>
                    <asp:TextBox ID="txtEndBasic1" runat="server" Width="140px" Height="25px" Font-Size="Large"
                        CssClass="cls text"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblBar2" runat="server" Text="Bar2"></asp:Label></h3>
                    <asp:TextBox ID="txtBar2" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblNolabel2" runat="server" Text="No Label2"></asp:Label></h3>
                    <asp:TextBox ID="txtNolabel2" runat="server" Width="140px" Height="25px" Font-Size="Large"
                        CssClass="cls text" AutoPostBack="True" OnTextChanged="txtNolabel2_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblEndBasic2" runat="server" Text="End Basic2"></asp:Label></h3>
                    <asp:TextBox ID="txtEndBasic2" runat="server" Width="140px" Height="25px" Font-Size="Large"
                        CssClass="cls text"></asp:TextBox>
                </td>
                 <td>
                    <h3>
                        <asp:Label ID="lblConsulted" runat="server" Text="Consolidated"></asp:Label></h3>
                    <asp:TextBox ID="txtconsulted" runat="server" Width="140px" Height="25px" Font-Size="Large"
                        CssClass="cls text"></asp:TextBox>
                </td>


            </tr>
        </table>
    </div>
    <br />
    <div>
        <table>
            <tr>
                <td class="auto-style2"></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Width="91px" Font-Size="Large"
                        ForeColor="White" Height="25px" Font-Bold="True" OnClientClick="return ValidationBeforeSave() "
                        CssClass="button green" OnClick="BtnSubmit_Click" />
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Width="91px" Font-Size="Large"
                        ForeColor="White" Height="25px" Font-Bold="True" ToolTip="Update Information"
                        OnClientClick="return ValidationBeforeUpdate() " CssClass="button  blue" OnClick="BtnUpdate_Click" />
                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" Width="91px" Font-Size="Large"
                        ForeColor="White" Height="25px" Font-Bold="True" ToolTip="Delete Information"
                        OnClientClick="return ValidationBeforeDelete() " CssClass="button" OnClick="BtnDelete_Click" />
                    <asp:Button ID="BtnView" runat="server" Text="View" Font-Size="Large"
                        ForeColor="#FFFFCC" Height="25px" Width="86px" Font-Bold="True" ToolTip="View"
                        CausesValidation="False" CssClass="button yellow" OnClick="BtnView_Click" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" Font-Size="Large"
                        ForeColor="#FFFFCC" Height="25px" Width="86px" Font-Bold="True" ToolTip="Exit Page"
                        CausesValidation="False" CssClass="button red" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound1" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField HeaderText="Base" DataField="Base" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Grade" DataField="Payscale" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Start Basic" DataField="StartBasic" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="Bar1" DataField="Bar1" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="label1" DataField="label1" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="End1Basic" DataField="End1Basic" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="Bar2" DataField="Bar2" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="label2" DataField="label2" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="End2Basic" DataField="End2Basic" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />
                 <asp:BoundField HeaderText="Consulted" DataField="Consolidated" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />

            </Columns>

        </asp:GridView>
        </div>

        <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo1" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo1_RowDataBound1" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField HeaderText="Base" DataField="Base" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="PayScale" DataField="Payscale" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="PayLabel" DataField="PayLabel" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Bar" DataField="Bar" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="Basic" DataField="Basic" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="right" />

            </Columns>

        </asp:GridView>

    </div>


</asp:Content>

