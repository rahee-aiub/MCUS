<%@ Page Language="C#" MasterPageFile="~/MasterPages/HKMasterPage.Master" AutoEventWireup="true"
    CodeBehind="A2ZERPUserIdReset.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZERPUserIdReset"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
function ValidationBeforeReset(){ 
           
    	  var ddlUserId=document.getElementById('<%=ddlIdsNo.ClientID%>');
    	 
	     
	      if (ddlUserId.selectedIndex ==0 || ddlUserId.length=='' ) 
	        alert('Please Select User Id.');
	       
    	   else
            return confirm('Are you sure you want Reset User Passward?');
         return false;
        }
	    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3" style="color: Black" align="center">
                        <p align="center">
                            Reset User Passward</p>
                    </th>
                </tr>
            </thead>
            <tr>
                <th>
                    ID No.
                </th>
                <td>
                    :
                </td>

                <td>
                    <asp:TextBox ID="txtIdsNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large" AutoPostBack="true" ToolTip="Enter Ids" OnTextChanged="txtIdsNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlIdsNo" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlIdsNo_SelectedIndexChanged" > 
                                      
                    </asp:DropDownList>
                </td>

            </tr>
            
        </table>
    </div>
    
    
    <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnReset" runat="server" Text="Reset Passward" OnClientClick="return ValidationBeforeReset(event,this)"
                        CssClass="button green size-120" OnClick="btnReset_Click"/>

                    &nbsp;<asp:Button ID="btnBooth" runat="server" Text="All Booths" CssClass="button blue size-100" OnClick="btnBooth_Click"
                       />
                    &nbsp;<asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-100"
                        OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    
</asp:Content>
