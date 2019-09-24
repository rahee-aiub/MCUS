<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZERP.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZERP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A2ZWEBMCUS</title>
    <link href="../Styles/A2ZERPStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/structure.css" rel="stylesheet" />
    <link href="../Styles/styleButton.css" rel="stylesheet" />
    <script src="../scripts/validation.js" type="text/javascript"></script>

     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../scripts/amount.js" type="text/javascript"></script>

    <script src="../scripts/validation.js" type="text/javascript"></script>
    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">

        function capLock(e) {
            kc = e.keyCode ? e.keyCode : e.which;
            sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                document.getElementById('divMayus').style.visibility = 'visible';
            else
                document.getElementById('divMayus').style.visibility = 'hidden';
        }
    </script>

    <style type="text/css">
        .box2 {
            background: transparent url(ashera.jpg);
            no-repeat: ttopleft;
            width: 450px;
            height: 140px;
            margin: 0auto35pxauto;
            padding: 30px;
            color: #fff;
            font-weight: bold;
            font-size: learge;
            border: 11pxsolid#ffffff;
            -moz-border-radius: 32px;
            -webkit-border-radius: 32px;
            border-radius: 32px;
            behavior: url(border-radius.htc);
        }

        .rel {
            margin: 50px 0 0 33px;
            padding: 25px;
            position: relative;
            z-index: inherit;
            zoom: 1; /* For IE6 */
        }

        .auto-style1 {
            height: 40px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ValidationEmployee() {


            var txtPassword = document.getElementById('<%=txtPassword.ClientID%>').value;

            if (txtPassword == '' || txtPassword.length == 0)
                alert('Please Input Password.');

            else
                return;
            return false;
        }
    </script>

      <script language="javascript" type="text/javascript">
          $(function () {
              $("#<%= txtChgProcDt.ClientID %>").datepicker();

<%--            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtChgProcDt.ClientID %>").datepicker();

            });--%>

        });
          
         </script>


