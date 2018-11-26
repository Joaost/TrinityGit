<%@ Import Namespace="System.Data.SqlClient" %> 

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Trinity.Master" CodeBehind="campaign.aspx.vb" Inherits="TrinityViewer.campaign" %>

<%@ Register Assembly="TrinityViewer" Namespace="TrinityViewer" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="Main" runat="server" ID="cntCampaign">
    .iutbs<script runat="server">
    Public Campaign As New TrinityViewer.TrinityViewer.cCampaignInfo
        
    Protected Sub Test(ByVal sender As Object, ByVal e As EventArgs) Handles cmbShow.SelectedIndexChanged, cmbTRP.SelectedIndexChanged
        cmbTRP.visible=true
        UpdateChart()
    End Sub
        
    Public Sub UpdateChart()
        If Campaign.Channels.Count = 0 Then Exit Sub
        If cmbShow.Text = "Channel" Then
            chtMonitor.Dimensions = 0
            For Each TmpChan As TrinityViewer.TrinityViewer.cChannelInfo In Campaign.Channels
                Dim UseIt As Boolean = False
                For Each TmpBT As TrinityViewer.TrinityViewer.cBookingTypeInfo In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        UseIt = True
                    End If
                Next
                If UseIt Then
                    chtMonitor.Dimensions += 1
                    chtMonitor.DimensionLabel(chtMonitor.Dimensions - 1) = TmpChan.ChannelName
                    If cmbTRP.Text = "TRP" Then
                        chtmonitor.numberformat="N1"
                        chtMonitor.Data(chtMonitor.Dimensions - 1) = TmpChan.PlannedTRP
                    Else
                        chtmonitor.numberformat="C0"
                        chtMonitor.Data(chtMonitor.Dimensions - 1) = TmpChan.NetBudget
                    End If
                End If
            Next
            chtMonitor.Draw()
        ElseIf cmbShow.Text = "Allocation" Then
            chtMonitor.Dimensions = 0
            For Each TmpChan As TrinityViewer.TrinityViewer.cChannelInfo In Campaign.Channels
                For Each TmpBT As TrinityViewer.TrinityViewer.cBookingTypeInfo In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        chtMonitor.Dimensions += 1
                        chtMonitor.DimensionLabel(chtMonitor.Dimensions - 1) = TmpChan.ChannelName & " " & TmpBT.ShortName
                        chtMonitor.Data(chtMonitor.Dimensions - 1) = 0
                        For Each TmpWeek As TrinityViewer.TrinityViewer.cWeekInfo In TmpBT.Weeks
                            If cmbTRP.Text = "TRP" Then
                                chtmonitor.numberformat="N1"
                                chtMonitor.Data(chtMonitor.Dimensions - 1) += TmpWeek.TRP
                            Else
                                chtmonitor.numberformat="C0"
                                chtMonitor.Data(chtMonitor.Dimensions - 1) += TmpWeek.NetBudget
                            End If
                        Next
                    End If
                Next
            Next
            chtMonitor.Draw()
        ElseIf cmbShow.Text = "Week" Then
            chtMonitor.Dimensions = Campaign.Channels(1).BookingTypes(1).Weeks.Count
            For Each TmpChan As TrinityViewer.TrinityViewer.cChannelInfo In Campaign.Channels
                For Each TmpBT As TrinityViewer.TrinityViewer.cBookingTypeInfo In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        Dim i As Integer = 0
                        For Each TmpWeek As TrinityViewer.TrinityViewer.cWeekInfo In TmpBT.Weeks
                            chtMonitor.DimensionLabel(i) = TmpWeek.Name
                            If cmbTRP.Text = "TRP" Then
                                chtmonitor.numberformat="N1"
                                chtMonitor.Data(i) += TmpWeek.TRP
                            Else
                                chtmonitor.numberformat="C0"
                                chtMonitor.Data(i) += TmpWeek.NetBudget
                            End If
                            i += 1
                        Next
                    End If
                Next
            Next
            chtMonitor.Draw()
        ElseIf cmbShow.Text = "Film" Then
            chtMonitor.Dimensions = Campaign.Channels(1).BookingTypes(1).Weeks(1).films.Count
            For Each TmpChan As TrinityViewer.TrinityViewer.cChannelInfo In Campaign.Channels
                For Each TmpBT As TrinityViewer.TrinityViewer.cBookingTypeInfo In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        For Each TmpWeek As TrinityViewer.TrinityViewer.cWeekInfo In TmpBT.Weeks
                            Dim i As Integer = 0
                            for each TmpFilm as TrinityViewer.TrinityViewer.cFilmInfo in tmpweek.Films 
                                chtMonitor.DimensionLabel(i) = TmpFilm.Name
                                If cmbTRP.Text = "TRP" Then
                                    chtmonitor.numberformat="N1"
                                    chtMonitor.Data(i) += TmpWeek.TRP * (tmpfilm.Share/100)
                                Else
                                    chtmonitor.numberformat="C0"
                                    chtMonitor.Data(i) += TmpWeek.NetBudget * (TmpFilm.BudgetShare /100)
                                End If                                
                                i+=1
                            next
                        Next
                    End If
                Next
            next
            chtMonitor.Draw()
        ElseIf cmbShow.Text = "Reach" Then
            chtMonitor.Dimensions = 10
            chtMonitor.NumberFormat="N1"
            For i as integer=1 to 10
                chtMonitor.DimensionLabel(i-1) = i &"+"
                chtMonitor.Data(i-1)=Campaign.ReachPlanned(i)
            next
            chtMonitor.Draw()
            cmbTRP.visible=false
       End If
    End Sub
