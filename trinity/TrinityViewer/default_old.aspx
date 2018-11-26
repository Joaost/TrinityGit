<%@ Import Namespace="System.Data.SqlClient" %> 

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default_old.aspx.vb" Inherits="TrinityViewer._default" MasterPageFile="~/Trinity.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="Main">
    <% 
    'Dim conn As SqlConnection
    'Dim cmd As SqlCommand
    'Dim dr As SqlDataReader

    'conn = New SqlConnection("server=STO-SQL01;database=extranet;uid=johanh;pwd=turbo")
    'conn.Open()
    'cmd = New SqlCommand("SELECT * FROM campaign", conn)
    'dr = cmd.ExecuteReader
    
    'While dr.Read
    '    Response.Write("<tr>")
    '    Response.Write("<td><a href='campaign.aspx?id=" & dr!id.ToString & "'>" & dr!name & "</a></td>")
    '    Response.Write("<td>" & Format(Verify(dr!ctc), "C0") & "</td>")
    '    Response.Write("<td>" & Format(Verify(dr!startdate), "Short date") & "</td>")
    '    Response.Write("<td>" & Format(Verify(dr!enddate), "Short date") & "</td>")
    '    Response.Write("</tr>")
    'End While
    'dr.Close()
    'conn.Close()
%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="id" DataSourceID="SqlDataSourceExtranet" ForeColor="#333333" GridLines="None">
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="campaign.aspx?id={0}"
                DataTextField="name" HeaderText="Name" NavigateUrl="~/campaign.aspx" />
            <asp:TemplateField HeaderText="Start date" SortExpression="startdate">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("startdate") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("startdate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End date" SortExpression="enddate">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("enddate") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("enddate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cost to Client" SortExpression="ctc">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ctc") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ctc", "{0:C0}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#009999" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceExtranet" runat="server" ConnectionString="<%$ ConnectionStrings:extranetConnectionString %>"
        ProviderName="<%$ ConnectionStrings:extranetConnectionString.ProviderName %>"
        SelectCommand="SELECT [id], [name], [ctc], [startdate], [enddate] FROM [campaign] ORDER BY [name]">
    </asp:SqlDataSource>
</asp:Content>
