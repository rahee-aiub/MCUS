<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSLoanExpiryReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSLoanExpiryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


     <script language="javascript" type="text/javascript">
         $(function () {
             $("#<%= txtTranDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtTranDate.ClientID %>").datepicker();

              });

        });


            </script>


    <br />
    <br />
   
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="5">Loan A/cs Expired Date Report
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
                        Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtTranDate_TextChanged" ></asp:TextBox>
                   
            </tr>

            <tr>


                <td>
                    <asp:label id="lblacType" runat="server" forecolor="Red" text="A/C Type : "></asp:label>
                </td>
          
                
                <td>
                    <asp:textbox id="txtAccType" runat="server" cssclass="cls text" width="115px" height="25px"
                        font-size="Large" autopostback="true" tooltip="Enter Code" ontextchanged="txtAccType_TextChanged"></asp:textbox>

                    <asp:dropdownlist id="ddlAcType" runat="server" height="25px" width="400px" autopostback="True"
                        font-size="Large" onselectedindexchanged="ddlAcType_SelectedIndexChanged">
                        
                    </asp:dropdownlist>
                </td>


            </tr>

            <tr>



                <td>

                    <asp:checkbox id="ChkAllCrUnion" runat="server" forecolor="Red" text="All" oncheckedchanged="ChkAllCrUnion_CheckedChanged" autopostback="True" />

                <%--</td>

                <td>--%>
                    &nbsp;&nbsp;
                    <asp:label id="lblCu" runat="server" forecolor="Red" text="Credit Union "></asp:label>

                </td>

                <td>
                    <asp:textbox id="txtCrUnion" runat="server" cssclass="cls text" width="114px" height="25px"
                        font-size="Large" autopostback="true" tooltip="Enter Code" ontextchanged="txtCrUnion_TextChanged"></asp:textbox>
                    <asp:dropdownlist id="ddlCrUnion" runat="server" height="31px" width="441px" autopostback="True"
                        font-size="Large" onselectedindexchanged="ddlCrUnion_SelectedIndexChanged" style="margin-left: 7px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:dropdownlist>

                </td>

            </tr>


             <tr>



                <td>

                    <asp:checkbox id="ChkAllDistrict" runat="server" forecolor="Red" text="All" oncheckedchanged="ChkAllDistrict_CheckedChanged" autopostback="True" />

                <%--</td>

                <td>--%>
                    &nbsp;&nbsp;
                    <asp:label id="lblDistrict" runat="server" forecolor="Red" text="District "></asp:label>

                </td>

                <td>
                   
                    <asp:dropdownlist id="ddlDistrict" runat="server" height="31px" width="230px"
                        font-size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:dropdownlist>

                </td>

            </tr>

            <tr>

                <td></td>
                
                <td>
                    <asp:button id="BtnView" runat="server" text="Preview" font-size="Large" forecolor="White"
                        font-bold="True" height="27px" width="120px" cssclass="button green" onclick="BtnView_Click" />
                    &nbsp;
                  &nbsp;
                    <asp:button id="BtnExit" runat="server" text="Exit" font-size="Large" forecolor="#FFFFCC"
                        height="27px" width="86px" font-bold="True" tooltip="Exit Page" causesvalidation="False"
                        cssclass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>


            </tr>





        </table>
    </div>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtHidden" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblprocdate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnDate" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

