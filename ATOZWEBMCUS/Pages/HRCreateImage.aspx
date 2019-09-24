<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRCreateImage.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRCreateImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div align="center">

        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Signature/Picture Card Authorization
                    </th>
                </tr>

            </thead>
            

            <%--<tr>
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
                    <asp:Label ID="lblhideCuNumber" runat="server" Text=" " ForeColor="#FF3300" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Width="160px" Height="25px"
                        Font-Size="Large" Visible="false"></asp:TextBox>

                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="lblMemNo" runat="server" Text="Staff No: " ForeColor="Red"></asp:Label>
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
                    <asp:Label ID="lblUpload" runat="server" Text="Upload a Photo" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" ForeColor="#FF3300" />
                    <asp:ImageButton ID="ibtnUpload" runat="server" ImageUrl="~/Images/uploadicon.jpg" Width="60px" Height="51px" OnClick="ibtnUpload_Click" />
                     <asp:ImageButton ID="BtnUpdate" runat="server" ImageUrl="~/Images/update.jpg" Width="60px" Height="51px" OnClick="BtnUpdate_Click" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlImage" runat="server" Height="260px" Width="350px">
                        <asp:Image ID="ImgPicture" runat="server" Height="256px" ImageUrl="~/Images/index.jpg" Width="387px" />
                        <asp:ImageButton ID="ibtnDelete" runat="server" Height="26px" ImageUrl="~/Images/delete_user.png" Style="position: relative; top: 2px; left: 8px;" OnClick="ibtnDelete_Click" Width="41px" />

                    </asp:Panel>
                </td>

            </tr>
        </table>
    </div>
    <div align="center">
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
            Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
    </div>



</asp:Content>
