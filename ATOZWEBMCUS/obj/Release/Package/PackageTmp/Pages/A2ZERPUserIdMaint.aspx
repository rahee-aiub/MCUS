<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZERPUserIdMaint.aspx.cs"
    Inherits="ATOZWEBMCUS.Pages.A2ZERPUserIdMaint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Id Maintenance</title>
    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/validation.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function Userid() {

            var ddlIdLevel = document.getElementById('<%=ddlIdLevel.ClientID%>');
           var txtIdNo = document.getElementById('<%=txtIdNo.ClientID%>').value;
            var txtPerNo = document.getElementById('<%=txtPerNo.ClientID%>');
            var txtGlCashCode = document.getElementById('<%=txtGlCashCode.ClientID%>');




           if (ddlIdLevel.selectedIndex == '' || ddlIdLevel.length == 0)
               alert('Please Select Lavel Name ');

           else if (txtIdNo == '' || txtIdNo.length == 0)
               alert('Please Input Id No.');

           else if (txtPerNo == '' || txtPerNo.length == 0)
               alert('Please Input PER No.');
           else if (txtGlCashCode == '' || txtGlCashCode.length == 0)
               alert('Please Input GL Cash Code');
           else
               return confirm('Are you sure you want save the data');
           return false;
       }
    </script>

    <style type="text/css">
        .style1 {
            height: 31px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
   
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3" style="color: Black" aligne="center">
                        <p align="center">
                            Add User Id</p>
                    </th>
                </tr>
            </thead>
            <tr>
                <th>
                    ID Level
                </th>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlIdLevel" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlIdLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    ID No.
                </th>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtIdNo" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        onkeypress="return IsNumberKey(event)" AutoPostBack="True" OnTextChanged="txtIdNo_TextChanged"
                        MaxLength="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblPerNo" runat="server" Text="PER No."></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtPerNo" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnTextChanged="txtPerNo_TextChanged" ></asp:TextBox>
                    <asp:DropDownList ID="ddlPerNo" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                    AutoPostBack="True" Width="248px" OnSelectedIndexChanged="ddlPerNo_SelectedIndexChanged">
                                </asp:DropDownList>
                    
                    <asp:Label ID="lblIdsName" runat="server" Text="" Visible="false"></asp:Label>
                    
                    
                    
          <%--          <asp:Label ID="lblFatherName" runat="server" Text="Father's Name"></asp:Label>
                    <asp:TextBox ID="txtFatherName" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        Enabled="False"></asp:TextBox>--%>
                </td>
            </tr>
           <%-- <tr>
                <th>
                    <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtLocation" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtDepartMent" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblSection" runat="server" Text="Section"></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtSection" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblDesignation" runat="server" Text="Designation"></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtDesignation" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>--%>
           <%-- <tr>
                <th>
                    <asp:Label ID="lblPermission" runat="server" Text="Permission"></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPermission" runat="server" BorderColor="#1293D1" BorderStyle="Ridge">
                    </asp:DropDownList>
                </td>
            </tr>--%>

            <tr>
                <th>
                    <asp:Label ID="lblGlCashCode" runat="server" Text="GL Cash Code "></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtGlCashCode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnTextChanged="txtGlCashCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlGLCashCode" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGLCashCode_SelectedIndexChanged" Width="248px">
                                </asp:DropDownList>
                  
                </td>
            </tr>

            <tr>
                <th>
                    <asp:Label ID="lblSODfag" runat="server" Text="SOD Permission "></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="ChkSODflag" runat="server" Font-Size="Large" ForeColor="Red" />

                </td>
            </tr>

            <tr>
                <th>
                    <asp:Label ID="lblVchPrintfag" runat="server" Text="Voucher Print "></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="ChkVPrintflag" runat="server" Font-Size="Large" ForeColor="Red" />

                </td>
            </tr>

            <tr>
                <th>
                    <asp:Label ID="lblCWarehouse" runat="server" Text="Central Warehouse "></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="ChkCWarehouse" runat="server" Font-Size="Large" ForeColor="Red" />

                </td>
            </tr>

            <%--<tr>
                <th>
                    <asp:Label ID="lblManagment" runat="server" Text="Managment Name"></asp:Label>
                </th>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtManagment" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnAdd" runat="server" Text="Add User Id" OnClientClick="return Userid(event,this)"
                        CssClass="button green size-120" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnShow" runat="server" Text="Show Id Info" 
                        CssClass="button blue size-100" onclick="btnShow_Click" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-100"
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
    
    <div id="DivGridView" runat="server" align="center" visible="False">
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
                        <asp:GridView ID="gvUserIdInfromation" runat="server" BorderColor="#1293D1"
                            BorderStyle="Ridge">
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="temp">
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtDp" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtDn" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtloc" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtsec" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
