<%@ Page Language="C#" MasterPageFile="~/MasterPages/HKMasterPage.Master" AutoEventWireup="true"
    CodeBehind="A2ZERPUserAccessibility.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZERPUserAccessibility"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
function ValidationBeforeSave(){ 
           
    	  var ddlUserId=document.getElementById('<%=ddlIdsNo.ClientID%>');
    	  var ddlModule=document.getElementById('<%=ddlModule.ClientID%>');
	     
	      if (ddlUserId.selectedIndex ==0 || ddlUserId.length=='' ) 
	        alert('Please Select User Id.');
	        
	    else if (ddlModule.selectedIndex ==0 || ddlModule.length=='' )
	     
    	          alert('Please Select Module.');
    	   else
            return confirm('Are you sure you want update data?');
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
                            General User Accessibility</p>
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
            <tr>
                <th>
                    Module
                </th>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlModule" Width="316px" Height="25px" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        AutoPostBack="True" Font-Size="Large" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivGridView" runat="server" align="center" visible="False" style="height: 320px;
        overflow: auto;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        Module Information
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="gvModule" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="DivGridView2" runat="server" align="center" visible="False">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        User Id Information
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="gvUserIdInfromation" runat="server" BorderColor="#1293D1" BorderStyle="Ridge">
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Save" OnClientClick="return ValidationBeforeSave(event,this)"
                        CssClass="button green size-120" OnClick="btnAdd_Click" />
                    &nbsp;<asp:Button ID="btnMark" runat="server" Text="All Mark" CssClass="button blue size-100"
                        OnClick="btnMark_Click" />
                    &nbsp;<asp:Button ID="btnUnMark" runat="server" Text="All Un-Mark" CssClass="button green size-100"
                        OnClick="btnUnMark_Click" />
                    &nbsp;<asp:Button ID="btnBooth" runat="server" Text="All Booths" CssClass="button blue size-100" OnClick="btnBooth_Click"
                       />
                    &nbsp;<asp:Button ID="btnCS" runat="server" Text="All CS" CssClass="button green size-100" OnClick="btnCS_Click"
                       />
                    &nbsp;<asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-100"
                        OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DivMessage" runat="server" align="center">
        <table class="style1">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnHideMessageDiv" runat="server" Text="OK" CssClass="button blue size-100"
                        OnClick="btnHideMessageDiv_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
