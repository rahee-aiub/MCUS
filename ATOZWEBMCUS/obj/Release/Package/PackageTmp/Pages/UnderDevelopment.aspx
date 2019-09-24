<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("A2ZERPModule.aspx");
    }

   
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Title</title>
    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/A2ZERPStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="HtmlForm" runat="server">
    <br />
    <br />
    <div id="DivMain" runat="server" align="center">
        <h1 style="color: #15317E">
            <%--AtoZ ERP SYSTEM for Pharmaceutical Industry--%>
        </h1>
        <h2 style="color: #15317E">
            <%--For--%></h2>
        <h2 style="color: #C35817">
            <%--MONICOPHARMA LIMITED.--%></h2>
        <br />
        <br />
        <table class="style1">
            <tr>
                <td style="font-size: large">
                    <marquee direction="left" scrollamount="3"><strong>This Page is Under Development </strong></marquee>
                </td>
            </tr>
            <%--<tr>
                <td style="font-size: large">
                    <marquee direction="right" scrollamount="3"><strong>Comming up Soon.......... </strong></marquee>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Back" CssClass="button green size-120"
                        OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div id="footer">
        <h1 style="color: #15317E">
            Developed By AtoZ Computer Services.</h1>
    </div>
    </form>
</body>
</html>
