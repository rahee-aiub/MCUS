<%@ Page Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true"
    CodeBehind="HRAllowanceControlMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRAllowanceControlMaint"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 160px;
            width: 1000px;
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
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3" style="color: Black" align="center">
                        <p align="center">
                            Allowance Control Maintenance
                        </p>
                    </th>
                </tr>
            </thead>
            <tr>
                <th class="auto-style1">Allowance Code
                </th>
                <td>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlAllowance" runat="server" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Height="25px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlAllowance_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th class="auto-style1">Depends on
                </th>
                <td>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDependsOn" runat="server" Height="25px" Width="200px" CssClass="cls text"
                        Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" OnSelectedIndexChanged="ddlDependsOn_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Grade Basis</asp:ListItem>
                        <asp:ListItem Value="2">Designation Basis</asp:ListItem>
                        <asp:ListItem Value="3">All</asp:ListItem>
                    </asp:DropDownList>



                </td>
            </tr>
        </table>
    </div>

    <div align="center">
        <table class="style1">
            <tr>
                <td>
                    <asp:CheckBox ID="ChkLocation" runat="server" ForeColor="Black" Text="District/Area Basis" Checked="false" AutoPostBack="True" OnCheckedChanged="ChkLocation_CheckedChanged" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="ChkPercentage" runat="server" ForeColor="Black" Text="Percentage" Checked="false" AutoPostBack="True" OnCheckedChanged="ChkPercentage_CheckedChanged" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="ChkServiceType" runat="server" ForeColor="Black" Text="Service Type Basis" Checked="false" OnCheckedChanged="ChkServiceType_CheckedChanged" AutoPostBack="True" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="ChkReligion" runat="server" ForeColor="Black" Text="Religion Basis" Checked="false" AutoPostBack="True" OnCheckedChanged="ChkReligion_CheckedChanged"  />
                </td>
            </tr>
            
        </table>
    </div>


    <div align="center">
        <table class="style1">
            <tr>

                <td>
                    <h3>
                        <asp:Label ID="lblColumn1" runat="server" Text=""></asp:Label></h3>

                    <asp:DropDownList ID="ddlColumn1" runat="server" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Height="25px" Width="200px">
                  <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Head Office</asp:ListItem>
                        <asp:ListItem Value="2">Field Office</asp:ListItem>
                  </asp:DropDownList>


                </td>

                <td>
                    <h3>
                        <asp:Label ID="lblColumn2" runat="server" Text=""></asp:Label></h3>

                    <asp:DropDownList ID="ddlColumn2" runat="server" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Height="25px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlColumn2_SelectedIndexChanged">
                    </asp:DropDownList>


                </td>
                
                <td>&nbsp;</td>
                <td>

                    <h3>
                        <asp:Label ID="lblColumn3" runat="server" Text=""></asp:Label></h3>

                    <asp:DropDownList ID="ddlColumn3" runat="server" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Height="25px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlColumn3_SelectedIndexChanged">
                    </asp:DropDownList>


                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblColumn4" runat="server" Text=""></asp:Label></h3>

                    <asp:DropDownList ID="ddlColumn4" runat="server" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Height="25px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlColumn4_SelectedIndexChanged">
                    </asp:DropDownList>


                </td>

                <td>
                    <h3>
                        <asp:Label ID="lblColumn5" runat="server" Text=""></asp:Label></h3>

                    <asp:DropDownList ID="ddlColumn5" runat="server" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge" Height="25px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlColumn5_SelectedIndexChanged">
                    </asp:DropDownList>


                </td>
                <td>&nbsp;</td>
                <td>
                    <h3>
                        <asp:Label ID="lblColumn6" runat="server" Text=""></asp:Label></h3>
                    <asp:TextBox ID="txtColumn6" runat="server" Width="140px" Height="25px" Font-Size="Large" onchange="javascript:this.value=Comma(this.value);"
                         
                        CssClass="cls text"></asp:TextBox>
                </td>

                <td>
                    <h3>
                        <asp:Label ID="lblStat" runat="server" Text="Status"></asp:Label></h3>

                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="ChkStatus" runat="server" Width="140px" Height="25px" ForeColor="Black" Text="" Checked="true" />
                </td>


            </tr>
        </table>
    </div>
    <br />


    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound" OnSelectedIndexChanged="gvDetailInfo_SelectedIndexChanged">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:TemplateField HeaderText="Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AllowCode" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblAllowCode" runat="server" Text='<%# Eval("AllowCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="BaseGrade" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblBaseGrade" runat="server" Text='<%# Eval("BaseGrade") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="BaseGrade" Visible="true" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblBaseGradeDesc" runat="server" Text='<%# Eval("BaseGradeDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Grade" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GradeCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Grade" Visible="true" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblGradeDesc" runat="server" Text='<%# Eval("GradeDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Desig." Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesig" runat="server" Text='<%# Eval("DesignationCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Desig." Visible="true" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesigDesc" runat="server" Text='<%# Eval("DesigDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Location" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("LocationCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Location" Visible="true" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationDesc" runat="server" Text='<%# Eval("LocationDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Service Type" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblServType" runat="server" Text='<%# Eval("ServTypeCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Service Type" Visible="true" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblServTypeDesc" runat="server" Text='<%# Eval("ServTypeDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Religion" Visible="false" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblReligion" runat="server" Text='<%# Eval("ReligionCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Religion" Visible="true" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblReligionDesc" runat="server" Text='<%# Eval("ReligionDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Amt./Perc" Visible="true" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status" Visible="true" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblStat" runat="server" Text='<%# Eval("Status") %>'></asp:Label>                       
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



    <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return ValidationBeforeSave(event,this)"
                        CssClass="button green size-120" OnClick="btnSubmit_Click" />&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return ValidationBeforeSave(event,this)"
                        CssClass="button green size-120" OnClick="btnUpdate_Click" />&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return ValidationBeforeSave(event,this)"
                        CssClass="button green size-120" OnClick="btnDelete_Click" />&nbsp;
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-100" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DivMessage" runat="server" align="center">
        <table class="style1">
            <tr>
                <td colspan="2" class="auto-style2">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style2"></td>
            </tr>
        </table>
    </div>

    <asp:Label ID="lblAllowance" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDependson" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="rowlbl1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="rowlbl2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="rowlbl3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblRecFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="MsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="DupMsg" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ReadFlag" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
