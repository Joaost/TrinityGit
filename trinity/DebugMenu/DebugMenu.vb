Imports TrinityPlugin
Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection

<Export(GetType(IPlugin))>
Public Class DebugMenu
    Implements IPlugin

    Private _mnu As New IPlugin.PluginMenu

    Public Function GetSaveData() As System.Xml.Linq.XElement Implements TrinityPlugin.IPlugin.GetSaveData
        Return Nothing
    End Function

    Public Function Menu() As TrinityPlugin.IPlugin.PluginMenu Implements TrinityPlugin.IPlugin.Menu
        Return _mnu
    End Function

    Public ReadOnly Property PluginName As String Implements TrinityPlugin.IPlugin.PluginName
        Get
            Return "Debug"
        End Get
    End Property

    Public Event SaveDataAvailale(sender As Object, e As System.EventArgs) Implements TrinityPlugin.IPlugin.SaveDataAvailale

    Public Sub EditScheduleTemplates(sender As Object, e As EventArgs)
        Dim _frm As New frmEditor
        _frm.Show()

    End Sub

    Public Sub DetailedSpotlist(sender As Object, e As EventArgs)
        Dim _camp = Application.ActiveCampaign
        Dim _stringBuilder As New Text.StringBuilder()

        _stringBuilder.Append("ID")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Date")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Week")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Month")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Time")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("MaM")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Daypart")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Channel")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Bookingtype")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Program")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("TRP")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("TRP30")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Gross CPP30")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Index")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Discount")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Filmcode")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("Film length")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("SpotIndex")
        _stringBuilder.Append(vbTab)
        _stringBuilder.Append("ActualNetValue")
        _stringBuilder.AppendLine()

        For Each _spot In _camp.ActualSpots
            _stringBuilder.Append("")
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(Date.FromOADate(_spot.AirDate))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(DatePart(DateInterval.WeekOfYear, Date.FromOADate(_spot.AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(DatePart(DateInterval.Month, Date.FromOADate(_spot.AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(Mam2Time(_spot.MaM))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.MaM)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Bookingtype.Dayparts.GetDaypartIndexForMam(_spot.MaM))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Channel.ChannelName)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Bookingtype.Name)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Programme)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Rating(4))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Rating30(4))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Bookingtype.GetGrossCPP30ForDate(Date.FromOADate(_spot.AirDate), _spot.Bookingtype.Dayparts.GetDaypartIndexForMam(_spot.MaM)))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Bookingtype.GetIndexForDate(Date.FromOADate(_spot.AirDate)))
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Bookingtype.Buyingtarget.Discount)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Filmcode)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.SpotLength)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.Week.Films(_spot.Filmcode).Index)
            _stringBuilder.Append(vbTab)
            _stringBuilder.Append(_spot.ActualNetValue)
            _stringBuilder.AppendLine()
        Next
        My.Computer.Clipboard.SetText(_stringBuilder.ToString)

        Dim _excel As New CultureSafeExcel.Application(False)
        With _excel.AddWorkbook
            With DirectCast(.Sheets(1), Microsoft.Office.Interop.Excel.Worksheet)
                .Paste()
            End With
        End With
        _excel.Visible = True
    End Sub

    Function Mam2Time(MaM As Integer) As String
        Dim h = MaM \ 60
        Dim m = MaM Mod 60
        Return h.ToString.PadLeft(2, "0") & ":" & m.ToString.PadLeft(2, "0")
    End Function

    <Import(GetType(ITrinityApplication))>
    Property Application() As ITrinityApplication

    Public Sub New()
        _mnu.AddToMenu = ""
        _mnu.Caption = "Debug"

        Dim _itm As New IPlugin.PluginMenuItem
        _itm.Text = "Export detailed spotlist"
        _itm.OnClickFunction = AddressOf DetailedSpotlist
        _mnu.Items.Add(_itm)

        _itm = New IPlugin.PluginMenuItem
        _itm.Text = "Edit Schedule Templates"
        _itm.OnClickFunction = AddressOf EditScheduleTemplates
        _mnu.Items.Add(_itm)

        _itm = New IPlugin.PluginMenuItem
        _itm.Text = "Create new message"
        _itm.OnClickFunction = AddressOf CreateNewMessage
        _mnu.Items.Add(_itm)

        _itm = New IPlugin.PluginMenuItem
        _itm.Text = "Handle Connect.dll"
        _itm.OnClickFunction = AddressOf HandleDLL
        _mnu.Items.Add(_itm)

        Dim catalog As New AggregateCatalog()
        catalog.Catalogs.Add(New AssemblyCatalog(Assembly.GetEntryAssembly))
        Dim container As New CompositionContainer(catalog)
        container.SatisfyImportsOnce(Me)
    End Sub

    Sub HandleDLL()
        Dim _frm As New frmDLL
        _frm.ShowDialog()
    End Sub

    Sub CreateNewMessage()
        Dim _frm As New frmNewMessage
        _frm.ShowDialog()
    End Sub

    Public ReadOnly Property PreferencesTab As TrinityPlugin.pluginPreferencesTab Implements TrinityPlugin.IPlugin.PreferencesTab
        Get
            Return Nothing
        End Get
    End Property
End Class
