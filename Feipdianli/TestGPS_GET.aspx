<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestGPS_GET.aspx.cs" Inherits="Feipdianli.TestGPS_GET" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" action="/Handle/Service/GetGPS_ByBound.ashx">
    <div>
    
    </div>
       maxlo: <asp:TextBox ID="maxlo" runat="server"></asp:TextBox>
        <br />
        <br />
       minlo: <asp:TextBox ID="minlo" runat="server"></asp:TextBox>
        <br />
        <br />
       maxla: <asp:TextBox ID="maxla" runat="server"></asp:TextBox>
        <br />
        <br />
       minla: <asp:TextBox ID="minla" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </form>
</body>
</html>
