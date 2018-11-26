<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Balthazar.Master" CodeBehind="userinfo.aspx.vb" Inherits="BaltazarWeb.userinfo" 
    title="Inställningar" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <script type="text/javascript">
      function Navigate()
      {
        javascript:window.open("\info.aspx","infowindow","status=no,toolbar=no,scrollbars=yes,width=500,height=600");
      }    

    </script>
    <asp:Panel runat="server" ID="pnlStaff">
        <asp:Table runat="server" Font-Size="12px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Table ID="Table1" runat="server" Width="344px" >
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server" ID="cellFirstName">Förnamn</asp:TableCell>
                            <asp:TableCell ID="TableCell1" runat="server"><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowLastname" runat="server">
                            <asp:TableCell ID="TableCell2" runat="server">Efternamn</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server"><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowBirthday" runat="server">
                            <asp:TableCell ID="TableCell4" runat="server">Födelsedatum</asp:TableCell>
                            <asp:TableCell ID="TableCell5" runat="server"><asp:TextBox ID="txtBirthday" runat="server"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowGender" runat="server">
                            <asp:TableCell ID="TableCell6" runat="server">Kön</asp:TableCell>
                            <asp:TableCell ID="TableCell7" runat="server">
                                <asp:DropDownList ID="cmbGender" runat="server">
                                    <asp:ListItem Value="2">Man</asp:ListItem>
                                    <asp:ListItem Value="1">Kvinna</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowDriver" runat="server">
                            <asp:TableCell ID="TableCell8" runat="server">Körkort</asp:TableCell>
                            <asp:TableCell ID="TableCell9" runat="server">
                                <asp:CheckBoxList ID="lstDriver" runat="server" RepeatColumns="5">
                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                </asp:CheckBoxList>
                            </asp:TableCell>
                        </asp:TableRow>     
                        <asp:TableRow ID="TableRow5" runat="server"><asp:TableCell runat="server">&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow ID="rowEmail" runat="server">
                            <asp:TableCell ID="TableCell30" runat="server">E-mail</asp:TableCell>
                            <asp:TableCell ID="TableCell31" runat="server"><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowStreet1" runat="server">
                            <asp:TableCell ID="TableCell10" runat="server">Gatuadress 1</asp:TableCell>
                            <asp:TableCell ID="TableCell11" runat="server"><asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowStreet2" runat="server">
                            <asp:TableCell ID="TableCell12" runat="server">Gatudress 2</asp:TableCell>
                            <asp:TableCell ID="TableCell13" runat="server"><asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowZipCode" runat="server">
                            <asp:TableCell ID="TableCell14" runat="server">Postnummer</asp:TableCell>
                            <asp:TableCell ID="TableCell15" runat="server"><asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowZipArea" runat="server">
                            <asp:TableCell ID="TableCell16" runat="server">Postort</asp:TableCell>
                            <asp:TableCell ID="TableCell17" runat="server"><asp:TextBox ID="txtZipArea" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow10" runat="server"><asp:TableCell runat="server">&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow ID="rowHomephone" runat="server">
                            <asp:TableCell ID="TableCell18" runat="server">Hemtelefon</asp:TableCell>
                            <asp:TableCell ID="TableCell19" runat="server"><asp:TextBox ID="txtHomePhone" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowJobPhone" runat="server">
                            <asp:TableCell ID="TableCell20" runat="server">Jobbtelefon</asp:TableCell>
                            <asp:TableCell ID="TableCell21" runat="server"><asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowMobilePhone" runat="server">
                            <asp:TableCell ID="TableCell22" runat="server">Mobiltelefon</asp:TableCell>
                            <asp:TableCell ID="TableCell23" runat="server"><asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow20" runat="server"><asp:TableCell ID="TableCell34" runat="server">&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow ID="rowTasks1" runat="server">
                            <asp:TableCell VerticalAlign="Top">Önskade arbetsuppgifter</asp:TableCell>
                            <asp:TableCell>
                                <asp:ListBox ID="lstCategories" runat="server" SelectionMode="Multiple" Rows="8"></asp:ListBox>
                            </asp:TableCell>                        
                            <asp:TableCell VerticalAlign="Top">
                                <asp:LinkButton runat="server" OnClientClick="Navigate()"><asp:Image ID="Image1" runat="server" AlternateText="i" ImageUrl="~/Pics/information.gif" /></asp:LinkButton>
                                </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowTasks2" runat="server">
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell Font-Size="8" Font-Italic="true">Använd Ctrl för att välja flera. Om du valt "Speciell kompetens", var vänlig specificera i Info-rutan nedan.</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowInfo" runat="server">
                            <asp:TableCell ID="TableCell32" runat="server" VerticalAlign="Top">Information om dig</asp:TableCell>
                            <asp:TableCell ID="TableCell33" runat="server"><asp:TextBox Rows="6" runat="server" ID="txtInfo" TextMode="MultiLine"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow14" runat="server"><asp:TableCell runat="server">&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow ID="TableRow15" runat="server" Visible="false">
                            <asp:TableCell ID="TableCell24" runat="server">Bank</asp:TableCell>
                            <asp:TableCell ID="TableCell25" runat="server"><asp:TextBox ID="txtBank" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow16" runat="server" Visible="false">
                            <asp:TableCell ID="TableCell26" runat="server">Clearing nr</asp:TableCell>
                            <asp:TableCell ID="TableCell27" runat="server"><asp:TextBox ID="txtClearingNo" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow17" runat="server" Visible="false">
                            <asp:TableCell ID="TableCell28" runat="server">Kontonummer</asp:TableCell>
                            <asp:TableCell ID="TableCell29" runat="server"><asp:TextBox ID="txtAccountNo" runat="server"></asp:TextBox><br /></asp:TableCell>
                        </asp:TableRow>  
                        <asp:TableRow ID="TableRow19" runat="server" Visible="false">
                            <asp:TableCell ColumnSpan="2" runat="Server" Font-Italic="true" Font-Size="10px">Uppgifterna används vid löneutbetalning och kommer inte spridas vidare</asp:TableCell>
                        </asp:TableRow>   
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:LinkButton runat="server" ID="lblChangePassword" Text="Ändra lösenord" ForeColor="#009999"></asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" ID="rowChangePassword" Visible="false">
                            <asp:TableCell ColumnSpan="2" >
                                <asp:Table runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Nytt lösenord:" />
                                        <asp:TableCell><asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password"></asp:TextBox></asp:TableCell>                        
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Bekräfta lösenord:" />
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password"></asp:TextBox>
                                        </asp:TableCell>                        
                                    </asp:TableRow>
                                    <asp:TableRow ID="rowPasswordError" runat="server" Visible="false">
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:Label Text="" ForeColor="Red" runat="server" ID="lblPasswordError"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>         
                    </asp:Table>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Top" ID="cellPicture">
                    <asp:Table runat="server" ID="tblPicture">
                        <asp:TableRow VerticalAlign="Top">
                            <asp:TableCell>
                                <asp:Image runat="server" ID="imgUser" BorderStyle="Solid" BorderWidth="1" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:LinkButton runat="server" ID="lblUpload" Text="Ladda upp bild" ForeColor="#009999" />                         
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Panel runat="server" ID="pnlUpload" BorderWidth="1" Visible="false">
                        <asp:Table ID="Table2" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>Bild:</asp:TableCell>
                                <asp:TableCell>
                                    <asp:FileUpload ID="uplImage" runat="server"  BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />                
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Button runat="server" ID="cmdUpload" Text="Ladda upp"  BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table> 
                    </asp:Panel> 
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSalesman">
        
    </asp:Panel>
    <br />
    <asp:Button ID="cmdSave" runat="server" Text="Spara"  BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" /><br />
</asp:Content>
