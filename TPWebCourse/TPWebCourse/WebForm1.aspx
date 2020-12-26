<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TPWebCourse.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Просмотр результатов</title>
</head>
<body style="height: 275px; color: #000000;" align = center background = "https://i0.wp.com/saintpolicesystems.com/wp-content/uploads/2018/06/blue-light-backgrounds-background-opera-image-graphics-community-speeddials.jpg">
  
    <form id="form1" runat="server">
    <div>
      <img src="https://web.archive.org/web/20170830123105im_/http://www.mtuci.ru/images/logo.png">
    <h1 > Московский технический университет связи и информатики</h1>
    <h2 >Просмотр результатов testing.mtuci</h2>
    <br>
        Введите свой идентификатор (для студентов, напр. 1БСТ17012)<br />
        <asp:TextBox ID="TextBox1" runat="server" Width="122px"></asp:TextBox>
        &nbsp
        
            <asp:Button ID="Button1" runat="server" Text="Показать" Width="95px" OnClick="Button1_Click" />
          <br />
          <br />
          Введите название предмета<br />
        <asp:TextBox ID="TextBox2" runat="server" Width="122px"></asp:TextBox>
        &nbsp
        
            <asp:Button ID="Button2" runat="server" Text="Показать" Width="95px" OnClick="Button2_Click" />
          <br />
        <br />
        Введите свой идентификатор (для преподавателей, напр. ИТ005)<br />
        <asp:TextBox ID="TextBox3" runat="server" Width="122px"></asp:TextBox>
        &nbsp
        
            <asp:Button ID="Button3" runat="server" Text="Показать" Width="95px" OnClick="Button3_Click" />
          </div>

    <p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        </p>

    </form>
    </body>
</html>

