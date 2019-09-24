<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAccountCertificateMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountCertificateMaint" %>

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
            width:1300px;
            
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <div id="DivMainHeader" runat="server" align="center">

        <table class="style1">
            <tr>
                <th colspan="4">
                    <p align="center" style="color: blue">
                        Account Certificate No. Maintenance
                    </p>
                </th>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Account Type :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAcType" runat="server" Height="30px" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSumbit" runat="server" Text="Search" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="24px" Width="86px" Font-Bold="True" ToolTip="Search" CausesValidation="False"
                        CssClass="button green" OnClick="btnSumbit_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="With Certificate No. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ChkWithCerti" runat="server"   Font-Size="Large" ForeColor="Red" Text="" />

                </td>
            </tr>

        </table>

    </div>
    <div id="DivButton" runat="server" align="center">
        <table>
            <tr>
                <td align="center">

                    <asp:Button ID="BtnUpd" runat="server" Text="Update" OnClick="BtnUpd_Click" Font-Size="Large"
                        OnClientClick="return UpdateValidation()" Height="27px" Width="86px" Font-Bold="True" CssClass="button green" />
                    &nbsp;&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                </td>
            </tr>
        </table>
    </div>


    <div align="left" class="grid_scroll">
        <asp:GridView ID="gvCertiDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px" OnRowDataBound="gvCertiDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:TemplateField HeaderText="CU No" HeaderStyle-Width="90px"  ItemStyle-Width="90px" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblcuno" runat="server" Text='<%# Eval("CuType").ToString() + "-" + Eval("CuNo").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CU Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcutype" runat="server" Text='<%# Eval("CuType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CU No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcno" runat="server" Text='<%# Eval("CuNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Depositor" HeaderStyle-Width="90px"  ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemNo" runat="server" Text='<%# Eval("MemNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A/C Type" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccType" runat="server" Text='<%# Eval("AccType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A/C No." HeaderStyle-Width="130px"  ItemStyle-Width="120px" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Old A/C" HeaderStyle-Width="120px"  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblOldAccNo" runat="server" Text='<%# Eval("AccOldNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/C Name" HeaderStyle-Width="340px"  ItemStyle-Width="350px" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccName" runat="server" Text='<%# Eval("MemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="AccOpenDate" HeaderText="Open Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="90px"  ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"/>

                                <asp:TemplateField HeaderText="Period" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="90px"  ItemStyle-Width="90px" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("AccPeriod") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FD Org.Amt." HeaderStyle-Width="135px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="140px" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrgAmt" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("AccOrgAmt"))) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Certi.No." HeaderStyle-Width="168px"  ItemStyle-Width="145px" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCertificateNo" runat="server" Text='<%# Bind("AccCertNo") %>' MaxLength="12" Font-Bold="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                 </Columns>
            
        </asp:GridView>




 
    </div>
    <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Approve Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Approve" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>

    <asp:Label ID="lblAccTypeDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccFlag" runat="server" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="lblAType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIntRate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblExpDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblInstlAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblLastInstlAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNoInstl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFirstInstlDt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGrace" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="LienAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblApplicationNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblWithFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

