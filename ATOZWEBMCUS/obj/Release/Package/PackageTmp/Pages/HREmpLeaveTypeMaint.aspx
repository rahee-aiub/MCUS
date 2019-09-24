<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmpLeaveTypeMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmpLeaveTypeMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {

            var txtcode = document.getElementById('<%=txtcode.ClientID%>').value;
            var txtLName = document.getElementById('<%=txtLName.ClientID%>').value;


            if (txtcode == '' || txtcode.length == 0) {
                 document.getElementById('<%=txtcode.ClientID%>').focus();
	               alert('Please Input Leave type Code.');
            }
            else if (txtLName == '' || txtLName.length == 0)
                {
                 document.getElementById('<%=txtLName.ClientID%>').focus();
                  alert('Please Input Leave type Name.');
            }
	           else
	               return confirm('Are you sure you want to save information?');
               return false;
           }


        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>



    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            width: 560px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 543px;
            
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Leave Type Maintenance
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:label id="lblcode" runat="server" text="Leave Code:" font-size="Large" forecolor="Red"></asp:label>
                </td>
                <td>
                    <asp:textbox id="txtcode" runat="server" cssclass="cls text" width="115px" height="25px" bordercolor="#1293D1" borderstyle="Ridge"
                        font-size="Large" autopostback="true" tooltip="Enter Code" OnTextChanged="txtcode_TextChanged"></asp:textbox>
                    <asp:dropdownlist id="ddlLType" runat="server" height="25px" bordercolor="#1293D1" borderstyle="Ridge"
                        width="316px" autopostback="True"
                        font-size="Large" OnSelectedIndexChanged="ddlLType_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                    </asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:label id="lblLName" runat="server" text="Leave Name:" font-size="Large"
                        forecolor="Red"></asp:label>
                </td>
                <td>
                    <asp:textbox id="txtLName" runat="server" cssclass="cls text" width="316px" bordercolor="#1293D1" borderstyle="Ridge"
                        height="25px" font-size="Large" tooltip="Enter Name"></asp:textbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:label id="lblTotalDays" runat="server" text="Total Days:" font-size="Large"
                        forecolor="Red"></asp:label>
                </td>
                <td>
                    <asp:textbox id="txtTotalDays" runat="server" cssclass="cls text" width="115px" bordercolor="#1293D1" borderstyle="Ridge"
                        height="25px" font-size="Large" tooltip="Enter Days"></asp:textbox>
                </td>
            </tr>


            <tr>
                <td>
                    <asp:label id="lblSalEffect" runat="server" text="Effect in Salary :" font-size="Large"
                        forecolor="Red"></asp:label>
                </td>
                <td>
                    <asp:CheckBox ID="ChkSalEffect" runat="server"  width="100px" ForeColor="Black" Text="" Checked="false"/>
                    
                </td>
            </tr>
                <tr>
                <td>
                    <asp:label id="lblstatus" runat="server" text="Leave Status :" font-size="Large"
                        forecolor="Red"></asp:label>
                </td>
                <td>
                      <asp:dropdownlist id="ddlLstatus" runat="server" height="25px" bordercolor="#1293D1" borderstyle="Ridge"
                        width="150px" autopostback="True"
                        font-size="Large">
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="0">InActive</asp:ListItem>
                        

                          </asp:dropdownlist>
                </td>
            </tr>




            <tr>
                <td></td>
                <td>
                    <asp:button id="BtnSubmit" runat="server" text="Submit" font-size="Large" forecolor="White"
                        font-bold="True" tooltip="Insert Information" cssclass="button green"
                        onclientclick="return ValidationBeforeSave()" onclick="BtnSubmit_Click" />
                    &nbsp;
                    <asp:button id="BtnUpdate" runat="server" text="Update" font-bold="True" font-size="Large"
                        forecolor="White" tooltip="Update Information" cssclass="button green"
                        onclientclick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" />
                    &nbsp;
                    <asp:button id="BtnView" runat="server" text="View" font-bold="True" font-size="Large"
                        forecolor="White" tooltip="View Information" cssclass="button green"
                        onclick="BtnView_Click" />
                    &nbsp;
                    <asp:button id="BtnExit" runat="server" text="Exit" font-size="Large" forecolor="#FFFFCC"
                        height="27px" width="86px" font-bold="True" tooltip="Exit Page" causesvalidation="False"
                        cssclass="button red" onclick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>

    <div align="center" class="grid_scroll">
        <asp:gridview id="gvDetailInfo" runat="server" headerstyle-cssclass="FixedHeader" headerstyle-backcolor="YellowGreen"
            autogeneratecolumns="false" alternatingrowstyle-backcolor="WhiteSmoke" rowstyle-height="10px" onrowdatabound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                  
                <asp:BoundField HeaderText="Code" DataField="EmpleaveCode" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Name" DataField="EmpleaveName"  HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"/>
                <asp:BoundField HeaderText="Days" DataField="TotalDays"   HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Effect Salary" DataField="EffectSalary"   HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Status" DataField="Status"   HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>

              </Columns>
          
        </asp:gridview>
    </div>


     <asp:Label ID="LeaveStatus" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

