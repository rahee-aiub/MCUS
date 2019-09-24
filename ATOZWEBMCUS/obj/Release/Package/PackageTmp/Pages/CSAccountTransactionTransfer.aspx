<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAccountTransactionTransfer.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountTransactionTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--     <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtTranDate.ClientID %>").dynDateTime({
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
            $("#<%= txtTranDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtTranDate.ClientID %>").datepicker();

              });

        });


    </script>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save New Sanction Amount?');
        }

        function ValidationBeforeUpdate() {
            var txtTrnCuNo = document.getElementById('<%=txtTrnCuNo.ClientID%>').value;
            var txtTrnMemNo = document.getElementById('<%=txtTrnMemNo.ClientID%>').value;

            if (txtTrnCuNo == '' || txtTrnCuNo.length == 0)
                alert('Please Input From Credit Union No.');
            else
                if (txtTrnMemNo == '' || txtTrnMemNo.length == 0)
                    alert('Please Input From Depositor No.');

            return confirm('Are you sure you want to Transfer Transaction Balance?');
            return false;

        }

    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Account Transaction Transfer From..............
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Transaction Date:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTranDate" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtTranDate_TextChanged"></asp:TextBox>


                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                             
                      <asp:Label ID="Label3" runat="server" Font-Size="Medium" Text="Vch.No. :" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnSearch_Click" />

                </td>

            </tr>

        </table>

    </div>
    <div align="center">
        <table class="style1">



            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px">
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <Columns>

                        <%-- <asp:BoundField HeaderText="TrnDate" DataField="TrnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" />--%>




                        <asp:TemplateField HeaderText="TrnDate" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="TrnDate" runat="server" Text='<%#Eval("TrnDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CuType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="CuType" runat="server" Text='<%#Eval("CuType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CuNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="CuNo" runat="server" Text='<%#Eval("CuNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DepositorNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="MemNo" runat="server" Text='<%#Eval("MemNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="AccNo" runat="server" Text='<%#Eval("AccNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="180px" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />

                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Amount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("GLAmount"))) %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        

                    </Columns>

                </asp:GridView>
            </div>
        </table>
    </div>
    <br />
    <br />
    <div id="divTrnfer" align="center" runat="server">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Account Transaction Transfer To..............
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblTrnCuNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnCuNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="true" OnTextChanged="txtTrnCuNo_TextChanged"></asp:TextBox>
                    <asp:CheckBox ID="chkOldSearch" runat="server" Font-Size="Medium" ForeColor="Red" Text="Old No. Search" />
                    <asp:Label ID="lblCuName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrnmemno" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtTrnMemNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblMemName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnTrfSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnTrfSearch_Click" />
                </td>
            </tr>
            <%--<tr>
            <td>
                <asp:Label ID="lblTrnAccType" runat="server" Text="Account Type:" Font-Size="Large"
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTrnAccType" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                    Font-Size="Medium" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtTrnAccType_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Account No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                <asp:TextBox ID="txtTrnAccNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                    Font-Size="Medium" AutoPostBack="True" ToolTip="Enter Code" OnTextChanged="txtTrnAccNo_TextChanged"></asp:TextBox>


                <asp:DropDownList ID="ddlTrnAccNo" runat="server" Height="25px" Width="187px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Large" OnSelectedIndexChanged="ddlTrnAccNo_SelectedIndexChanged"></asp:DropDownList>


            </td>
        </tr>--%>
        </table>
        <table class="style1">
            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvTrnferInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px">
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <Columns>

                        <asp:BoundField HeaderText="TrnDate" DataField="TrnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" />

                        <asp:TemplateField HeaderText="CuType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="CuType" runat="server" Text='<%#Eval("CuType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CuNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="CuNo" runat="server" Text='<%#Eval("CuNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DepositorNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="MemNo" runat="server" Text='<%#Eval("MemNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="AccNo" runat="server" AutoPostBack="True" OnTextChanged="AccNo_TextChanged" Text='<%#Eval("AccNo") %>' CssClass="cls text"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="180px" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />

                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Amount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("GLAmount"))) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CuType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="FrmCuType" runat="server" Text='<%#Eval("CuType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CuNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="FrmCuNo" runat="server" Text='<%#Eval("CuNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DepositorNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="FrmMemNo" runat="server" Text='<%#Eval("MemNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--   <asp:TemplateField HeaderText="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="FrmAccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="FrmAccNo" runat="server" Text='<%#Eval("AccNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OrgAccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="OrgAccNo" runat="server" Text='<%#Eval("AccNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="paytype" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Paytype" runat="server" Text='<%#Eval("PayType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Id" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>
        </table>


    </div>

    <div align="center">
        <table>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnTransfer" runat="server" Text="Transfer" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" CssClass="button green" Height="27px" Width="140px"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnTransfer_Click" />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                    <br />
                </td>
            </tr>

        </table>
    </div>
    <asp:TextBox ID="txtHidden" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnferCuType" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnferCuNo" runat="server" Visible="false"></asp:Label>
    <asp:TextBox ID="txtTrnHidden" runat="server" Visible="false"></asp:TextBox>

    <asp:Label ID="lblId1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblId2" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnPrmValue" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnUserId" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlVoucherNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUnPostDataDr" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBalance" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAvailBalance" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnfrMemno" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnfrAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnfrAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnToMemNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnToAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnToAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="OrgAccType" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="OrgAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblLastAccNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblOrgAccNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblPayType" runat="server" Text="" Visible="false"></asp:Label>



</asp:Content>

