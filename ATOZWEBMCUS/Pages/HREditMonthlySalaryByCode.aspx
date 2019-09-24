<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREditMonthlySalaryByCode.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREditMonthlySalaryByCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function UpdateValidation() {
            return confirm('Do you want to Update Data?');
        }
        function RejectValidation() {
            return confirm('Do you want to Reject Data?');
        }
    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width: 1000px;
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

    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Edit Monthly Salary By Code
                    </th>
                </tr>
            </thead>


             <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Process Month :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    </td>
                 <td>
                   
                    <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="300px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    
                </td>
            </tr>

            <tr>
                <td>
                    <asp:label id="lblCodeType" runat="server" text="Code Type :" font-size="Large"
                        forecolor="Red"></asp:label>
                </td>
                <td>
                    <asp:dropdownlist id="ddlCodeType" runat="server" height="30px" width="316px" bordercolor="#1293D1" borderstyle="Ridge"
                        font-size="Large" AutoPostBack="True" onselectedindexchanged="ddlCodeType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">-Allowance-</asp:ListItem>
                        <asp:ListItem Value="2">-Deduction-</asp:ListItem>
                    </asp:dropdownlist>
                </td>


            </tr>


            <tr>
                <td>
                    <asp:label id="lblcode" runat="server" font-size="Large"
                        forecolor="Red"></asp:label>
                </td>
                <td>
                    <asp:dropdownlist id="ddlCode" runat="server" height="30px" width="316px" bordercolor="#1293D1" borderstyle="Ridge"
                        font-size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:dropdownlist>
                </td>

                <td>&nbsp;
                    <asp:button id="btnSumbit" runat="server" text="Search" font-size="Medium" forecolor="#FFFFCC"
                        height="24px" width="86px" font-bold="True" tooltip="Search" causesvalidation="False"
                        cssclass="button green" onclick="btnSumbit_Click" />
                </td>
            </tr>


        </table>

    </div>
    <div id="DivButton" runat="server" align="center">
        <table>
            <tr>
                <td align="center">

                    <asp:button id="BtnUpd" runat="server" text="Update" onclick="BtnUpd_Click" font-size="Large"
                        onclientclick="return UpdateValidation()" height="27px" width="86px" font-bold="True" cssclass="button green" />
                    &nbsp;&nbsp;
                    <asp:button id="BtnExit" runat="server" text="Exit" font-size="Large" forecolor="#FFFFCC"
                        height="27px" width="86px" font-bold="True" tooltip="Exit Page" causesvalidation="False"
                        cssclass="button red" onclick="BtnExit_Click" />
                     &nbsp;&nbsp;
                    <asp:label id="Label2" runat="server" text="Total Amount :" font-size="Large"
                        forecolor="Red"></asp:label>
                    &nbsp;&nbsp;
                    <asp:label id="lblTotalAmt" runat="server" Font-Bold="true" font-size="Large"
                        forecolor="Black"></asp:label>

                </td>
            </tr>
        </table>
    </div>

     <div align="Center" class="grid_scroll">
        <asp:gridview id="gvCodeDetailInfo" runat="server" headerstyle-cssclass="FixedHeader" headerstyle-backcolor="YellowGreen"
            autogeneratecolumns="false" alternatingrowstyle-backcolor="WhiteSmoke" rowstyle-height="10px" onrowdatabound="gvCodeDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                                <asp:TemplateField HeaderText="Emp. No." HeaderStyle-Width="90px"  ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                             <asp:TemplateField HeaderText="Emp. Name" HeaderStyle-Width="250px"  ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
             
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="170px" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount" runat="server" Font-Bold="true" Text-align="right" onFocus="javascript:this.select();" AutoPostBack="True"  OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Effect T" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkT1Select" runat="server" Font-Bold="true" onFocus="javascript:this.select();" AutoPostBack="True"  OnCheckedChanged="chkT1Select_CheckedChanged"/>
                                        
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Effect R" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkR1Select" runat="server" Font-Bold="true" onFocus="javascript:this.select();" AutoPostBack="True"  OnCheckedChanged="chkR1Select_CheckedChanged"/>
                                        
                                    </ItemTemplate>
                                    </asp:TemplateField>


                 </Columns>
            
        </asp:gridview>

    </div>
    <div align="center">
        <asp:label id="lblmsg1" runat="server" text="All Record Approve Successfully Completed" font-bold="True" font-size="XX-Large" forecolor="#009933"></asp:label>
        <br />
        <asp:label id="lblmsg2" runat="server" text="No More Record for Approve" font-bold="True" font-size="XX-Large" forecolor="#009933"></asp:label>
    </div>

    <asp:label id="lblAccTypeDesc" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblAtyClass" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblAccFlag" runat="server" text="" visible="false"></asp:label>

    <asp:label id="lblAType" runat="server" text="" visible="false"></asp:label>
    <asp:label id="Label1" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblIntRate" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblExpDate" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblInstlAmt" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblLastInstlAmt" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblNoInstl" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblFirstInstlDt" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblGrace" runat="server" text="" visible="false"></asp:label>
    <asp:label id="LienAmount" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblApplicationNo" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblModule" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblWithFlag" runat="server" text="" visible="false"></asp:label>

    <asp:label id="hdnCashCode" runat="server" text="" visible="false"></asp:label>
    <asp:label id="hdnPeriod" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblID" runat="server" text="" visible="false"></asp:label>

    <asp:Label ID="EffectFlag1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="EffectFlag2" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

