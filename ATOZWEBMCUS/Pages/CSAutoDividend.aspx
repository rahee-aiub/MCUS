<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAutoDividend.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAutoDividend" %>

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
            width:1300px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width:1355px;*/
           
     
        }  
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Auto Dividend Processing
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAccType" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="lblPdate" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
                </td>
            </tr>

            

            <tr>
                <td>
                    <asp:Label ID="lblIntRate" runat="server" Text="Interest Rate :" Font-Size="Large" Width="155px" Height="25px"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIntRate" runat="server" CssClass="cls text" Style="text-align: right" Width="80px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" 
                        Font-Size="Medium" MaxLength ="6" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Description :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="cls text" Width="350px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Text="Voucher No. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnCalculate" runat="server" Text="Calculate" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnCalculate_Click" />&nbsp;
                       <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="White"
                           Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnPrint_Click" />&nbsp;
                      <asp:Button ID="BtnPost" runat="server" Text="Post" Font-Size="Large" ForeColor="White"
                          Font-Bold="True" CssClass="button green" OnClick="BtnPost_Click" OnClientClick="return ValidationBeforeSave()"/>&nbsp;
                    <asp:Button ID="BtnReverse" runat="server" Text="Reverse" Font-Size="Large" ForeColor="White"
                          Font-Bold="True" CssClass="button green" OnClick="BtnReverse_Click" OnClientClick="return ValidationBeforeSave()"/>&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>

     <div id="Div1" class="grid_scroll" runat="server" align="center" style="height: 264px; overflow: auto; width: 100%;">

          <asp:GridView ID="gvDetailInfo1" runat="server" HeaderStyle-BackColor="YellowGreen"
                AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo1_RowDataBound">
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>

                <asp:BoundField HeaderText="CU" DataField="CuNumber" HeaderStyle-Width="65px"  ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Dep." DataField="MemNo" HeaderStyle-Width="38px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Ty." DataField="AccType"  HeaderStyle-Width="15px" ItemStyle-Width="65px"/>
                <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="196px" ItemStyle-Width="130px" />
                <asp:BoundField HeaderText="Jul." DataField="AmtJul" DataFormatString="{0:0,0.00}" HeaderStyle-Width="130px" ItemStyle-Width="130px"  HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Aug." DataField="AmtAug" DataFormatString="{0:0,0.00}" HeaderStyle-Width="130px" ItemStyle-Width="130px"  HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Sep." DataField="AmtSep" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Oct." DataField="AmtOct" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Nov." DataField="AmtNov" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Dec." DataField="AmtDec" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Jan." DataField="AmtJan" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Feb." DataField="AmtFeb" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Mar." DataField="AmtMar" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Apr." DataField="AmtApr" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="May" DataField="AmtMay" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Jun." DataField="AmtJun" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="120px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Total Product" DataField="AmtProduct" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Net Interest" DataField="AmtInterest" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                              
            </Columns>
            
        </asp:GridView>
  
              

    </div>

    



    <br />
    <div>
             
        <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="250px" BorderStyle="Ridge" Font-Size="Large" Visible="False"></asp:TextBox>
                    
             
    </div>

    <div align="right" style="width: 1290px; height: 24px;">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalProd" runat="server" Text="TOTAL PRODUCT :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
        &nbsp;&nbsp;
              <asp:Label ID="txtTotalProd" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
       
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Label ID="lblTotalInt" runat="server" Text="TOTAL INTEREST :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
        &nbsp;&nbsp;
              <asp:Label ID="txtTotalInt" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
             
    </div>

    
    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblBegYear" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
     <asp:Label ID="lblEndYear" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
     <asp:Label ID="lblCurrMth" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    

</asp:Content>

