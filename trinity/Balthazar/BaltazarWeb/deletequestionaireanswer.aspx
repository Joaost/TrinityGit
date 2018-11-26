<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Balthazar.Master" CodeBehind="deletequestionaireanswer.aspx.vb" Inherits="BaltazarWeb.deletequestionaireanswer" 
    title="Frågeformulär" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <br />
    <table style="width: 280px">
        <tr>
            <td colspan="3" style="height: 39px">
Är du säker på att du vill ta bort svaret?</td>

        </tr>
        <tr>
            <td style="width: 83px"></td>
            <td>
                <asp:Button ID="cmdYes" runat="server" Text="Ja" Width="64px"  BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55"/>
            </td>
            <td>
                <asp:Button ID="cmdNo" runat="server" Text="Nej" Width="64px" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
            </td>
        </tr>
    </table>
</asp:Content>
