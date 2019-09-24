<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="SYSHolidayMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.SYSHolidayMaintenance" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
	    function ValidationBeforeSave()
	    {
	     
	        var txtHolDate = document.getElementById('<%=txtHolDate.ClientID%>').value;
	        var ddlHolType = document.getElementById('<%=ddlHolType.ClientID%>');
	        if (txtHolDate == '' || txtHolDate.length == 0) {
	            document.getElementById('<%=txtHolDate.ClientID%>').focus();
                 alert('Please Input Holiday Date.');
	        }
	        else if ((ddlHolType.selectedIndex == 0)) {
	            document.getElementById('<%=ddlHolType.ClientID%>').focus();
                      alert('Please Select Holiday Type.');
                  }
	       
	        else
	            return confirm('Are you sure you want to save information?');
                return false;
     	  
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
            width: 715px;
            margin: 0 auto;
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
     

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtHolDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtHolDate.ClientID %>").datepicker();

            });

        });

          </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
             <thead>
                <tr>
                    <th colspan="3">
                  National Holiday Maintenance
                    </th>
                </tr>
              
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblHolDate" runat="server" Text="Holiday Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtHolDate" runat="server" CssClass="cls text" Width="131px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" 
                        TabIndex="1" onkeypress="return IsNumberKey(event)" AutoPostBack="True" OnTextChanged="txtHolDate_TextChanged" ></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblDayName" runat="server" Text="" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="HolidayType :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlHolType" runat="server" Height="25px" 
                        Width="316px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" >
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNote" runat="server" Text="Holiday Note:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHolNote" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button blue" 
                        OnClientClick="return ValidationBeforeSave()"  Height="28px" OnClick="BtnDelete_Click" />
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeSave()" onclick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green" 
                         onclick="BtnView_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"/>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
 AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField HeaderText="Date" DataField="HolDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ItemStyle-Width="100px" />   
                <asp:BoundField HeaderText="Type" DataField="HolType" HeaderStyle-Width="65px" ItemStyle-Width="65px"/>
                <asp:BoundField HeaderText="Description" DataField="HolTypeDesc" HeaderStyle-Width="155px" ItemStyle-Width="155px" />    
                <asp:BoundField HeaderText="Note" DataField="HolNote" HeaderStyle-Width="260px" ItemStyle-Width="260px" /> 
                <asp:BoundField HeaderText="Day" DataField="HolDayName" HeaderStyle-Width="90px" ItemStyle-Width="90px" />      
              </Columns>
          
        </asp:GridView>
     </div>

<asp:Label ID="lblHolTypeDesc" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>
