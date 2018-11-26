<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trinity.Master" CodeBehind="login.aspx.vb" Inherits="TrinityViewer.login" %>
<%@ MasterType TypeName="TrinityViewer.Trinity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <div>
    <asp:Table ID="Table" runat="server" Width="324px" CellSpacing="0" BackColor="#E3EAEB" Font-Size="8" ForeColor="White">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server" BackColor="#1C5E55" VerticalAlign="Top">
                <asp:Table ID="Table1" Height="100%" BackColor="#1C5E55" runat="server"  Width="110px">
                    <asp:TableRow ID="TableRow9" runat="server">
                        <asp:TableCell ID="TableCell11" runat="server" ForeColor="White" Font-Size="8" VerticalAlign="Top"><asp:LinkButton runat="server" ID="lblLogin" Text="Login" ForeColor="White" Font-Underline="false"></asp:LinkButton></asp:TableCell>
                    </asp:TableRow>                    
                    <asp:TableRow ID="TableRow4" runat="server">
                        <asp:TableCell ID="cellCreatedAccount" runat="server" ForeColor="White" Font-Size="8" VerticalAlign="Top"><asp:LinkButton runat="server" ID="lblRecoverPassword" Text="Recover password" ForeColor="White" Font-Underline="false"></asp:LinkButton></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow5" runat="server" Height="100%">
                        <asp:TableCell ID="TableCell2" runat="server">&nbsp;</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell ID="cellLogin" runat="server" VerticalAlign="Top" Width="100%">
                <asp:Table ID="tblLogin" runat="server" Width="100%">
                    <asp:TableHeaderRow ID="TableHeaderRow3" BackColor="#1C5E55" runat="server" Font-Bold="true">
                        <asp:TableHeaderCell ColumnSpan="3">Login</asp:TableHeaderCell>
                    </asp:TableHeaderRow>                        
                    <asp:TableRow ID="TableRow10" runat="server">
                        <asp:TableCell ID="TableCell12" runat="server" ForeColor="Black">Username:</asp:TableCell>
                        <asp:TableCell ID="TableCell13" runat="server" Width="100%"><asp:TextBox ID="txtLogin" runat="server" Width="100%"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow11" runat="server">
                        <asp:TableCell ID="TableCell15" runat="server" ForeColor="Black">Password:</asp:TableCell>
                        <asp:TableCell ID="TableCell16" runat="server" Width="100%"><asp:TextBox ID="txtPassword" runat="server" Width="100%" TextMode="Password"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow Visible="false" runat="server" ID="rowLoginError">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblLoginError" Text="Invalid password!" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow13" runat="server">
                        <asp:TableCell runat="server"></asp:TableCell>
                        <asp:TableCell runat="server" ForeColor="Black" ColumnSpan="2"><asp:CheckBox runat="server" ID="chkRememberMe" Text="Remember me" /></asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow ID="TableRow12" runat="server">
                        <asp:TableCell ID="TableCell18" runat="server" ColumnSpan="2" HorizontalAlign="Right">
                            <asp:Button ID="cmdLogin" Text="Login" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                        </asp:TableCell>
                    </asp:TableRow>  
                </asp:Table>
                <asp:Table ID="tblRecoverPassword" runat="server" Width="100%">
                    <asp:TableHeaderRow ID="TableHeaderRow1" BackColor="#1C5E55" runat="server" Font-Bold="true">
                        <asp:TableHeaderCell ColumnSpan="3">Recover password</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                     <asp:TableRow>
                        <asp:TableCell ForeColor="Black">Username:</asp:TableCell>
                        <asp:TableCell Width="100%"><asp:TextBox ID="txtRecoverUsername" runat="server" Width="100%"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow Visible="false" runat="server" ID="rowRecoverStatus">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblRecoverStatus" Text="" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow18" runat="server">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell ID="TableCell20" runat="server" ColumnSpan="2" HorizontalAlign="Right">
                            <asp:Button ID="cmdRecover" Text="Send password" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                        </asp:TableCell>
                    </asp:TableRow>                          
               </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>           
    </div>

</asp:Content>
