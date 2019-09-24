<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSVerifyAccountEditMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSVerifyAccountEditMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function VerifyValidation() {
            return confirm('Do you want to Verify data?');
        }
        function ReVerifyValidation() {
            return confirm('Do you want to ReVerify data?');
        }
        function ReEditValidation() {
            return confirm('Do you want to ReEdit data?');
        }
    </script>

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
                        Verify Account Edit Maintenance
                    </p>
                </th>
            </tr>
        </table>

    </div>
    <div id="DivButton" runat="server" align="center">
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DivGridViewCancle" runat="server" align="center" style="height: 245px; overflow: auto; width: 100%;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        <p align="center" style="color: blue">
                            Verify - Spooler
                        </p>
                        <asp:GridView ID="gvAccInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="757px">
                            <Columns>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnVerify" runat="server" Text="Verify" OnClick="BtnVerify_Click" OnClientClick="return VerifyValidation()" Width="68px" CssClass="button green" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="CU No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcuno" runat="server" Text='<%# Eval("CuNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Depositor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmemno" runat="server" Text='<%# Eval("MemNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="MemName" HeaderText="Depositor Name" />

                                 <asp:TemplateField HeaderText="Account No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblaccno" runat="server" Text='<%# Eval("AccNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Verify Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Verify" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>
</asp:Content>

