<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREnquireEmpMasterFile.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREnquireEmpMasterFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 450px;
            margin: 0 auto;
            width: 950px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

       .FixedHeader3 {
            position: absolute;
            font-weight: bold;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
       <table style="border: groove">
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Enquire Employee Master File Information " Font-Size="X-Large" ForeColor="#336600" BorderColor="#FF99CC" Font-Bold="True" Font-Italic="True" Font-Underline="True"></asp:Label>

                </td>
            </tr>
                      </table>
        <br />
        <br />
        <br />
        <table>
           <tr>
               <td>
                    <asp:TextBox ID="txtEmpID" runat="server"  Width="104px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" 
                                    Font-Size="Medium" onblur="if(this.value=='')" value="Enter Emp Id" onfocus="if(this.value==this.defaultValue)this.value='';"></asp:TextBox>
               </td>
               <td>
                   <asp:TextBox ID="txtEmpName" runat="server" Width="390px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Medium" onblur="if(this.value=='')" value="Enter Emp Name" onfocus="if(this.value==this.defaultValue)this.value='';"></asp:TextBox>
               </td>
               <td>
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                     ForeColor="White"  CssClass="button green" 
                     Height="30px" Width="83px" OnClick="BtnSearch_Click"/>&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="30px" Width="73px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"/>

               </td>
           </tr>

       </table>
    </div>
   <div align="center" class="grid_scroll">
                    <asp:GridView ID="gvDetails" runat="server" HeaderStyle-CssClass="FixedHeader3" HeaderStyle-BackColor="YellowGreen"
                        AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" EnableModelValidation="True"  OnRowDataBound="gvDetails_RowDataBound" Width="950px" OnSelectedIndexChanged="gvDetails_SelectedIndexChanged">
                        <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader3" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" />
                        <Columns>
                            <asp:TemplateField Visible="false" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EmpCode" >

                                 <ItemTemplate>
                                    <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label>
                                </ItemTemplate>
                                  <HeaderStyle Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="EmpName" HeaderText="Emp.Name">
                                <HeaderStyle Width="290px" HorizontalAlign="Left"/>
                                <ItemStyle HorizontalAlign="Left" Width="240px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpDesigDesc" HeaderText="Designation">
                                <HeaderStyle Width="230px" HorizontalAlign="Left"/>
                                <ItemStyle HorizontalAlign="Left" Width="190px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpGradeDesc" HeaderText="Grade">
                                <HeaderStyle Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpPayLabel" HeaderText="Step">
                                <HeaderStyle Width="95px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                           <%-- <asp:BoundField DataField="EmpCashCode" HeaderText="Cash Code">
                                <HeaderStyle Width="85px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="GLAccDesc" HeaderText="Cash Code Name">
                                <HeaderStyle Width="300px" />
                                <ItemStyle HorizontalAlign="Left" Width="350px" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderStyle-Width="65px" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:LinkButton Text="Details" ID="lnkSelect" runat="server" CommandName="Select"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                        </Columns>

                    </asp:GridView>
                </div>

</asp:Content>

