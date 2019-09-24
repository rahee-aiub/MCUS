<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmpLeaveVerify.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmpLeaveVerify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


   <%-- <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtOpenDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtOpenDate.ClientID %>").datepicker();

            });

        });


    </script>--%>

    <script language="javascript" type="text/javascript">

        function ApproveValidation() {
            return confirm('Do you want to Verify Data?');
        }
        function SelectValidation() {
            return confirm('Do you want to Select Data?');
        }
        function RejectValidation() {
            return confirm('Do you want to Reject Data?');
        }
    </script>

    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div id="DivMainHeader" runat="server" align="center">

        <table class="style1">
            <tr>
                <th colspan="4">
                    <p align="center" style="color: blue">
                        Verify Emloyee's Leave Application
                    </p>
                </th>
            </tr>
        </table>

    </div>

    <div id="DivButton" runat="server" align="center">
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="DivGridViewCancle" runat="server" align="center" style="height: 276px; overflow: auto; width: 100%;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        <p align="center" style="color: blue">
                            Verify - Spooler
                        </p>
                        <asp:GridView ID="gvLeaveInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="757px">
                            <Columns>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnSelect" runat="server" Text="Select" OnClick="BtnSelect_Click" Width="68px" CssClass="button green" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnRejectSelect" runat="server" Text="Reject" OnClick="BtnRejectSelect_Click" Width="68px" CssClass="button red" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                 <asp:TemplateField HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Emp. No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpNo" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Leave Type" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeaveType" runat="server" Text='<%# Eval("EmpleaveName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Start Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("LStartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="End Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("LEndDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Apply Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lblapplyDays" runat="server" Text='<%# Eval("LApplyDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="LBalance" HeaderText="Leave Balance" DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="Right" />

                            </Columns>
                        </asp:GridView>


                    </th>
                </tr>
            </thead>
        </table>
    </div>

    <div id="DivReject" runat="server">
        <table class="style1">
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="lblNote" runat="server" Font-Size="Large" ForeColor="Red"
                        Text=" Reject Note :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRejectNote"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="1095px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>

                <td colspan="6" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnReject" runat="server" Text="Reject" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClientClick="return RejectValidation()" OnClick="BtnReject_Click" />
                    &nbsp;
                    <asp:Button ID="BtnCanReject" runat="server" Text="Cancel" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button blue" OnClick="BtnCanReject_Click" />
                </td>
            </tr>
        </table>

    </div>


    <div id="Div1" runat="server" align="center">
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnApproved" runat="server" Text="Verify" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" ToolTip="Approved Page" CausesValidation="False"
                        CssClass="button red" OnClientClick="return ApproveValidation()" OnClick="BtnApproved_Click" />
                    &nbsp;
                    <asp:Button ID="btnCanApproved" runat="server" Text="Cancel" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button blue" OnClick="BtnCanApproved_Click" />
                </td>
            </tr>
        </table>
    </div>




    <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Verify Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Verify" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>

    <asp:Label ID="lblAccTypeDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccFlag" runat="server" Text="" Visible="false"></asp:Label>

   

    <asp:Label ID="lblNewAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccPeriod" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="hdnApprovedAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNoInstl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAppNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="SingleRec" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblLoanExpiryDt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAppDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblOrgSancAmt" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblchk1Hide" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblchk2Hide" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblchk3Hide" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="MsgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lAppDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlId" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>

    <asp:HiddenField ID="hInstallmentAmt" runat="server" />
    <asp:HiddenField ID="hOpenDate" runat="server" />
</asp:Content>

