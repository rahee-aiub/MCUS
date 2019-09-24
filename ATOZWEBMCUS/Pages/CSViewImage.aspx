<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSViewImage.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSViewImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Signature/Picture Card Verification
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <table>
                        

                        <tr>
                            <td>
                                <asp:Label ID="lblCuNo" runat="server" Text="Credit Union No: " ForeColor="#FF3300"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCuNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                                    Font-Size="Large" AutoPostBack="True" OnTextChanged="txtCuNo_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlCreditUNo" runat="server" Height="25px" Width="504px"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlCreditUNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                 <asp:Label ID="lblhideCuNo" runat="server" Text=" " ForeColor="#FF3300" Visible="false"></asp:Label>
                       <asp:Label ID="lblhideCutype" runat="server" Text=" " ForeColor="#FF3300" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Width="160px" Height="25px"
                        Font-Size="Large" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMemNo" runat="server" Text="Depositor No: " ForeColor="#FF3300"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                                    Font-Size="Large" AutoPostBack="True" OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlMemNo" runat="server" Height="25px" Width="504px"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlMemNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ibtnView" runat="server" ImageUrl="~/Images/view-button.png" Width="112px" Height="35px" OnClick="ibtnView_Click" />
                            </td>
                        </tr>

                    </table>
                </td>
                &nbsp;
    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <table>
             <tr>
                 <td>
                     <asp:Panel ID="pnlImage" runat="server" Height="260px" Width="350px">
                         <asp:Image ID="ImgPicture" runat="server" Height="257px" ImageUrl="~/Images/index.jpg" Width="349px" />

                     </asp:Panel>
                 </td>
             </tr>
         </table>
    </td>
            </tr>
        </table>
    </div>
    <div align="center">
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
            Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
    </div>
</asp:Content>

