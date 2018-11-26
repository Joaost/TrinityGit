<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Balthazar.Master" CodeBehind="booking.aspx.vb" Inherits="BaltazarWeb.booking" 
    title="Bokning" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>


<asp:Content ID="cntBooking" ContentPlaceHolderID="Main" runat="server">
    <script type="text/javascript" language="javascript">
        function update(elemValue)
        {
            document.getElementById('ctl00_Main_txtStore').innerText = elemValue[0];
            document.getElementById('ctl00_Main_txtAddress').innerText = elemValue[1];
            document.getElementById('ctl00_Main_txtCity').innerText = elemValue[2];
            document.getElementById('ctl00_Main_txtPhoneNr').innerText = elemValue[3];
        }
        function addWkColumn(tblId, wkStart) {
            var tbl = document.getElementById(tblId);
            if (tbl == null) {
                return 0;
            }
            var tblBodyObj = tbl.tBodies[0];
            for (var i = 0; i < tblBodyObj.rows.length; i++) {
                // Month Header
                if (i == 0) {
                    // Add extra colspan column
                    tblBodyObj.rows[i].cells[0].colSpan = 8;
                }
                // Week Header
                if (i == 1) {
                    // Add week column headline
                    var newCell = tblBodyObj.rows[i].insertCell(0);
                    newCell.innerHTML = 'V';
                    newCell.style.fontSize = '8px';
                    newCell.style.fontWeight = 'bold';
                    newCell.style.verticalAlign = 'bottom';
                    newCell.style.backgroundColor = '#ffffee';
                }
                // Normal row
                if (i >= 2) {
                    // Add the weeknumbers 
                    var newCell = tblBodyObj.rows[i].insertCell(0);
                    newCell.innerHTML = wkStart;
                    wkStart += 1;
                    newCell.style.fontSize = '8px';
                    newCell.style.backgroundColor = '#ffffee';
                }
            }
        }
        function validateDates(oSrc, args) {
            if (!(document.getElementById('ctl00_Main_lblNoDates') == null)) {
                args.IsValid = false;
                document.getElementById('ctl00_Main_lblNoDates').style.color = 'red';
            } else {
            args.IsValid = true;
            }
        }
        function validatePage() {
            Page_ClientValidate();
            args=new Object;
            validateDates(null,args);
            if (!(Page_IsValid && args.IsValid)) {
                alert('Vissa obligatoriska uppgifter är inte ifyllda.');
            }
        }
        function validateTimes(oSrc, args) {
            var days = document.getElementById("ctl00$Main$txtDateCount").value;
            var foundDays = 0;
            var nextDay = 1;
            while (foundDays<days) {
                if (document.getElementById("optTime" + nextDay) == null) {
                    nextDay += 1;
                }
                else {
                    if (!(document.forms[0]['optTime'+nextDay][0].status || document.forms[0]['optTime'+nextDay][1].status)) {
                        args.IsValid = false;
                        return;
                    }
                    nextDay += 1;
                    foundDays += 1;
                }
            }
            args.isValid = true;
        }
    </script>
    <asp:Panel runat="server" ID="pnlSalesman">
        <asp:Table runat="server" Font-Size="12px" Font-Names="Arial">
            <asp:TableRow runat="server">
                <asp:TableCell VerticalAlign="Top">Önskade datum:</asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Left">  
                    <asp:GridView ID="grdDates" runat="server" ShowHeader="false" AutoGenerateColumns="false" GridLines="None">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# format(DataBinder.Eval(Container.DataItem,"Date"),"yyyy-MM-dd") %>'></asp:Label>                            
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                                
                                    <input name='<%# "optTime" & DataBinder.Eval(Container.DataItem,"ID") %>' type="radio" value="1" <%# cstr("checked").Substring(0,-7* (Databinder.Eval(Container.DataItem,"Time")=1))%> /><%#CStr("10-18").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) < 6)) & CStr("10-15").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) > 5))%> 
                                    <input name='<%# "optTime" & DataBinder.Eval(Container.DataItem,"ID") %>' type="radio" value="2" <%# cstr("checked").Substring(0,-7* (Databinder.Eval(Container.DataItem,"Time")=2))%> /><%#CStr("11-19").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) < 6)) & CStr("11-16").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) > 5))%>                                                                         
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                            
                                    <asp:LinkButton runat="server" CausesValidation="false" Text="Ta bort" Font-Size="6" ForeColor="Red" OnCommand="DeleteDate" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>         
                    <asp:Label runat="server" ID="lblNoDates" Text="Inga datum är valda." Font-Size="10px" Font-Italic="true"></asp:Label>
                    <asp:CustomValidator ID="valDates" runat="server" ControlToValidate="" ClientValidationFunction="validateDates"></asp:CustomValidator>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    <asp:LinkButton runat="server" ID="lblAddDates" Text="Välj datum" CausesValidation="false" ForeColor="#009999" Font-Size="14px"></asp:LinkButton>
                </asp:TableCell>
                <asp:TableCell RowSpan="8" VerticalAlign="Top" runat="server">
                    <asp:Panel runat="server" ID="pnlAddDates" Visible="false" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" >   
                        <asp:Table runat="server">
                            <asp:TableRow VerticalAlign="Top" runat="server">
                                <asp:TableCell runat="server">
                                    <asp:HiddenField runat="server" ID="hdnDate" />
                                    <asp:Calendar runat="server" ID="calDates" 
                                        BackColor="White" BorderColor="#999999" CellPadding="4" 
                                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                        Height="180px" Width="200px" FirstDayOfWeek="Monday" >
                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                            <SelectorStyle BackColor="#CCCCCC" />
                                            <WeekendDayStyle BackColor="#FFFFCC" />
                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <OtherMonthDayStyle ForeColor="#808080" />
                                            <NextPrevStyle VerticalAlign="Bottom" />
                                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                            <TitleStyle BackColor="#1C5E55" BorderColor="Black" Font-Bold="True" />        
                                    </asp:Calendar>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button runat="server" ID="cmdAddDates" Text="Lägg till" CausesValidation="false" Font-Size="10px" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55"  />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>                 
                    </asp:Panel>                
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell><asp:CustomValidator ID="valTimes" ControlToValidate="" ClientValidationFunction="validateTimes" runat="server" Font-Size="14px" Font-Bold="true" Text="Tid måste väljas för alla datum"></asp:CustomValidator>
</asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Wrap="true">Antal produkter att demonstrera:</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="cmbProducts" AutoPostBack="true" >
                        <asp:ListItem Value="1">1</asp:ListItem> 
                        <asp:ListItem Value="2">2</asp:ListItem> 
                        <asp:ListItem Value="3">3</asp:ListItem> 
                    </asp:DropDownList>
                </asp:TableCell>
           </asp:TableRow>
           <asp:TableRow runat="server">
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell ColumnSpan="2">                            
                    <asp:Table runat="server" ID="tblProducts">
                        <asp:TableRow runat="server" ID="rowProduct1" Visible="true">
                            <asp:TableCell>Produkt</asp:TableCell>
                            <asp:TableCell><asp:TextBox runat="server" ID="txtProduct1"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" ID="rowProduct2" Visible="false">
                            <asp:TableCell>Produkt</asp:TableCell>
                            <asp:TableCell><asp:TextBox runat="server" ID="txtProduct2"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>   
                        <asp:TableRow runat="server" ID="rowProduct3" Visible="false">
                            <asp:TableCell>Produkt</asp:TableCell>
                            <asp:TableCell><asp:TextBox runat="server" ID="txtProduct3"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>                                       
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" ID="rowCollaborations">
                <asp:TableCell VerticalAlign="Top">Samdemo:</asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top" ColumnSpan="3">
                    <asp:CheckBox runat="server" ID="chkCollaboration" Text="Ja" AutoPostBack="true" />
                    <asp:Table runat="server" ID="tblCollaboration" Visible="false">
                        <asp:TableRow>
                            <asp:TableCell>Partners:</asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" Width="100%">
                                <asp:DropDownList runat="server" ID="cmbCollaborations" AutoPostBack="true">
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" ID="rowCollaboration1" Visible="true">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Table runat="server" ID="tblPartner1"  BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth ="1px">
                                    <asp:TableHeaderRow BackColor="#1C5E55">
                                        <asp:TableHeaderCell ColumnSpan="2">Partner 1</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Företag
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerCompany1"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Produkt
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerProduct1"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            Fakturaandel
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerInvoiceShare1" Width="50px"></asp:TextBox>%
                                        </asp:TableCell>
                                    </asp:TableRow>  
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Referens
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerReference1"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                             
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Telefon nr
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerPhoneNr1"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Faktureringsadress
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerInvoiceAddress1"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Postnr & ort
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerZipCode1"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" ID="rowCollaboration2" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Table runat="server" ID="Table1"  BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth ="1px">
                                    <asp:TableHeaderRow BackColor="#1C5E55">
                                        <asp:TableHeaderCell ColumnSpan="2">Partner 2</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Företag
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerCompany2"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Produkt
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerProduct2"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            Fakturaandel
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerInvoiceShare2" Width="50px"></asp:TextBox>%
                                        </asp:TableCell>
                                    </asp:TableRow>  
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Referens
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerReference2"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                             
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Telefon nr
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerPhoneNr2"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Faktureringsadress
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerInvoiceAddress2"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Postnr & ort
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerZipCode2"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" ID="rowCollaboration3" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Table runat="server" ID="tblCollaboration3"  BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth ="1px">
                                    <asp:TableHeaderRow BackColor="#1C5E55">
                                        <asp:TableHeaderCell ColumnSpan="2">Partner 3</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Företag
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerCompany3"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Produkt
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerProduct3"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            Fakturaandel
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerInvoiceShare3" Width="50px"></asp:TextBox>%
                                        </asp:TableCell>
                                    </asp:TableRow>  
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Referens
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerReference3"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                             
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Telefon nr
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerPhoneNr3"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Faktureringsadress
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerInvoiceAddress3"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            Postnr & ort
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPartnerZipCode3"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>       
            <asp:TableRow>
                <asp:TableCell>
                    Butik:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtStore" MaxLength="50" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valStore" ControlToValidate="txtStore" ErrorMessage="&nbsp;*" Font-Bold="true" Font-Size="14px" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:LinkButton runat="server" ID="lnkFindStore" CausesValidation="false" Text="Hitta butik" ForeColor="#009999"></asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>                                                       
            <asp:TableRow>
                <asp:TableCell>
                    Adress:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtAddress" MaxLength="50" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valAddress" ControlToValidate="txtAddress" ErrorMessage="&nbsp;*" Font-Bold="true" Font-Size="14px" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell>
                    Ort:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtCity" MaxLength="50" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valCity" ControlToValidate="txtCity" ErrorMessage="&nbsp;*" Font-Bold="true" Font-Size="14px" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell>
                    Tel butik:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtPhoneNr" MaxLength="50" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valPhoneNr" ControlToValidate="txtPhoneNr" ErrorMessage="&nbsp;*" Font-Bold="true" Font-Size="14px" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell>
                    Kontaktperson:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtContact" MaxLength="50" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valContact" ControlToValidate="txtContact" ErrorMessage="&nbsp;*" Font-Bold="true" Font-Size="14px" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
              </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell>
                    Placering butik:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtPlacement" MaxLength="200" Width="200px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top">
                    Övriga önskemål:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtWishlist" Rows="4" TextMode="MultiLine"  Width="200px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow runat="server">
                <asp:TableCell>
                    Demobolag:
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:DropDownList runat="server" ID="cmbCompany" AutoPostBack="true" AppendDataBoundItems="true">
                        <asp:ListItem Text="MEC Access väljer" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:TextBox runat="server" ID="txtCompany" Visible="false" MaxLength="100"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell>
                    Butikskök:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox runat="server" ID="chkKitchen" Text="Butikens eget kök skall användas" />
                </asp:TableCell>
            </asp:TableRow>                                                         
        </asp:Table>
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button runat="server" ID="cmdSend" Text="Skicka bokning" OnClientClick="validatePage()" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" Font-Size="18px" />                
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Middle">
                    <asp:Label runat="server" ID="lblStatus" ForeColor="Red" Text=""></asp:Label>                
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlProvider" Visible="false" >
        <asp:Table runat="server" Font-Size="12px" Font-Names="Arial" ID="tblProvider">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true" Width="200px">
                    Företag:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lblCompany"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Säljare:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lblSalesPerson"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Mobil säljare:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lblMobile"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Datum:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:GridView runat="server" ID="grdProviderDates" AutoGenerateColumns="false" GridLines="None" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# format(DataBinder.Eval(Container.DataItem,"Date"),"yyyy-MM-dd") %>'></asp:Label>                            
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    kl <asp:Label runat="server" Text='<%#(CStr("10-18").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) < 6)) & CStr("10-15").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) > 5))).Substring(0, -5 * (DataBinder.Eval(Container.DataItem, "Time") = 1)) & (CStr("11-19").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) < 6)) & CStr("11-16").Substring(0, -5 * (Weekday(DataBinder.Eval(Container.DataItem, "Date"), Microsoft.VisualBasic.FirstDayOfWeek.Monday) > 5))).Substring(0, -5 * (DataBinder.Eval(Container.DataItem, "Time") = 2))%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                        
                    </asp:GridView>
                </asp:TableCell>                
            </asp:TableRow> 
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Antal produkter att demonstrera:
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    <asp:GridView runat="server" ID="grdProducts" AutoGenerateColumns="false" GridLines="None" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Databinder.Eval(Container.DataItem,"Product") %>                              
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top" Font-Bold="true">
                    Samdemo:
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblCollaboration" Text="Nej"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    &nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Table runat="server" ID="tblProviderCollaborations">
                        <asp:TableRow runat="server" ID="rowProviderCollaboration1" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Table runat="server" ID="Table2"  BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth ="1px">
                                    <asp:TableHeaderRow BackColor="#1C5E55">
                                        <asp:TableHeaderCell ColumnSpan="2">Partner 1</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Företag
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerCompany1"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Produkt
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerProduct1"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Fakturaandel
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerInvoiceShare1"></asp:Label>%
                                        </asp:TableCell>
                                    </asp:TableRow>  
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Referens
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerReference1"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                             
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Telefon nr
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerPhoneNr1"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Faktureringsadress
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerInvoiceAddress1"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Postnr & ort
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerZipCode1"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>                    
                        <asp:TableRow runat="server" ID="rowProviderCollaboration2" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Table runat="server" ID="Table3"  BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth ="1px">
                                    <asp:TableHeaderRow BackColor="#1C5E55">
                                        <asp:TableHeaderCell ColumnSpan="2">Partner 2</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Företag
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerCompany2"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Produkt
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerProduct2"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Fakturaandel
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerInvoiceShare2"></asp:Label>%
                                        </asp:TableCell>
                                    </asp:TableRow>  
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Referens
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerReference2"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                             
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Telefon nr
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerPhoneNr2"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Faktureringsadress
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerInvoiceAddress2"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Postnr & ort
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerZipCode2"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>                    
                        <asp:TableRow runat="server" ID="rowProviderCollaboration3" Visible="false">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Table runat="server" ID="Table4"  BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth ="1px">
                                    <asp:TableHeaderRow BackColor="#1C5E55">
                                        <asp:TableHeaderCell ColumnSpan="2">Partner 2</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Företag
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerCompany3"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Produkt
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerProduct3"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Fakturaandel
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerInvoiceShare3"></asp:Label>%
                                        </asp:TableCell>
                                    </asp:TableRow>  
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Referens
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerReference3"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                             
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Telefon nr
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerPhoneNr3"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Faktureringsadress
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerInvoiceAddress3"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="true">
                                            Postnr & ort
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label runat="server" ID="lblPartnerZipCode3"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>                                                
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>                    
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Demobolag:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblProvider"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat="server" Font-Bold="true">
                <asp:TableCell>
                    Namn personal:
                </asp:TableCell>
                <asp:TableCell ID="TableCell1" runat="server">
                    <asp:Label runat="server" ID="lblStaffName" Visible="false" Font-Bold="false"></asp:Label>
                    <asp:TextBox runat="server" ID="txtStaffName"></asp:TextBox>&nbsp;
                    <asp:Button runat="server" Text="Ändra" ID="cmdChangeStaffName" Visible="false" BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
                </asp:TableCell>
            </asp:TableRow> 
           <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Butik:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblStore"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Adress:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblAddress"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Ort:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblCity"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Tel butik:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblPhoneNr"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Kontaktperson:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblContact"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Placering i butik:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblPlacement"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>  
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Övriga önskemål:
                </asp:TableCell>
                <asp:TableCell runat="server" VerticalAlign="Top">
                    <asp:Label runat="server" ID="lblWishlist"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>   
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Kök:
                </asp:TableCell>                
                <asp:TableCell>
                    <asp:Label runat="server" ID="lblKitchen" Text="Butikens eget kök skall användas."></asp:Label>
                </asp:TableCell>
            </asp:TableRow>    
            <asp:TableRow ID="rowInstructions" runat="server" Visible="false">
                <asp:TableCell runat="server" VerticalAlign="Top" Font-Bold="true">
                    Dokument:
                </asp:TableCell>
                <asp:TableCell runat="server" ID="cellInstructions">
                    &nbsp;
                </asp:TableCell>
            </asp:TableRow>                                    
        </asp:Table> 
        <br />  
        <asp:Panel runat="server" ID="pnlButtons">
            <asp:Button runat="server" ID="cmdAccept" Text="Bekräfta"  BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />&nbsp;
            <asp:Button runat="server" ID="cmdReject" Text="Tacka nej"  BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" />
        </asp:Panel>
        <asp:LinkButton ID="lblBack" Visible="false" runat="server" Text="<-- Tillbaka" PostBackUrl="~/default.aspx" ForeColor="#009999"></asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
        <span style="font-size:14px; font-weight:bold;">Du har inte tillgång till den begärda sidan</span>
    </asp:Panel>
    <asp:TextBox runat="server" ID="txtDateCount" ></asp:TextBox>
</asp:Content>
