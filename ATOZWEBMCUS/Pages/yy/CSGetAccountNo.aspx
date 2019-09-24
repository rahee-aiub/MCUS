<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSGetAccountNo.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSGetAccountNo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 313px;
            width: 725px;
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
                                    <asp:Label ID="lblTransFunction" runat="server" Text="Search Account Information"></asp:Label>
                                </th>
                            </tr>

                        </thead>
               <tr>
                   <td></td>
                   <td>
                       <asp:TextBox ID="TextBox1" runat="server" CssClass="cls text" Width="600px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code"></asp:TextBox>
                   </td>
                  
               </tr>
                            <tr>
                            <td>
                                <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code"></asp:TextBox>
                                <asp:Label ID="lblCuName" runat="server" Width="400px" Height="25px" Text=""></asp:Label>
                                
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                
                             
                                
                                
                                <asp:Label ID="lblOldCuNo" runat="server" Font-Size="Medium" Text="Old CU No. :" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtOldCuNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                              

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" ></asp:TextBox>
                                <asp:Label ID="lblMemName" runat="server" Width="400px" Height="25px" Text=""></asp:Label>
                                

                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                                 
                                
                                <asp:Label ID="lblOldMemNo" runat="server" Font-Size="Medium" Text="Old Depositor No. :" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtOldMemNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"  ></asp:TextBox>

                        

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

    <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound" OnSelectedIndexChanged="gvDetailInfo_SelectedIndexChanged" >
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>
                            

                            <asp:TemplateField HeaderText="Acc Type" HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acc Title" HeaderStyle-Width="300px" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                     <asp:Label ID="TrCodeDesc" runat="server" Text='<%#Eval("TrnCodeDesc") %>'></asp:Label>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Old A/c" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="AccNo" runat="server" Text='<%#Eval("AccOldNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                                  
                            <asp:BoundField HeaderText="New A/c" DataField="AccNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" />
                       
                            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-Width="60px">
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



</asp:Content>

