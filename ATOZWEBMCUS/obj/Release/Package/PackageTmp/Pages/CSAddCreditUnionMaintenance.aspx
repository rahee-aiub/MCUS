<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="CSAddCreditUnionMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAddCreditUnionMaintenance"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave()
        {
            var txtCreUName = document.getElementById('<%=txtCreUName.ClientID%>').value;
            
            var txtGLCashCode = document.getElementById('<%=txtGLCashCode.ClientID%>').value;
            var txtCuOpenDate = document.getElementById('<%=txtCuOpenDate.ClientID%>').value;

            
            if (txtCreUName == '' || txtCreUName.length == 0)
                alert('Please Input Credit Union Name');
            else
                if (txtCuOpenDate == '' || txtCuOpenDate.length == 0)
                    alert('Please Input Open Date');
            else
            if (txtGLCashCode == '' || txtGLCashCode.length == 0)
                alert('Please Input GL Cash Code');
            else
                return confirm('Are you sure you want to save information?');
            return false;
        }

        function ValidationBeforeSubmit() {
            return confirm('Do you want to Submit Data?');
        }

        function ValidationBeforeReEdit() {
            return confirm('Do you want to Re-Edit Data?');
        }
    </script>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />--%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtCuOpenDate.ClientID %>").dynDateTime({
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
             $("#<%= txtCuOpenDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtCuOpenDate.ClientID %>").datepicker();

            });

        });
            </script>




    <style type="text/css">
        .auto-style1 {
            width: 784px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div align="center">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Add- Credit Union Maintenance
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblCreUName" runat="server" Text="Credit Union Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCreUName" runat="server" CssClass="cls text" Width="430px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ToolTip="Enter Name" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Last Application No.:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                        
                    <asp:Label ID="lblLastSRL" runat="server" Text="" Font-Size="Large" Visible="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuOpenDate" runat="server" Text="Open Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuOpenDate" runat="server" CssClass="cls text" Width="115px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" img src="../Images/calender.png"
                        TabIndex="2" AutoPostBack="True" OnTextChanged="txtCuOpenDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="lblCuMemType" runat="server" Text="Depositor Mode:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlCuMemberFlag" runat="server" Height="25px" Width="200px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" CssClass="cls text" TabIndex="2">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Single Member</asp:ListItem>
                        <asp:ListItem Value="2">Multiple Member</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <%--   <tr>
                <td>
                    <asp:Label ID="lblCuMemStatus" runat="server" Text="Member Status:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCuMemberType" runat="server" Height="25px" Width="200px"
                        Font-Size="Large" CssClass="cls text" TabIndex="3">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Affiliate Member</asp:ListItem>
                        <asp:ListItem Value="2">Associate Member</asp:ListItem>
                        <asp:ListItem Value="3">Regular Member</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            --%>
            <tr>
                <td>
                    <asp:Label ID="lblCuCertNo" runat="server" Text="Certificate No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuCertificateNo" runat="server" CssClass="cls text" Width="115px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL1" runat="server" Text="Address Line1:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuAddressL1" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL2" runat="server" Text="Address Line2:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuAddressL2" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL3" runat="server" Text="Address Line3:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuAddressL3" runat="server" CssClass="cls text" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuDivision" runat="server" Text="Division:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlDivision" runat="server" Height="25px" Width="153px" AutoPostBack="true" BorderColor="#1293D1" BorderStyle="Ridge"
                        CssClass="cls text" Font-Size="Large"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" TabIndex="7">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuDistrict" runat="server" Text="District:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="153px" AutoPostBack="true"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" TabIndex="8">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuUpzila" runat="server" Text="Upzila:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlUpzila" runat="server" Height="25px" Width="153px" AutoPostBack="true"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                         TabIndex="9" OnSelectedIndexChanged="ddlUpzila_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuThana" runat="server" Text="Thana:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlThana" runat="server" Height="25px" Width="153px" AutoPostBack="true"
                        CssClass="cls text" Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlThana_SelectedIndexChanged" TabIndex="10">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuTelNo" runat="server" Text="Telephone No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuTelNo" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="11"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuMobNo" runat="server" Text="Mobile No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuMobileNo" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="12"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuFax" runat="server" Text="Fax:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuFax" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="13"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuEmail" runat="server" Text="E-mail:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCuEmail" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCashCode" runat="server" Text="GL Cash Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    
                   
                    <asp:TextBox ID="txtGLCashCode" runat="server" CssClass="cls text" Width="136px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="15" AutoPostBack="True" OnTextChanged="txtGLCashCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlGLCashCode" runat="server" Height="25px" Width="400px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" OnSelectedIndexChanged="ddlGLCashCode_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                          
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">
                    <asp:Button ID="BtnCreUnionSubmit" runat="server" Text="Input" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                        OnClick="BtnCreUnionSubmit_Click" OnClientClick="return ValidationBeforeSave()" />&nbsp;
                    <asp:Button ID="BtnCreUnionUpdate" runat="server" Text="Update" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                         OnClientClick="return ValidationBeforeSave()" OnClick="BtnCreUnionUpdate_Click" />&nbsp;
                    
                    <asp:Button ID="BtnCreUniontExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red"
                        OnClick="BtnCreUniontExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblCUNoMsg" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCUTypeMsg" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblNewSRL" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblProcFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="ProcDate" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="PrevOpenDate" runat="server" Text="" Visible="false"></asp:Label>

    </div>

    <div id="DivGridViewCancle" runat="server" align="center" style="height: 245px; overflow: auto; width: 100%;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        <p align="center" style="color: blue">
                            Input - Spooler
                        </p>
                        <asp:GridView ID="gvCUInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="757px">
                            <Columns>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnReEdit" runat="server" Text="ReEdit"  Width="68px" CssClass="button green" OnClientClick="return ValidationBeforeReEdit()" OnClick="BtnReEdit_Click" />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnInput" runat="server" Text="Submit" OnClientClick="return ValidationBeforeSubmit()" Width="68px" CssClass="button green" OnClick="BtnInput_Click1" />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" Width="60px" CssClass="button black size-100" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Application">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcuno" runat="server" Text='<%# Eval("CuNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CU Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcutype" runat="server" Text='<%# Eval("CuType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CU No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcno" runat="server" Text='<%# Eval("CuNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="CuTypeName" HeaderText="Credit Union Type" />
                                <asp:BoundField DataField="CuName" HeaderText="Credit Union Name" />
                                <asp:BoundField DataField="CuOpDt" HeaderText="Open Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="CuProcDesc" HeaderText="Mode" />

                            </Columns>
                        </asp:GridView>

                        
                    </th>
                </tr>
            </thead>
        </table>
    </div>
     <%--</ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlGLCashCode" />
                             <asp:AsyncPostBackTrigger ControlID="ddlDivision" />
                             <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
                             <asp:AsyncPostBackTrigger ControlID="ddlUpzila" />
                             <asp:AsyncPostBackTrigger ControlID="ddlThana" />
                        </Triggers>  
      </asp:UpdatePanel>--%>
    
    <%--</div>--%>
</asp:Content>
