<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSChqCounter.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSChequeCounter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <script language="javascript" type="text/javascript">
          function ValidationBeforeSave() {
              return confirm('Reserve New Counter Cheque Book ?');
          }

       </script>

         <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         $(document).ready(function () {
             $("#<%=txtIssueDate.ClientID %>").dynDateTime({
            showsTime: false,
            ifFormat: "%d/%m/%Y",
            daFormat: "%l;%M %p, %e %m, %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
           <thead>
                <tr>
                    <th colspan="3">
                  Counter Cheque Book Issue
                    </th>
                </tr>
              
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblChqprefix" runat="server" Text="Cheque Prefix:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChqprefix" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblNumPage" runat="server" Text="Number Of Pages :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumPage" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtNumPage_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblBeginingNo" runat="server" Text="Beginning No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBeginingNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtBeginingNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblSlNo" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblbPage" runat="server" Text="" Visible="false"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEndNo" runat="server" Text="Ending No :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIssueDate" runat="server" Text="Issue Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate" runat="server" CssClass="cls text" Width="115px" Height="25px" DateFormatString="dd/MM/yyyy"
                        Font-Size="Large" img src="../Images/calender.png" AutoPostBack="True" OnTextChanged="txtIssueDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />
                    &nbsp;
                   <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

