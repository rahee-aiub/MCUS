﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSODLedgerBalanceReportList.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSODLedgerBalanceReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 34px;
        }

        .auto-style5 {
            height: 34px;
            width: 557px;
        }

        .auto-style8 {
            width: 60px;
        }

        .auto-style11 {
            width: 557px;
        }

        </style>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDate.ClientID %>").dynDateTime({
                showsTime: false,
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


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtDate.ClientID %>").datepicker();

            });

        });

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />
    <br />

    <%--<div id = "DivSelectOption" align="center">
        <table class="style1">
                <thead>
                <tr>
                    <th colspan="5" class="auto-style1">
                       Ledger Balance Reports - Select Option
                    </th>
                </tr>

            </thead>
            <tr>

                <td>

                    <asp:RadioButton ID="rbtLedgerBalance" runat="server" Text="  Upto Ledger Balance Report " Checked="True" GroupName="GRN" />
                &nbsp;</td>
               
               

            </tr>
             <tr>

                <td>

                    
                </td>

            </tr>


               

    
            </table>
        </div>


    
     <br />--%>


    <div id="DivParameter" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="7">OD Due Interest Ledger Balance
                    </th>
                </tr>

            </thead>

            <tr>
                <td colspan="7" style="background-color: #fce7f9" class="auto-style8">
                    <%-- <td colspan ="7">--%>

                </td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9" class="auto-style8"></td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblDate" runat="server" ForeColor="Red" Text="Balance Date  "></asp:Label>
                </td>
                <%--<td class="auto-style11">--%>
                <td style="background-color: #fce7f9" class="auto-style8">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9" class="auto-style8"></td>
                <td style="background-color: #fce7f9">

                    <asp:Label ID="lblAccType" runat="server" ForeColor="Red" Text="A/C Type   "></asp:Label>

                </td>

                <td class="auto-style5">
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlAcType" runat="server" Height="31px" Width="441px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAcType_SelectedIndexChanged" Style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>


                <%--<td class="auto-style2">
                 <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
                 <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
                 <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
                 <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>
               </td>--%>
            </tr>


            

            <tr>



                <td style="background-color: #fce7f9" class="auto-style8">

                    <asp:CheckBox ID="ChkAllCrUnion" runat="server" ForeColor="Red" Text=" All" OnCheckedChanged="ChkAllCrUnion_CheckedChanged" AutoPostBack="True" />

                </td>

                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblCu" runat="server" ForeColor="Red" Text="Credit Union "></asp:Label>



                </td>
                <%--<td>
                   :

                </td>--%>
                <td class="auto-style11">
                    <asp:TextBox ID="txtCrUnion" runat="server" CssClass="cls text" Width="114px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCrUnion_TextChanged"></asp:TextBox>
                    
                    &nbsp;<asp:Label ID="lblCuName" runat="server"  Width="300px" Height="25px"
                        Font-Size="Large"></asp:Label>
                </td>

            </tr>


            <tr>



                <td style="background-color: #fce7f9" class="auto-style8">

                    <asp:CheckBox ID="ChkAllMemNo" runat="server" ForeColor="Red" Text=" All" OnCheckedChanged="ChkAllMemNo_CheckedChanged" AutoPostBack="True" />


                    


                </td>

                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblMemNo" runat="server" ForeColor="Red" Text="Depositor No. "></asp:Label>



                </td>
                <%--<td>
                   :

                </td>--%>
                <td class="auto-style11">
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="114px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                    
                    &nbsp;<asp:Label ID="lblMemName" runat="server"  Width="300px" Height="25px"
                        Font-Size="Large"></asp:Label>

                    
                </td>

            </tr>


            <tr>



                <td style="background-color: #fce7f9" class="auto-style8">

                    <asp:CheckBox ID="ChkOnlyBalance" runat="server" ForeColor="Red" Text=" Only Balance" />


                    


                </td>

                
               

            </tr>
           
            <tr>

                <%--<td colspan ="2" class="auto-style10">--%>


                <td colspan="5" style="background-color: #fce7f9" class="auto-style8">

                    <%--<td colspan ="5" class="auto-style3">--%>
                    <%--<td colspan="5" style="background-color: #fce7f9" class="auto-style8">--%>
                    <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="216px" CssClass="button green" OnClick="BtnView_Click" />&nbsp;

                   
                     &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />




                    <br />
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>


        </table>
    </div>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAmtSign" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccAccessFlag" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
