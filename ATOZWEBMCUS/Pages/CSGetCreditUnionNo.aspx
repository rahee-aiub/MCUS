﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSGetCreditUnionNo.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSGetCreditUnionNo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 313px;
            width: 1000px;
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
    <div align ="center">
           <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="3">
                                    <asp:Label ID="lblTransFunction" runat="server" Text="Search Credit Union Information"></asp:Label>
                                </th>
                            </tr>

                        </thead>
               <tr>
                   <td></td>
                   <td>
                       <asp:TextBox ID="txtSearchCuName" runat="server" CssClass="cls text" Width="600px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Name" 
                           placeholder="                    Search Credit Union Name" 
                           OnTextChanged="txtSearchCuName_TextChanged" AutoPostBack="true" ></asp:TextBox>
                   </td>
                  
               </tr>

               <tr>
                   <td></td>
                   <td>
                       <asp:TextBox ID="txtSearchMemName" runat="server" CssClass="cls text" Width="600px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Name" 
                           placeholder="                    Search Depositor Name" 
                            AutoPostBack="true" OnTextChanged="txtSearchMemName_TextChanged" ></asp:TextBox>
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
                                <asp:Label ID="lblCuName" runat="server" Width="400px" Height="25px" Text=""></asp:Label>
                                
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                
                             
                                
                                
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
                                    Font-Size="Medium" ToolTip="Enter Code" onkeydown="return (event.keyCode !=13);" ></asp:TextBox>
                                <asp:Label ID="lblMemName" runat="server" Width="400px" Height="25px" Text=""></asp:Label>
                                

                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                                 
                                
                                <asp:Label ID="lblOldMemNo" runat="server" Font-Size="Medium" Text="Old Depositor No. :" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtOldMemNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" onkeydown="return (event.keyCode !=13);" ></asp:TextBox>

                        

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
                        CssClass="button red" OnClick="BtnExit_Click"/>
                <br />
            </td>
        </tr>
    </table>
                 
                    </div>
    </br>
    </br>
    <div align="left" class="grid_scroll">

          

    <asp:GridView ID="gvSearchCUInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
                        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSearchCUInfo_RowDataBound" OnSelectedIndexChanged="gvSearchCUInfo_SelectedIndexChanged" >
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>
                            

                            <asp:BoundField HeaderText="Credit Union Name" DataField="CuName" HeaderStyle-Width="460px" ItemStyle-Width="460px" ItemStyle-HorizontalAlign="left" />
                             <%-- <asp:BoundField HeaderText="CU No" DataField="CuType" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" Visible="false" />
                              <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right"  Visible="false" />--%>

                              <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px"  HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                              <asp:BoundField HeaderText="Old CU No" DataField="CuOldCuNo" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old CU No" DataField="CuOld1CuNo" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old CU No" DataField="CuOld2CuNo" HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                       
                            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Select" ID="LinkButton1" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

         <asp:GridView ID="gvSearchMEMInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#00ffff"
                        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSearchMEMInfo_RowDataBound" OnSelectedIndexChanged="gvSearchMEMInfo_SelectedIndexChanged" >
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>
                            

                            <asp:BoundField HeaderText="Depositor Name" DataField="MemName" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="left" />
                             <%-- <asp:BoundField HeaderText="CU No" DataField="CuType" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" Visible="false" />
                              <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right"  Visible="false" />--%>

                              <asp:BoundField HeaderText="Depositor No." DataField="MemNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old Depositor" DataField="MemOldMemNo" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old Depositor" DataField="MemOld1MemNo" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old Depositor" DataField="MemOld2MemNo" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             
                       
                            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Select" ID="LinkButton2" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

         <asp:GridView ID="gvSearchMEMBERInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="#ffcc00"
                        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvSearchMEMBERInfo_RowDataBound" OnSelectedIndexChanged="gvSearchMEMBERInfo_SelectedIndexChanged" >
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>
                            

                            <asp:BoundField HeaderText="Depositor Name" DataField="MemName" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="left" />
                            <%--<asp:BoundField HeaderText="CU No" DataField="CuType" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" Visible="false" />
                            <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right"  Visible="false" />--%>

                             <asp:BoundField HeaderText="Depositor" DataField="MemNo" HeaderStyle-Width="70px" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="CU No" DataField="CuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old CU" DataField="MemOldCuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old CU" DataField="MemOld1CuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old CU" DataField="MemOld2CuNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old Dep" DataField="MemOldMemNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old Dep" DataField="MemOld1MemNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             <asp:BoundField HeaderText="Old Dep" DataField="MemOld2MemNo" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign ="center" ItemStyle-HorizontalAlign="center" />
                             
                       
                            <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
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
    <asp:Label ID="lblCtrlTrnFlag" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblNflag" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblIncraseLoan" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCTypeName" runat="server" Text="" Visible="false"></asp:Label>



</asp:Content>

