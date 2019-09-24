<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAccountTransfer.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountTransfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script language="javascript" type="text/javascript">
         function ValidationBeforeSave() {
             return confirm('Are you sure you want to save New Sanction Amount?');
         }

         function ValidationBeforeUpdate()
         {
             var txtCreditUNo = document.getElementById('<%=txtCreditUNo.ClientID%>').value;
             var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;
             var txtAccType = document.getElementById('<%=txtAccType.ClientID%>').value;
             var txtAccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
             var txtTrnCuNo = document.getElementById('<%=txtTrnCuNo.ClientID%>').value;
             var txtTrnMemNo = document.getElementById('<%=txtTrnMemNo.ClientID%>').value;

             if (txtCreditUNo == '' || txtCreditUNo.length == 0)
                 alert('Please Input From Credit Union No.');
             else
             if (txtMemNo == '' || txtMemNo.length == 0)
                 alert('Please Input From Depositor No.');
             else
             if (txtAccType == '' || txtAccType.length == 0)
                 alert('Please Input Account Type');
             else
             if (txtAccNo == '')
                alert('Please Input Account No.');
             else
             if (txtTrnCuNo == '' || txtTrnCuNo.length == 0)
                 alert('Please Input To Credit Union No.');
             else
             if (txtTrnMemNo == '' || txtTrnMemNo.length == 0)
                alert('Please Input To Depositor No.');
             else
                return confirm('Are you sure you want to Transfer Account?');
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
                    <th colspan="3">Account Transfer From..........
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="1" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                     <asp:Label ID="lblCuName" runat="server" Height="25px" Text=""></asp:Label>&nbsp;
                        <asp:Button ID="BtnHelp" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                                 Font-Bold="True" CssClass="button green" OnClick="BtnHelp_Click" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="2" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                        OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblMemName" runat="server" Width="400px" Height="25px" Text=""></asp:Label>
                           
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="3" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="4" AutoPostBack="True" ToolTip="Enter Code" OnTextChanged="txtAccNo_TextChanged"  ></asp:TextBox>


                        <asp:DropDownList ID="ddlAccNo" runat="server" Height="25px" Width="190px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Large" OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged"></asp:DropDownList>


                </td>
            </tr>
           </table>
        <table class="style1">
              <thead>
                <tr>
                    <th colspan="3">Account Transfer To..............
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
                        Font-Size="Medium" TabIndex="5" AutoPostBack="true" OnTextChanged="txtTrnCuNo_TextChanged"></asp:TextBox>
                         <asp:Label ID="lblTrfCuName" runat="server" Text=""></asp:Label>&nbsp;
                        <asp:Button ID="BtnTrfHelp" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                                 Font-Bold="True" CssClass="button green" OnClick="BtnTrfHelp_Click" />

                    </td>
                </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrnmemno" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtTrnMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="6" AutoPostBack="True" OnTextChanged="txtTrnMemNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblTrfMemName" runat="server" Width="400px" Height="25px" Text=""></asp:Label>
                </td>
            </tr>
               
            <tr>
                    <td>
                    </td>
                    <td>
                        
                    </td>
                </tr>
            
            <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="BtnTransfer" runat="server" Text="Transfer" Font-Size="Large"
                            Height="27px" Width="150px" ForeColor="White" Font-Bold="True"  CssClass="button green"
                            OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnTransfer_Click"/>&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                        <br />
                    </td>
                </tr>

        </table>
     <asp:TextBox ID="txtHidden" runat="server"  Visible="false"></asp:TextBox>
     <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuTypeName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnferCuType" runat="server"   Visible="false"></asp:Label>
    <asp:Label ID="lblTrnferCuNo" runat="server"  Visible="false"></asp:Label>
    <asp:Label ID="lblTrnferCuTypeName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnferAccNo" runat="server"  Visible="false"></asp:Label>
    <asp:TextBox ID="txtTrnHidden" runat="server"  Visible="false"></asp:TextBox>

    <asp:Label ID="hdnID" runat="server"  Visible="false"></asp:Label>
    <asp:Label ID="hdnNewAccNo" runat="server"  Visible="false"></asp:Label>
    <asp:Label ID="hdnAccFlag" runat="server"  Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNumber" runat="server"  Visible="false"></asp:Label>  
        <asp:Label ID="CtrTrlFlag" runat="server"  Visible="false"></asp:Label>
        <asp:Label ID="lblId1" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblId2" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblFlag" runat="server"  Visible="false"></asp:Label>

    <asp:Label ID="lblAccStatus" runat="server"  Visible="false"></asp:Label>  
    <asp:Label ID="lblAccFlag" runat="server"  Visible="false"></asp:Label>  
    <asp:Label ID="lblAccessType1" runat="server"  Visible="false"></asp:Label>  
    <asp:Label ID="lblAccessType2" runat="server"  Visible="false"></asp:Label>  
    <asp:Label ID="lblAccessType3" runat="server"  Visible="false"></asp:Label>  

    <asp:Label ID="lblID" runat="server"  Visible="false"></asp:Label>  
             
    </div>
    <div>

       <%-- <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />--%>
    </div>


</asp:Content>

