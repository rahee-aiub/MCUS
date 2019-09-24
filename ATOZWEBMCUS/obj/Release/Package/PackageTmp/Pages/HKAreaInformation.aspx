<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="HKAreaInformation.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HKAreaInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style type="text/css">
        
        .auto-style8 {
            width: 60px;
        }

        .auto-style11 {
            width: 557px;
        }

        </style>

    <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <br />
    <br />
    <br />



    <div id="DivParameter" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="7">Area Information Reports
                    </th>
                </tr>

            </thead>

            <tr>
                <td colspan="7" style="background-color: #fce7f9" class="auto-style8">
                </td>
            </tr>
                                 
            
            <tr>
                <td style="background-color: #fce7f9" class="auto-style8">
                   <asp:CheckBox ID="ChkAllDivision" runat="server" ForeColor="Red" Text="All" OnCheckedChanged="ChkAllDivision_CheckedChanged" AutoPostBack="True" />
                </td>

                <td style="background-color: #fce7f9">
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

                </td>

                <td style="background-color: #fce7f9">
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

                </td>

                <td style="background-color: #fce7f9">
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

                </td>

                <td style="background-color: #fce7f9">
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
                   <td colspan="2" style="background-color: #fce7f9" class="auto-style8">
                   </td>

                   <td colspan="5" style="background-color: #fce7f9" class="auto-style8">
                      <asp:Button ID="BtnView" runat="server" Text="Preview / Print" Font-Size="Large" ForeColor="White"
                      Font-Bold="True" CssClass="button green" OnClick="BtnView_Click" Width="216px" />&nbsp;
                                          
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="66px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" PostBackUrl="~/pages/A2ZERPModule.aspx" OnClick="BtnExit_Click" />
                        
                    
                    </td>
              </tr>


        </table>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
