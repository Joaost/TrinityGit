<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="info.aspx.vb" Inherits="BaltazarWeb.info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Arbetsuppgifter</title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <asp:gridview runat="server" ID="grdCategories" AutoGenerateColumns="False" 
            GridLines="None" ShowHeader="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Category">
                    <ItemStyle Font-Bold="True" Font-Size="Small" Width="150px" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                    <ItemStyle Font-Italic="True" Font-Size="Small" VerticalAlign="Top" />
                </asp:BoundField>
            </Columns>
        
        </asp:gridview>
    </div>
    </form>
</body>
</html>
