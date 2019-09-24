<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLYearEnd.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLYearEnd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Process Year End?');
        }

    </script>
     <script type="text/javascript">
         function MyFunction() {
             var FNumber = Math.abs(document.getElementById('txtFirstNumber').value);
             var SNumber = Number(document.getElementById("txtSecondNumber").value);
             var Sum = FNumber + SNumber;
             alert(Sum);
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
                    <th colspan="3" style="color: Black">
                        Year End Process

                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblEndYear" runat="server" Text="Year End"></asp:Label>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtBegYear" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="150px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lblDash" runat="server" Text=" - " Height="38px" Width="20px" Font-Size="X-Large"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtEndYear" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="150px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblYearEnd" runat="server" Text="Process Year End" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnProcess" runat="server" Text="Start Year End" CssClass="button green size-180"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="btnProcess_Click"  />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-80" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
  <%--  <div id="DivGridView" runat="server" align="center" visible="True">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        Following User Id is Using
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="gvUserInfo" runat="server" BorderColor="#1293D1" BorderStyle="Ridge">
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <br />
    <div id="DivMessage" runat="server" align="center" visible="false">
        <table class="style1">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnHideMessageDiv" runat="server" Text="OK" CssClass="button blue size-100"
                        OnClick="btnHideMessageDiv_Click" />
                </td>
            </tr>
        </table>
    </div>--%>

     <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="CtrlTranStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdndbname" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdndatapath" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblYearEndFlag" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>
