<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HKMasterPage.Master" AutoEventWireup="true" CodeBehind="SYSModuleControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.SYSModuleControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 3px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function UIFieldValidation() {

            var ddlUserId = document.getElementById('<%=ddlIdsNo.ClientID%>');

             if (ddlUserId.selectedIndex == 0)
                 alert('Please Select User Id.');

             else
                 return confirm('Are You Sure Want To Add Module?');
             return false;
         }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3" style="color: Black" aligne="center">
                        <p align="center">
                            System Module Control
                        </p>
                    </th>
                </tr>
            </thead>
            <tr>
                <th>ID No.
                </th>
                <td class="auto-style2">:
                </td>

                <td>
                    <asp:TextBox ID="txtIdsNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large" AutoPostBack="true" ToolTip="Enter Ids" OnTextChanged="txtIdsNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlIdsNo" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlIdsNo_SelectedIndexChanged"> 
                      
                   
                    </asp:DropDownList>
                </td>


            </tr>
        </table>
        <table class="style1">
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
    <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Save" OnClientClick="return UIFieldValidation()"
                        CssClass="button green size-120" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-120" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
