<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="BaltazarWeb._Default" MasterPageFile="Balthazar.master" Title="MEC Access" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType TypeName="BaltazarWeb.BalthazarMaster" %>
    <asp:Content id="DefaultContents" contentplaceholderid="Main" runat="server">
        <script type="text/javascript">
            var launch = false;
            function launchModal() {
                launch = true;
            }
            function pageLoad() {
                if (launch) {
                    $find("mpeAlert").show();
                } 
            } 
        </script>
        <asp:Panel ID="pnlStaff" runat="server">
        <div style="position:static;">
            <div style="border-style: solid; border-width:1px; position:static;">
                <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                   <span style="vertical-align:middle">Lediga jobb</span> 
                </div>
            </div>        
           
            <div style="border-style: solid; border-width:1px; position:static;">
                <asp:DataList ID="lstAvailableJobs" runat="server"
                   BorderColor="black"
                   CellPadding="3"
                   Font-Name="Arial"
                   Font-Size="8pt" Visible="False">

                 <HeaderStyle BackColor="#AAAADD">
                 </HeaderStyle>
                       
                 <ItemTemplate>
                    <asp:LinkButton runat="server" PostBackUrl='<%# "availablework.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                 </ItemTemplate>
              </asp:DataList>
              <asp:Label ID="lblAvailableJobs" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns det inga lediga jobb." Visible="False"></asp:Label></div>
        </div>
        <br />
        <div style="position:static;">
            <div style="border-style: solid; border-width:1px; position:static;">
                <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                   <span style="vertical-align:middle">Pågående och kommande jobb</span> 
                </div>
            </div>        
           
            <div style="border-style: solid; border-width:1px; position:static;">
                <asp:DataList id="lstCurrentJobs" runat="server"
                   BorderColor="black"
                   CellPadding="3"
                   Font-Name="Arial"
                   Font-Size="8pt" Visible="False">

                 <HeaderStyle BackColor="#AAAADD">
                 </HeaderStyle>
                       
                 <ItemTemplate>
                    <asp:LinkButton runat="server" PostBackUrl='<%# "workschedule.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                 </ItemTemplate>
              </asp:DataList>
              <asp:Label ID="lblCurrentJobs" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns det inga pågående jobb." Visible="False"></asp:Label></div>
        </div>
        <br />
        <div>
            <div style="border-style: solid; border-width:1px;">
                <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px;">
                    <span style="vertical-align:middle">Frågeformulär</span> 
                </div>
            </div>
            <div style="border-style:solid; border-width:1px;">
                <asp:DataList id="lstQuestionaires" runat="server"
                   BorderColor="black"
                   CellPadding="3"
                   Font-Name="Arial"
                   Font-Size="8pt" Visible="False">

                 <HeaderStyle BackColor="#AAAADD">
                 </HeaderStyle>
                       
                 <ItemTemplate>
                    <asp:LinkButton runat="server" PostBackUrl='<%# "questionaire.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") %>' Text='<%#DataBinder.Eval(Container.DataItem, "Label")%>' ForeColor="#009999"></asp:LinkButton>
                </ItemTemplate>
              </asp:DataList>
              <asp:Label ID="lblQuestionaires" runat="server" style="font-style:italic; font-size:10px;" Text="Det finns inga frågeformulär." Visible="False"></asp:Label></div>
        </div>
        <br />
        <div style="position:static;">
            <div style="border-style: solid; border-width:1px; position:static;">
                <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                   <span style="vertical-align:middle">Besvarade frågeformulär</span> 
                </div>
            </div>        
           
            <div style="border-style: solid; border-width:1px; position:static;">
                <asp:DataList id="lstAnsweredQuestionaires" runat="server"
                   BorderColor="black"
                   CellPadding="3"
                   Font-Name="Arial"
                   Font-Size="8pt" Visible="False">

                 <HeaderStyle BackColor="#AAAADD">
                 </HeaderStyle>
                       
                 <ItemTemplate>
                    <asp:LinkButton runat="server" PostBackUrl='<%# "questionaire.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") & "&answerid=" & DataBinder.Eval(Container.DataItem, "AnswerID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Label") & " (" & Format(DataBinder.Eval(Container.DataItem, "AnswerDate"), "Short date") &")" %>' ForeColor="#009999"></asp:LinkButton>
                    <td><asp:LinkButton runat="server" PostBackUrl='<%# "deletequestionaireanswer.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") & "&answerid=" & DataBinder.Eval(Container.DataItem, "AnswerID") %>' Text="Ta bort" ForeColor="#009999"></asp:LinkButton></td> 
                 </ItemTemplate>
              </asp:DataList>
              <asp:Label ID="lblAnsweredQuestionaires" runat="server" style="font-style:italic; font-size:10px;" Text="Du har inte besvarat några frågeformulär." Visible="False"></asp:Label></div>
        </div>    
    </asp:Panel>
        <asp:Panel runat="server" ID="pnlSalesman">
            <asp:Panel runat="server" ID="pnlBooking">
                <div style="position:static;">
                    <div style="border-style: solid; border-width:1px; position:static;">
                        <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                           <span style="vertical-align:middle">Kampanjer för bokning</span> 
                        </div>
                    </div>
                    <div style="border-style: solid; border-width:1px; position:static;">
                        <asp:DataList ID="lstCampaigns" runat="server"
                            BorderColor="black"
                            CellPadding="3"
                            Font-Name="Arial"
                            Font-Size="8pt" Visible="False">

                        <HeaderStyle BackColor="#AAAADD">
                        </HeaderStyle>
                           
                        <ItemTemplate>
                        <asp:LinkButton runat="server" PostBackUrl='<%# "booking.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                        <asp:Label ID="Label2" runat="server" Text='<%#"(" & CheckNull(Container.DataItem!Daysleft) &" dag" & "ar".substring(2* int(CheckNull(Container.DataItem!Daysleft)=1)) & " kvar att boka)"%>' ForeColor='<%# BookedDaysColors(math.abs(int(CheckNull(Container.DataItem!DaysLeft)<0))) %>'></asp:Label>
                        </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lblNoCampaigns" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns det inga kampanjer för bokning." Visible="False"></asp:Label>
                    </div>
                </div>
                <br />
                <div style="position:static;">
                    <div style="border-style: solid; border-width:1px; position:static;">
                        <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                           <span style="vertical-align:middle">Bokade demonstrationer som avvaktar bekräftelse</span> 
                        </div>
                    </div>                       
                    <div style="border-style: solid; border-width:1px; position:static;">
                        <asp:GridView ID="grdPendingBookings" runat="server"
                            BorderColor="black"
                            CellPadding="3"
                            Font-Name="Arial"
                            Font-Size="8pt" Visible="False" AutoGenerateColumns="false" 
                            BorderStyle="None" GridLines="Vertical" >

                            <HeaderStyle BackColor="#AAAADD">                        
                            </HeaderStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Kampanj">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" PostBackUrl='<%# "booking.aspx?id=" & DataBinder.Eval(Container.DataItem, "eventid") &"&bookingid="& DataBinder.Eval(Container.DataItem, "ID")  %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                                    </ItemTemplate>                                
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Datum" DataField="Dates" />
                                <asp:BoundField HeaderText="Butik" DataField="Store" />                            
                                <asp:BoundField HeaderText="Ort" DataField="city" />                            
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblNoPendingBookings" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu väntar inga bokningar på att bli godkända." Visible="False"></asp:Label>
                    </div>
                </div>
                <br />
                <div style="position:static;">
                    <div style="border-style: solid; border-width:1px; position:static;">
                        <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                           <span style="vertical-align:middle">Ej godkända bokningar</span> 
                        </div>
                    </div>                       
                    <div style="border-style: solid; border-width:1px; position:static;">
                        <asp:GridView ID="grdRejectedBookings" runat="server"
                            BorderColor="black"
                            CellPadding="3"
                            Font-Name="Arial"
                            Font-Size="8pt" Visible="False" AutoGenerateColumns="false" >

                            <HeaderStyle BackColor="#AAAADD">                        
                            </HeaderStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Kampanj">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# "booking.aspx?id=" & DataBinder.Eval(Container.DataItem, "eventid") &"&bookingid="& DataBinder.Eval(Container.DataItem, "ID")  %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                                    </ItemTemplate>                                
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Datum" DataField="Dates" />
                                <asp:BoundField HeaderText="Butik" DataField="Store" />                            
                                <asp:BoundField HeaderText="Ort" DataField="city" />                            
                                <asp:BoundField HeaderText="Kommentar" DataField="rejectioncomment" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblNoRejectedBookings" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns inga bokningar som inte godkänts." Visible="true"></asp:Label>
                    </div>
                </div>
                <br />
            </asp:Panel>
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:14px; font-weight:bold; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align: middle">Bekräftade demonstrationer</span>    
                       <span><a href="excellist.aspx" target="_blank"><img style="vertical-align: middle" src="Pics/excel.gif" border="0" width="16" height="16" alt="Excel" /></a></span>
                    </div>
                </div>                       
                <div style="border-style: solid; border-width:1px; position:static;">
                    <asp:GridView ID="grdConfirmedBookings" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Kampanj">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" PostBackUrl='<%# "booking.aspx?id=" & DataBinder.Eval(Container.DataItem, "eventid") &"&bookingid="& DataBinder.Eval(Container.DataItem, "ID")  %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Datum" DataField="Dates" />
                            <asp:BoundField HeaderText="Butik" DataField="Store" />                            
                            <asp:BoundField HeaderText="Ort" DataField="city" />     
                            <asp:BoundField HeaderText="Namn personal" DataField="chosenproviderstaffname" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblNoConfirmedBookings" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu har du inga bekräftade demonstrationer." Visible="true"></asp:Label>
                </div>
            </div>    
            <br />    
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Återrapporterade demonstrationer</span> 
                    </div>
                </div>                         
                <div style="border-style: solid; border-width:1px; position:static;">
                     <asp:GridView ID="grdSalesmanQuestionaires" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" 
                        >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Rapport">
                                <ItemTemplate>
                                    <asp:LinkButton  runat="server" PostBackUrl='<%# "questionaire.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") & "&answerid=" & DataBinder.Eval(Container.DataItem, "AnswerID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Label")   %>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Datum">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "AnswerDate"), "Short date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Butik" DataField="Location" />                            
                            <asp:BoundField HeaderText="Ort" DataField="City" />                           
                        </Columns>
                    </asp:GridView>                       
                    <asp:Label ID="lblNoSalesmanQuestionaires" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns inga återrapporterade demonstrationer." Visible="False"></asp:Label></div>
                </div>
        </asp:Panel>
        <asp:Panel ID="pnlHeadOfSales" runat="server">
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:14px; font-weight:bold; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Bekräftade demonstrationer</span> 
                    </div>
                </div>                       
                <div style="border-style: solid; border-width:1px; position:static;">
                    <asp:GridView ID="grdHOSConfirmed" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Kampanj">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" PostBackUrl='<%# "booking.aspx?id=" & DataBinder.Eval(Container.DataItem, "eventid") &"&bookingid="& DataBinder.Eval(Container.DataItem, "ID")  %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Datum" DataField="Dates" />
                            <asp:BoundField HeaderText="Butik" DataField="Store" />                            
                            <asp:BoundField HeaderText="Ort" DataField="city" />     
                            <asp:BoundField HeaderText="Säljare" DataField="salesman" />
                            <asp:BoundField HeaderText="Namn personal" DataField="chosenproviderstaffname" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblHOSNoComfirmed" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu har du inga bekräftade demonstrationer." Visible="true"></asp:Label>
                </div>
            </div>    
            <br />    
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Återrapporterade demonstrationer</span> 
                    </div>
                </div>                         
                <div style="border-style: solid; border-width:1px; position:static;">
                     <asp:GridView ID="grdHOSQuestionaires" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" 
                        >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Rapport">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" PostBackUrl='<%# "questionaire.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") & "&answerid=" & DataBinder.Eval(Container.DataItem, "AnswerID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Label")   %>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Datum">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "AnswerDate"), "Short date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Butik" DataField="Location" />                            
                            <asp:BoundField HeaderText="Ort" DataField="City" />                           
                        </Columns>
                    </asp:GridView>                       
                    <asp:Label ID="lblHOSNoQuestionaires" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns inga återrapporterade demonstrationer." Visible="False"></asp:Label>
                </div>
            </div>     
            <br />
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Sammanställningar av återrapporter</span> 
                    </div>
                </div>                         
                <div style="border-style: solid; border-width:1px; position:static;">
                     <asp:GridView ID="grdQuestionaireSummary" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" 
                        >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Rapport">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" PostBackUrl='<%# "questionairesummary.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") & "&qid=" & DataBinder.Eval(Container.DataItem, "QuestionaireID")%>' Text='<%# DataBinder.Eval(Container.DataItem, "Name")   %>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>                      
                        </Columns>
                    </asp:GridView>                       
                    <asp:Label ID="lblNoQuestionaireSummary" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns inga återrapporterade demonstrationer." Visible="False"></asp:Label>
                </div>
            </div>             
        </asp:Panel>
        <asp:Panel ID="pnlProvider" runat="server">
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Ej bekräftade bokningar</span> 
                    </div>
                </div>                       
                <div style="border-style: solid; border-width:1px; position:static;">
                    <asp:GridView ID="grdOfferedBookings" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Kampanj">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" PostBackUrl='<%# "booking.aspx?id=" & DataBinder.Eval(Container.DataItem, "eventid") &"&bookingid="& DataBinder.Eval(Container.DataItem, "ID")  %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Datum" DataField="Dates" />
                            <asp:BoundField HeaderText="Butik" DataField="Store" />                            
                            <asp:BoundField HeaderText="Ort" DataField="city" />                            
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="lblNoOfferedBookings" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns det inga bokningar." Visible="False"></asp:Label>
                </div>
            </div>
            <br />            
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:14px; font-weight:bold; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Bekräftade bokningar</span> 
                    </div>
                </div>                       
                <div style="border-style: solid; border-width:1px; position:static;">
                    <asp:GridView ID="grdAcceptedBookings" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Kampanj">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl='<%# "booking.aspx?id=" & DataBinder.Eval(Container.DataItem, "eventid") &"&bookingid="& DataBinder.Eval(Container.DataItem, "ID")  %>' Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Datum" DataField="Dates" />
                            <asp:BoundField HeaderText="Butik" DataField="Store" />                            
                            <asp:BoundField HeaderText="Ort" DataField="city" />                            
                            <asp:BoundField HeaderText="Namn personal" DataField="chosenproviderstaffname" />
                            
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblNoAcceptedBookings" runat="server" style="font-style:italic; font-size:10px;" Text="Just nu finns det inga bokningar." Visible="False"></asp:Label>
                </div>
            </div>
            <br /> 
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Ej avrapporterade dagar</span> 
                    </div>
                </div>                       
                <div style="border-style: solid; border-width:1px; position:static;">
                     <asp:GridView ID="grdNotAnsweredProviderQuestionaires" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" 
                        >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Rapport">
                                <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl='<%# "questionaire.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") & "&answerid=" & DataBinder.Eval(Container.DataItem, "AnswerID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Label")   %>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Datum">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "AnswerDate"), "Short date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Butik" DataField="Location" />                            
                            <asp:BoundField HeaderText="Ort" DataField="City" />         
                         </Columns>
                    </asp:GridView>                       
                    <asp:Label ID="lblNoNotAnsweredProviderQuestionaires" runat="server" style="font-style:italic; font-size:10px;" Text="Det finns inga frågeformulär som inte är avrapporterade." Visible="False"></asp:Label>
                </div>
            </div>            
            <br /> 
            <div style="position:static;">
                <div style="border-style: solid; border-width:1px; position:static;">
                    <div style="background-color: gray; color:White; font-size:12px; height: 20px; border-style:solid; border-width:1px; position:static;">
                       <span style="vertical-align:middle">Avrapporterade dagar</span> 
                    </div>
                </div>                       
                <div style="border-style: solid; border-width:1px; position:static;">
                     <asp:GridView ID="grdAnsweredProviderQuestionaires" runat="server"
                        BorderColor="black"
                        CellPadding="3"
                        Font-Name="Arial"
                        Font-Size="8pt" Visible="False" AutoGenerateColumns="false" 
                        >

                        <HeaderStyle BackColor="#AAAADD">                        
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Rapport">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" PostBackUrl='<%# "questionaire.aspx?id=" & DataBinder.Eval(Container.DataItem, "ID") & "&answerid=" & DataBinder.Eval(Container.DataItem, "AnswerID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Label")   %>' ForeColor="#009999"></asp:LinkButton>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Datum">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "AnswerDate"), "Short date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Butik" DataField="Location" />                            
                            <asp:BoundField HeaderText="Ort" DataField="City" />         
                        </Columns>
                    </asp:GridView>                       
                    <asp:Label ID="lblNoAnsweredProviderQuestionaires" runat="server" style="font-style:italic; font-size:10px;" Text="Det finns inga avrapporterade frågeformulär." Visible="False"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <br />
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upnMail">
            <ContentTemplate>
                <asp:Panel ID="pnlContact" runat="server" BorderStyle="Solid" BorderWidth="2px" 
                    CssClass="paddedPanel" >
                    Vid frågor eller kommentarer var vänlig kontakta MEC Access:<br />
                    <br />
                    <asp:Table ID="tblPhoneNumbers" runat="server" Width="225px">
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">Karin</asp:TableCell>
                            <asp:TableCell runat="server">08 - 463 15 34</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">Ann</asp:TableCell>
                            <asp:TableCell runat="server">08 - 463 15 38</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                
                    <br />
                    Eller klicka
                    <asp:LinkButton ID="lnkMail" runat="server" ForeColor="#009999">här</asp:LinkButton>
                    &nbsp;för att maila.<br />
                    <br />
                    <asp:Panel ID="pnlMailForm" runat="server" Font-Size="12px" Visible="False">
                        Din e-mail:<br />
                        <asp:TextBox ID="txtMailFrom" runat="server" Width="300px"></asp:TextBox>
                        <br />
                        <br />
                        Ditt meddelande:<br />
                        <asp:TextBox ID="txtMailBody" runat="server" Rows="5" TextMode="MultiLine" 
                            Width="300px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="cmdSendMail" runat="server" BackColor="White" 
                            BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" ForeColor="#1C5E55" 
                            Text="Skicka" Width="83px"/>
                        <br />
                        <br />
                        <asp:Label ID="lblAlertMessage" runat="server" Width="600px" Height="60px"></asp:Label>
                    </asp:Panel>
               </asp:Panel>                
            </ContentTemplate>        
    </asp:UpdatePanel>
    </asp:Content>

