<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLDailyReverseTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLDailyReverseTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Delete information?');
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />

    <div align="center">

        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">GL Daily Reverse Transaction
                    </th>
                </tr>

            </thead>

        </table>

        <table>
            <tr>
                <td></td>


            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVoucherNo" runat="server" Text="Voucher No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>

                    &nbsp;

                    <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound" Width="880px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField HeaderText="TrnDate" DataField="TrnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
           
                
                <asp:TemplateField HeaderText="Debit Code" HeaderStyle-Width="125px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNoDr" runat="server" Text='<%#Eval("TrnGLAccNoDr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Credit Code" HeaderStyle-Width="125px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNoCr" runat="server" Text='<%#Eval("TrnGLAccNoCr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Credit Amt" HeaderStyle-Width="150px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:TextBox ID="lblCreditAmt" runat="server"  Enabled="false" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("GLCreditAmt"))) %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Debit Amt" HeaderStyle-Width="150px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:TextBox ID="lblDebitAmt" runat="server"  Enabled="false" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("GLDebitAmt"))) %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DrCr" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnDrCr" runat="server" Text='<%#Eval("TrnDrCr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="TrnType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnType" runat="server" Text='<%#Eval("TrnType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="sign" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnSign" runat="server" Text='<%#Eval("TrnSign") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:BoundField HeaderText="Debit Code" DataField="TrnGLAccNoDr" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Credit Code" DataField="TrnGLAccNoCr" HeaderStyle-Width="80px" ItemStyle-Width="92px" />
                <asp:BoundField HeaderText="Credit Amt" DataField="GLCreditAmt" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />
                <asp:BoundField HeaderText="Debit Amt" DataField="GLDebitAmt" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />
--%>
                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <asp:Button ID="BtnDel" runat="server"  Text="Delete" OnClick="BtnDel_Click" CssClass="button green" />
                        <asp:Button ID="BtnChg" runat="server" Text="Change" OnClick="BtnChg_Click" CssClass="button blue" />
                        <asp:Button ID="BtnUpd" runat="server" Visible="false" Text="Update" OnClick="BtnUpd_Click" CssClass="button blue" />
                        <asp:Button ID="BtnCan" runat="server" Visible="false" Text="Cancel" OnClick="BtnCan_Click" CssClass="button green" />
                       
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>
        <%--</div>--%>
        <br />
        <br />
        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnDelete" runat="server" Text="All Delete" Font-Bold="True" Font-Size="Medium" Height="27px" Width="120px"
                    ForeColor="White" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnDelete_Click" />&nbsp;
                     
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Font-Bold="True" Font-Size="Medium" Height="27px" Width="120px"
                    ForeColor="White" CssClass="button blue" OnClick="BtnCancel_Click" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="CtrlTrnDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="HdnFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnFOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnPrmValue" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTrnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblChgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCanFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblRecID" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

