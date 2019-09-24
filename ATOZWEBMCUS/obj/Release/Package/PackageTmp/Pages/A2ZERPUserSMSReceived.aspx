<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZERPUserSMSReceived.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZERPUserSMSReceived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function ApproveValidation() {
            return confirm('Do you want to Approve Data?');
        }
        function SelectValidation() {
            return confirm('Do you want to Select Data?');
        }
        function RejectValidation() {
            return confirm('Do you want to Reject Data?');
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
                        Received SMS Messages
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

    <div id="DivGridViewSMS" runat="server" align="center" style="height: 276px; overflow: auto; width: 100%;">
            <table class="style1">
                <thead>
                    <tr>
                        <th>
                            <p align="center" style="color: blue">
                                SMS - Spooler
                            </p>
                            <asp:GridView ID="gvSMSInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="757px">
                                <Columns>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnSelect" runat="server" Text="Select"  Width="68px" CssClass="button green"  OnClick="BtnSelect_Click"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    
                                    <asp:BoundField DataField="SMSDate" HeaderText="SMS Date" DataFormatString="{0:dd/MM/yyyy}" />
    
                                    <asp:TemplateField HeaderText="SMS No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSMSNo" runat="server" Text='<%# Eval("SMSNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SMS from Ids No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSMSFromIds" runat="server" Text='<%# Eval("SMSFromIdsNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSMSFromIdsName" runat="server" Text='<%# Eval("SMSFromIdsName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Messages" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSMSNote" runat="server" Visible="false" Text='<%# Eval("SMSNote") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSMSStatus" runat="server" Visible="false" Text='<%# Eval("SMSStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>


                        </th>
                    </tr>
                </thead>
            </table>
        </div>


    <div id="DivTextMsg" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="6" style="color: Red" align="center">
                       <%-- <p align="center">--%>
                            Text Note Messages
                       <%-- </p>--%>
                    </th>
                </tr>
            </thead>
            <tr>
                
                <td>
                    <asp:TextBox ID="txtsmsMsg" runat="server"  Width="490px" Height="100px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>


    <div id="Div1" runat="server" align="center">
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnBack" runat="server" Text="Back" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnBack_Click" />
                </td>
            </tr>
        </table>
    </div>




    

    <asp:Label ID="lblAccTypeDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
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

    <asp:Label ID="lblNewAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="hdnApprovedAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNoInstl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAppNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="SingleRec" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUserName" runat="server" Text="" Visible="false"></asp:Label>
   
    <asp:Label ID="lblsmsNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlPram" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlFunc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
   

</asp:Content>

