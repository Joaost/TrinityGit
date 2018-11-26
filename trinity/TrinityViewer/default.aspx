<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trinity.Master" CodeBehind="default.aspx.vb" Inherits="TrinityViewer._default1" %>
<%@ MasterType TypeName="TrinityViewer.Trinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    .Dim6t,åpubli.&quot;.per&quot;BP Da VALUEe<asp:Panel runat="server">
        <asp:Panel runat="server" ID="pnlPendingCampaigns">
            <asp:Panel runat="server" CssClass="header">
                   <span style="vertical-align:middle">Campaigns awaiting authorization</span> 
            </asp:Panel>
            <asp:Panel runat="server" CssClass="block">
                <asp:GridView ID="grdPendingCampaigns" runat="server"
                    BorderColor="black"
                    CellPadding="3"
                    Font-Name="Arial"
                    Font-Size="8pt" Visible="False" AutoGenerateColumns="false" >

                    <HeaderStyle BackColor="#AAAADD">                        
                    </HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Campaign">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" PostBackUrl='<%# "campaign.aspx?id=" & Container.DataItem!campaignid  %>' Text='<%# Container.DataItem!Name %>' ForeColor="#009999"></asp:LinkButton>
                            </ItemTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period">
                            <ItemTemplate>
                                <%#Container.DataItem!Dates%>
                            </ItemTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost to Client">
                            <ItemTemplate>
                                <%#Format(Container.DataItem!CTC, "C0")%>
                            </ItemTemplate>                                
                        </asp:TemplateField>                           
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoPendingCampaigns" runat="server" style="font-style:italic; font-size:10px;" Text="At the moment no campaigns await authorization." Visible="true"></asp:Label>
            </asp:Panel>
        </asp:Panel> 
        <br />
        <asp:Panel runat="server" ID="pnlAuthorizedCampaigns">
            <asp:Panel runat="server" CssClass="header">
                   <span style="vertical-align:middle">Campaigns authorized for booking</span> 
            </asp:Panel>
            <asp:Panel runat="server" CssClass="block">
                <asp:GridView ID="grdAuthorizedCampaigns" runat="server"
                    BorderColor="black"
                    CellPadding="3"
                    Font-Name="Arial"
                    Font-Size="8pt" Visible="False" AutoGenerateColumns="false" >

                    <HeaderStyle BackColor="#AAAADD">                        
                    </HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Campaign">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# "campaign.aspx?id=" & Container.DataItem!campaignid  %>' Text='<%# Container.DataItem!Name %>' ForeColor="#009999"></asp:LinkButton>
                            </ItemTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period">
                            <ItemTemplate>
                                <%#Container.DataItem!Dates%>
                            </ItemTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost to Client">
                            <ItemTemplate>
                                <%#Format(Container.DataItem!CTC, "C0")%>
                            </ItemTemplate>                                
                        </asp:TemplateField>                            
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoAuthorizedCampaigns" runat="server" style="font-style:italic; font-size:10px;" Text="At the moment no campaigns are authorized for booking." Visible="true"></asp:Label>
            </asp:Panel>
        </asp:Panel> 
    </asp:Panel>
</asp:Content>
