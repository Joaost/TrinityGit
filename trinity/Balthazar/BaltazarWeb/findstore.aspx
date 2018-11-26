<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="findstore.aspx.vb" Inherits="BaltazarWeb.findstore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
        <script language="javascript" type="text/javascript">
            function sendback(store, address, city, phone) {
                var ret = new Array(store, address, city, phone);
                window.opener.update(ret);
                window.close();
            }
            </script>    
</head>
<body style="height: 380px;">
    <form id="form1" runat="server">
    <div style="width: 100%; height: 100%; border: solid 1px #009999;">
        <asp:Table ID="tblSearch" runat="server" Font-Size="12px" Font-Names="Arial">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" Text="Sök:"></asp:Label>                
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="cmdSearch" runat="server" Text="Sök" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:DataGrid runat="server" ID="grdSearchResults" AutoGenerateColumns="False" 
            GridLines="None" CellPadding="4" ForeColor="#333333" Font-Names="Arial" AllowPaging="true" AllowCustomPaging="true" PageSize="10" >
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Width="100%" />
            <EditItemStyle BackColor="#7C6F57" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#E3EAEB" />            
            <Columns>
                <asp:BoundColumn HeaderText="Butik" DataField="name"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Adress" DataField="address"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Stad" DataField="city"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="">
                    <ItemTemplate>
                        <a href="#" onclick="sendback('<%# DataBinder.eval(Container.DataItem,"name") %>','<%# DataBinder.eval(Container.DataItem,"address") %>','<%# DataBinder.eval(Container.DataItem,"city") %>','<%# DataBinder.eval(Container.DataItem,"phoneno") %>')">OK</a>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid>
    </div>
    </form>
</body>
</html>
