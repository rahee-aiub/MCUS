<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLTrialBalanceHeaderReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLTrialBalanceHeaderReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <%-- <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtfdate.ClientID %>").dynDateTime({
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
        $(document).ready(function () {
            $("#<%=txttdate.ClientID %>").dynDateTime({
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
    </script>--%>


      <script language="javascript" type="text/javascript">
          $(function () {
              $("#<%= txtfdate.ClientID %>").datepicker();

              var prm = Sys.WebForms.PageRequestManager.getInstance();

              prm.add_endRequest(function () {
                  $("#<%= txtfdate.ClientID %>").datepicker();

              });

          });
          $(function () {
              $("#<%= txttdate.ClientID %>").datepicker();

              var prm = Sys.WebForms.PageRequestManager.getInstance();

              prm.add_endRequest(function () {
                  $("#<%= txttdate.ClientID %>").datepicker();

              });

          });

            </script>

    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }

        .auto-style4 {
            height: 27px;
        }
    </style>

</asp:Content>

    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

     <br />
    
   
    <div = "DivParameter" align="center">
        <table class="style1">
                <thead>
                <tr>
                    <th colspan="8">GL Trial Balance With Header Report
                    </th>
                </tr>

            </thead>
            
            
            <tr>
                
                <td>
                     <asp:Label ID="lblfdate" runat="server" Text="From Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                 </td>
                
               
                <td>
                      &nbsp;
                      <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                
                     &nbsp;&nbsp;

                
                     <asp:Label ID="lbltdate" runat="server" Text="To Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                  
                      &nbsp;
                  
                      <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>
                
               
            </tr>

            <tr>

                <td>
                    <asp:Label ID="lblGLCode" runat="server" Text=" GL Code" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td>
                    &nbsp;
                    <asp:TextBox ID="txtGLCode" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtGLCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlGLCode" runat="server" Height="25px" Width="350px" AutoPostBack="True"
                        Font-Size="Large" Style="margin-left: 10px" OnSelectedIndexChanged="ddlGLCode_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>



                </td>

            </tr>

            <tr>

                <td>

                    <asp:CheckBox ID="ChkAllFCashCode" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllFCashCode_CheckedChanged" />

                    </td>
                    &nbsp;

                <td>
                    <asp:Label ID="lblFCashCode" runat="server" Text=" From Cash Code" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                
                    &nbsp;&nbsp;

                
                    <asp:TextBox ID="txtFCashCode" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtFCashCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlFCashCode" runat="server" Height="25px" Width="350px" AutoPostBack="True"
                        Font-Size="Large" Style="margin-left: 10px" OnSelectedIndexChanged="ddlFCashCode_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>



                </td>

            </tr>

              <tr>
              <td>    
                  <asp:CheckBox ID="ChkZeroBalanceShow" runat="server" Font-Size="Large" ForeColor="Red" Text="    Show  With Zero Balance"  />         
                 
              </td>    
                  </tr>       
             
<tr>
          
    <td></td>
    
     <td class="auto-style1">
                    <asp:Button ID="BtnView" runat="server" Text="Preview / Print" Font-Size="Large" ForeColor="White"
                       Height="27px" Width="160px" Font-Bold="True"  CssClass="button green"  OnClick="BtnView_Click" />&nbsp;
                  &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"  />
                    <br />
                </td>
                  
     
   
          

    </tr>
                

    
            </table>
        </div>

     <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>
