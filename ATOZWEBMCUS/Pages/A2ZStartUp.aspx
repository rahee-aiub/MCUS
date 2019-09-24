<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZStartUp.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZStartUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A2ZMCUS</title>
    <link href="../Styles/A2ZERPStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />
    <%-- <link href="../Styles/reset.css" rel="stylesheet" />--%>
    <link href="../Styles/styleButton.css" rel="stylesheet" />
    <link href="../Styles/structure.css" rel="stylesheet" />
    <script src="../scripts/validation.js" type="text/javascript"></script>


    <style type="text/css">
        .auto-style1 {
            width: 60px;
        }
    </style>


    <style type="text/css">
        .textbox {
            background: white;
            border: 1px solid #DDD;
            border-radius: 5px;
            box-shadow: 0 0 5px #DDD inset;
            color: #666;
            outline: none;
            height: 25px;
            width: 127px;
        }
    </style>



    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            var txtOldPassword = document.getElementById('<%=txtOldPassword.ClientID%>').value;
            var txtNewPassword = document.getElementById('<%=txtNewPassword.ClientID%>').value;
            var txtConfirmPassword = document.getElementById('<%=txtConfirmPassword.ClientID%>').value;

            if (txtOldPassword == 0 || txtOldPassword.length == 0) {
                document.getElementById('<%=txtOldPassword.ClientID%>').focus();
                alert('Please Input Old Password.');
            }
            else if (txtNewPassword == '' || txtNewPassword.length == 0) {
                document.getElementById('<%=txtNewPassword.ClientID%>').focus();
                alert('Please Input New Password.');
            }
            else if (txtConfirmPassword == 1 || txtConfirmPassword.length == 0) {
                document.getElementById('<%=txtConfirmPassword.ClientID%>').focus();
                    alert('Please Input Confirm Password.');
                }

                else
                    return confirm('Are you sure you want save the data');
        return false;
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Process Start of Day?');
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="OrgPass" runat="server" />
        <asp:HiddenField ID="hdnfldMac" runat="server" />
        <asp:HiddenField ID="hdnfldMacCompare" runat="server" />
        <asp:HiddenField ID="hdnIDFlag" runat="server" />
        <asp:HiddenField ID="hdnCashCode" runat="server" />


        <div id="DivMain" runat="server">

            <div class="box login">
                <fieldset class="boxBody">
                    <label style="font-weight: bolder">User Id</label>

                    <asp:TextBox ID="txtIdNo" runat="server" placeholder="Input User Id" OnTextChanged="txtUserId_TextChanged" AutoPostBack="True"></asp:TextBox>

                    <label style="font-weight: bolder">Password</label>

                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="8" placeholder="Input Password" TextMode="Password"></asp:TextBox>
                </fieldset>
                <footer>
                    <table>
                        <tr>
                            <td class="auto-style1"></td>
                            <td>

                                <asp:Button ID="btnLogin" CssClass="btnLogin" runat="server" Text="SignIn" OnClick="btnLogin_Click" />
                            </td>

                        </tr>
                    </table>

                    <div id="DivchangePass" runat="server">

                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btChangePassword" runat="server" Text="Change Password" CssClass="btnLogin"
                            OnClick="btChangePassword_Click" Width="153px" Height="33px" />

                    </div>

                    <div id="DivChangePassword" runat="server" visible="false">
                        <fieldset class="boxBody">
                            <label style="font-weight: bolder">
                                Old Password
                            </label>
                            &nbsp;<asp:TextBox ID="txtOldPassword" runat="server"
                                TextMode="Password" MaxLength="8" placeholder="Input old Password"></asp:TextBox>
                            <asp:Label ID="lblold" runat="server"></asp:Label>

                            <label style="font-weight: bolder">New Password</label>

                            <asp:TextBox ID="txtNewPassword" runat="server"
                                TextMode="Password" MaxLength="8" placeholder="Input new Password"></asp:TextBox>
                            <label style="font-weight: bolder">Confirm Password</label>

                            <asp:TextBox ID="txtConfirmPassword" runat="server"
                                TextMode="Password" MaxLength="8" placeholder="Input Confirm Password"></asp:TextBox>

                            <table>
                                <tr>

                                    <td>
                                        <asp:Button ID="btnChangePassword" runat="server" Text="Submit" CssClass="btnLogin" OnClientClick="return  ValidationBeforeSave()"
                                            OnClick="btnChangePassword_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" Text="Exit" CssClass="btnLogin"
                                            OnClick="btnCancel_Click" />
                                    </td>
                                </tr>


                            </table>

                        </fieldset>
                    </div>


                </footer>
            </div>
        </div>
        <div id="DivDetails" runat="server">
            <table class="style1" style="width: 420px; height: 50px">
                <thead>
                    <tr>
                        <th colspan="4">
                            <asp:Label ID="lblAcDetails" runat="server"></asp:Label>
                        </th>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblLastProcDate" runat="server" Text="Last Transaction Date" Font-Size="Large"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtLastProcDt" runat="server" Enabled="False" BorderColor="#1293D1"
                                Width="351px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNewProcDt" runat="server" Text="Today's Transaction Date is" Font-Size="Large"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtNewProcDt" runat="server" Enabled="False" BorderColor="#1293D1"
                                Width="351px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                       <%-- <td>Please Type &quot;START OF DAY&quot;
                        </td>--%>
                        <td>
                            <asp:Label ID="lblSOD" runat="server" Text="Please Type START OF DAY" Font-Size="Large"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtStDay" runat="server" Style="font-size: medium" ForeColor="Red" Width="120px" MaxLength="12" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="textbox"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Start of Day" CssClass="myButtonGreen"
                                Height="27"  OnClientClick="return ValidationBeforeUpdate()" OnClick="Button1_Click" />

                            <asp:Button ID="btnHideMessageDiv" runat="server" Text="Back" CssClass="myButtonRed"
                                Height="27"
                                OnClick="btnHideMessageDiv_Click" />
                        </td>

                    </tr>
                </thead>
            </table>
        </div>

    </form>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <div align="center">
        <table class="style3">
            <thead>
                <tr>
                    <th>Developed By AtoZ Computer Services - Version 1.5<br />
                        Last Update: June, 2019.
                    </th>
                </tr>
            </thead>
        </table>
    </div>

</body>
</html>
