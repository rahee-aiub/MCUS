<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRGenerateMonthlySalary.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRGenerateMonthlySalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed ?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Print or Preview information ?');
        }

    </script>

     <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width:4000px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
            width:2470px;
           
            
     
        }  
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Generate Monthly Salary
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Process Month :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="324px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    
                </td>
            </tr>
           
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnProcess" runat="server" Text="Process" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="130px" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnProcess_Click" />&nbsp;
                       
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    <div align="left" class="grid_scroll">
        <asp:GridView ID="gvDetailInfoFDR" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px" OnRowDataBound="gvDetailInfoFDR_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="Sl No." DataField="EmpCode" HeaderStyle-Width="50px"  ItemStyle-Width="50px" />
                <asp:BoundField HeaderText="Name" DataField="EmpName"  HeaderStyle-Width="300px" ItemStyle-Width="300px"/>
                <asp:BoundField HeaderText="Designation" DataField="EmpDesigDesc"  HeaderStyle-Width="250px" ItemStyle-Width="250px"/>
                <asp:BoundField HeaderText="Grade" DataField="EmpGradeDesc"  HeaderStyle-Width="70px" ItemStyle-Width="70px"/>
                <asp:BoundField HeaderText="Basic" DataField="BasicAmount" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField HeaderText="Consolidated" DataField="ConsolidatedAmt" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" ItemStyle-Width="100px" Visible ="true"/>
               

                <asp:BoundField DataField="TAAmount1" HeaderText="Amount1" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount2" HeaderText="Amount2" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount3" HeaderText="Amount3" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount4" HeaderText="Amount4" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount5" HeaderText="Amount5" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount6" HeaderText="Amount6" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount7" HeaderText="Amount7" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount8" HeaderText="Amount8" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount9" HeaderText="Amount9" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount10" HeaderText="Amount10" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount11" HeaderText="Amount11" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount12" HeaderText="Amount12" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount13" HeaderText="Amount13" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount14" HeaderText="Amount14" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount15" HeaderText="Amount15" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount16" HeaderText="Amount16" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount17" HeaderText="Amount17" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount18" HeaderText="Amount18" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount19" HeaderText="Amount19" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TAAmount20" HeaderText="Amount20" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
       
                
                <asp:BoundField DataField="GrossTotal" HeaderText="Gross Amt." DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>

                <asp:BoundField DataField="TDAmount1" HeaderText="Amount1" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount2" HeaderText="Amount2" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount3" HeaderText="Amount3" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount4" HeaderText="Amount4" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount5" HeaderText="Amount5" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount6" HeaderText="Amount6" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount7" HeaderText="Amount7" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount8" HeaderText="Amount8" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount9" HeaderText="Amount9" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount10" HeaderText="Amount10" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount11" HeaderText="Amount11" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount12" HeaderText="Amount12" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount13" HeaderText="Amount13" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount14" HeaderText="Amount14" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount15" HeaderText="Amount15" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount16" HeaderText="Amount16" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount17" HeaderText="Amount17" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount18" HeaderText="Amount18" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount19" HeaderText="Amount19" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>
                <asp:BoundField DataField="TDAmount20" HeaderText="Amount20" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px"  ItemStyle-Width="80px" Visible ="true"/>

                <asp:BoundField DataField="DeductTotal" HeaderText="Total Ded." DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>

                <asp:BoundField DataField="NetPayment" HeaderText="Net Payment" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"  ItemStyle-Width="100px" Visible ="true"/>




                
            </Columns>
            
        </asp:GridView>
        
        

    </div>
    <br />

    <%--<div align="right" style="width: 1290px; height: 24px;">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
       
        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
              <asp:Label ID="txtTotalFDAmt" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
             <asp:Label ID="txtTotaluptolastmnth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="txtTotalthisMonth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Label ID="txtTotalUptoMonth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
    </div>--%>

    
    <asp:Label ID="hdnID" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnSalDate" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnNoDays" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>


    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc1" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc2" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc3" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc4" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc5" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc6" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc7" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc8" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc9" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc10" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="CtrlSalPostStat" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>


</asp:Content>