</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div id="divMayus" style="visibility: hidden">
            <asp:Label ID="Label1" runat="server" Text="Caps Lock is On.!"></asp:Label>
        </div>

        <div id="DivMain" runat="server">
            <%--<h1 style="color: #0000FF; font-style: italic; font-weight: bold;">CREDIT UNION SYSTEM for
            </h1>
            <h2 style="color: #0000FF; font-weight: bold;" align="center">
                <asp:Label ID="lblCompany" runat="server" Text="Label" Font-Size="XX-Large" ForeColor="#FF66FF"></asp:Label>


            </h2>--%>

            <%--<br />--%>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnCustomerService" runat="server" Text="Customer Service" CssClass="btnLogin"
                            Font-Size="Large" Font-Bold="True" Font-Italic="False" BorderColor="#FF6600"
                            Font-Names="Arial" OnClick="btnCustomerService_Click" Width="200px" ForeColor="White" Height="69px" />

                    </td>
                    <td>
                        <asp:Button ID="btnGl" runat="server" Text="General Ledger" BorderColor="#FF6600" CssClass="btnLogin"
                            OnClick="btnGl_Click" Font-Size="Large" Font-Bold="True" Font-Names="Arial" Width="200px" ForeColor="White" Height="69px" />
                    </td>


                    <td>
                        <asp:Button ID="btnHr" runat="server" Text="Human Resource" Font-Bold="True" BorderColor="#FF6600" CssClass="btnLogin"
                            OnClick="btnHr_Click" Font-Size="Large" Font-Names="Arial" Width="200px" ForeColor="White" Height="69px" />

                    </td>
                    <td>
                        <asp:Button ID="btnInv" runat="server" Text="Inventory" Font-Bold="True" BorderColor="#FF6600" CssClass="btnLogin"
                            OnClick="btnInv_Click" Font-Size="Large" Font-Names="Arial" Width="154px" ForeColor="White" Height="69px" />
                    </td>

                    <td>
                        <asp:Button ID="btnBooth" runat="server" Text="Booth" Font-Bold="True" BorderColor="#FF6600" CssClass="btnLogin"
                            Font-Size="Large" Font-Names="Arial" OnClick="btnBooth_Click" ForeColor="White" Height="69px" />
                    </td>
                    <td>
                        <asp:Button ID="btnOffBooth" runat="server" Text="Off Booth" Font-Bold="True" BorderColor="#FF6600" CssClass="btnLogin"
                            Font-Size="Large" Font-Names="Arial" ForeColor="White" Height="69px" OnClick="btnOffBooth_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnHk" runat="server" Text="House Keeping " Font-Bold="True" BorderColor="#FF6600" CssClass="btnLogin"
                            OnClick="btnHk_Click" Font-Size="Large" Font-Names="Arial" Width="200px" ForeColor="White" Height="69px" />


                    </td>
                    <td>
                        <asp:Button ID="btnHome" runat="server" Text="Sign Out" Font-Names="Arial" BorderColor="#FF6600" CssClass="btnLogin"
                            Font-Bold="True" Font-Size="Large" Height="69px" OnClick="btnHome_Click" Width="150px" ForeColor="White" />

                    </td>
                </tr>
            </table>
            <br />
            <br />

            <div>
            <asp:LinkButton ID="linkSMSReload" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red" Height="70px" OnClick="linkSMSReload_Click" >LinkButton</asp:LinkButton>
        </div>

            <div id="DivSMS" runat="server" align="center">
                <br />
                <asp:Label ID="lblNo" runat="server" Text="" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red" Height="70px"></asp:Label>
                &nbsp;&nbsp;
            <asp:LinkButton ID="linkSMSMessage" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red" Height="70px" OnClick="linkSMSMessage_Click">LinkButton</asp:LinkButton>
                <%-- <asp:Label ID="lblSMSMassage" runat="server" Text="" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red" Height="70px"></asp:Label>
                --%>
            </div>

            <div id="DivGridViewSMS" runat="server" align="center" style="height: 120px; overflow: auto; width: 100%;">
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
                                                <asp:Button ID="BtnSelect" runat="server" Text="Select" Width="68px" CssClass="button green" OnClick="BtnSelect_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="SMSDate" HeaderText="SMS Date" DataFormatString="{0:dd/MM/yyyy}" />

                                        <asp:TemplateField HeaderText="SMS No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSMSNo" runat="server" Text='<%# Eval("SMSNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sender Ids No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSMSFromIds" runat="server" Text='<%# Eval("SMSFromIdsNo") %>'></asp:Label>
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


            <div id="DivTextMsg" runat="server" align="center" style="height: 100px; overflow: auto; width: 100%;">
                <%-- <div id="DivTextMsg" align="center">--%>
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
                            <asp:TextBox ID="txtsmsMsg" runat="server" Width="490px" Height="50px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large" TextMode="MultiLine"></asp:TextBox>
                            <asp:Button ID="BtnBack" runat="server" Text="Back" Width="68px" CssClass="button green" OnClick="BtnBack_Click" />
                        </td>
                    </tr>

                </table>
            </div>



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


            <div id="hide" runat="server" visible="false">
                <div id="DivLogin" runat="server" visible="false">
                    <table class="style2">
                        <thead>
                            <tr>
                                <th colspan="3" style="color: white">User Login
                                </th>
                            </tr>
                            <tr>
                                <th style="color: white">User ID
                                </th>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIdNo1" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" placeholder="Input User Id"
                                        Width="209px" MaxLength="4" OnKeyPress="return IsNumberKey(event)" AutoPostBack="True"
                                        OnTextChanged="txtIdNo_TextChanged"></asp:TextBox>
                                    <asp:Label ID="lblWelcome" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="color: white">Password
                                </th>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                        TextMode="Password" onkeypress="capLock(event)" Width="207px" MaxLength="8" placeholder="Input Password"></asp:TextBox>
                                    <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </thead>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:Button ID="btnLogin" runat="server" Text="Log In" Height="27" CssClass="button green size-100"
                                    OnClick="btnLogin_Click" OnClientClick="return ValidationEmployee(event,this)" />
                                <asp:Button ID="btnExit" runat="server" Text="Exit" Height="27" CssClass="button red size-100"
                                    OnClick="btnExit_Click" />
                                <asp:Button ID="btChangePassword" runat="server" Text="Change Password" Height="27"
                                    CssClass="button blue size-140" OnClick="btChangePassword_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="DivChangePassword" runat="server" visible="false">
                    <table class="style2">
                        <thead>
                            <tr>
                                <th colspan="3" style="color: white">Change Password
                                </th>
                            </tr>
                            <tr>
                                <th style="color: white">Old Password
                                </th>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOldPassword" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                        TextMode="Password" Width="209px" MaxLength="8"></asp:TextBox>
                                    <asp:Label ID="lblold" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th style="color: white">New Password
                                </th>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewPassword" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                        TextMode="Password" Width="209px" MaxLength="8"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th style="color: white">Confirm Password
                                </th>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                        TextMode="Password" Width="209px" MaxLength="8"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnChangePassword" runat="server" Text="Submit" Height="27" CssClass="button green size-100"
                                        OnClick="btnChangePassword_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Exit" Height="27" CssClass="button red size-100"
                                        OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </thead>
                    </table>
                </div>
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

                <div id="table">
                    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtTranDate" runat="server"> </asp:TextBox>
                    <asp:TextBox ID="txtIdsLevel" runat="server"> </asp:TextBox>
                    <asp:TextBox ID="txtIdsLockFlag" runat="server"> </asp:TextBox>
                    <asp:TextBox ID="txtIdsLogInFlag" runat="server"> </asp:TextBox>
                    <asp:TextBox ID="txtIdsType" runat="server"> </asp:TextBox>
                    <asp:TextBox ID="txtIdsStatus" runat="server"> </asp:TextBox>
                    <asp:TextBox ID="txtIdsEmpCode" runat="server"> </asp:TextBox>
                    <asp:TextBox ID="txtIdsFlag" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtOrgPass" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtUserEmpCode" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtUserIP" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtServerIP" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtServerName" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtGLCashCode" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtIdNo" runat="server"></asp:TextBox>
                    <asp:TextBox ID="lblMsgFlag" runat="server"></asp:TextBox>
                    <asp:TextBox ID="lblAccessStatus" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hdnModule" runat="server" />
                </div>

            </div>

        </div>

        <div id="DivDetails" runat="server">

           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <table class="style1" style="width: 420px; height: 50px; background-color: #00FFFF;">
                        <thead>
                            <tr>
                                <th colspan="4" style="background-color: #669999">
                                    <asp:Label ID="lblAcDetails" runat="server" Text="START OF DAY" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </th>
                            </tr>

                            <tr>
                                <td style="background-color: #669999">
                                    <asp:Label ID="lblLastProcDate" runat="server" Text="Last Transaction Date" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </td>

                                <td class="auto-style1">
                                    <asp:TextBox ID="txtLastProcDt" runat="server" Enabled="False" BorderColor="#1293D1"
                                        Width="375px" BorderStyle="Ridge" Font-Size="X-Large" Font-Bold="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #669999">
                                    <asp:Label ID="lblNewProcDt" runat="server" Text="Today's Transaction Date is" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtNewProcDt" runat="server" Enabled="False" BorderColor="#1293D1"
                                        Width="375px" BorderStyle="Ridge" Font-Size="X-Large" Font-Bold="true"></asp:TextBox>

                                    &nbsp;

                                    <asp:Button ID="BtnChg" runat="server" Text="Change" CssClass="myButtonBlue"
                                        Width="110px" Height="27" OnClick="BtnChg_Click" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblChgProcDt" runat="server" Text="Change Today's Transaction Date is" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtChgProcDt" runat="server" BorderColor="#1293D1"
                                        Width="150px" BorderStyle="Ridge" Font-Size="X-Large" Font-Bold="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%-- <td>Please Type &quot;START OF DAY&quot
                        </td>--%>
                                <td style="background-color: #669999">
                                    <asp:Label ID="lblSOD" runat="server" Text="Please Type START OF DAY" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStDay" runat="server" MaxLength="12" autocomplete="off" Style="font-size: Large" ForeColor="Red" Width="135px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="textbox"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #669999">
                                    <asp:Label ID="lblProessing" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Start of Day" CssClass="myButtonGreen"
                                        Height="27" OnClientClick="return ValidationBeforeUpdate()" OnClick="Button1_Click" />

                                    <asp:Button ID="Button2" runat="server" Text="Back" CssClass="myButtonRed"
                                        Height="27" OnClick="Button2_Click" />
                                </td>

                            </tr>
                        </thead>
                    </table>

          <%--      </ContentTemplate>
            </asp:UpdatePanel>--%>

            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0;  filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;"></div>

               <div style="position: fixed; top: 79%;"> <asp:Image ID="Image1" ImageUrl="~/images/22.gif" runat="server"/></div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>

            
                    <asp:HiddenField ID="hdnCashCode" runat="server" />
                    <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblsmsNo" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblSwFlag" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblNoRec" runat="server" Text="" Visible="false"></asp:Label>


        </div>

         <div id="DivFromCashCode" runat="server">

            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <table class="style1" style="width: 420px; height: 50px; background-color: #00FFFF;">
                <thead>
                    <tr>
                        <th colspan="4" style="background-color: #669999">
                            <asp:Label ID="lblModuleFunc" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFCurrency" runat="server" Text="Select Cash Code :" Font-Size="Large"
                                ForeColor="Red"></asp:Label>
                        </td>
                        <td>

                            <asp:DropDownList ID="ddlFCashCode" runat="server" CssClass="cls text" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Width="400px" Font-Size="Large">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </td>



                    </tr>

                    <tr></tr>
                    <tr></tr>
                    <tr></tr>
                    <tr></tr>
                    <tr>
                        <td style="background-color: #669999">
                            
                        </td>
                        <td>
                            <asp:Button ID="BtnProceed" runat="server" Text="Proceed" CssClass="myButtonGreen"
                                Height="27" OnClick="BtnProceed_Click" />

                            <asp:Button ID="Button3" runat="server" Text="Back" CssClass="myButtonRed"
                                Height="27" OnClick="BtnBack1_Click" />
                        </td>

                    </tr>
                </thead>
            </table>



        </div>
    </form>
</body>
</html>