</script><%
        Dim CampID As String = Request.QueryString("id")
        Dim CampConn As SqlConnection
        Dim CampCmd As SqlCommand
        Dim Camp As String
      
        CampConn = New SqlConnection("server=STO-APP60;database=extranet;uid=johanh;pwd=turbo")
        CampConn.Open()
        CampCmd = New SqlCommand("SELECT campaign FROM campaign WHERE id='" & CampID & "'", CampConn)
        Camp = CampCmd.ExecuteScalar
        
        Campaign.LoadCampaignInfo(Camp)
        
     %>
    <table border="0" width="100%">
        <tr>
            <td style="font-size:large; width: 100%;"><% Response.Write(Campaign.Name)%></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Cost to client:" Font-Bold="true"></asp:Label> 
                <% Response.Write(format(Campaign.BudgetTotalCTC,"C0"))%>           
            </td>
        </tr>
        <tr>
            <td style="font-size:x-small; " colspan="2">
                <% Response.Write(format(Campaign.startdate,"Short date") &" - " & format(Campaign.enddate,"Short date")) %>
            </td>
        </tr>
       <tr>
           <td style="">
                <asp:DropDownList ID="cmbShow" runat="server" AutoPostBack="true" Width="160px">
                    <asp:ListItem Selected="True">Channel</asp:ListItem>
                    <asp:ListItem>Allocation</asp:ListItem>
                    <asp:ListItem>Week</asp:ListItem>
                    <asp:ListItem>Film</asp:ListItem>
                    <asp:ListItem>Reach</asp:ListItem>
                </asp:DropDownList>&nbsp;
                <asp:DropDownList ID="cmbTRP" runat="server" AutoPostBack="true">
                    <asp:ListItem Selected="True">TRP</asp:ListItem>
                    <asp:ListItem>Budget</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 100%" colspan="2">
                <cc1:BarChart ID="chtMonitor" runat="server" Height="504px" Width="432px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Font-Bold="true" Text="Documents:"></asp:Label>
                <table>
                    <%
                    Dim DocConn as SqlConnection
                    Dim cmd As SqlCommand
                    
                    DocConn = New SqlConnection("server=STO-APP60;database=extranet;uid=johanh;pwd=turbo")
                    DocConn.Open()
                    cmd = New SqlCommand("SELECT id,name,mimetype FROM docs WHERE campaignid=" & CampID , DocConn)
                    Dim rd as SqlDataReader
                    rd=cmd.ExecuteReader
                    While rd.read
                        Response.Write("<tr>")
                        Select Case rd!MIMEType
                            Case "application/msexcel"
                                Response.Write("<td><img src='Pics/exceldoc.gif' border='0'>")
                        End Select
                        Response.Write("<td><a href='docs.aspx?id=" & rd!id & "'>" & rd!Name & "</a></td><tr>")
                    end while
                    %>
                </table>
            </td>
        </tr>
        <tr>
            <td style="">
                <%
                    Dim ID As String = Request.QueryString("id")
                    Dim conn As SqlConnection
                    Dim auth as Boolean 
                    
                    conn = New SqlConnection("server=STO-APP60;database=extranet;uid=johanh;pwd=turbo")
                    conn.Open()
                    cmd = New SqlCommand("SELECT authorized FROM campaign WHERE id='" & ID & "'", conn)
                    auth= cmd.ExecuteScalar 
                    
                    if not auth then
                        Response.Write("<a href='authorize.aspx?id=" & id &  "'>Authorize booking</a>")
                    else
                        Response.Write("Campaign has been autorized for booking")
                    end if
                 %>
            </td>
        </tr>
<%
            UpdateChart()
 %>
    </table>
</asp:Content>
