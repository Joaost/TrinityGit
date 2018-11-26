<%@ Page Title="Sammanställning" Language="vb" AutoEventWireup="false" MasterPageFile="~/Balthazar.Master" CodeBehind="questionairesummary.aspx.vb" Inherits="BaltazarWeb.questionairesummary" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>

<asp:Content ID="cntQuestionaireSummary" ContentPlaceHolderID="Main" runat="server">
    <asp:ScriptManager ID="scmQuestionaire" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnQuestionaire" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <asp:Table runat="server" ID="Summary">
            
            </asp:Table>                
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Summary"/>
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="prgQuestionaire" runat="server">
        <ProgressTemplate>
            Hämtar återrapporter...
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <asp:LinkButton runat="server" Text="<-- Tillbaka" PostBackUrl="~/Default.aspx" ForeColor="#009999"></asp:LinkButton>
</asp:Content>

