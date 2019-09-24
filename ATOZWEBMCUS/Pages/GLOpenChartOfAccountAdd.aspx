<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLOpenChartOfAccountAdd.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLOpenChartOfAccountAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 48px;
            width: 644px;
        }

        .auto-style4 {
            height: 34px;
            width: 644px;
        }

        .auto-style5 {
            height: 30px;
            width: 644px;
        }

        .auto-style6 {
            height: 33px;
            width: 644px;
        }

        .auto-style7 {
            width: 644px;
        }
    </style>


    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            var ddlHeader = document.getElementById('<%=ddlHeader.ClientID%>');
            var txtDesc = document.getElementById('<%=txtDesc.ClientID%>').value;

            if (ddlHeader.selectedIndex == 0 || ddlHeader.SelectedValue == "0")
                alert('Please Select Header List');
            else
            if (txtDesc == '' || txtDesc.length == 0)
                alert('Please Input GL Description');
            else
               return confirm('Are you sure you want to Add information?');
            return false;
        }

        
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div id="main" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th class="auto-style2">Open Chart Of Account
                    </th>

                </tr>

            </thead>
            <tr>

                <td style="background-color: #fce7f9" class="auto-style4">&nbsp;&nbsp;&nbsp;
                
                    &nbsp;<asp:RadioButton ID="rbtMainHead" Text="Main Head " runat="server" GroupName="Add" AutoPostBack="True" OnCheckedChanged="rbtMainHead_CheckedChanged"  />
                    <asp:RadioButton ID="rbtSubHeader" Text="SubHead" runat="server" GroupName="Add" AutoPostBack="True" OnCheckedChanged="rbtSubHeader_CheckedChanged"  />
                    &nbsp;
                <asp:RadioButton ID="rbtDeatail" Text="Detail" runat="server" GroupName="Add" AutoPostBack="True" OnCheckedChanged="rbtDeatail_CheckedChanged"  />
                </td>



            </tr>

            <tr>

                <td style="background-color: #fce7f9" class="auto-style5"><asp:Label ID="Label5" runat="server" Text="Header"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:DropDownList ID="ddlHeader" runat="server" Width="193px" AutoPostBack="True" OnSelectedIndexChanged="ddlHeader_SelectedIndexChanged"> </asp:DropDownList>
                  
                    &nbsp;&nbsp;<asp:Label ID="lblHeaderDesc" runat="server" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>



            </tr>

            <tr>

                <td style="background-color: #fce7f9" class="auto-style5"><asp:Label ID="lblMainHead" runat="server" Text="Main Head"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<asp:DropDownList ID="ddlMainHead" runat="server" Width="193px" AutoPostBack="True" OnSelectedIndexChanged="ddlMainHead_SelectedIndexChanged"> </asp:DropDownList>
                  
                    &nbsp;&nbsp;<asp:Label ID="lblMainHeaddesc" runat="server" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMainHead" runat="server" Visible="False"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>



            </tr>
            <tr>

                <td style="background-color: #fce7f9" class="auto-style6">
                   <asp:Label ID="lblSubHead" runat="server" Text="Sub Head"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:DropDownList ID="ddlSubHead" runat="server" Height="21px" Width="253px" Style="margin-left: 0px" OnSelectedIndexChanged="ddlSubHead_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                    <asp:Label ID="lblsubHeadDesc" runat="server" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="txthidesubhead" runat="server" Visible="False"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                </td>




            </tr>

            <tr>

                <td style="background-color: #fce7f9" class="auto-style6">
             <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtDetailNo" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                   
                                      
                </td>


            </tr>

            <tr>

                <td style="background-color: #fce7f9" class="auto-style6">
                    <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtDesc" runat="server" Width="389px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                &nbsp;
                &nbsp;
                    &nbsp;
                    <asp:HiddenField ID="hdComNo" runat="server" />
                </td>




            </tr>


            <tr>

                <td style="background-color: #fce7f9" class="auto-style5"><asp:Label ID="lblAccMode" runat="server" Text="Transaction Mode"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlAccMode" runat="server" Width="193px"> 
                        <asp:ListItem Value="0">G/L Code</asp:ListItem>
                        <asp:ListItem Value="1">Non G/L Code</asp:ListItem>

                    </asp:DropDownList>
                  
                    
                </td>

            </tr>


            <tr>

                <td style="background-color: #fce7f9" class="auto-style6">
                    <asp:Label ID="lblOverDraft" runat="server" Text="OverDraft A/c"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;

                    <asp:CheckBox ID="ChkOverDraft" runat="server"  Font-Size="Large" ForeColor="Red"  />
                </td>

            </tr>


            <tr>

                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>

                <td class="auto-style7">


                    <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="btnAdd_Click" />&nbsp;
                    &nbsp;
                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red"
                        OnClick="BtnExit_Click" />

            </tr>


        </table>


    </div>

   <asp:Label ID="hdnGLSubHead" runat="server" Text="" Visible="false"></asp:Label>
   <asp:Label ID="lblBalanceType" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
