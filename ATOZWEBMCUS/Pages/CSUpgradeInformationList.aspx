<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSUpgradeInformationList.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSUpgradeInformationList" %>

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
                    <th colspan="3">Upgrade Information List
                    </th>
                </tr>
            </thead>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllOldAccNo" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllOldAccNo_CheckedChanged" />


                <%--</td>
                <td style="background-color: #fce7f9">--%>

                     &nbsp;&nbsp;
                    <asp:Label ID="lblOldAccNo" runat="server" Text=" Old Account No." Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtOldAccType" runat="server" CssClass="cls text" Width="50px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" ToolTip="Enter Code" ></asp:TextBox>
                     &nbsp;&nbsp;
                    <asp:TextBox ID="txtOldAccNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" ToolTip="Enter Code" ></asp:TextBox>
                    

                </td>

            </tr>

            
            <tr>
                <td style="background-color: #fce7f9">
                   <asp:CheckBox ID="chkAllCashCode" runat="server" ForeColor="Red" Font-Size="Large" Text="   All"  AutoPostBack="True" OnCheckedChanged="chkAllCashCode_CheckedChanged" />
                <%--</td>

                <td style="background-color: #fce7f9">--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblCashCode" runat="server" Font-Size="Large" ForeColor="Red" Text="Cash Code "></asp:Label>
                </td>
               
                <td>
                    <asp:DropDownList ID="ddlCashCode" runat="server" Height="31px" Width="418px"
                        Font-Size="Large" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
           
                </td>

            </tr>

             <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllOldCuNo" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllOldCuNo_CheckedChanged" />


               <%-- </td>
                <td style="background-color: #fce7f9">--%>

                     &nbsp;&nbsp;
                    <asp:Label ID="lblOldCuNo" runat="server" Text=" Old Credit Union No." Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtOldCuNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" ToolTip="Enter Code"></asp:TextBox>
                    

                </td>

            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllOldMemNo" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllOldMemNo_CheckedChanged" />


                <%--</td>
                <td style="background-color: #fce7f9">--%>

                     &nbsp;&nbsp;
                    <asp:Label ID="lblOldMemNo" runat="server" Text=" Old Depositor No." Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtOldMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" ToolTip="Enter Code" ></asp:TextBox>
                    

                </td>

            </tr>

            
           
            <tr>
                <td></td>
                <td>
                    
                       <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="White" Height="27px" Width="86px"
                           Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnPrint_Click" />&nbsp;
                      
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
    
    <br />
    
    
    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblPdate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="txtToDaysDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeName" runat="server" Text="" Visible="false"></asp:Label>
    
    

</asp:Content>

