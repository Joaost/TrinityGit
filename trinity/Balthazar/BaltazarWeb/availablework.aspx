<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Balthazar.Master" CodeBehind="availablework.aspx.vb" Inherits="BaltazarWeb.availablework" 
    title="Tillgängliga skift" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:Label ID="lblShifts" runat="server" Text="Tillgängliga arbetsskift:" Font-Bold="True" Font-Size="14px"></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Var vänlig kryssa för de skift som du vill jobba." Font-Size="12px"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="grdSchedule" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Font-Size="12px" >
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
            <asp:TemplateField headertext="">               
                <EditItemTemplate></EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkChecked" runat="server" Checked='<%# not isdbnull(DataBinder.Eval(Container.DataItem, "Checked"))%>'></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField headertext="">               
                <EditItemTemplate></EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' Visible="false" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle Font-Size="12px" BackColor="#E3EAEB" />
        <HeaderStyle Font-Size="14px" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <asp:Button ID="cmdSave" runat="server" Text="Spara"  BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
</asp:Content>

