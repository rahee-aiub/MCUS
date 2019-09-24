<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAutoTransferBenefit.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAutoTransferBenefit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed ?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Print or Preview information??');
        }

        function ValidationBeforeDelete() {
            return confirm('Are you sure you want to Delete ?');
        }

    </script>


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width: 1700px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 1480px;*/
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
                    <th colspan="3">Auto Tansfer Monthly Savings Plus Benefit
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
                        Font-Size="Large"  Enabled="false">
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
                        Font-Bold="True" Height="27px" Width="120px" CssClass="button green" OnClick="BtnCalculate_Click" />&nbsp;
                       <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="White"
                           Font-Bold="True" Height="27px" Width="86px" CssClass="button green" OnClick="BtnPrint_Click" />&nbsp;
                      <asp:Button ID="BtnPost" runat="server" Text="Post" Font-Size="Large" ForeColor="White"
                          Font-Bold="True" Height="27px" Width="86px" CssClass="button green" OnClick="BtnPost_Click" />&nbsp;
                    <asp:Button ID="BtnReverse" runat="server" Text="Reverse" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="120px" CssClass="button green" OnClick="BtnReverse_Click"/>&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    <div align="left" class="grid_scroll">

        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>

                <asp:BoundField HeaderText="CU No." DataField="CuNumber" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="AccNo" DataField="AccNo"  HeaderStyle-Width="150px" ItemStyle-Width="150px"/>
                <asp:BoundField HeaderText="Name" DataField="MemName"  HeaderStyle-Width="430px" ItemStyle-Width="430px"/>
                <asp:BoundField HeaderText="Mth.Benefit" DataField="AccMthBenefitAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="No.Mth." DataField="NoMonths"  HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField HeaderText="Total Benefit" DataField="AccTotalBenefitAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>     
                <asp:BoundField HeaderText="Corr. A/c" DataField="AccCorrAccNo"  HeaderStyle-Width="150px" ItemStyle-Width="150px"/>             
            </Columns>
           
        </asp:GridView>

        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL BENEFIT :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
    
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       
       
        <asp:Label ID="txtTotalBenefit" runat="server" Font-Size="Large" ForeColor="Black" Font-Bold="True" Font-Overline="False"></asp:Label>  
        

    </div>
      
    
    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMsgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
    
   <asp:Label ID="lblVPrintFlag" runat="server" Text="" Visible="false"></asp:Label>

   <asp:Label ID="lblPostFlag" runat="server" Text="" Visible="false"></asp:Label>

   <asp:Label ID="ChkBenefitAmt" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

