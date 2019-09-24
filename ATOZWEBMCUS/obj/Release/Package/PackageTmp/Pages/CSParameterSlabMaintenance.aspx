<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="CSParameterSlabMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSParameterSlabMaintenance"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
	    function ValidationBeforeSave()
	    {    
	     return confirm('Are you sure you want to save information?');	  
	    }
	    
	    function ValidationBeforeUpdate()
	    {    
	     return confirm('Are you sure you want to Update information?');	  
	    }
	   
	   	    
    </script>

   
      <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 190px;
            margin: 0 auto;
            width:1000px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
     
        }  
    </style>

    <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <%--<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtPensionDate.ClientID %>").dynDateTime({
            showsTime:false,
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
        $("#<%=txtPreDate.ClientID %>").dynDateTime({
            showsTime:false,
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div align="center">
        <asp:Label ID="lblTypecls" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="lblTCode" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblTest" runat="server" Text="" Visible="false"></asp:Label>
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="2">
                        <asp:Label ID="lblPensionHead" runat="server" Text="Slab Parameter Maintenance"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tr>
           <%--     <td>
                    <h3>
                        <asp:Label ID="lblDate" runat="server" Text="Date "></asp:Label></h3>
                    <asp:TextBox ID="txtPensionDate" runat="server" Width="140px" Height="25px" CssClass="border_color"
                        Font-Size="Large" img src="../Images/calender.png" AutoPostBack="true" OnTextChanged="txtPensionDate_TextChanged"
                        TabIndex="1"></asp:TextBox>
                </td>
           --%>      <td>
                    <h3>
                        <asp:Label ID="lblSlabFlag" runat="server" Text="Mem.Type"></asp:Label></h3>
                        
                    <asp:DropDownList 
                        ID="ddlSlabFlag" runat="server" Height="25px" Width="96px" CssClass="cls text"
                        Font-Size="Large" TabIndex="2" AutoPostBack="true"
                         onselectedindexchanged="ddlSlabFlag_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Premium</asp:ListItem>
                        <asp:ListItem Value="2">General</asp:ListItem>
                    </asp:DropDownList>   
                                     
                        
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblRecord" runat="server" Text="Deposit"></asp:Label></h3>
                    <asp:TextBox ID="txtPensionRecord" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" OnTextChanged="txtPensionRecord_TextChanged" 
                        TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblPeriodMth" runat="server" Text="Period(Mth) "></asp:Label></h3>
                    <asp:TextBox ID="txtPeriodMth" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" AutoPostBack="true" OnTextChanged="txtPeriodMth_TextChanged"
                        TabIndex="4"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblBenefitAmt" runat="server" Text="BenefitAmt"></asp:Label></h3>
                    <asp:TextBox ID="txtBenefitAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                        CssClass="cls text" TabIndex="5" AutoPostBack="true" ontextchanged="txtBenefitAmt_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblPensionIntRate" runat="server" Text="Int.Rate"></asp:Label></h3>
                    <asp:TextBox ID="txtPensionIntRate" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" TabIndex="6"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblPenalAmt" runat="server" Text="PenalAmt"></asp:Label></h3>
                    <asp:TextBox ID="txtPenalAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                        CssClass="cls text" TabIndex="8"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblBonusAmt" runat="server" Text="BonusAmt"></asp:Label></h3>
                    <asp:TextBox ID="txtBonusAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                        CssClass="cls text" TabIndex="7"></asp:TextBox>
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Width="91px" Font-Size="Large"
                        ForeColor="White" Height="25px" Font-Bold="True" OnClick="BtnSubmit_Click" OnClientClick="return ValidationBeforeSave() "
                        CssClass="button green" />
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Width="91px" Font-Size="Large"
                        ForeColor="White" Height="25px" Font-Bold="True" ToolTip="Update Information"
                        OnClientClick="return ValidationBeforeUpdate() " CssClass="button green" OnClick="BtnUpdate_Click" />
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" Font-Bold="True" Font-Size="Large"
                        ForeColor="#FFFFCC" Height="25px" Width="91px" CssClass="button red" OnClick="BtnDelete_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="2">
                        <asp:Label ID="lblPrematureHead" runat="server" Text=" Premature Parameter  Maintenance"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tr>
           <%--     <td>
                    <h3>
                        <asp:Label ID="lblPreDate" runat="server" Text="Date"></asp:Label></h3>
                    <asp:TextBox ID="txtPreDate" runat="server" Width="140px" Height="25px" AutoPostBack="True"
                        CssClass="border_color" Font-Size="Large" img src="../Images/calender.png" OnTextChanged="txtPreDate_TextChanged"
                        TabIndex="1"></asp:TextBox>
                </td>
       --%>         <td>
                    <h3>
                        <asp:Label ID="lblMnthBelow" runat="server" Text="Month Below"></asp:Label></h3>
                    <asp:TextBox ID="txtMnthBelow" runat="server" Width="96px" Height="25px" AutoPostBack="True"
                        Font-Size="Large" CssClass="cls text" OnTextChanged="txtMnthBelow_TextChanged"
                        TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblPreIntRate" runat="server" Text="Int.Rate"></asp:Label></h3>
                    <asp:TextBox ID="txtIntRate" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnPreSubmit" runat="server" Text="Submit" Width="91px" Font-Size="Large"
                        ForeColor="White" Height="25px" Font-Bold="True" ToolTip="Insert Information"
                        OnClientClick="return ValidationBeforeSave() " CssClass="button green" OnClick="BtnPreSubmit_Click" />
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnPreUpdate" runat="server" Text="Update" Width="91px" Font-Size="Large"
                        ForeColor="White" Height="25px" Font-Bold="True" ToolTip="Update Information"
                        OnClientClick="return ValidationBeforeUpdate() " CssClass="button green" OnClick="BtnPreUpdate_Click" />
                </td>
                <td>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="BtnPreDelete" runat="server" Text="Delete" Font-Bold="True" Font-Size="Large"
                        ForeColor="#FFFFCC" Height="25px" Width="91px" CssClass="button red" OnClick="BtnPreDelete_Click" />
                </td>
            </tr>
        </table>
        <br />
        <div align="left" class="grid_scroll">
            <asp:GridView ID="gvDetailInfo" runat="server"  HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound1" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <Columns>
                    <asp:BoundField DataField="AtyAccType" HeaderText="A/C Type" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                    <%--<asp:BoundField DataField="AtyDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />--%>
                    <asp:BoundField DataField="AtyFlag" HeaderText="Mem.Type" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="AtyRecords" HeaderText="Deposit" DataFormatString="{0:0,0.00}" HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="AtyPeriod" HeaderText="Period(Mth)" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="AtyMaturedAmt" HeaderText="BenefitAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="AtyIntRate" HeaderText="Int.Rate" DataFormatString="{0:0,0.00}" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="AtyPenalAmt" HeaderText="PenalAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="80px" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="AtyBonusAmt" HeaderText="BonusAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" />
                </Columns>
               
            </asp:GridView>
        </div>
    </div>
    <br />
    <div align="center" style="width: 1079px">
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Bold="True" Font-Size="Large"
            ForeColor="#FFFFCC" Height="25px" Width="91px" CssClass="button red" OnClick="BtnExit_Click"
            OnClientClick="window.close('CSParameterOpeningMaintenance.aspx')" /></div>

    <asp:Label ID="Errmsg" runat="server"  Visible="false"></asp:Label>  


</asp:Content>
