<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSBoothNomineeMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSBoothNomineeMaint" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
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
            height: 200px;
            margin: 0 auto;
            width:1200px;
            
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div align="center">
        <table class="style1">
            <%--<tr>
                <td>
                    <asp:Label ID="lblNo" runat="server" Text="Nominee No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large" AutoPostBack="true" OnTextChanged="txtNo_TextChanged"></asp:TextBox>

                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Nominee Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ToolTip="Enter Name" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblRelation" runat="server" Text="Relation:" Font-Size="Large" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:TextBox ID="txtRelation" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAddL1" runat="server" Text="Address Line1:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddressL1" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="2"></asp:TextBox>&nbsp;
                    <asp:Label ID="lblTelNo" runat="server" Text="Telephone No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>&nbsp;<asp:TextBox ID="txtTelNo" runat="server" CssClass="cls text" Width="200px" Height="25px"
                            Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddL2" runat="server" Text="Address Line2:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddressL2" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="3"></asp:TextBox>&nbsp;
                    <asp:Label ID="lblMobNo" runat="server" Text="Mobile No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="cls text" Width="200px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddL3" runat="server" Text="Address Line3:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddressL3" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="4"></asp:TextBox>&nbsp;
                    <asp:Label ID="lblEmail" runat="server" Text="E-mail:" Font-Size="Large" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="cls text" Width="200px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDivision" runat="server" Text="Division:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" Height="25px" Width="153px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        CssClass="cls text" Font-Size="Large" TabIndex="5" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblDistrict" runat="server" Text="District:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="153px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        CssClass="cls text" Font-Size="Large" TabIndex="6" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblUpzila" runat="server" Text="Upzila:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlUpzila" runat="server" Height="25px" Width="153px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        CssClass="cls text" Font-Size="Large" TabIndex="7" OnSelectedIndexChanged="ddlUpzila_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblThana" runat="server" Text="Thana:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlThana" runat="server" Height="25px" Width="153px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        CssClass="cls text" Font-Size="Large" TabIndex="7" OnSelectedIndexChanged="ddlThana_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNomSharePer" runat="server" Text="Share Percentage(%):" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNomSharePer" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="121px" Height="25px"
                        Font-Size="Large" AutoPostBack="True"
                        OnTextChanged="txtNomSharePer_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:GridView ID="gvPerTotal" runat="server" AutoGenerateColumns="False" Visible="false">
                        <Columns>
                            <asp:BoundField DataField="NOMSHAREPER" />
                        </Columns>

                    </asp:GridView>
                    <asp:Label ID="lblTotal" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <div align="center" class="grid_scroll">
                        <asp:GridView ID="gvDetailInfo" runat="server"  HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px"
                            OnSelectedIndexChanged="gvDetailInfo_SelectedIndexChanged" OnRowDataBound="gvDetailInfo_RowDataBound">
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <Columns>

                                <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                               <ItemTemplate>
                               <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                               </ItemTemplate>
                               </asp:TemplateField>
                               <%-- <asp:BoundField DataField="NOMNO" HeaderText="Nominee No" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />--%>
                                <asp:BoundField DataField="NomName" HeaderText="Nominee Name" HeaderStyle-Width="120px" ItemStyle-Width="120px" />
                                <asp:BoundField DataField="NomAdd1" HeaderText="Address1" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="NomAdd2" HeaderText="Address2"  HeaderStyle-Width="80px" ItemStyle-Width="80px"/>
                                <asp:BoundField DataField="NomAdd3" HeaderText="Address3" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="NomTel" HeaderText="TelePhone No"  HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                                <asp:BoundField DataField="NomMobile" HeaderText="Mobile No" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="NomRela" HeaderText="Relation"  HeaderStyle-Width="80px" ItemStyle-Width="80px"/>
                                <asp:BoundField DataField="NomSharePer" HeaderText="Share Percentage(%)" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                 
                                <asp:TemplateField HeaderText="Email" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomEmail" runat="server" Text='<%# Eval("NomEmail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Divi" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomDivi" runat="server" Text='<%# Eval("NomDivi") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dist" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomDist" runat="server" Text='<%# Eval("NomDist") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Thana" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomThana" runat="server" Text='<%# Eval("NomThana") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                          
                        </asp:GridView>

                    </div>
                    <br />

                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True"
                        Font-Size="Large" ForeColor="White" OnClientClick="return ValidationBeforeUpdate()"
                        ToolTip="Update Information" CssClass="button green"
                        OnClick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" Font-Bold="True"
                        Font-Size="Large" ForeColor="White" OnClientClick="return ValidationBeforeDelete()"
                        ToolTip="Delete Information" CssClass="button green" OnClick="BtnDelete_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CssClass="button red"  OnClientClick="window.close('CSNewAccountOpenScreen.aspx')" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblmemno" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HoldPerc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:HiddenField ID="hdnID" runat="server" />

</asp:Content>

