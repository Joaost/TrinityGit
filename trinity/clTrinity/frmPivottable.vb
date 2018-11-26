Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.IO


Public Class frmPivottable
    '4 variables for the calculated pivot copy
    'they are declared up here since we need to access them from different subs/functions
    Dim leftFrame As Windows.Forms.Panel
    Dim topFrame As Windows.Forms.Panel
    Dim cX As List(Of List(Of String))
    Dim cY As List(Of List(Of String))
    'this boolean is used to skip updateing cellvalues while other actions are in progress
    Dim bolDontUpdate As Boolean
    'color of the label on the created chart
    Dim c As Color

    'this variable holds the label width used
    Dim intColWidth As Integer

    Dim rs As New ADODB.Recordset

    Dim fields As String() = {"Daypart", "Channel", "Bookingtype", "Film", "Filmcode", "Weekday", "Week", "Status", "Unit", "Value"}
    'Dim OWC11 As New OWC11Wrapper.PivotMemberWrapper

    Private Sub frmPivot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim TmpRS As New ADODB.Recordset

        rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        rs.CursorType = ADODB.CursorTypeEnum.adOpenDynamic
        rs.LockType = ADODB.LockTypeEnum.adLockOptimistic
        rs.Fields.Append("Daypart", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Channel", ADODB.DataTypeEnum.adChar, 255)
        rs.Fields.Append("Bookingtype", ADODB.DataTypeEnum.adChar, 100)
        rs.Fields.Append("Film", ADODB.DataTypeEnum.adChar, 255)
        rs.Fields.Append("Filmcode", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Weekday", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Week", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Status", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Unit", ADODB.DataTypeEnum.adChar, 255)
        rs.Fields.Append("Value", ADODB.DataTypeEnum.adSingle)
        rs.Open()

        pvt.DataSource = rs
        pvt.CreateGraphics()

        setupPivot()
        TmpRS.DataSource = rs.DataSource
        With pvt
            With .ActiveView
                '.RowAxis.InsertFieldSet(.FieldSets("Unit"))
                pvt.ActiveView.RowAxis.InsertFieldSet(.FieldSets("Unit"))
                .DataAxis.InsertTotal(pvt.ActiveView.AddTotal("Sum", pvt.ActiveView.FieldSets("Value").Fields(0), Microsoft.Office.Interop.Owc11.PivotTotalFunctionEnum.plFunctionSum))
                '.FieldSets("Unit").AddCalculatedField("NetCPP", "NetCPP", "NetCPP", "[Value]")
                .Totals("Sum").NumberFormat = "##,##0.0"
                .AllowEdits = False
                .AllowAdditions = False
                .AllowDeletions = False
                .TitleBar.Caption = ""
                .TitleBar.Visible = False
                .FieldSets("Unit").Fields(0).Subtotals(0) = False
            End With
            .DisplayDesignTimeUI = False
            .DisplayFieldList = False
            .DisplayOfficeLogo = False
            .DisplayToolbar = False
            .AllowPropertyToolbox = False
            .Commands(Microsoft.Office.Interop.Owc11.PivotCommandId.plCommandDropzones).Execute()
            .ActiveData.HideDetails()
            .Refresh()
        End With
        With chtPivot
            '.DisplayOfficeLogo = False
            .DisplayFieldList = False
            .DisplayFieldButtons = False
            .DisplayToolbar = False
            .AllowLayoutEvents = True
            .HasChartSpaceLegend = True
            .HasChartSpaceTitle = False
            .Border.Color = RGB(255, 255, 255)
            .AllowUISelection = True
            .AllowRenderEvents = True
            .AllowPointRenderEvents = True
        End With
        pvt.ActiveView.FieldSets("Value").DisplayInFieldList = False
        pvt.ActiveView.FieldLabelFont.Name = "Segoe UI"
        pvt.ActiveView.PropertyCaptionFont.Name = "Segoe UI"
        pvt.ActiveView.PropertyValueFont.Name = "Segoe UI"
        pvt.ActiveView.TotalFont.Name = "Segoe UI"
        pvt.ActiveView.FieldLabelFont.Size = 8
        pvt.ActiveView.PropertyValueFont.Size = 8
        pvt.ActiveView.PropertyCaptionFont.Size = 8
        pvt.ActiveView.TotalFont.Size = 8
        For i = 0 To pvt.ActiveView.FieldSets.Count - 1
            pvt.ActiveView.FieldSets(i).Fields(0).Subtotals(0) = False
            pvt.ActiveView.FieldSets(i).Fields(0).GroupedFont.Name = "Segoe UI"
            pvt.ActiveView.FieldSets(i).Fields(0).SubtotalFont.Size = 8
            pvt.ActiveView.FieldSets(i).Fields(0).GroupedFont.Size = 8
            pvt.ActiveView.FieldSets(i).Fields(0).SubtotalLabelFont.Size = 8
            pvt.ActiveView.FieldSets(i).AllIncludeExclude = Microsoft.Office.Interop.Owc11.PivotFieldSetAllIncludeExcludeEnum.plAllInclude
        Next
        For i = 0 To pvt.ActiveView.Totals.Count - 1
            pvt.ActiveView.Totals(i).Field.GroupedFont.Size = 8
            pvt.ActiveView.Totals(i).Field.GroupedFont.Name = "Segoe UI"
            pvt.ActiveView.Totals(i).Field.SubtotalFont.Size = 8
            pvt.ActiveView.Totals(i).Field.GroupedFont.Size = 8
            pvt.ActiveView.Totals(i).Field.SubtotalLabelFont.Size = 8
        Next
        SetupSorting()
        chtPivot.DataSource = Nothing
        chtPivot.DataSource = pvt.GetOcx
    End Sub

    Public Sub setupPivot()

        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim TmpFilm As Trinity.cFilm
        Dim TmpBookedSpot As Trinity.cBookedSpot
        Dim TmpPlannedSpot As Trinity.cPlannedSpot

        Dim DP As Short
        Dim Percent As Single
        Dim WD As Short
        Dim WDs As Object
        Dim WeekD As String
        Dim Disc As Single
        Dim Mnth As String
        Dim Months As Object
        Dim Net As Decimal
        Dim Gross As Decimal
        Dim NetCPP30 As Single
        Dim UseSpots As New Dictionary(Of Trinity.cBookingType, Boolean)

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        'System.Windows.Forms.Application.DoEvents()
        WDs = New Object() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        Months = New Object() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    If Campaign.PlannedSpots.TotalNetBudget(TmpChan.ChannelName, TmpBT.Name) = TmpBT.ConfirmedNetBudget Then
                        UseSpots.Add(TmpBT, True)
                    Else
                        UseSpots.Add(TmpBT, False)
                    End If
                    For Each TmpWeek In TmpBT.Weeks
                        For Each TmpFilm In TmpWeek.Films
                            For DP = 0 To TmpBT.Dayparts.Count - 1
                                For WD = 1 To 7
                                    Percent = ((TmpFilm.Share / 100) * (TmpBT.Dayparts(DP).Share / 100)) / 7
                                    WeekD = WDs(WD - 1)
                                    Mnth = Months(Month(Date.FromOADate(TmpWeek.StartDate + WD - 1)) - 1)
                                    Net = TmpWeek.NetBudget
                                    Gross = TmpWeek.GrossBudget
                                    NetCPP30 = TmpWeek.NetCPP30
                                    If Net = 0 Then
                                        Disc = 0
                                    Else
                                        Disc = Gross / Net
                                    End If
                                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Planned", "TRP Main", TmpWeek.TRP * Percent)
                                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Planned", "TRP Second", TmpWeek.TRP * Percent * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget))
                                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Planned", "TRP Buying", TmpWeek.TRPBuyingTarget * Percent)
                                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Planned", "'000 Main", (TmpWeek.TRP * Percent / 100) * Campaign.MainTarget.UniSize)
                                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Planned", "'000 Buying", (TmpWeek.TRPBuyingTarget * Percent / 100) * TmpBT.BuyingTarget.Target.UniSize)
                                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Planned", "Gross Budget", ((TmpWeek.TRPBuyingTarget * Percent) * NetCPP30 * (TmpFilm.Index / 100)) * Disc)
                                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Planned", "Net Budget", (TmpWeek.TRPBuyingTarget * Percent) * NetCPP30 * (TmpFilm.Index / 100))
                                    If Not UseSpots(TmpBT) Then
                                        Percent = ((TmpWeek.TRPBuyingTarget * Percent) * NetCPP30 * (TmpFilm.Index / 100)) / TmpBT.PlannedNetBudget
                                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Confirmed", "Gross Budget", TmpBT.ConfirmedNetBudget * Percent * Disc)
                                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Confirmed", "Net Budget", TmpBT.ConfirmedNetBudget * Percent)
                                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Actual", "Gross Budget", TmpBT.ConfirmedNetBudget * Percent * Disc)
                                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBT.Dayparts(DP).Name, TmpChan.ChannelName, TmpBT.Name, TmpFilm, TmpFilm.Filmcode, WeekD, TmpWeek.Name, Mnth, "Actual", "Net Budget", TmpBT.ConfirmedNetBudget * Percent)
                                    End If
                                Next
                            Next
                        Next TmpFilm
                    Next TmpWeek
                End If
            Next TmpBT
        Next TmpChan
        For Each TmpBookedSpot In Campaign.BookedSpots
            If TmpBookedSpot.Bookingtype.BookIt Then
                WeekD = WDs(Weekday(TmpBookedSpot.AirDate, FirstDayOfWeek.Monday) - 1)
                AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name, TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, Months(Month(TmpBookedSpot.AirDate) - 1), "Booked", "TRP Main", TmpBookedSpot.MyEstimate)
                AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name, TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, Months(Month(TmpBookedSpot.AirDate) - 1), "Booked", "TRP Second", TmpBookedSpot.MyEstimate * (TmpBookedSpot.Bookingtype.IndexSecondTarget / TmpBookedSpot.Bookingtype.IndexMainTarget))
                AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name, TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, Months(Month(TmpBookedSpot.AirDate) - 1), "Booked", "TRP Buying", TmpBookedSpot.MyEstimateBuyTarget)
                AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name, TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, Months(Month(TmpBookedSpot.AirDate) - 1), "Booked", "'000 Main", (TmpBookedSpot.MyEstimate / 100) * Campaign.MainTarget.UniSize)
                AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name, TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, Months(Month(TmpBookedSpot.AirDate) - 1), "Booked", "'000 Buying", (TmpBookedSpot.MyEstimateBuyTarget / 100) * TmpBookedSpot.Bookingtype.BuyingTarget.Target.UniSize)
                AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name, TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, Months(Month(TmpBookedSpot.AirDate) - 1), "Booked", "Gross Budget", TmpBookedSpot.GrossPrice)
                AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name, TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, Months(Month(TmpBookedSpot.AirDate) - 1), "Booked", "Net Budget", TmpBookedSpot.NetPrice)
            End If
        Next TmpBookedSpot
        For Each TmpPlannedSpot In Campaign.PlannedSpots
            If TmpPlannedSpot.Bookingtype.BookIt Then
                WeekD = WDs(Weekday(Date.FromOADate(TmpPlannedSpot.MaM), FirstDayOfWeek.Monday) - 1) 'TmpPlannedSpot.AirDate
                If Not TmpPlannedSpot.Week Is Nothing Then
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Confirmed", "TRP Main", TmpPlannedSpot.MyRating)
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Confirmed", "TRP Second", TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget))
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Confirmed", "TRP Buying", TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Confirmed", "'000 Main", (TmpPlannedSpot.MyRating / 100) * Campaign.MainTarget.UniSize)
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Confirmed", "'000 Buying", (TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / 100) * TmpPlannedSpot.Bookingtype.BuyingTarget.Target.UniSize)
                    If UseSpots(TmpPlannedSpot.Bookingtype) Then
                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Confirmed", "Gross Budget", TmpPlannedSpot.PriceGross)
                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Confirmed", "Net Budget", TmpPlannedSpot.PriceNet)
                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Actual", "Gross Budget", TmpPlannedSpot.PriceGross)
                        AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name, TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1), "Actual", "Net Budget", TmpPlannedSpot.PriceNet)
                    End If
                End If
            End If
        Next TmpPlannedSpot
        For Each TmpActualSpot As Trinity.cActualSpot In Campaign.ActualSpots
            If TmpActualSpot.Bookingtype.BookIt Then
                WeekD = WDs(Weekday(Date.FromOADate(TmpActualSpot.MaM), FirstDayOfWeek.Monday) - 1) 'TmpActualSpot.AirDate
                If Not TmpActualSpot.Week Is Nothing Then
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpActualSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpActualSpot.MaM).Name, TmpActualSpot.Channel.ChannelName, TmpActualSpot.Bookingtype.Name, TmpActualSpot.Week.Films(TmpActualSpot.Filmcode), TmpActualSpot.Filmcode, WeekD, TmpActualSpot.Week.Name, Months(Month(Date.FromOADate(TmpActualSpot.AirDate)) - 1), "Actual", "TRP Main", TmpActualSpot.Rating)
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpActualSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpActualSpot.MaM).Name, TmpActualSpot.Channel.ChannelName, TmpActualSpot.Bookingtype.Name, TmpActualSpot.Week.Films(TmpActualSpot.Filmcode), TmpActualSpot.Filmcode, WeekD, TmpActualSpot.Week.Name, Months(Month(Date.FromOADate(TmpActualSpot.AirDate)) - 1), "Actual", "TRP Second", TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget))
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpActualSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpActualSpot.MaM).Name, TmpActualSpot.Channel.ChannelName, TmpActualSpot.Bookingtype.Name, TmpActualSpot.Week.Films(TmpActualSpot.Filmcode), TmpActualSpot.Filmcode, WeekD, TmpActualSpot.Week.Name, Months(Month(Date.FromOADate(TmpActualSpot.AirDate)) - 1), "Actual", "TRP Buying", TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget))
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpActualSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpActualSpot.MaM).Name, TmpActualSpot.Channel.ChannelName, TmpActualSpot.Bookingtype.Name, TmpActualSpot.Week.Films(TmpActualSpot.Filmcode), TmpActualSpot.Filmcode, WeekD, TmpActualSpot.Week.Name, Months(Month(Date.FromOADate(TmpActualSpot.AirDate)) - 1), "Actual", "'000 Main", TmpActualSpot.Rating000 * 1000)
                    AddToPivot(Campaign.Name, Campaign.Client, Campaign.Product, TmpActualSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpActualSpot.MaM).Name, TmpActualSpot.Channel.ChannelName, TmpActualSpot.Bookingtype.Name, TmpActualSpot.Week.Films(TmpActualSpot.Filmcode), TmpActualSpot.Filmcode, WeekD, TmpActualSpot.Week.Name, Months(Month(Date.FromOADate(TmpActualSpot.AirDate)) - 1), "Actual", "'000 Buying", TmpActualSpot.Rating000(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget) * 1000)
                End If
            End If
        Next
        ' chtSummary.DataSource = Nothing
        'chtSummary.DataSource = pvtSummary
        pvt.Refresh()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Sub AddToPivot(ByVal Campaign As String, ByVal Client As String, ByVal Product As String, ByVal Daypart As String, ByVal Channel As String, ByVal Bookingtype As String, ByVal Film As Trinity.cFilm, ByVal Filmcode As String, ByVal Weekday As String, ByVal week As String, ByVal Month As String, ByVal Status As String, ByVal Unit As String, ByVal Value As Single)
        rs.AddNew()
        rs.Fields(0).Value = Daypart
        rs.Fields(1).Value = Channel
        rs.Fields(2).Value = Bookingtype
        If Film Is Nothing Then
            rs.Fields(3).Value = "<Unknown>"
        Else
            rs.Fields(3).Value = Film.Name
        End If
        rs.Fields(4).Value = Filmcode
        rs.Fields(5).Value = Weekday
        rs.Fields(6).Value = week
        rs.Fields(7).Value = Status
        rs.Fields(8).Value = Unit
        rs.Fields(9).Value = Value
        If Not rs.Fields(9).Value.ToString = Value.ToString Then
            Stop
        End If
    End Sub

    Private Sub cmdDefine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefine.Click
        Dim frmDef As New frmDefine(pvt)
        frmDef.ShowDialog()
    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub TabPage2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage2.Enter
        cmdExcel.Enabled = False
        cmdChartLayout.Visible = True
        cmdDefine.Visible = False
        cmdPictureToCB.Enabled = True
        cmdSaveToFile.Enabled = True
        CreateChart(pvt, chtPivot)
    End Sub

    Sub CreateChart(ByVal pivot As AxMicrosoft.Office.Interop.Owc11.AxPivotTable, ByVal chart As AxMicrosoft.Office.Interop.Owc11.AxChartSpace)
        Dim Categories() = {}
        Dim Values()

        ReDim Values(pvt.ActiveData.ColumnMembers.Count - 1)
        chtPivot.ScreenUpdating = False
        For i As Integer = 0 To pvt.ActiveData.ColumnMembers.Count - 1
            ReDim Preserve Categories(pvt.ActiveData.ColumnMembers.Count - 1)
            Categories(i) = pvt.ActiveData.ColumnMembers(i).Caption
        Next
        'chart.Clear()
        'With chart.Charts.Add
        '    For i As Integer = 0 To pvt.ActiveData.RowMembers.Count - 1
        '        With .SeriesCollection.Add
        '            .Caption = pvt.ActiveData.RowMembers(i).Caption
        '        End With
        '        For c As Integer = 0 To pvt.ActiveData.ColumnMembers.Count - 1
        '            Values(c) = pvt.ActiveData.Cells(pvt.ActiveData.RowMembers(i), pvt.ActiveData.ColumnMembers(c)).Aggregates(0).Value
        '        Next
        '        With .SeriesCollection(i)
        '            .SetData(Microsoft.Office.Interop.Owc11.ChartDimensionsEnum.chDimCategories, chtPivot.Constants.chDataLiteral, Categories)
        '            .SetData(Microsoft.Office.Interop.Owc11.ChartDimensionsEnum.chDimValues, chtPivot.Constants.chDataLiteral, Values)
        '        End With
        '    Next
        'End With
        chart.DataSource = pivot.GetOcx
        chtPivot.ScreenUpdating = True

    End Sub

    Private Sub chtPivot_BeforeContextMenu(ByVal sender As Object, ByVal e As AxMicrosoft.Office.Interop.Owc11.IChartEvents_BeforeContextMenuEvent) Handles chtPivot.BeforeContextMenu
        e.cancel.Value = True
    End Sub

    Private Sub chtPivot_DataSetChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtPivot.DataSetChange
        If Not chtPivot.Charts.Count = 0 Then
            For i As Integer = 0 To chtPivot.Charts(0).SeriesCollection.Count - 1
                chtPivot.Charts(0).SeriesCollection(i).Interior.Color = chtPivot.Charts(0).SeriesCollection(i).Line.Color
            Next
        End If
    End Sub

    Private Sub chtPivot_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chtPivot.Enter

    End Sub

    Private Sub pvt_BeforeContextMenu(ByVal sender As Object, ByVal e As AxMicrosoft.Office.Interop.Owc11.IPivotControlEvents_BeforeContextMenuEvent) Handles pvt.BeforeContextMenu
        e.cancel.Value = True
    End Sub

    Private Sub cmdPictureToCB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPictureToCB.Click
        Dim TmpFile As String = My.Computer.FileSystem.GetTempFileName & ".gif"
        My.Computer.FileSystem.WriteAllBytes(TmpFile, chtPivot.GetPicture("gif", 800, 600), False)
        Dim img As New Drawing.Bitmap(TmpFile)

        My.Computer.Clipboard.SetImage(img)
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        pvt.Export()
    End Sub

    Private Sub cmdSaveToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveToFile.Click
        If tabPivot.SelectedIndex = 1 Then
            Dim dlg As New Windows.Forms.SaveFileDialog
            dlg.OverwritePrompt = True
            dlg.DefaultExt = "*.gif"
            dlg.Filter = "CompuServe GIF|*.gif|JPEG|*.jpg"
            dlg.FileName = "chart.gif"
            If dlg.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
            If dlg.FilterIndex = 1 Then
                My.Computer.FileSystem.WriteAllBytes(dlg.FileName, chtPivot.GetPicture("gif", 800, 600), False)
            Else
                My.Computer.FileSystem.WriteAllBytes(dlg.FileName, chtPivot.GetPicture("jpg", 800, 600), False)
            End If
        Else
            Dim dlg As New Windows.Forms.SaveFileDialog
            dlg.OverwritePrompt = True
            dlg.DefaultExt = "*.htm"
            dlg.Filter = "HTML|*.htm|XML|*.xml"
            dlg.FileName = "pivot.htm"
            If dlg.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
            If dlg.FilterIndex = 1 Then
                My.Computer.FileSystem.WriteAllText(dlg.FileName, pvt.HTMLData, False)
            Else
                My.Computer.FileSystem.WriteAllText(dlg.FileName, pvt.XMLData, False)
            End If
        End If
    End Sub

    Private Sub TabPage1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage1.Enter
        cmdExcel.Enabled = True
        cmdChartLayout.Visible = False
        cmdDefine.Visible = True
        cmdPictureToCB.Enabled = False
        cmdSaveToFile.Enabled = True
    End Sub

    Sub SetupSorting()

        Dim SortArray As Object
        Dim i As Integer

        With pvt
            With .ActiveView
                ReDim SortArray(0 To Campaign.Channels.Count - 1)
                For i = 1 To Campaign.Channels.Count
                    SortArray(i - 1) = Campaign.Channels(i).ChannelName
                Next
                .FieldSets("Channel").Fields(0).OrderedMembers = SortArray
                .FieldSets("Channel").Fields(0).SortDirection = Microsoft.Office.Interop.Owc11.PivotFieldSortDirectionEnum.plSortDirectionCustom

                ReDim SortArray(0 To Campaign.Channels(1).BookingTypes.Count)
                For i = 1 To Campaign.Channels(1).BookingTypes.Count
                    SortArray(i - 1) = Campaign.Channels(1).BookingTypes(i).Name
                Next
                .FieldSets("Bookingtype").Fields(0).OrderedMembers = SortArray
                .FieldSets("Bookingtype").Fields(0).SortDirection = Microsoft.Office.Interop.Owc11.PivotFieldSortDirectionEnum.plSortDirectionCustom

                ReDim SortArray(0 To Campaign.Channels(1).BookingTypes(1).Weeks.Count)
                For i = 1 To Campaign.Channels(1).BookingTypes(1).Weeks.Count
                    SortArray(i - 1) = Campaign.Channels(1).BookingTypes(1).Weeks(i).Name
                Next
                .FieldSets("Week").Fields(0).OrderedMembers = SortArray
                .FieldSets("Week").Fields(0).SortDirection = Microsoft.Office.Interop.Owc11.PivotFieldSortDirectionEnum.plSortDirectionCustom

                'TODO: ***DAYPARTS*** Will not really work. All dayparts from all bookingtypes should be added
                ReDim SortArray(0 To Campaign.Dayparts.Count - 1)
                For i = 0 To Campaign.Dayparts.Count - 1
                    SortArray(i) = Campaign.Dayparts(i).Name
                Next
                .FieldSets("Daypart").Fields(0).OrderedMembers = SortArray
                .FieldSets("Daypart").Fields(0).SortDirection = Microsoft.Office.Interop.Owc11.PivotFieldSortDirectionEnum.plSortDirectionCustom

                Dim StatusArray() As Object = {"Planned", "Booked", "Confirmed", "Actual"}
                .FieldSets("Status").Fields(0).OrderedMembers = StatusArray
                .FieldSets("Status").Fields(0).SortDirection = Microsoft.Office.Interop.Owc11.PivotFieldSortDirectionEnum.plSortDirectionCustom

                Dim DayArray() As Object = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
                .FieldSets("Weekday").Fields(0).OrderedMembers = DayArray
                .FieldSets("Weekday").Fields(0).SortDirection = Microsoft.Office.Interop.Owc11.PivotFieldSortDirectionEnum.plSortDirectionCustom
            End With
        End With

    End Sub

    Private Sub cmdChartLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChartLayout.Click
        Dim frmLayout As New frmChartLayout
        Dim sr As Microsoft.Office.Interop.Owc11.ChSeries
        Dim clr As Color
        Dim ub As Integer = 0
        Dim Primary As New List(Of Microsoft.Office.Interop.Owc11.ChSeries)
        Dim Secondary As New List(Of Microsoft.Office.Interop.Owc11.ChSeries)
        Dim PrimaryRow As New Dictionary(Of Microsoft.Office.Interop.Owc11.ChSeries, Integer)
        Dim SecondaryRow As New Dictionary(Of Microsoft.Office.Interop.Owc11.ChSeries, Integer)
        Dim SecondAxis As Microsoft.Office.Interop.Owc11.ChAxis = Nothing

        Dim TmpAxis As Microsoft.Office.Interop.Owc11.ChAxis

        frmLayout.grdSeries.Rows.Clear()

        For Each TmpAxis In chtPivot.Charts(0).Axes
            If TmpAxis.Position = Microsoft.Office.Interop.Owc11.ChartAxisPositionEnum.chAxisPositionRight Then
                SecondAxis = TmpAxis
                Exit For
            End If
        Next
        For Each sr In chtPivot.Charts(0).SeriesCollection
            Dim idx As Integer = frmLayout.grdSeries.Rows.Add()
            Dim gfx As Bitmap
            clr = Color.FromArgb(sr.Line.Color)
            clr = Color.FromArgb(255, clr.B, clr.G, clr.R)
            gfx = New Bitmap(10, 10)
            For x As Integer = 0 To 9
                For y As Integer = 0 To 9
                    If x = 0 OrElse y = 0 OrElse x = 9 OrElse y = 9 Then
                        gfx.SetPixel(x, y, Color.Black)
                    Else
                        gfx.SetPixel(x, y, clr)
                    End If
                Next
            Next
            frmLayout.grdSeries.Rows(idx).Cells(0).Value = sr.Caption
            With DirectCast(frmLayout.grdSeries.Rows(idx).Cells(1), Windows.Forms.DataGridViewImageCell)
                .ValueIsIcon = False
                .Value = gfx
            End With
            With DirectCast(frmLayout.grdSeries.Rows(idx).Cells(2), ExtendedComboBoxCell)
                .Items.add("Bar chart")
                .Items.add("Line chart")
                Select Case sr.Type
                    Case Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeColumnClustered
                        .Value = "Bar chart"
                    Case Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeLine
                        .Value = "Line chart"
                End Select
            End With
            With DirectCast(frmLayout.grdSeries.Rows(idx).Cells(3), ExtendedComboBoxCell)
                .Items.add("Primary")
                .Items.add("Secondary")
                If SecondAxis Is Nothing OrElse Not SecondAxis.Scaling Is sr.Scalings(Microsoft.Office.Interop.Owc11.ChartDimensionsEnum.chDimValues) Then
                    .Value = "Primary"
                Else
                    .Value = "Secondary"
                End If
            End With
        Next
        If chtPivot.HasChartSpaceTitle Then
            frmLayout.txtTitle.Text = chtPivot.ChartSpaceTitle.Caption
        End If
        If frmLayout.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            chtPivot.DataSource = Nothing
            chtPivot.DataSource = pvt.GetOcx
            If frmLayout.txtTitle.Text = "" Then
                chtPivot.HasChartSpaceTitle = False
            Else
                chtPivot.HasChartSpaceTitle = True
                chtPivot.ChartSpaceTitle.Caption = frmLayout.txtTitle.Text
            End If
            If Not frmLayout.Tag Is Nothing And chtPivot.Charts.Count > 0 Then
                For Each TmpAxis In chtPivot.Charts(0).Axes
                    TmpAxis.Font.Name = frmLayout.Tag
                Next
                For Each TmpSeries As Microsoft.Office.Interop.Owc11.ChSeries In chtPivot.Charts(0).SeriesCollection
                    For Each TmpLabel As Microsoft.Office.Interop.Owc11.ChDataLabel In TmpSeries.DataLabelsCollection
                        TmpLabel.Font.Name = frmLayout.Tag
                    Next
                Next
                chtPivot.ChartSpaceLegend.Font.Name = frmLayout.Tag
                If chtPivot.HasChartSpaceTitle Then
                    chtPivot.ChartSpaceTitle.Font.Name = frmLayout.Tag
                End If
            End If
            Dim gfx As Bitmap
            For i As Integer = 0 To frmLayout.grdSeries.Rows.Count - 1
                sr = chtPivot.Charts(0).SeriesCollection(frmLayout.grdSeries.Rows(i).Cells(0).Value)
                gfx = frmLayout.grdSeries.Rows(i).Cells(1).Value
                clr = gfx.GetPixel(6, 6)
                sr.Line.Color = RGB(clr.R, clr.G, clr.B)
                sr.Line.Color = RGB(clr.R, clr.G, clr.B)
                Select Case frmLayout.grdSeries.Rows(i).Cells(3).Value
                    Case "Primary"
                        Primary.Add(sr)
                        PrimaryRow.Add(sr, i)
                    Case "Secondary"
                        Secondary.Add(sr)
                        SecondaryRow.Add(sr, i)
                End Select
            Next
            If Primary.Count = 0 Then
                Windows.Forms.MessageBox.Show("At least one series must be allocated to the Primary axis.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                Exit Sub
            End If

            Select Case frmLayout.grdSeries.Rows(PrimaryRow(Primary(0))).Cells(2).Value
                Case "Bar chart"
                    chtPivot.Charts(0).Type = Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeColumnClustered
                Case "Line chart"
                    chtPivot.Charts(0).Type = Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeLine
            End Select
            For i As Integer = 0 To Primary.Count - 1
                'Primary(i).Ungroup(True)
                Select Case frmLayout.grdSeries.Rows(PrimaryRow(Primary(i))).Cells(2).Value
                    Case "Line chart"
                        Primary(i).Type = Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeLine
                End Select
            Next

            If Secondary.Count > 0 Then
                Secondary(0).Ungroup(True)
                TmpAxis = chtPivot.Charts(0).Axes.Add(Secondary(0).Scalings(Microsoft.Office.Interop.Owc11.ChartDimensionsEnum.chDimValues))
                With TmpAxis
                    .Position = Microsoft.Office.Interop.Owc11.ChartAxisPositionEnum.chAxisPositionRight
                    .HasMajorGridlines = False
                End With
                Select Case frmLayout.grdSeries.Rows(SecondaryRow(Secondary(0))).Cells(2).Value
                    Case "Bar chart"
                        Secondary(0).Type = Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeColumnClustered
                    Case "Line chart"
                        Secondary(0).Type = Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeLine
                End Select
                For i As Integer = 1 To Secondary.Count - 1
                    Secondary(i).Ungroup(False)
                    Secondary(i).Group(Secondary(0))
                    Select Case frmLayout.grdSeries.Rows(SecondaryRow(Secondary(i))).Cells(2).Value
                        Case "Bar chart"
                            Secondary(i).Type = Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeColumnClustered
                        Case "Line chart"
                            Secondary(i).Type = Microsoft.Office.Interop.Owc11.ChartChartTypeEnum.chChartTypeLine
                    End Select
                Next
            End If

            chtPivot.Refresh()
        End If
    End Sub


    Private Sub cmdAddReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddReport.Click
        Dim TmpName As String
        Dim FileName As String
        TmpName = InputBox("Name of report:", "T R I N I T Y", "New report")
        If TmpName = "" Then Exit Sub
        If Not My.Computer.FileSystem.DirectoryExists(Trinity.Helper.Pathify(TrinitySettings.LocalDataPath) & "Reports") Then
            My.Computer.FileSystem.CreateDirectory(Trinity.Helper.Pathify(TrinitySettings.LocalDataPath) & "Reports")
        End If
        FileName = CreateGUID() & ".xml"

        Dim XMLDoc As New Xml.XmlDocument
        Dim Node As Xml.XmlNode
        Dim TmpNode As Xml.XmlElement
        Dim PivotNode As Xml.XmlElement
        Dim ChartNode As Xml.XmlElement
        Dim TmpReport As Xml.XmlElement
        Dim ReportDoc As New Xml.XmlDocument

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

        XMLDoc.PreserveWhitespace = True
        Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
        XMLDoc.AppendChild(Node)

        Node = XMLDoc.CreateComment("Trinity report.")
        XMLDoc.AppendChild(Node)

        TmpNode = XMLDoc.CreateElement("Report")
        TmpNode.SetAttribute("Name", TmpName)
        XMLDoc.AppendChild(TmpNode)

        PivotNode = XMLDoc.CreateElement("Pivot")
        ChartNode = XMLDoc.CreateElement("Chart")

        ReportDoc.LoadXml(chtPivot.XMLData)
        TmpReport = XMLDoc.ImportNode(ReportDoc.FirstChild, True)
        ChartNode.AppendChild(TmpReport)

        ReportDoc.LoadXml(pvt.XMLData)
        TmpReport = XMLDoc.ImportNode(ReportDoc.FirstChild, True)
        PivotNode.AppendChild(TmpReport)

        TmpNode.AppendChild(ChartNode)
        TmpNode.AppendChild(PivotNode)

        XMLDoc.Save(Trinity.Helper.Pathify(TrinitySettings.LocalDataPath) & "Reports\" & FileName)

    End Sub

    Private Sub cmdReports_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReports.DropDownOpening

        Dim TmpNode As Xml.XmlElement

        cmdReports.DropDownItems.Clear()
        If Not My.Computer.FileSystem.DirectoryExists(Trinity.Helper.Pathify(TrinitySettings.LocalDataPath) & "Reports") Then
            Exit Sub
        Else
            For Each file As IO.FileInfo In My.Computer.FileSystem.GetDirectoryInfo(Trinity.Helper.Pathify(TrinitySettings.LocalDataPath) & "Reports").GetFiles
                Dim XMLDoc As New Xml.XmlDocument
                XMLDoc.Load(file.FullName)
                TmpNode = XMLDoc.GetElementsByTagName("Report").Item(0)
                With cmdReports.DropDownItems.Add(TmpNode.GetAttribute("Name"))
                    .Tag = TmpNode.OuterXml
                    AddHandler .Click, AddressOf LoadReport
                End With
            Next
        End If
    End Sub

    Sub LoadReport(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim XMLDoc As New Xml.XmlDocument

        XMLDoc.LoadXml(sender.tag)
        pvt.XMLData = XMLDoc.GetElementsByTagName("Pivot").Item(0).FirstChild.OuterXml
        chtPivot.XMLData = XMLDoc.GetElementsByTagName("Chart").Item(0).FirstChild.OuterXml

        chtPivot.DataSource = pvt.GetOcx

    End Sub

    Private Sub cmdRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub calcChart_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles calcChart.Enter
        c = Color.DarkGray
        bolDontUpdate = True

        cmdExcel.Enabled = False
        cmdChartLayout.Visible = False
        cmdDefine.Visible = False
        cmdPictureToCB.Enabled = False
        cmdSaveToFile.Enabled = False


        If Not leftFrame Is Nothing Then
            leftFrame.Dispose()
        End If
        If Not topFrame Is Nothing Then
            topFrame.Dispose()
        End If
        grdCalcChart.Rows.Clear()
        grdCalcChart.Columns.Clear()
        grdCalcChart.RowTemplate.Height = 14

        'get the fileds from the leftside
        cX = createPivotObject(pvt.ActiveView.RowAxis.FieldSets)
        'get the frames from the top
        cY = createPivotObject(pvt.ActiveView.ColumnAxis.FieldSets)

        leftFrame = createLeftSide(cX)
        topFrame = createTopSide(cY)
        If Not leftFrame Is Nothing Then
            panChart.Controls.Add(leftFrame)
            leftFrame.Show()
            leftFrame.BringToFront()
            leftFrame.Left = -1
            Try
                leftFrame.Top = topFrame.Height - 1
            Catch
                leftFrame.Top = -1
            End Try
        End If
        If Not topFrame Is Nothing Then
            panChart.Controls.Add(topFrame)
            topFrame.Show()
            topFrame.BringToFront()
            Try
                topFrame.Left = leftFrame.Width - 1
            Catch
                topFrame.Left = -1
            End Try
            topFrame.Top = -1
        End If

        Try
            grdCalcChart.Left = leftFrame.Width - 2
        Catch
            grdCalcChart.Left = -2
        End Try
        Try
            grdCalcChart.Top = topFrame.Height - 2
        Catch ex As Exception
            grdCalcChart.Top = -2
        End Try


        'add row and columns according tot he size
        Dim sum As Integer = 1
        Dim i As Integer
        Dim j As Integer
        For j = 0 To cY.Count - 1
            sum = sum * cY(j).Count
        Next

        For i = 0 To sum - 1
            j = grdCalcChart.Columns.Add(i, i)
            grdCalcChart.Columns(j).Width = intColWidth - 1
        Next

        sum = 1
        For j = 0 To cX.Count - 1
            sum = sum * cX(j).Count
        Next
        grdCalcChart.Rows.Add(sum)
        Try
            grdCalcChart.Width = topFrame.Width + 1
        Catch
            grdCalcChart.Width = grdCalcChart.PreferredSize.Width
        End Try
        Try
            grdCalcChart.Height = leftFrame.Height + 1
        Catch
            grdCalcChart.Height = grdCalcChart.PreferredSize.Height
        End Try
        grdCalcChart.ScrollBars = Windows.Forms.ScrollBars.None

        bolDontUpdate = False
    End Sub

    Private Function createTopSide(ByVal list As List(Of List(Of String))) As Windows.Forms.Panel
        If list.Count = 0 Then Return Nothing
        Dim i As Integer
        Dim pLeaf As New Windows.Forms.Panel
        Dim p As New Windows.Forms.Panel
        Dim tmpLen As Integer = 0
        Dim count As Integer = 0
        Dim LP As New List(Of List(Of Windows.Forms.Panel))
        For i = 0 To list.Count - 1
            Dim l As New List(Of Windows.Forms.Panel)
            LP.Add(l)
        Next
        i = list.Count - 1
        While i > -1
            Dim tmpL As List(Of String) = list(i)
            If i = list.Count - 1 Then 'if we are att the leafs
                Dim j As Integer
                Dim sum As Integer = 1
                For j = 0 To i - 1
                    sum = sum * list(j).Count
                Next

                While sum > 0
                    j = 0
                    p = New Windows.Forms.Panel

                    'get the prefered width
                    Dim maxWidth As Integer = 60

                    Dim testL As New Windows.Forms.Label
                    For Each s As String In tmpL
                        testL.Text = s.Trim
                        If testL.PreferredWidth > maxWidth Then
                            maxWidth = testL.PreferredWidth + 20
                        End If
                    Next

                    'we want some space
                    maxWidth = maxWidth + 20

                    'we need to set the column width to match the labels
                    intColWidth = maxWidth


                    'create the leaf labels
                    For Each s As String In tmpL
                        Dim l As New Windows.Forms.Label
                        l.Text = s.Trim
                        l.Height = 15
                        l.Width = maxWidth
                        p.Controls.Add(l)
                        l.BringToFront()
                        l.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
                        l.Top = 0
                        l.Left = (maxWidth * j) - j
                        l.BackColor = c
                        j += 1
                    Next
                    p.Width = (maxWidth * j) - (j - 1)
                    p.Height = 15
                    p.SendToBack()
                    LP(i).Add(p)
                    sum -= 1
                End While
                pLeaf = p
            Else 'if not at leafs we need to add the leafs
                Dim j As Integer
                Dim sum As Integer = 1
                For j = 0 To i - 1
                    sum = sum * list(j).Count
                Next
                count = 0
                While sum > 0
                    p = New Windows.Forms.Panel
                    j = 0
                    For Each s As String In tmpL
                        Dim l As New Windows.Forms.Label
                        l.Text = s.Trim
                        p.Controls.Add(l)
                        l.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
                        l.Top = 0
                        l.Left = (j * pLeaf.Width) - j
                        l.BringToFront()
                        l.BackColor = c
                        Dim pT As Windows.Forms.Panel
                        pT = LP(i + 1)(count)
                        p.Controls.Add(pT)
                        pT.BringToFront()
                        pT.Top = 15
                        pT.Left = l.Left
                        l.Width = pT.Width
                        pT.BackColor = c
                        p.BackColor = c
                        p.SendToBack()
                        j += 1
                        count += 1
                        p.Height = pT.Height + 15
                    Next
                    p.Width = (pLeaf.Width * j) - (j - 1)
                    p.SendToBack()
                    LP(i).Add(p)
                    sum -= 1
                End While
                pLeaf = p
            End If
            i -= 1
        End While

        pLeaf = LP(0)(0)
        If i > 2 Then
            pLeaf.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
        End If
        Return pLeaf
    End Function


    Private Function createLeftSide(ByVal list As List(Of List(Of String))) As Windows.Forms.Panel
        If list.Count = 0 Then Return Nothing
        Dim i As Integer
        Dim pLeaf As New Windows.Forms.Panel
        Dim p As New Windows.Forms.Panel
        Dim tmpLen As Integer = 0
        Dim count As Integer = 0
        Dim LP As New List(Of List(Of Windows.Forms.Panel))
        For i = 0 To list.Count - 1
            Dim l As New List(Of Windows.Forms.Panel)
            LP.Add(l)
        Next
        i = list.Count - 1
        While i > -1
            Dim tmpL As List(Of String) = list(i)
            If i = list.Count - 1 Then 'if we are att the leafs
                Dim j As Integer
                Dim sum As Integer = 1
                For j = 0 To i - 1
                    sum = sum * list(j).Count
                Next

                While sum > 0
                    j = 0
                    p = New Windows.Forms.Panel

                    'get the prefered width
                    Dim maxWidth As Integer = 60

                    Dim testL As New Windows.Forms.Label
                    For Each s As String In tmpL
                        testL.Text = s.Trim
                        If testL.PreferredWidth > maxWidth Then
                            maxWidth = testL.PreferredWidth + 20
                        End If
                    Next

                    'we want some space
                    maxWidth = maxWidth + 20

                    'create the leaf labels
                    For Each s As String In tmpL
                        Dim l As New Windows.Forms.Label
                        l.Text = s.Trim
                        l.Height = 15
                        l.Width = maxWidth
                        p.Controls.Add(l)
                        l.BringToFront()
                        l.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
                        l.Left = 0
                        l.Top = (15 * j) - j
                        l.BackColor = c
                        j += 1
                    Next

                    'set the panel width and height
                    p.Height = (15 * j) - (j - 1)
                    p.Width = maxWidth
                    p.SendToBack()
                    LP(i).Add(p)
                    sum -= 1
                End While
                pLeaf = p
            Else 'if not at leafs we need to add the leafs
                Dim j As Integer
                Dim sum As Integer = 1
                For j = 0 To i - 1
                    sum = sum * list(j).Count
                Next
                count = 0
                While sum > 0
                    p = New Windows.Forms.Panel
                    j = 0

                    'get the prefered width
                    Dim maxWidth As Integer = 50

                    Dim testL As New Windows.Forms.Label
                    For Each s As String In tmpL
                        testL.Text = s.Trim
                        If testL.PreferredWidth > maxWidth Then
                            maxWidth = testL.PreferredWidth + 20
                        End If
                    Next

                    'we want some space
                    maxWidth = maxWidth + 20

                    For Each s As String In tmpL
                        Dim l As New Windows.Forms.Label
                        l.Text = s.Trim
                        l.Width = maxWidth
                        p.Controls.Add(l)
                        l.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
                        l.Left = -1
                        l.Top = (j * pLeaf.Height) - 1
                        l.BringToFront()
                        l.BackColor = c
                        Dim pT As Windows.Forms.Panel
                        pT = LP(i + 1)(count)
                        p.Controls.Add(pT)
                        pT.BringToFront()
                        pT.Left = maxWidth - 2
                        pT.Top = (j * pLeaf.Height) - 1
                        If j > 0 Then pT.Top -= j
                        If j > 0 Then l.Top -= j
                        l.Height = pT.Height
                        pT.BackColor = c
                        p.BackColor = c
                        p.SendToBack()
                        j += 1
                        count += 1
                    Next
                    p.Height = (pLeaf.Height * j) - j + 1
                    'p.Width = p.PreferredSize.Width - 2
                    p.Width = pLeaf.Width + maxWidth - 2
                    p.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
                    p.SendToBack()
                    LP(i).Add(p)
                    sum -= 1
                End While
                pLeaf = p
            End If
            i -= 1
        End While

        pLeaf = LP(0)(0)
        If i > 2 Then
            pLeaf.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
        End If
        Return pLeaf
    End Function

    Private Function createPivotSubObject(ByVal fs As Microsoft.Office.Interop.Owc11.PivotFieldSet) As List(Of String)
        Dim l As New List(Of String)
        If fs.Name = "Unit" Then 'we want to replace the categories under Unit
            l.Add("CPT Main")
            'l.Add("CPT Main 30""")
            l.Add("CPP Main")
            'l.Add("CPP Main 30""")
            l.Add("CPT Buying")
            l.Add("CPP Buying")
        Else 'is its not a Unit row we can safely add it

            Dim i As Integer
            For i = 0 To fs.Member.ChildMembers.Count - 1
                If Not l.Contains(fs.Member.ChildMembers(i).Name) Then
                    l.Add(fs.Member.ChildMembers(i).Name)
                End If
            Next
        End If

        Return l
    End Function

    Private Function createPivotObjectExist(ByVal s As String, ByVal pm As Microsoft.Office.Interop.Owc11.PivotMembers) As Boolean
        Dim m As Microsoft.Office.Interop.Owc11.PivotMember
        For Each m In pm
            If m.Name = s Then
                createPivotObjectExist = True
                Exit Function
            End If
        Next
        createPivotObjectExist = False
    End Function

    Private Function createPivotObject(ByVal fs As Microsoft.Office.Interop.Owc11.PivotFieldSets) As List(Of List(Of String))
        Dim lFields As New List(Of List(Of String))
        Dim j As Integer
        'get all fields
        Dim i As Integer = fs.Count - 1
        While i > -1
            Try
                'if it has ordered members we use them
                Dim ar = fs.Item(i).Fields(0).OrderedMembers
                Dim tmpList As New List(Of String)
                For j = 0 To ar.length - 1 'empty in the end?
                    If createPivotObjectExist(ar(j).name, fs.Item(i).Member.ChildMembers) Then
                        tmpList.Add(ar(j).name)
                    End If
                Next
                lFields.Add(tmpList)
            Catch ex As Exception
                'if it has no ordered members
                lFields.Add(createPivotSubObject(fs.Item(i)))
            End Try

            i -= 1
        End While
        lFields.Reverse()
        Return lFields
    End Function

    Private Sub grdCalcChart_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCalcChart.CellValueNeeded

        If bolDontUpdate Then Exit Sub

        Dim i As Integer
        Dim j As Integer
        Dim xList As New List(Of String)
        Dim yList As New List(Of String)
        Dim cList As New List(Of String)
        Dim cListEnabled As Boolean = False
        Dim row As Integer = e.RowIndex
        Dim col As Integer = e.ColumnIndex
        Dim type As String = ""
        Dim bolRow As Boolean
        'these are used to get the second value used in calculations
        Dim pvtCRow As Microsoft.Office.Interop.Owc11.PivotRowMember = Nothing
        Dim pvtCCol As Microsoft.Office.Interop.Owc11.PivotColumnMember = Nothing

        For i = 0 To cX.Count - 1
            Dim numberOfPos As Integer = cX(cX.Count - 1 - i).Count
            Dim tmpNumberofpos As Integer = numberOfPos
            For j = 1 To i
                tmpNumberofpos = numberOfPos * cX(cX.Count - 1 - i + j).Count
            Next
            If Not tmpNumberofpos = numberOfPos Then
                tmpNumberofpos = tmpNumberofpos / numberOfPos
                row = row \ tmpNumberofpos
            End If
            Dim myNumber As Integer = (row) Mod numberOfPos

            'we need to add different calculations depending on what rows are present
            If cX(cX.Count - 1 - i)(myNumber) = "CPT Main" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Main")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = True
            ElseIf cX(cX.Count - 1 - i)(myNumber) = "CPP Main" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Main")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = True
            ElseIf cX(cX.Count - 1 - i)(myNumber) = "CPT Main 30""" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Main 30""")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = True
            ElseIf cX(cX.Count - 1 - i)(myNumber) = "CPP Main 30""" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Main 30""")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = True
            ElseIf cX(cX.Count - 1 - i)(myNumber) = "CPT Buying" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Buying")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = True
            ElseIf cX(cX.Count - 1 - i)(myNumber) = "CPP Buying" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Buying")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = True
            ElseIf cX(cX.Count - 1 - i)(myNumber) = "CPT Custom" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Custom")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = True
            ElseIf cX(cX.Count - 1 - i)(myNumber) = "CPP Custom" Then
                For z As Integer = 0 To xList.Count - 1
                    Dim s As String = xList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Custom")
                xList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = True
            Else
                If cListEnabled Then
                    cList.Add(cX(cX.Count - 1 - i)(myNumber))
                End If
                xList.Add(cX(cX.Count - 1 - i)(myNumber))
            End If
        Next
        xList.Reverse()
        'if we have the Unit on the X-axle we make the calculations
        If cListEnabled Then
            cList.Reverse()
            pvtCRow = getRowMember(pvt.ActiveData.RowMembers(cList(0)), cList, 1)
        End If

        If e.ColumnIndex = 1 Then
            Dim sdfd As Integer = e.RowIndex
        End If

        cListEnabled = False
        For i = 0 To cY.Count - 1
            Dim numberOfPos As Integer = cY(cY.Count - 1 - i).Count
            Dim tmpNumberofpos As Integer = numberOfPos
            For j = 1 To i
                tmpNumberofpos = numberOfPos * cY(cY.Count - 1 - i + j).Count
            Next
            If Not tmpNumberofpos = numberOfPos Then
                tmpNumberofpos = tmpNumberofpos / numberOfPos
                col = col \ tmpNumberofpos
            End If
            Dim myNumber As Integer = (col) Mod numberOfPos

            'we need to add different calculations depending on what columns are present
            If cY(cY.Count - 1 - i)(myNumber) = "CPT Main" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Main")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPP Main" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Main")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPT Main 30""" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Main 30""")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPP Main 30""" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Main 30""")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPT Buying" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Buying")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPP Buying" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Buying")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPT Custom" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Custom")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPP Custom" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Custom")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPT Custom 30""" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("'000 Custom 30""")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPT"
                bolRow = False
            ElseIf cY(cY.Count - 1 - i)(myNumber) = "CPP Custom 30""" Then
                For z As Integer = 0 To yList.Count - 1
                    Dim s As String = yList(z)
                    cList.Add(s)
                Next
                cList.Add("TRP Custom 30""")
                yList.Add("Net Budget")
                cListEnabled = True
                type = "CPP"
                bolRow = False
            Else
                If cListEnabled Then
                    cList.Add(cY(cY.Count - 1 - i)(myNumber))
                End If
                yList.Add(cY(cY.Count - 1 - i)(myNumber))
            End If

        Next
        yList.Reverse()

        'if we have the Unit on the Y-axle we make the calculations
        If cListEnabled Then
            cList.Reverse()
            pvtCCol = getColumnMember(pvt.ActiveData.ColumnMembers(cList(0)), cList, 1)
        End If


        Dim pvtRow As Microsoft.Office.Interop.Owc11.PivotRowMember
        Dim pvtCol As Microsoft.Office.Interop.Owc11.PivotColumnMember
        Try
            pvtRow = getRowMember(pvt.ActiveData.RowMembers(xList(0)), xList, 1)
        Catch ex As Exception
            If ex.GetBaseException.TargetSite.Name = "ThrowArgumentOutOfRangeException" Then
                pvtRow = pvt.ActiveData.RowAxis.RowMember
            Else
                pvtRow = Nothing
            End If
        End Try

        Try
            pvtCol = getColumnMember(pvt.ActiveData.ColumnMembers(yList(0)), yList, 1)
        Catch ex As Exception
            If ex.GetBaseException.TargetSite.Name = "ThrowArgumentOutOfRangeException" Then
                pvtCol = pvt.ActiveData.ColumnAxis.ColumnMember
            Else
                pvtCol = Nothing
            End If
        End Try


        Try
            If pvtRow Is Nothing OrElse pvtCol Is Nothing Then
                e.Value = ""
            Else
                If type = "CPT" Then
                    If bolRow Then
                        e.Value = FormatNumber((pvt.ActiveData.Cells(pvtRow, pvtCol).Aggregates(0).Value()) / (pvt.ActiveData.Cells(pvtCRow, pvtCol).Aggregates(0).Value()))
                        If e.Value = "INF" Then
                            e.Value = 0
                        End If
                    Else
                        e.Value = FormatNumber((pvt.ActiveData.Cells(pvtRow, pvtCol).Aggregates(0).Value()) / (pvt.ActiveData.Cells(pvtRow, pvtCCol).Aggregates(0).Value()))
                        If e.Value = "INF" Then
                            e.Value = 0
                        End If
                    End If
                ElseIf type = "CPP" Then
                    If bolRow Then
                        e.Value = FormatNumber((pvt.ActiveData.Cells(pvtRow, pvtCol).Aggregates(0).Value()) / (pvt.ActiveData.Cells(pvtCRow, pvtCol).Aggregates(0).Value()))
                        If e.Value = "INF" Then
                            e.Value = 0
                        End If
                    Else
                        e.Value = FormatNumber((pvt.ActiveData.Cells(pvtRow, pvtCol).Aggregates(0).Value()) / (pvt.ActiveData.Cells(pvtRow, pvtCCol).Aggregates(0).Value()))
                        If e.Value = "INF" Then
                            e.Value = 0
                        End If
                    End If
                Else
                    'pvt.ActiveData.Cells(pvt.ActiveData.RowMembers(2).ChildMembers.Item(0), pvt.ActiveData.ColumnMembers(0).ChildMembers.Item(0)).Aggregates(0).Value()
                    e.Value = FormatNumber(pvt.ActiveData.Cells(pvtRow, pvtCol).Aggregates(0).Value())
                End If
            End If
        Catch ex As Exception
            e.Value = "#ERR"
        End Try

    End Sub


    Private Function getRowMember(ByVal rm As Microsoft.Office.Interop.Owc11.PivotRowMember, ByVal l As List(Of String), ByVal i As Integer) As Microsoft.Office.Interop.Owc11.PivotRowMember
        If i = l.Count Then Return rm
        Dim m As Microsoft.Office.Interop.Owc11.PivotRowMember
        m = getRowMember(rm.ChildMembers.Item(l(i)), l, i + 1)
        Return m
    End Function

    Private Function getColumnMember(ByVal rm As Microsoft.Office.Interop.Owc11.PivotColumnMember, ByVal l As List(Of String), ByVal i As Integer) As Microsoft.Office.Interop.Owc11.PivotColumnMember
        If i = l.Count Then Return rm
        Dim m As Microsoft.Office.Interop.Owc11.PivotColumnMember
        m = getColumnMember(rm.ChildMembers.Item(l(i)), l, i + 1)
        Return m
    End Function
End Class



'Private Sub frmPivot_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
'    pvtPivot.Clear()
'    Dim WDs() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
'    For Each TmpChan As Trinity.cChannel In Campaign.Channels
'        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
'            If TmpBT.BookIt Then
'                For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
'                    For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
'                        For DP As Integer = 0 To Campaign.DaypartCount - 1
'                            For WD As Integer = 1 To 7
'                                Dim Percent As Single = ((TmpFilm.Share / 100) * (TmpBT.DaypartSplit(DP) / 100)) / 7
'                                Dim WeekD As String = WDs(WD - 1)
'                                AddToPivot(Campaign.DaypartName(DP), TmpChan.ChannelName, TmpBT.Name, TmpFilm.Name, TmpFilm.Filmcode, WeekD, TmpWeek.Name, "Planned", _
'                                    "TRP Main", TmpWeek.TRP * Percent)
'                                AddToPivot(Campaign.DaypartName(DP), TmpChan.ChannelName, TmpBT.Name, TmpFilm.Name, TmpFilm.Filmcode, WeekD, TmpWeek.Name, "Planned", _
'                                    "TRP Buying", TmpWeek.TRPBuyingTarget * Percent)
'                                '                            AddToPivot Campaign.DaypartName(dp), TmpChan.ChannelName, TmpBT.Name, TmpFilm.Name, TmpFilm.Filmcode, WeekD, TmpWeek.Name, "Planned", _
'                                '                                "TRP " & Campaign.AllAdults, TmpWeek.TRPAllAdults * Percent
'                                AddToPivot(Campaign.DaypartName(DP), TmpChan.ChannelName, TmpBT.Name, TmpFilm.Name, TmpFilm.Filmcode, WeekD, TmpWeek.Name, "Planned", _
'                                    "Gross budget", TmpWeek.GrossBudget * Percent)
'                                AddToPivot(Campaign.DaypartName(DP), TmpChan.ChannelName, TmpBT.Name, TmpFilm.Name, TmpFilm.Filmcode, WeekD, TmpWeek.Name, "Planned", _
'                                    "Net budget", TmpWeek.NetBudget * Percent)
'                                'If TmpWeek.TRP * Percent = 0 Then Stop
'                                AddToPivot(Campaign.DaypartName(DP), TmpChan.ChannelName, TmpBT.Name, TmpFilm.Name, TmpFilm.Filmcode, WeekD, TmpWeek.Name, "Planned", _
'                                    "Main CPP 30""", (TmpWeek.NetBudget * Percent) / (TmpWeek.TRP * Percent))
'                            Next
'                        Next
'                    Next
'                Next
'            End If
'        Next
'    Next
'    For Each TmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
'        If TmpBookedSpot.Bookingtype.BookIt Then

'            Dim WeekD As String = WDs(Weekday(TmpBookedSpot.AirDate, vbMonday) - 1)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpBookedSpot.MaM)), TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film.Name, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, "Booked", _
'                "TRP Main", TmpBookedSpot.MyEstimate)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpBookedSpot.MaM)), TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film.Name, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, "Booked", _
'                "TRP Buying", TmpBookedSpot.ChannelEstimate)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpBookedSpot.MaM)), TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film.Name, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, "Booked", _
'                "Gross budget", TmpBookedSpot.GrossPrice)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpBookedSpot.MaM)), TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film.Name, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, "Booked", _
'                "Net budget", TmpBookedSpot.NetPrice)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpBookedSpot.MaM)), TmpBookedSpot.Channel.ChannelName, TmpBookedSpot.Bookingtype.Name, TmpBookedSpot.Film.Name, TmpBookedSpot.Filmcode, WeekD, TmpBookedSpot.week.Name, "Booked", _
'                "Main CPP 30""", TmpBookedSpot.NetPrice / TmpBookedSpot.MyEstimate)
'        End If
'    Next
'    For Each TmpPlannedSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots
'        If TmpPlannedSpot.Bookingtype.BookIt Then
'            Dim WeekD As String = WDs(Weekday(Date.FromOADate(TmpPlannedSpot.AirDate), vbMonday) - 1)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpPlannedSpot.MaM)), TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film.Name, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, "Confirmed", _
'                "TRP Main", TmpPlannedSpot.MyRating)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpPlannedSpot.MaM)), TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film.Name, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, "Confirmed", _
'                "TRP Buying", TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpPlannedSpot.MaM)), TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film.Name, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, "Confirmed", _
'                "Gross budget", TmpPlannedSpot.PriceGross)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpPlannedSpot.MaM)), TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film.Name, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, "Confirmed", _
'                "Net budget", TmpPlannedSpot.PriceNet)
'            AddToPivot(Campaign.DaypartName(Trinity.Helper.GetDaypart(TmpPlannedSpot.MaM)), TmpPlannedSpot.Channel.ChannelName, TmpPlannedSpot.Bookingtype.Name, TmpPlannedSpot.Film.Name, TmpPlannedSpot.Filmcode, WeekD, TmpPlannedSpot.Week.Name, "Confirmed", _
'                "Main CPP 30""", TmpPlannedSpot.PriceNet / TmpPlannedSpot.MyRating)
'        End If
'    Next
'    pvtPivot.ColumnAxis.FieldSets.Add("Channel")
'    pvtPivot.ColumnAxis.FieldSets.Add("Unit")
'    pvtPivot.ColumnAxis.FieldSets.Add("Status")
'    pvtPivot.RowAxis.FieldSets.Add("Film")
'    pvtPivot.Total.FieldSet = pvtPivot.FieldSets("Unit")

'    pvtPivot.Total.TotalFields("TRP Main").NumberFormat = "N1"
'    pvtPivot.Total.TotalFields("TRP Main").TotalFunction = Pivot.PivotTotalFunctionEnum.plFunctionSum

'    pvtPivot.Total.TotalFields("TRP Buying").NumberFormat = "N1"
'    pvtPivot.Total.TotalFields("TRP Buying").TotalFunction = Pivot.PivotTotalFunctionEnum.plFunctionSum

'    pvtPivot.Total.TotalFields("Gross budget").NumberFormat = "C0"
'    pvtPivot.Total.TotalFields("Gross budget").TotalFunction = Pivot.PivotTotalFunctionEnum.plFunctionSum

'    pvtPivot.Total.TotalFields("Net budget").NumberFormat = "C0"
'    pvtPivot.Total.TotalFields("Net budget").TotalFunction = Pivot.PivotTotalFunctionEnum.plFunctionSum

'    pvtPivot.Total.TotalFields("Main CPP 30""").NumberFormat = "N1"
'    pvtPivot.Total.TotalFields("Main CPP 30""").TotalFunction = Pivot.PivotTotalFunctionEnum.plFunctionAvg

'    'pvtPivot.FieldSets("Unit").IncludeFields.Add("TRP Main")
'    pvtPivot.FieldSets("Unit").IncludeFields.Add("TRP Buying")
'    pvtPivot.FieldSets("Unit").IncludeFields.Add("Gross budget")
'    'pvtPivot.FieldSets("Unit").IncludeFields.Add("Net budget")
'    'pvtPivot.FieldSets("Unit").IncludeFields.Add("Main CPP 30""")

'    pvtPivot.Refresh()

'End Sub

'Sub AddToPivot(ByVal Daypart As String, ByVal Channel As String, ByVal Bookingtype As String, ByVal Film As String, ByVal Filmcode As String, ByVal Weekday As String, ByVal week As String, ByVal Status As String, ByVal Unit As String, ByVal Value As Single)
'    If Value.ToString = "NaN" Then Value = 0
'    Dim Fields() As String = {"Daypart", "Channel", "Bookingtype", "Film", "Filmcode", "Weekday", "Week", "Status", "Unit", "Value"}
'    Dim Values() As String = {Daypart, Channel, Bookingtype, Film, Filmcode, Weekday, week, Status, Unit, Value}
'    pvtPivot.AddNew(Fields, Values)
'End Sub

'Private Sub cmdDefine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefine.Click
'    frmDefine.ShowDialog()
'End Sub