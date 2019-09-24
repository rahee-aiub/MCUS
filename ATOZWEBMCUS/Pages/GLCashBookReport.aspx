<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLCashBookReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLCashBookReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../scripts/validation.js" type="text/javascript"></script>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

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
                    <th colspan="7"> Cash Book Report
                    </th>
                </tr>

            </thead>
            
            
            <tr>
                 
                
                <td style="background-color: #fce7f9" class="auto-style3">
                     <asp:Label ID="lblfdate" runat="server" Text="From Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>&nbsp;
                     

                
                
                <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox> 
                </td>  
                
                &nbsp;&nbsp;
                <td style="background-color: #fce7f9">
                     <asp:Label ID="lbltdate" runat="server" Text="To Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>&nbsp;
                    

                

                        <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" AutoPostBack="True" OnTextChanged="txttdate_TextChanged"></asp:TextBox>
                  </td>

            </tr>

             <tr>
               
             
                
                 <td style="background-color: #fce7f9">
                     
                    <asp:CheckBox ID="ChkAllCashCode" runat="server" ForeColor="Red" Text="All" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAllCashCode_CheckedChanged" />
                     
                     &nbsp;
                     
                     <asp:Label ID="Label2" runat="server" Text="Cash Code :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>&nbsp;

                     <asp:TextBox ID="txtCashCode" runat="server" CssClass="cls text" onkeypress="return IsNumberKey(event)" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCashCode_TextChanged" MaxLength="8"></asp:TextBox> 

                </td>
              
                  
              <td style="background-color: #fce7f9">
                     
                    
                    <asp:Label ID="lblCodeDesc" runat="server" Text="" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                   
                  </td>
                 </tr>

            <tr>
                 
                   
                   <td style="background-color: #fce7f9" colspan ="8">
                         
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                      
                   <asp:RadioButton ID="rbtOptAfterFunc" runat="server" GroupName="GLRptOptPrm" Text="After Closing" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" Checked="True"  />

                    &nbsp;&nbsp;<asp:RadioButton ID="rbtOptBeforeFunc" runat="server" GroupName="GLRptOptPrm" Text="Before Closing" AutoPostBack="True" style="font-weight: 700" Font-Italic="True"  />   
                 </td>
         

            </tr>


            <tr>
                 
                   
                   <td style="background-color: #fce7f9" colspan ="8">
                             
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:RadioButton ID="rbtOptDetails" runat="server" GroupName="GLRptGrpPrm" Text="Details" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" Checked="True"  />
               
                   &nbsp;&nbsp; <asp:RadioButton ID="rbtOptSummary" runat="server" GroupName="GLRptGrpPrm" Text="Summary" AutoPostBack="True" style="font-weight: 700" Font-Italic="True"  />
                        
                 </td>
         

            </tr>

             
              <tr>
               <%--<td style="background-color: #fce7f9" class="auto-style3" colspan ="6">
                </td>--%>
                 
              <td style="background-color: #fce7f9">             
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnView" runat="server" Text="Preview / Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True"  CssClass="button green" Height="27px" Width="170px" OnClick="BtnView_Click" />&nbsp;
                  &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"  />
                    <br />
                </td>
            </tr>
            </table>
        </div>


    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="lblCashCodeName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtGLPLCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblOptFuncFlag" runat="server" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="hdnGLSubHead1" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

