<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSGetAccountNoTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSGetAccountNoTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 313px;
            width: 1350px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 490px;*/
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .auto-style1 {
            width: 225px;
        }

        .auto-style2 {
            height: 27px;
        }

        .auto-style3 {
            width: 225px;
            height: 27px;
        }
        .auto-style4 {
            width: 466px;
        }
    </style>




    <script language="javascript" type="text/javascript">
        function ValidationBeforeAdd() {


            var txtCreditUNo = document.getElementById('<%=txtCreditUNo.ClientID%>').value;
            var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;



            if (txtCreditUNo == '' || txtCreditUNo.length == 0)
                alert('Please Input Credit Union No.');
            else
                if (txtMemNo == '' || txtMemNo.length == 0)
                    alert('Please Input Depositor No.');

                else
                    return;
            return false;



        }



    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    </br>
    </br>
    <div align="Left">

        <table>
            <tr>
                <td>

        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">
                        <asp:Label ID="lblTransFunction" runat="server" Text="Search Account Information"></asp:Label>
                    </th>
                </tr>

            </thead>
            <tr>
                <td></td>
                <td>
                    <asp:TextBox ID="txtSearchCuName" runat="server" CssClass="cls text" Width="469px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" ToolTip="Enter Name"
                        placeholder="                    Search Credit Union Name"
                        OnTextChanged="txtSearchCuName_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:TextBox ID="txtSearchMemName" runat="server" CssClass="cls text" Width="465px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" ToolTip="Enter Name"
                        placeholder="                    Search Depositor Name"
                        AutoPostBack="true" OnTextChanged="txtSearchMemName_TextChanged"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" ToolTip="Enter Code" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                    <asp:Label ID="lblCuName" runat="server" Width="300px" Height="25px" Text=""></asp:Label>

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                
                                <asp:Label ID="lblOldCuNo" runat="server" Font-Size="Medium" Text="Old CU No. :" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtOldCuNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" onkeydown="return (event.keyCode !=13);"></asp:TextBox>


                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" ToolTip="Enter Code" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                    <asp:Label ID="lblMemName" runat="server" Width="300px" Height="25px" Text=""></asp:Label>


                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                                 
                                
                                <asp:Label ID="lblOldMemNo" runat="server" Font-Size="Medium" Text="Old Depositor No. :" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtOldMemNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" onkeydown="return (event.keyCode !=13);"></asp:TextBox>



                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;
                <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" ToolTip="Search Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnSearch_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
        </td>
        
        <td>
            

        <table class="style1">
                        <thead>
                            <tr>
                                <th colspan="3">Account Summary Informations
                                </th>
                            </tr>

                        </thead>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec1" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData1" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                            

                                <asp:Label ID="lblBalRec" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:Label ID="lblBalData" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblRec2" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData2" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                              
                             
                                <asp:Label ID="lblUnPostCr" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:Label ID="lblUnPostDataCr" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec3" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData3" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblUnPostDr" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:Label ID="lblUnPostDataDr" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec4" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData4" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec11" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>

                            <td class="auto-style4">
                                <asp:Label ID="lblData11" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="lblRec5" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style3">
                                <asp:Label ID="lblData5" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec9" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>

                            <td class="auto-style4">
                                <asp:Label ID="lblData9" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec6" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData6" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec10" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>
                            <td class="auto-style4">
                                <asp:Label ID="lblData10" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec7" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData7" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec8" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:Label ID="lblData8" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>

                    </table>

                  
            </tr>
        </table>
            








    </div>
    </br>
    </br>
   


        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound" OnSelectedIndexChanged="gvDetailInfo_SelectedIndexChanged">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:TemplateField HeaderText="Acc Type" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acc Title" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="TrCodeDesc" runat="server" Text='<%#Eval("TrnCodeDesc") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="New A/c" DataField="AccNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />

                <asp:TemplateField HeaderText="Old A/c" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccNo" runat="server" Text='<%#Eval("AccOldNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>



                <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
                    <ItemTemplate>
                        <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>


    <asp:GridView ID="gvSearchCUInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSearchCUInfo_RowDataBound" OnSelectedIndexChanged="gvSearchCUInfo_SelectedIndexChanged">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <Columns>


            <asp:BoundField HeaderText="Credit Union Name" DataField="CuName" HeaderStyle-Width="460px" ItemStyle-Width="460px" ItemStyle-HorizontalAlign="left" />
            <%-- <asp:BoundField HeaderText="CU No" DataField="CuType" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" Visible="false" />
                              <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right"  Visible="false" />--%>

            <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old CU No" DataField="CuOldCuNo" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old CU No" DataField="CuOld1CuNo" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old CU No" DataField="CuOld2CuNo" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />

            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

    <asp:GridView ID="gvSearchMEMInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#00ffff"
        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSearchMEMInfo_RowDataBound" OnSelectedIndexChanged="gvSearchMEMInfo_SelectedIndexChanged">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <Columns>


            <asp:BoundField HeaderText="Depositor Name" DataField="MemName" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="left" />
            <%-- <asp:BoundField HeaderText="CU No" DataField="CuType" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" Visible="false" />
                              <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right"  Visible="false" />--%>

            <asp:BoundField HeaderText="Depositor No." DataField="MemNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old Depositor" DataField="MemOldMemNo" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old Depositor" DataField="MemOld1MemNo" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old Depositor" DataField="MemOld2MemNo" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />


            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

    <asp:GridView ID="gvSearchMEMBERInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSearchMEMBERInfo_RowDataBound" OnSelectedIndexChanged="gvSearchMEMBERInfo_SelectedIndexChanged">
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <Columns>


            <asp:BoundField HeaderText="Depositor Name" DataField="MemName" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="left" />
            <%--<asp:BoundField HeaderText="CU No" DataField="CuType" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" Visible="false" />
                            <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right"  Visible="false" />--%>

            <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="70px" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old CU" DataField="MemOldCuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old CU" DataField="MemOld1CuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old CU" DataField="MemOld2CuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old Dep" DataField="MemOldMemNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old Dep" DataField="MemOld1MemNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField HeaderText="Old Dep" DataField="MemOld2MemNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />


            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    </div>


    <div id="DivMainTrans" runat="server" align="left" style="background-color: #E0FFFF">
        <table>
            <tr>
                <td>
                    <h3>Trans.Type</h3>
                </td>
                <td>
                    <h3>&nbsp;&nbsp;&nbsp;&nbsp;Description</h3>
                </td>
                <td>
                    <h3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Amount</h3>
                </td>

            </tr>
            <tr>
                <td>

                    <asp:TextBox ID="txtTrnType1" runat="server" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtTrnType1_TextChanged" Width="100px" Height="25px" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;

                                            <asp:TextBox ID="txtPayDesc1" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>

                    <%--<asp:Label ID="lblPayDesc1" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         
                        <asp:TextBox ID="txtAmount1" runat="server" Style="text-align: right" Font-Size="Medium" TabIndex="9" CssClass="cls text" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" OnTextChanged="txtAmount1_TextChanged" AutoPostBack="True" onFocus="javascript:this.select();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtTrnType2" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnTextChanged="txtTrnType2_TextChanged"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPayDesc2" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                    <%-- <asp:Label ID="lblPayDesc2" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                </td>

                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtAmount2" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" TabIndex="10" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnTextChanged="txtAmount2_TextChanged" onFocus="javascript:this.select();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtTrnType3" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" AutoPostBack="True" OnTextChanged="txtTrnType3_TextChanged"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="txtPayDesc3" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                    <%--<asp:Label ID="lblPayDesc3" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtAmount3" runat="server" Style="text-align: right" Font-Size="Medium" TabIndex="11" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtAmount3_TextChanged" onFocus="javascript:this.select();"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:TextBox ID="txtTrnType4" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" AutoPostBack="True" OnTextChanged="txtTrnType4_TextChanged"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPayDesc4" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                    <%-- <asp:Label ID="lblPayDesc4" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtAmount4" runat="server" Style="text-align: right" Font-Size="Medium" TabIndex="12" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtAmount4_TextChanged" onFocus="javascript:this.select();"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnAdd" runat="server" TabIndex="13" Text="Add" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnAdd_Click" />&nbsp;
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="83px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnCancel_Click" />&nbsp;
                                            <asp:Button ID="BtnViewImage" runat="server" Text="View Image" Font-Bold="True" Font-Size="Medium"
                                                ForeColor="White" CssClass="button green" OnClick="BtnViewImage_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lbltotAmt" runat="server" Text="Net Amount :" Font-Size="large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Label ID="txttotAmt" runat="server" Font-Size="large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>


                </td>
            </tr>

        </table>
    </div>





    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNewMemberNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTranDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCtrlFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCTypeName" runat="server" Text="" Visible="false"></asp:Label>



</asp:Content>

