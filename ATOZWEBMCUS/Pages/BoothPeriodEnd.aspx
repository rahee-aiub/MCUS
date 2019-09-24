<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="BoothPeriodEnd.aspx.cs" Inherits="ATOZWEBMCUS.Pages.BoothPeriodEnd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Process Done?');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3" style="color: Black">Booth Process Done Confirmation

                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblToDate" runat="server" Text="Today's Date" Font-Size="Large"></asp:Label>
                </td>
                <%-- <td>:
                </td>--%>
                <td>
                    <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="351px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblEndOfDay" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblEndOfMonth" runat="server" Text="" ForeColor="Green"></asp:Label>
                </td>
            </tr>

            <tr>

                <td>
                    <asp:Label ID="lblDebit" runat="server" Text="TOTAL DEBIT :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="lblTotalDebit" runat="server" Font-Size="Large"
                        Enabled="False" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr>

                <td>
                    <asp:Label ID="lblCredit" runat="server" Text="TOTAL CREDIT :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="lblTotalCredit" runat="server" Font-Size="Large"
                        Enabled="False" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>

    <div id="Div1" runat="server" align="center">
        <table class="style1">
          
            <tr>
                <td>
                    <asp:Label ID="lblProcessType" runat="server" Text="Process Type :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList
                        ID="ddlProcessType" runat="server" TabIndex="5" Height="25px" Width="200px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="False">
                        <asp:ListItem Value="1">Process Done</asp:ListItem>
                        <asp:ListItem Value="2">No Transaction</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblDayEnd" runat="server" Text="Please Input 'PROCESS DONE'" Font-Size="Large"></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtDayEnd" runat="server" autocomplete="off" Style="font-size: Large" ForeColor="Red" MaxLength="12" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="textbox"></asp:TextBox>

                </td>


            </tr>




            <%--<tr>
                <td colspan="3">
                    <asp:Label ID="lblYearEnd" runat="server" Text="Process Also Year End" ForeColor="Blue"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td></td>
               
                <td>
                    <asp:Button ID="btnProcess" runat="server" Text="Update" CssClass="button green size-180"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="btnProcess_Click" />
                    &nbsp;
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-80"
                        OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <br />
    <br />


    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTranStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblNewYear" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblYearEnd" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdndbname" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdndatapath" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

