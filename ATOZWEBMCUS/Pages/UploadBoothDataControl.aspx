<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="UploadBoothDataControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.UploadBoothDataControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtProcessDate.ClientID %>").dynDateTime({
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

       
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
     <div align="center">
        <table class="style1">
            <tr>
                <th>
                    Upload Booth Data Control
                </th>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lblProcessDate" runat="server" Text="Process Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtProcessDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="150px" Height="25px" Font-Size="Medium" ></asp:TextBox><img src="../Images/calender.png" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                </td>
            </tr>
           
            </table>
          </div>
        <br />
    <div align="center">
      
             
        <asp:Button ID="BtnProcess" runat="server" Text="Process" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green" 
            Height="30px" OnClick="BtnProcess_Click"/>&nbsp;
        <asp:Button ID="BtnMerge" runat="server" Text="Merge" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button blue" 
            Height="30px" OnClick="BtnMerge_Click"/>&nbsp;

        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="30px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click"/>
    </div>
    <br />
    <div id="DivGridViewCancle" runat="server" align="center" style="height: 400px; overflow: auto;
            width: 100%;">
            <table class="style1">
                <thead>
                    <tr>
                        <th>
                            <p align="center" style="color: blue">
                               Upload Booth Data Control - Spooler
                            </p>
        <asp:GridView ID="gvUPControlInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" style="margin-top: 4px" Width="757px" >
            <Columns>
         
          
                <asp:BoundField DataField="ProcessDate" HeaderText="Process Date" DataFormatString="{0:dd/MM/yyyy}"/>
                <asp:BoundField DataField="CashCodeNo" HeaderText="Booth No" />
                <asp:BoundField DataField="CashCodeName" HeaderText="Booth Name" />
                   
                <asp:TemplateField HeaderText="Status flag" Visible="false">
            <ItemTemplate>
              <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ProcessStatus") %>'></asp:Label>
            </ItemTemplate>
             </asp:TemplateField>
                 <asp:BoundField DataField="ProcessStatusDesc" HeaderText="Status" />
                <asp:TemplateField HeaderText="Select" Visible="true">
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
                      
            </Columns>
        </asp:GridView>
                           
                       </th>        
                    </tr>
                </thead>
            </table>
        </div>
    <div>
        <asp:ListBox ID="filelist" runat="server" Width="529px" Visible="False"></asp:ListBox>
    </div>

</asp:Content>

