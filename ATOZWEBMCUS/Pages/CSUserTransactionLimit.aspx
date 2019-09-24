<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSUserTransactionLimit.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSUserTransactionLimit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>






</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <br />
    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">
                 Transaction Limit
                    </th>
                </tr>
              
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Ids No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIdsNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large" AutoPostBack="true" ToolTip="Enter Ids" OnTextChanged="txtIdsNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlIdsNo" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlIdsNo_SelectedIndexChanged"> 
                      
                   
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLIdsCashCredit" runat="server" Text="Limit Cash Credit Amt.   :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLIdsCashCredit" runat="server" CssClass="cls text" Width="178px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtLIdsCashCredit_TextChanged" MaxLength="14"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblLIdsCashDebit" runat="server" Text="Limit Cash Debit Amt.      :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLIdsCashDebit" runat="server" CssClass="cls text" Width="178px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtLIdsCashDebit_TextChanged" MaxLength="14"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblLIdsTrfCredit" runat="server" Text="Limit Transfer Credit Amt. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLIdsTrfCredit" runat="server" CssClass="cls text" Width="180px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtLIdsTrfCredit_TextChanged" MaxLength="14"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblLIdsTrfDebit" runat="server" Text="Limit Transfer Debit Amt.    :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLIdsTrfDebit" runat="server" CssClass="cls text" Width="179px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtLIdsTrfDebit_TextChanged" MaxLength="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeSave()" Height="27px" Width="86px"  onclick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeUpdate()" Height="27px" Width="86px"  onclick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnAll" runat="server" Text="All" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="All Information" CssClass="button green" Height="27px" Width="86px" 
                         onclick="BtnAll_Click" />&nbsp;
                  <%--    <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green" 
                         onclick="BtnView_Click" />&nbsp;--%>
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
   <%--     <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
 AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" Height="150px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                 <asp:BoundField HeaderText="Code" DataField="LPurposeCode" HeaderStyle-Width="230px" ItemStyle-Width="230px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Description" DataField="LPurposeDescription" HeaderStyle-Width="250px" ItemStyle-Width="250px" />    
              </Columns>
          
        </asp:GridView>
     </div>--%>

    </div>



</asp:Content>

