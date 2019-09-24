<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAutoProvision.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAutoProvision" %>

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
            width:1250px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
            Width:1230px
     
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
                    <th colspan="3">Auto Provision Processing
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
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    <div align="left" class="grid_scroll">
        <asp:GridView ID="gvDetailInfoCPS" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px" OnRowDataBound="gvDetailInfoCPS_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="CU No." DataField="CuNumber" HeaderStyle-Width="90px"  ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="AccType" DataField="AccType"  HeaderStyle-Width="65px" ItemStyle-Width="65px"/>
                <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="130px" ItemStyle-Width="130px" />
                <asp:BoundField HeaderText="Name" DataField="MemName"  HeaderStyle-Width="350px" ItemStyle-Width="350px"/>
                <asp:BoundField HeaderText="Open Date" DataField="AccOpenDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Mature Date" DataField="AccMatureDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ItemStyle-Width="100px"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField HeaderText="Period" DataField="AccPeriodMonths"  HeaderStyle-Width="60px" ItemStyle-Width="60px"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField HeaderText="Mth.Product" DataField="CurrMthProduct" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Rate" DataField="AccIntRate" DataFormatString="{0:0,0.00}" HeaderStyle-Width="65px" ItemStyle-Width="65px"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField HeaderText="Interest" DataField="CurrMthProvision" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                
            </Columns>
            
        </asp:GridView>
  
        <asp:GridView ID="gvDetailInfoFDR" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px" OnRowDataBound="gvDetailInfoFDR_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="CU No." DataField="CuNumber" HeaderStyle-Width="90px"  ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="AccType" DataField="AccType"  HeaderStyle-Width="65px" ItemStyle-Width="65px"/>
                <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Name" DataField="MemName"  HeaderStyle-Width="360px" ItemStyle-Width="360px"/>
                <asp:BoundField HeaderText="FD Amount" DataField="AccFDAmount" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Rate" DataField="AccIntRate" DataFormatString="{0:0,0.00}" HeaderStyle-Width="60px" ItemStyle-Width="60px"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField HeaderText="UPTO Last Month" DataField="UptoLastMthProvision" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="This Month" DataField="CurrMthProvision" DataFormatString="{0:0,0.00}" HeaderStyle-Width="110px" ItemStyle-Width="110px"  HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="UPTO Month" DataField="UptoMthProvision" DataFormatString="{0:0,0.00}" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" /> 
            </Columns>
            
        </asp:GridView>
        <asp:GridView ID="gvDetailInfo6YR" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px" OnRowDataBound="gvDetailInfo6YR_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="CU No." DataField="CuNumber" HeaderStyle-Width="90px"  ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="AccType" DataField="AccType"  HeaderStyle-Width="65px" ItemStyle-Width="65px"/>
                <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Name" DataField="MemName"  HeaderStyle-Width="360px" ItemStyle-Width="360px"/>
                <asp:BoundField HeaderText="FD Amount" DataField="AccFDAmount" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Rate" DataField="AccIntRate" DataFormatString="{0:0,0.00}" HeaderStyle-Width="60px" ItemStyle-Width="60px"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField HeaderText="UPTO Last Month" DataField="UptoLastMthProvision" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="This Month" DataField="CurrMthProvision" DataFormatString="{0:0,0.00}" HeaderStyle-Width="110px" ItemStyle-Width="110px"  HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="UPTO Month" DataField="UptoMthProvision" DataFormatString="{0:0,0.00}" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" /> 
            </Columns>
            
        </asp:GridView>
        
        <asp:GridView ID="gvDetailInfoLOAN" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px" OnRowDataBound="gvDetailInfoLOAN_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="CU No." DataField="CuNumber" HeaderStyle-Width="90px"  ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="AccType" DataField="AccType"  HeaderStyle-Width="65px" ItemStyle-Width="65px"/>
                <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Name" DataField="MemName"  HeaderStyle-Width="360px" ItemStyle-Width="360px"/>
                <asp:BoundField HeaderText="Outs.Bal" DataField="AccBalance" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="Rate" DataField="AccIntRate" DataFormatString="{0:0,0.00}" HeaderStyle-Width="60px" ItemStyle-Width="60px"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField HeaderText="UPTO Last Month" DataField="UptoLastMthProvision" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="This Month" DataField="CurrMthProvision" DataFormatString="{0:0,0.00}" HeaderStyle-Width="110px" ItemStyle-Width="110px"  HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                <asp:BoundField HeaderText="UPTO Month" DataField="UptoMthProvision" DataFormatString="{0:0,0.00}" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" /> 
            </Columns>
            
        </asp:GridView>

    </div>
    <br />
    <div>
             
        <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="250px" BorderStyle="Ridge" Font-Size="Large" Visible="False"></asp:TextBox>
                    
             
    </div>

    <div align="right" style="width: 1290px; height: 24px;">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
       
        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
              <asp:Label ID="txtTotalFDAmt" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
             <asp:Label ID="txtTotaluptolastmnth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="txtTotalthisMonth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Label ID="txtTotalUptoMonth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt1" runat="server" Text="TOTAL AMOUNT :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
       
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
              <asp:Label ID="txtTotalInt" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
             
    </div>

    
    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>
    

</asp:Content>

