<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLFinalAccountsReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLFinalAccountsReport" %>
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
                    <th colspan="8">GL Final Accounts Report
                    </th>
                </tr>

            </thead>
            
            
            <tr>
                
                <td>
                     <asp:Label ID="lblfdate" runat="server" Text="From Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                 </td>
                <td>
                    :
                </td>
                <td class="auto-style2">
                      <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" ></asp:TextBox>

                </td>
                
                <td>

                </td>
                <td>
                     <asp:Label ID="lbltdate" runat="server" Text="To Date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                 <td>
                     :
                 </td>
                  <td>
                      <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" AutoPostBack="True" OnTextChanged="txttdate_TextChanged"></asp:TextBox>

                </td>
                
               
            </tr>

            <tr>
                <td colspan =" 8">
                </td>
               
            </tr>
            <tr>
                        
                   <td style="background-color: #fce7f9" colspan ="8">
                                            
                   &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtOptAfterFunc" runat="server" GroupName="GLRptOptPrm" Text="After Closing" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" Checked="True"  />

                    &nbsp;&nbsp;<asp:RadioButton ID="rbtOptBeforeFunc" runat="server" GroupName="GLRptOptPrm" Text="Before Closing" AutoPostBack="True" style="font-weight: 700" Font-Italic="True"  />   
                 </td>
         

            </tr>


             <tr>
                <td colspan =" 8">
                </td>
               
            </tr>
            <tr>
                <td colspan="8" class="auto-style4" >
                 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <asp:RadioButton ID="rbtOpt1stLayer" runat="server" GroupName="GLRptGrpPrm" Text="1st Layer" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" OnCheckedChanged="rbtOpt1stLayer_CheckedChanged" />
                
             
                 &nbsp;&nbsp;<asp:RadioButton ID="rbtOpt2ndLayer" runat="server" GroupName="GLRptGrpPrm" Text="2nd Layer" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" OnCheckedChanged="rbtOpt2ndLayer_CheckedChanged" />
          
              
                 &nbsp;&nbsp; <asp:RadioButton ID="rbtOpt3rdLayer" runat="server" GroupName="GLRptGrpPrm" Text="3rd Layer" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" OnCheckedChanged="rbtOpt3rdLayer_CheckedChanged" />
               
                   &nbsp;&nbsp; <asp:RadioButton ID="rbtOpt4thLayer" runat="server" GroupName="GLRptGrpPrm" Text="4th Layer" AutoPostBack="True" style="font-weight: 700" Font-Italic="True" Checked="True" OnCheckedChanged="rbtOpt4thLayer_CheckedChanged" />
                        
                 </td>
         
               
             </tr>  
             
            
            
            </table>
        </div>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
     <div  id ="DivChkShowZero" align="center">
         <table class="style1">
         <tr>
         <td  colspan ="7">
                     <asp:CheckBox ID="ChkShowZero" runat="server" Text="  Show Zero ?" BackColor="#CCFFFF" ForeColor="#CC0000" style="font-weight: 700" />
        </td>
        </tr>
      </table>
    </div>

    <br />

     <div  id ="DivReportOption" align="center">
        <table class="style1">
                <thead>
                <tr>
                    <th>
                         Reports Name  Option
                    </th>
                </tr>

            </thead>

            <tr>
               <td  >
                   <asp:RadioButton ID="rbtOptTrialBalance" runat="server" Checked="True" GroupName="GLRptGrp" style="font-weight: 700" Text="  Trial Balance" />
               </td>
            </tr>
              
            <tr>
             <td  >
                   <asp:RadioButton ID="rbtOptReceivePayment" runat="server" GroupName="GLRptGrp" style="font-weight: 700" Text="  Received  and  Payment" Visible="False" />
               </td>
         
            
              
       
             </tr>
            <td  >
                   <asp:RadioButton ID="rbtOptIncomeExpenditure" runat="server" GroupName="GLRptGrp" style="font-weight: 700" Text="  Income and Expenditure" />
               </td>
                        
           <tr>
              
               <td  >
                   <asp:RadioButton ID="rbtOptBalanceSheet" runat="server" GroupName="GLRptGrp" style="font-weight: 700" Text="  Balance Sheet" />
               </td>

           </tr>
            <tr>

                <td colspan ="7" class="auto-style4">

                </td>
                
            </tr>
<tr>
          


     <td class="auto-style1">
                    <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                       Height="27px" Width="150px" Font-Bold="True"  CssClass="button green"  OnClick="BtnView_Click" />&nbsp;
                  &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"  />
         &nbsp;
                    <asp:Button ID="BtnGridView" runat="server" Text="GridView" Font-Size="Large" ForeColor="White"
                        Height="27px" Width="120px" Font-Bold="True"  CssClass="button blue"  OnClick="BtnGridView_Click" />
                    <br />
                </td>
                  
     
   
          

    </tr>
                

    
            </table>
        </div>

     <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblOptFuncFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblPLCode" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
