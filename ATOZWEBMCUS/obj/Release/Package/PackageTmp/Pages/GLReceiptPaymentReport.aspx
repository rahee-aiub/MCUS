<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLReceiptPaymentReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLReceiptPaymentReport" %>
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
        .auto-style2 {
            width: 121px;
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
                    <th colspan="8"> Received and Payment Report 
                    </th>
                </tr>

            </thead>
            
            
            <tr>
                
                 <td style="background-color: #fce7f9">
                     <asp:Label ID="lblfdate" runat="server" Text="From Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                 </td>
                <td style="background-color: #fce7f9">
                    :
                </td>
                <td class="auto-style2">

                      <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>
                
                <td style="background-color: #fce7f9">

                </td>
                 <td style="background-color: #fce7f9">
                     <asp:Label ID="lbltdate" runat="server" Text="To Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                                 
                      <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

               
                
               
            </tr>
             <tr>
               
             
                
                 <td style="background-color: #fce7f9">
                     
                    <asp:CheckBox ID="ChkAllCashCode" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllCashCode_CheckedChanged" />
                     
                     &nbsp;
                     
                     <asp:Label ID="Label2" runat="server" Text="Cash Code" Font-Size="Large"
                        ForeColor="Red"></asp:Label>&nbsp;
                  </td>

                  <td style="background-color: #fce7f9">
                     :
                  </td>

                  <td> 
                     <asp:TextBox ID="txtCashCode" runat="server" CssClass="cls text" onkeypress="return IsNumberKey(event)" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCashCode_TextChanged" MaxLength="8"></asp:TextBox> 
                  </td>
                  <td style="background-color: #fce7f9">
                     
                  </td>
                  
                 
                   <td style="background-color: #fce7f9">
                     
                    
                    <asp:Label ID="lblCodeDesc" runat="server" Text="" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                   
                  </td>
                 </tr>
             <tr>
                  <td style="background-color: #fce7f9" colspan ="8">
                     <asp:CheckBox ID="ChkShowZero" runat="server" Text="  Show Zero ?" BackColor="#CCFFFF" ForeColor="#CC0000" style="font-weight: 700" />
                 </td>
             </tr>
                  
            <tr>
                  <td style="background-color: #fce7f9" colspan ="8">
                     <asp:CheckBox ID="ChkWithSysTran" runat="server" Text="  With System Transaction ?" BackColor="#CCFFFF" ForeColor="#CC0000" style="font-weight: 700" />
                 </td>
             </tr>  
             
             <tr>
                   
                   <td style="background-color: #fce7f9" colspan ="8">
                             
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtOpt4thLayer" runat="server" GroupName="GLRptGrpPrm" Text="Details" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" Checked="True" />
               
                   &nbsp;&nbsp; <asp:RadioButton ID="rbtOpt3rdLayer" runat="server" GroupName="GLRptGrpPrm" Text="Summary" AutoPostBack="True" style="font-weight: 700" Font-Italic="True"  />
                        
                 </td>
         
               
             </tr>  
          
            <tr>

                <td style="background-color: #fce7f9" colspan ="8">

                </td>
                
            </tr>
       <tr>
    
                <td style="background-color: #fce7f9" colspan ="8">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnView" runat="server" Text="Preview / Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True"  CssClass="button green"  Height="27px" Width="170px" OnClick="BtnView_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"  />
                 </td>
        </tr>

            
            
            </table>
        </div>
    

    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
   
    <asp:Label ID="lblCashCodeName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtGLPLCode" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
   

</asp:Content>
