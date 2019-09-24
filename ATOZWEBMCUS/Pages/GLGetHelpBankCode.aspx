<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLGetHelpBankCode.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLGetHelpBankCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 313px;
            width: 1000px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 490px;*/
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .auto-style1 {
            width: 225px;
        }

        .auto-style2 {
            height: 27px;
        }

        .auto-style3 {
            width: 225px;
            height: 27px;
        }
    </style>




    



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

      </br>
    </br>
    <div align ="center">
           <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="3">
                                    <asp:Label ID="lblTransFunction" runat="server" Text="Search Bank Code Information"></asp:Label>
                                </th>
                            </tr>

                        </thead>
              

              
                            <tr>
                            <td>
                                <asp:Label ID="lblVchAmount" runat="server" Text="Voucher Amount :" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVchAmount" runat="server" CssClass="cls text" Width="183px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                               
                              

                            </td>
                        </tr>
                        
                        </table>
         <table>
        <tr>
            <td></td>
            <td>&nbsp;&nbsp;
                <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" Height="27px" Width="86px" ToolTip="Search Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnSearch_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"/>
                <br />
            </td>
        </tr>
    </table>
                 
                    </div>
    </br>
    </br>
    <div align="left" class="grid_scroll">

          

    <asp:GridView ID="gvSearchBankInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
                        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSearchBankInfo_RowDataBound" OnSelectedIndexChanged="gvSearchBankInfo_SelectedIndexChanged" >
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>
                            

                            <asp:BoundField HeaderText="Bank Code(old)" DataField="TBFGLCODEOLD" HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="left" />
                    
                              <asp:BoundField HeaderText="Bank Code(New)" DataField="TBFGLCODENEW" HeaderStyle-Width="140px" ItemStyle-Width="140px"  HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                              <asp:BoundField HeaderText="Code Description" DataField="TBFCACCOUNTDESC" HeaderStyle-Width="400px" ItemStyle-Width="400px" HeaderStyle-HorizontalAlign ="left" ItemStyle-HorizontalAlign="left" />
                            
                            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Select" ID="LinkButton1" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

         

         
        </div>

    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNewMemberNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTranDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCtrlTrnFlag" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblNflag" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblIncraseLoan" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCTypeName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblBankCode" runat="server" Text="" Visible="false"></asp:Label>



</asp:Content>

