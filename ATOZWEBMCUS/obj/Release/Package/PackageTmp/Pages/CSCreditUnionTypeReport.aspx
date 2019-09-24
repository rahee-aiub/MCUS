<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSCreditUnionTypeReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSCreditUnionTypeReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <script language="javascript" type="text/javascript">
          function ValidationBeforeSave() {
              return confirm('Are you sure you want to Proceed???');
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
                    <th colspan="3">Credit Union Report
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Credit Union Type :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
               </td>
                <td>
                    <asp:DropDownList ID="ddlCuType" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Affiliate </asp:ListItem>
                        <asp:ListItem Value="2">Associate </asp:ListItem>
                        <asp:ListItem Value="3">Regular</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

             <tr>
                <td style="background-color: #fce7f9" class="auto-style8">
                   <asp:CheckBox ID="chkAllCashCode" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" OnCheckedChanged="chkAllCashCode_CheckedChanged" />
                <%--</td>

                <td style="background-color: #fce7f9">--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblCashCode" runat="server" ForeColor="Red" Text="Cash Code "></asp:Label>
                </td>
               
                <td class="auto-style11">
                    <asp:DropDownList ID="ddlCashCode" runat="server" Height="31px" Width="418px"
                        Font-Size="Large" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
           
                </td>

            </tr>



             <tr>
                <td style="background-color: #fce7f9" class="auto-style8">
                   <asp:CheckBox ID="ChkAllDivision" runat="server" ForeColor="Red" Text="All" OnCheckedChanged="ChkAllDivision_CheckedChanged" AutoPostBack="True" />
                <%--</td>

                <td style="background-color: #fce7f9">--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblDivision" runat="server" ForeColor="Red" Text="Division "></asp:Label>
                </td>
               
                <td class="auto-style11">
                    <asp:DropDownList ID="ddlDivision" runat="server" Height="31px" Width="418px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
           
                </td>

            </tr>
            

             <%--District Information--%>

             <tr>


                <td style="background-color: #fce7f9" class="auto-style8">
                 <asp:CheckBox ID="ChkAllDistrict" runat="server" ForeColor="Red" Text="All" OnCheckedChanged="ChkAllDistrict_CheckedChanged" AutoPostBack="True" />

                <%--</td>

                <td style="background-color: #fce7f9">--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblDistrict" runat="server" ForeColor="Red" Text="District "></asp:Label>
                </td>
               
                <td class="auto-style11">
                    
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="31px" Width="418px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
           
                </td>

            </tr>


 <%--Upazila Information--%>

             <tr>


                <td style="background-color: #fce7f9" class="auto-style8">
                   <asp:CheckBox ID="ChkAllUpazila" runat="server" ForeColor="Red" Text="All" OnCheckedChanged="ChkAllUpazila_CheckedChanged" AutoPostBack="True" />

                <%--</td>

                <td style="background-color: #fce7f9">--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblUpazila" runat="server" ForeColor="Red" Text="Upazila "></asp:Label>
                </td>
               
                <td class="auto-style11">
                    
                    <asp:DropDownList ID="ddlUpazila" runat="server" Height="31px" Width="418px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlUpazila_SelectedIndexChanged" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
           
                </td>

            </tr>

            
 <%--Thana Information--%>

             <tr>


                <td style="background-color: #fce7f9" class="auto-style8">
                   <asp:CheckBox ID="ChkAllThana" runat="server" ForeColor="Red" Text="All" OnCheckedChanged="ChkAllThana_CheckedChanged" AutoPostBack="True" />

                <%--</td>

                <td style="background-color: #fce7f9">--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblThana" runat="server" ForeColor="Red" Text="Thana "></asp:Label>
                </td>
               
                <td class="auto-style11">
                    
                    <asp:DropDownList ID="ddlThana" runat="server" Height="31px" Width="418px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlThana_SelectedIndexChanged" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
           
                </td>

            </tr>




            
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnProcess" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnProcess_Click" />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>






</asp:Content>
