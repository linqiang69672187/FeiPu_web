<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_Login.aspx.cs" Inherits="Feipdianli.Test_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body  >
    <form id="form1" runat="server" action="/Handle/Service/Checklogin.ashx">
    <div>
    
        用户：<asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <br />
        密码：<asp:TextBox ID="pwd" runat="server"></asp:TextBox>
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
    
    </div>
    </form>
</body>
</html>
