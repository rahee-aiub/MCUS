<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLChartOfAccountReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLChartOfAccountReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            var ddlHeader = document.getElementById('<%=ddlHeader.ClientID%>');
            if (ddlHeader.selectedIndex == 0 || ddlHeader.SelectedValue == "0")
                alert('Please Select Header List');

            else
                return confirm('Are you sure you want to Add information?');
            return false;
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div id="main" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Chart Of Account Report
                    </th>

                </tr>

            </thead>


            <tr>


                <td>
                    <asp:CheckBox ID="ChkAllHeader" runat="server" Text="  All" BackColor="#CCFFFF" ForeColor="#CC0000" Style="font-weight: 700" AutoPostBack="True" OnCheckedChanged="ChkAllHeader_CheckedChanged" />
                </td>

                <td style="background-color: #fce7f9">
                    <asp:Label ID="Label5" runat="server" Text="Header"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:DropDownList ID="ddlHeader" runat="server" Width="193px" AutoPostBack="True" OnSelectedIndexChanged="ddlHeader_SelectedIndexChanged"></asp:DropDownList>

                    &nbsp;&nbsp;<asp:Label ID="lblHeaderDesc" runat="server" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>



            </tr>

            <tr>
                <td>
                    <asp:CheckBox ID="ChkAllMainHead" runat="server" Text="  All" BackColor="#CCFFFF" ForeColor="#CC0000" Style="font-weight: 700" AutoPostBack="True" OnCheckedChanged="ChkAllMainHead_CheckedChanged" />
                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblMainHead" runat="server" Text="Main Head"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<asp:DropDownList ID="ddlMainHead" runat="server" Width="193px" AutoPostBack="True" OnSelectedIndexChanged="ddlMainHead_SelectedIndexChanged"></asp:DropDownList>

                    &nbsp;&nbsp;<asp:Label ID="lblMainHeaddesc" runat="server" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMainHead" runat="server" Visible="False"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>



            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="ChkAllSubHead" runat="server" Text="  All" BackColor="#CCFFFF" ForeColor="#CC0000" Style="font-weight: 700" AutoPostBack="True" OnCheckedChanged="ChkAllSubHead_CheckedChanged" />
                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblSubHead" runat="server" Text="Sub Head"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:DropDownList ID="ddlSubHead" runat="server" Height="21px" Width="253px" Style="margin-left: 0px" AutoPostBack="True" OnSelectedIndexChanged="ddlSubHead_SelectedIndexChanged1">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                    <asp:Label ID="lblsubHeadDesc" runat="server" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="txthidesubhead" runat="server" Visible="False"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                </td>




            </tr>

            <tr>
                <td></td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblBankAccType" runat="server" Text="Bank Account Type"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:DropDownList ID="ddlBankAccType" runat="server" Height="21px" Width="253px" Style="margin-left: 0px" AutoPostBack="True" OnSelectedIndexChanged="ddlSubHead_SelectedIndexChanged1">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">All Bank Account</asp:ListItem>
                    <asp:ListItem Value="2">Overdraft Account</asp:ListItem>
                </asp:DropDownList>
                    
                </td>




            </tr>


            <tr>


                <td></td>
                <td>



                    <asp:Button ID="BtnView" runat="server" Text="Preview / Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnView_Click" />&nbsp;
                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                         Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red"
                         OnClick="BtnExit_Click" />

                </td>
            </tr>


        </table>


    </div>
</asp:Content>
