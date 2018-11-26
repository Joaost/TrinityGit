<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Balthazar.Master" CodeBehind="login.aspx.vb" Inherits="BaltazarWeb.login" 
    title="Login" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <div>
        <asp:Table ID="Table" runat="server" Width="324px" CellSpacing="0" BackColor="#E3EAEB" Font-Size="8" ForeColor="White">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server" BackColor="#1C5E55" VerticalAlign="Top">
                <asp:Table ID="Table1" Height="100%" BackColor="#1C5E55" runat="server"  Width="95px">
                    <asp:TableRow ID="TableRow9" runat="server">
                        <asp:TableCell ID="TableCell11" runat="server" ForeColor="White" Font-Size="8" VerticalAlign="Top"><asp:LinkButton runat="server" ID="lblLogin" Text="Logga in" ForeColor="White" Font-Underline="false"></asp:LinkButton></asp:TableCell>
                    </asp:TableRow>                    
                    <asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell ID="cellCreateAccount" runat="server" ForeColor="White" Font-Size="8" VerticalAlign="Top"><asp:LinkButton runat="server" ID="lblCreateAccount" Text="Skapa konto" ForeColor="White" Font-Underline="false"></asp:LinkButton></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow3" runat="server">
                        <asp:TableCell ID="cellVerifyAccount" runat="server" ForeColor="White" Font-Size="8" VerticalAlign="Top"><asp:LinkButton runat="server" ID="lblVerifyAccount" Text="Verifiera konto" ForeColor="White" Font-Underline="false"></asp:LinkButton></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow4" runat="server">
                        <asp:TableCell ID="cellCreatedAccount" runat="server" ForeColor="White" Font-Size="8" VerticalAlign="Top"><asp:LinkButton runat="server" ID="lblRecoverPassword" Text="Glömt lösenord" ForeColor="White" Font-Underline="false"></asp:LinkButton></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow5" runat="server" Height="100%">
                        <asp:TableCell ID="TableCell2" runat="server">&nbsp;</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell ID="cellLogin" runat="server" VerticalAlign="Top" Width="100%">
                <asp:Table ID="tblLogin" runat="server" Width="100%">
                    <asp:TableHeaderRow ID="TableHeaderRow3" BackColor="#1C5E55" runat="server" Font-Bold="true">
                        <asp:TableHeaderCell ColumnSpan="3">Logga in</asp:TableHeaderCell>
                    </asp:TableHeaderRow>                        
                    <asp:TableRow ID="TableRow10" runat="server">
                        <asp:TableCell ID="TableCell12" runat="server" ForeColor="Black">Användarnamn:</asp:TableCell>
                        <asp:TableCell ID="TableCell13" runat="server" Width="100%"><asp:TextBox ID="txtLogin" runat="server" Width="100%"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow11" runat="server">
                        <asp:TableCell ID="TableCell15" runat="server" ForeColor="Black">Lösenord:</asp:TableCell>
                        <asp:TableCell ID="TableCell16" runat="server" Width="100%"><asp:TextBox ID="txtPassword" runat="server" Width="100%" TextMode="Password"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow Visible="false" runat="server" ID="rowLoginError">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblLoginError" Text="Felaktigt lösenord!" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow13" runat="server">
                        <asp:TableCell runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell19" runat="server" ForeColor="Black" ColumnSpan="2"><asp:CheckBox runat="server" ID="chkRememberMe" Text="Kom ihåg mig" /></asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow ID="TableRow12" runat="server">
                        <asp:TableCell ID="TableCell18" runat="server" ColumnSpan="2" HorizontalAlign="Right">
                            <asp:Button ID="cmdLogin" Text="Logga in" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                        </asp:TableCell>
                    </asp:TableRow>  
                </asp:Table>
                <asp:Table ID="tblCreateAccount" runat="server" Width="100%">
                    <asp:TableHeaderRow ID="TableHeaderRow1" BackColor="#1C5E55" runat="server" Font-Bold="true">
                        <asp:TableHeaderCell ColumnSpan="3">Skapa konto</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow ID="TableRow6" runat="server">
                        <asp:TableCell ID="TableCell4" runat="server" ForeColor="Black">Användarnamn:</asp:TableCell>
                        <asp:TableCell ID="TableCell5" Width="100%" runat="server"><asp:TextBox ID="txtUsername" runat="server" Width="100%"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow7" runat="server">
                        <asp:TableCell ID="TableCell7" runat="server" ForeColor="Black">E-mail:</asp:TableCell>
                        <asp:TableCell ID="TableCell8" runat="server"><asp:TextBox ID="txtEmail" runat="server" Width="100%"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow Visible="false" runat="server" ID="rowCreateUserError">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblCreateUserError" Text="" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow8" runat="server">
                        <asp:TableCell ID="TableCell10" runat="server" ColumnSpan="2" HorizontalAlign="Right">
                            <asp:Button ID="cmdCreateAccount" Text="Skapa" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                        </asp:TableCell>
                    </asp:TableRow>                   
                </asp:Table>
                <asp:Table ID="tblVerifyAccount" runat="server" Width="100%">
                    <asp:TableHeaderRow ID="TableHeaderRow2" BackColor="#1C5E55" runat="server" Font-Bold="true">
                        <asp:TableHeaderCell ColumnSpan="3">Verifiera konto</asp:TableHeaderCell>
                    </asp:TableHeaderRow>                        
                    <asp:TableRow ID="TableRow14" runat="server">
                        <asp:TableCell ID="TableCell3" runat="server" ForeColor="Black">Användarnamn:</asp:TableCell>
                        <asp:TableCell ID="TableCell6" runat="server" Width="100%"><asp:TextBox ID="txtVerifyUsername" runat="server" Width="100%"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow15" runat="server">
                        <asp:TableCell ID="TableCell9" runat="server" ForeColor="Black">Verifieringskod:</asp:TableCell>
                        <asp:TableCell ID="TableCell14" runat="server" Width="100%"><asp:TextBox ID="txtVerifyCode" runat="server" Width="100%"></asp:TextBox></asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow ID="TableRow16" runat="server">
                        <asp:TableCell ID="TableCell23" runat="server" ForeColor="Black">Önskat lösen:</asp:TableCell>
                        <asp:TableCell ID="TableCell24" runat="server" Width="100%"><asp:TextBox ID="txtWantedPassword" runat="server" Width="100%" TextMode="Password"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow20" runat="server">
                        <asp:TableCell ID="TableCell25" runat="server" ForeColor="Black" Wrap="false">Repetera lösen:</asp:TableCell>
                        <asp:TableCell ID="TableCell26" runat="server" Width="100%"><asp:TextBox ID="txtRepeatpassword" runat="server" TextMode="Password" Width="100%"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Visible="true" runat="server" ID="rowVerifyError">
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblVerifyError" Text="Felaktigt lösenord!" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow19" runat="server">
                        <asp:TableCell ID="TableCell22" runat="server" HorizontalAlign="Right" ColumnSpan="2">
                            <asp:Button ID="cmdVerify" Text="Verifiera konto" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                        </asp:TableCell>
                    </asp:TableRow>  
                </asp:Table>
                <asp:Table ID="tblRecoverPassword" runat="server" Width="100%">
                    <asp:TableHeaderRow BackColor="#1C5E55" runat="server" Font-Bold="true">
                        <asp:TableHeaderCell ColumnSpan="3">Glömt lösenord</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                     <asp:TableRow>
                        <asp:TableCell ForeColor="Black">Användarnamn:</asp:TableCell>
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
                            <asp:Button ID="cmdRecover" Text="Skicka lösen" runat="server" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                        </asp:TableCell>
                    </asp:TableRow>                          
               </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>           
    </div>
    <br />
    <asp:Panel runat="server" ID="pnlWrongBrowser" ForeColor="Red" Visible="false">
        Den här websidan är optimerad för Internet Explorer 5 eller högre. Du har <asp:Label runat="server" ID="lblBrowser"></asp:Label> <asp:Label runat="server" ID="lblBrowserVersion"></asp:Label>.<br />
        Vissa funktioner kommer kanske inte fungera som förväntat.
    </asp:Panel>
</asp:Content>
