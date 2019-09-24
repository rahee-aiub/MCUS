    <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLFinalAccountsView.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLFinalAccountsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../scripts/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="../scripts/jQuery-1.8.2.min.js" type="text/javascript"></script>

    <script src="../scripts/ui/1.9.1/jquery-ui.js" type="text/javascript"></script>

    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>

    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />


  
   



 <%--<script language="javascript" type="text/javascript">
     $(function () {

         var prm = Sys.WebForms.PageRequestManager.getInstance();

         prm.add_endRequest(function () {
             $(".youpi").datepicker();
         });
     });

        
</script>--%>




  <%-- <script language="javascript" type="text/javascript">
       $(function () {

           var prm = Sys.WebForms.PageRequestManager.getInstance();

           prm.add_endRequest(function () {
               $(".youpii").chosen();
           });
       });


 </script>--%>



    <script type="text/javascript">
        $(document).ready(function () {
            $(document).keyup(function (event) {
                var key = event.keyCode || event.charCode || 0;
                if (key == 113) {
                    eval($("[id$='lnkPage1']").attr("href"));
                }
            });
        });
    </script>

    <link href="../Styles/chosen.css" rel="stylesheet" />


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 413px;
            width: 1340px;
            margin: 0 auto;
        }

        .grid_scroll2 {
            overflow: auto;
            height: 375px;
            width: 1020px;
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

        .text {
        }

        .auto-style1 {
            height: 15px;
        }

        .auto-style2 {
            width: 230px;
        }
    </style>








     <script src="../scripts/jquery-ui.js" type="text/javascript"></script>

    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        $(function () {
            $(".youpi").datepicker();
        });

    </script>

    

    <script language="javascript" type="text/javascript">
        function ValidationBeforeAdd() {

            return confirm('Are you sure you want to Add information?');
            return false;
        }
    </script>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeUpdate() {

            return confirm('Are you sure you want to Update information?');
            return false;
        }
    </script>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeDelete() {

            return confirm('Are you sure you want to Delete information?');
            return false;
        }
    </script>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeCancel() {

            return confirm('Are you sure you want to Cancel information?');
            return false;
        }
    </script>

    <script type="text/javascript">

        function ResetScrollPosition() {
            setTimeout("window.scrollTo(0,0)", 0);
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div align="center">
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
                      <asp:TextBox ID="txtfdate" runat="server" Enabled="false" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code"></asp:TextBox>

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
                      <asp:TextBox ID="txttdate" runat="server" Enabled="false" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code"></asp:TextBox>

                </td>
                
               
            </tr>


        </table>
        <table>
            <tr>
                <td></td>

            </tr>
        </table>

    </div>


    <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    --%>
    <asp:GridView ID="gvHead1" align="center" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvHead1_RowDataBound">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <Columns>


            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblOpBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLOpBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLDrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLCrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDtlClBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLClBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

        </Columns>

    </asp:GridView>


     <asp:GridView ID="gvHead2" align="center" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#00ffff"
        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvHead2_RowDataBound">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <Columns>


            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblOpBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLOpBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLDrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLCrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDtlClBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLClBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

        </Columns>

    </asp:GridView>


    <asp:GridView ID="gvHead3" align="center" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="RosyBrown"
        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvHead3_RowDataBound">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <Columns>


            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblOpBal" runat="server" Enabled="false" Style="color: blue" Text='<%#Eval("GLOpBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLDrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLCrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDtlClBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLClBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

        </Columns>

    </asp:GridView>







    <asp:Label ID="lblReverseFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblErrFlag" runat="server" Text="" Visible="false"></asp:Label>


    <div id="DivSearch" runat="server" align="Center">

        <table>
            <tr>
                <td></td>
                

                <td>&nbsp;&nbsp;
                &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />

                    &nbsp;&nbsp;
                    <asp:Button ID="BtnBack" runat="server" Text="Back" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Back Page" CausesValidation="False"
                        CssClass="button green" OnClick="BtnBack_Click" />

                    &nbsp;&nbsp;
                    <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Print Page" CausesValidation="False"
                        CssClass="button blue" OnClick="BtnPrint_Click" />

                    <br />
                </td>

            </tr>
           </table>

    </div>

   


    <div id="DivGV" runat="server" align="Center" class="grid_scroll">


        <asp:GridView ID="gvHeaderInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvHeaderInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:TemplateField HeaderText="Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblOpBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLOpBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLDrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLCrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDtlClBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLClBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                

                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="68px">
                    <ItemTemplate>
                        <asp:Button ID="BtnHdrSelect" runat="server" Font-Bold="True" Text="Select" OnClick="BtnHdrSelect_Click" CssClass="button green" />
                        
                    </ItemTemplate>
                </asp:TemplateField>

               


            </Columns>

        </asp:GridView>

        <asp:GridView ID="gvSubHeaderInfo" align="center" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#00ffff"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSubHeaderInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                 <asp:TemplateField HeaderText="Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblOpBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLOpBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLDrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLCrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDtlClBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLClBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                

                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="68px">
                    <ItemTemplate>
                        <asp:Button ID="BtnSubHdrSelect" runat="server" Font-Bold="True" Text="Select" OnClick="BtnSubHdrSelect_Click" CssClass="button green" />
                        
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>

        </asp:GridView>



        <asp:GridView ID="gvSubHeaderDtlInfo" align="center" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="RosyBrown"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSubHeaderDtlInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                 <asp:TemplateField HeaderText="Code" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAccNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description" HeaderStyle-Width="350px" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("GLAccDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblOpBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLOpBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLDrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLCrSum","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDtlClBal" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLClBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="90px">
                    <ItemTemplate>
                        <asp:Button ID="BtnSubHdrDtlSelect" runat="server" Font-Bold="True" Text="Statement" OnClick="BtnSubHdrDtlSelect_Click" CssClass="button green" />
                        
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>

        </asp:GridView>



         <asp:GridView ID="gvStatementInfo" align="center" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="Orange"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvStatementInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                 <asp:BoundField HeaderText="TrnDate" DataField="TrnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-Font-Bold="true" />

                 <asp:TemplateField HeaderText="Vch.No." HeaderStyle-Width="115px" ItemStyle-Width="115px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblVchNo" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("VchNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description" HeaderStyle-Width="380px" ItemStyle-Width="380px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="True" Enabled="false" Text='<%# Eval("TrnDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 
                <asp:TemplateField HeaderText="Debit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLDebitAmt","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Font-Bold="True" Enabled="false" Style="color: darkgreen" Text='<%#Eval("GLCreditAmt","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Balance" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblBalance" runat="server" Font-Bold="True" Enabled="false" Style="color: blue" Text='<%#Eval("GLClosingBal","{0:0,0.00}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                

                <%--<asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="68px">
                    <ItemTemplate>
                        <asp:Button ID="BtnSubHdrDtlSelect" runat="server" Text="Select" OnClick="BtnSubHdrDtlSelect_Click" CssClass="button green" />
                        
                    </ItemTemplate>
                </asp:TemplateField>--%>


            </Columns>

        </asp:GridView>





        
    </div>



<%--    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>--%>


    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTrfAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>

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
    <asp:Label ID="lblCtrlFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCTypeName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblSPflag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtypeGuaranty" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblBatchNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnLineNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnDt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchDt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDescPayTo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDescPurpose" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCostCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDebitAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCreditAmt" runat="server" Text="" Visible="false"></asp:Label>



    <asp:Label ID="lblVoucherNumber" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblScreenFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblVchType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPeriodNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMsgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblSkipFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblLNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblRowFlag" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblBRowFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblRowddlFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblMonth" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblIdNum" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="OrgTrnDate" runat="server" Text="" Visible="false"></asp:Label>
        
     <asp:Label ID="lblGLHeadNumber" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblGLSubHeadNumber" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblGLHead" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblGLSubHead" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblReportOpt" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblGLStatement" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblGLDesc" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblRepFlag" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

