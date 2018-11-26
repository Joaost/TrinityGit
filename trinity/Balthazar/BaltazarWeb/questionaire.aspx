<%@ Page Language="vb" AutoEventWireup="False" MasterPageFile="~/Balthazar.Master" CodeBehind="questionaire.aspx.vb" Inherits="BaltazarWeb.questionaire" 
    title="Fr�geformul�r" %>
<%@ Import Namespace="System.Data" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>


<asp:Content ID="cntQuestionaire" ContentPlaceHolderID="Main" runat="server">
    <asp:MultiView runat="server" ID="mvwQuestionaire" ActiveViewIndex="0">
        <asp:View runat="server" ID="vwQuestionaire"> 
           <asp:HiddenField runat="server" ID="fldCity" />
           <asp:HiddenField runat="server" ID="fldLocation" />
           <asp:HiddenField runat="server" ID="fldDate" />
           <asp:HiddenField runat="server" ID="fldBookingID" />
           <asp:ValidationSummary runat="server" HeaderText="F�lt markerade med * m�ste fyllas i" /> 
           <asp:Label runat="server" Text="" ID="lblName" Font-Bold="true" Font-Size="16px"></asp:Label>           
        </asp:View>
        <asp:View runat="server" ID="vwNoAccess">
            <p style="font-size:14px; font-weight:bold;">Du har inte tillg�ng till den beg�rda sidan</p>
            <asp:LinkButton runat="server" ForeColor="#009999" Text="<-- Tillbaka" PostBackUrl="~/Default.aspx"></asp:LinkButton>
        </asp:View>
    </asp:MultiView>

</asp:Content>


