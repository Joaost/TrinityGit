<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Balthazar.Master" CodeBehind="workschedule.aspx.vb" Inherits="BaltazarWeb.workschedule" 
    title="Arbetsschema" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:Label ID="lblShifts" runat="server" Text="Dina arbetsskift:" Font-Bold="True"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="grdSchedule" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" >
        <Columns>
            <asp:TemplateField headertext="Datum">                
                <EditItemTemplate></EditItemTemplate>
                <ItemTemplate>
                    <%#Format(DataBinder.Eval(Container.DataItem, "Date"), "Short date")%>                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField headertext="Starttid">                
                <EditItemTemplate></EditItemTemplate>
                <ItemTemplate>
                    <%#Master.Mam2Time(DataBinder.Eval(Container.DataItem, "StartMaM"))%>                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField headertext="Sluttid">                
                <EditItemTemplate></EditItemTemplate>
                <ItemTemplate>
                    <%#Master.Mam2Time(DataBinder.Eval(Container.DataItem, "EndMaM"))%>                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:boundfield datafield="Location" headertext="Plats" />
            <asp:boundfield datafield="Role" headertext="Roll" />
        </Columns>
        <RowStyle Font-Size="12px" BackColor="#E3EAEB" />
        <HeaderStyle Font-Size="14px" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</asp:Content>
