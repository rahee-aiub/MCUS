<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSApproveCreditUnionMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSApproveCreditUnionMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script language="javascript" type="text/javascript">

         function ApproveValidation() {
             return confirm('Do you want to Approve data?');
         }
         function ReVerifyValidation() {
             return confirm('Do you want to Reverify data?');
         }
         function RejectValidation() {
             return confirm('Do you want to Reject data?');
         }
    </script>

     <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <div id="DivMainHeader" runat="server" align="center">
            
            <table class="style1">
                <tr>
                    <th colspan="4">
                        <p align="center" style="color: blue">
                            Approve Credit Union Maintenance
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
     <div id="DivGridViewCancle" runat="server" align="center" style="height: 245px; overflow: auto;
            width: 100%;">
            <table class="style1">
                <thead>
                    <tr>
                        <th>
                            <p align="center" style="color: blue">
                               Approve - Spooler
                            </p>
        <asp:GridView ID="gvCUInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" style="margin-top: 4px" Width="757px" >
            <Columns>
                     
            <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
           <asp:Button ID="BtnApprove" runat="server" Text="Approve" OnClick="BtnApprove_Click"  OnClientClick="return ApproveValidation()" Width="68px" CssClass="button green" />
            </ItemTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
           <asp:Button ID="BtnReVerify" runat="server" Text="ReVerify" OnClick="BtnReVerify_Click"  OnClientClick="return ReVerifyValidation()" Width="68px" CssClass="button blue" />
            </ItemTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
           <asp:Button ID="BtnReject" runat="server" Text="Reject" OnClick="BtnReject_Click"  OnClientClick="return RejectValidation()" Width="68px" CssClass="button red" />
            </ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="View">
            <ItemTemplate>
              <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" Width="60px" CssClass="button black size-100" />
            </ItemTemplate>
        </asp:TemplateField>

                <asp:TemplateField HeaderText="Application">
            <ItemTemplate>
                 <asp:Label ID="lblcuno" runat="server" Text='<%#Eval("CuNo")%>'></asp:Label>
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
                <asp:BoundField DataField="CuOpDt" HeaderText="Open Date" DataFormatString="{0:dd/MM/yyyy}"/>
                <asp:BoundField DataField="CuProcDesc" HeaderText="Mode"  ControlStyle-ForeColor="Tomato" />
                      
            </Columns>
        </asp:GridView>
        

                       </th>        
                    </tr>
                </thead>
            </table>
        </div>
    <div align="center">
          <asp:Label ID="lblmsg1" runat="server" Text="All Record Approve Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
            <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Approve" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>  </div>

        <asp:Label ID="lblCUNoMsg" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCUTypeMsg" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblNewSRL" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

