<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSNewMemberOpenScreen.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSNewMemberOpenScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Reverse information?');
        }

    </script>

    <script type="text/javascript">
        function closechildwindow() {
            window.opener.document.location.href = 'CSDailyBoothTransaction.aspx';
            window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <body>
     
        <br />
        <br />
   
        <div align="center">
            <table>
                <tr>
                    <td>
                        <table class="style1">

                            <thead>
                                <tr>
                                    <th colspan="3">New Depositor Informations
                                    </th>
                                </tr>

                            </thead>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNewMemNo" runat="server" Text="New Depositor No:" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewMemNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium"></asp:TextBox>


                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblNewMemName" runat="server" Text="New Depositor Name :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewMemName" runat="server" CssClass="cls text" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMemType" runat="server" Text="Depositor Type :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                         <asp:DropDownList
                                ID="ddlMemType" runat="server" Height="25px" Width="150px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">Premium</asp:ListItem>
                                <asp:ListItem Value="2">General</asp:ListItem>
                            </asp:DropDownList>
                    </td>
                </tr>

            </table>
        </div>
        <br />
        <br />



        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnOkay" runat="server" Text="Okay" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" CssClass="button green" OnClick="BtnOkay_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="hdnFunc" runat="server" />
        <asp:HiddenField ID="hdnModule" runat="server" />
        <asp:HiddenField ID="hdnVchNo" runat="server" />
        <asp:HiddenField ID="hdnCuNo" runat="server" />
        <asp:HiddenField ID="hdnCType" runat="server" />
        <asp:HiddenField ID="hdnCNo" runat="server" />
        <asp:HiddenField ID="hdnCuName" runat="server" />

        <asp:HiddenField ID="hdnTranDate" runat="server" />

        <asp:HiddenField ID="hdnNewMemberNo" runat="server" />
        <asp:HiddenField ID="hdnNewMemberName" runat="server" />
        <asp:HiddenField ID="hdnMemType" runat="server" />

        <asp:HiddenField ID="hdnGLCashCode" runat="server" />

        <asp:Label ID="CtrlTrnDate" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="ValidityFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    </div>

    </div>

    </div>

        </div>


        </div>



        </div>




        </div>





         </div>






</asp:Content>

