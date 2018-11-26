Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class gfxSchedule2
    Public Event ProgramDoubleClicked(ByVal sender As Object, ByVal e As ProgramEvent)

    Dim countTop As Integer
    Dim countLeft As Integer

    Private _startDate As Date = Now
    Private _endDate As Date = Now
    Private _minuteHeight As Single = 2
    Private _dayWidth As Single = 100
    Private _selectedProg As gfxItem
    Private _topMaM As Integer = 360
    Private _headlineFont As New Font("Segoe UI", 8, FontStyle.Regular)
    Private _programFont As New Font("Segoe UI", 6, FontStyle.Regular)
    Dim _topItemBackColor As Color = Color.AliceBlue
    Dim _leftItemBackColor As Color = Color.AliceBlue
    Dim _programBackColor As Color = Color.White
    Private _extendedInfos As New Trinity.cWrapper
    Dim oldGfxItem As New gfxItem

    Dim tabBooking2 As System.Windows.Forms.TabPage
    Public tabBooking As System.Windows.Forms.TabPage
    Dim ObjectThread As System.Threading.Thread

    Private Sub scrollH(ByVal sender As Object) Handles sePanel.ScrollH
        topInnerPanel.Left = itemPanel.Left
    End Sub

    Private Sub scrollV(ByVal sender As Object) Handles sePanel.ScrollV
        leftInnerPanel.Top = itemPanel.Top
    End Sub

    Private Sub gridFrame_ScrollWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sePanel.MouseWheel
        topInnerPanel.Left = sePanel.AutoScrollPosition.X
        leftInnerPanel.Top = sePanel.AutoScrollPosition.Y
    End Sub

    Public Sub EventSelected(ByVal sender As gfxItem, ByVal e As System.EventArgs)
        oldGfxItem.resetColor()
        oldGfxItem = sender
        sender.BackColor = Color.Yellow
    End Sub

    Public Sub forwardEventDoubleklicked(ByVal sender As gfxItem, ByVal e As System.EventArgs)
        Dim tmp As New ProgramEvent
        tmp.ExtendedInfo = sender.ExtendedInfo
        RaiseEvent ProgramDoubleClicked(sender, tmp)
    End Sub

    Public Sub SetupDays()
        'Dim startTickCount As Long = My.Computer.Clock.TickCount
        Dim j As Integer
        j = 0
        Dim i As Integer

        Dim strDay As String
        Dim Days() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        'sets up the date bar in the top
        topInnerPanel.Controls.Clear()
        For i = _startDate.ToOADate To _endDate.ToOADate
            Dim it As New topItem()
            topInnerPanel.Controls.Add(it)
            strDay = Days(Weekday(Date.FromOADate(i), FirstDayOfWeek.Monday) - 1)
            it.setPreferences(strDay, Date.FromOADate(i), j * _dayWidth, _dayWidth, _headlineFont, _topItemBackColor)
            it.Show()
            j += 1
        Next
        topInnerPanel.Width = j * _dayWidth
        'set the item area in the middle to the same width as the top panel
        itemPanel.Width = j * _dayWidth
        topInnerPanel.SendToBack()

        leftInnerPanel.Controls.Clear()
        Dim t As New DateTime(2000, 1, 1, 6, 0, 0)
        Dim strT As String
        For i = 0 To 48
            If t.Minute = 0 Then
                strT = CStr(t.Hour) + ":00"
            Else
                strT = CStr(t.Hour) + ":30"
            End If
            Dim lit As New leftItem(strT, _minuteHeight, _headlineFont, _leftItemBackColor)
            leftInnerPanel.Controls.Add(lit)
            lit.Top = i * (_minuteHeight * 30) + 1
            t = t.AddMinutes(30)
        Next
        leftInnerPanel.Height = (i * (_minuteHeight * 30))
        itemPanel.Height = leftInnerPanel.Height


        'deletes all graphic items before we make new ones
        Dim c As Control
        For Each c In itemPanel.Controls
            c.Dispose()
        Next
        Dim EI As Trinity.cExtendedInfo
        For Each DE As DictionaryEntry In _extendedInfos
            EI = DE.Value
            If EI.AirDate >= StartDate AndAlso EI.AirDate <= EndDate Then
                Dim item As New gfxItem
                item.ExtendedInfo = EI
                itemPanel.Controls.Add(item)
                AddHandler item.iAmMarked, AddressOf Me.EventSelected
                AddHandler item.addThis, AddressOf Me.forwardEventDoubleklicked
                If item.ExtendedInfo.IsBooked Then
                    item.setPreferences((item.ExtendedInfo.AirDate.ToOADate - _startDate.ToOADate) * _dayWidth, _dayWidth, _minuteHeight, _topMaM, _programFont, Color.Gold)
                Else
                    item.setPreferences((item.ExtendedInfo.AirDate.ToOADate - _startDate.ToOADate) * _dayWidth, _dayWidth, _minuteHeight, _topMaM, _programFont, _programBackColor)
                End If
            End If
        Next
        itemPanel.SendToBack()
    End Sub

    Private Sub createGraphicsThread()
        'Dim startTickCount As Long = My.Computer.Clock.TickCount
        Dim j As Integer
        j = 0
        Dim i As Integer

        Dim strDay As String
        Dim Days() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        'sets up the date bar in the top
        topInnerPanel.Controls.Clear()
        For i = _startDate.ToOADate To _endDate.ToOADate
            Dim it As New topItem()
            topInnerPanel.Controls.Add(it)
            strDay = Days(Weekday(Date.FromOADate(i), FirstDayOfWeek.Monday) - 1)
            it.setPreferences(strDay, Date.FromOADate(i), j * _dayWidth, _dayWidth, _headlineFont, _topItemBackColor)
            it.Show()
            j += 1
        Next
        topInnerPanel.Width = j * _dayWidth
        'set the item area in the middle to the same width as the top panel
        itemPanel.Width = j * _dayWidth
        topInnerPanel.SendToBack()

        leftInnerPanel.Controls.Clear()
        Dim t As New DateTime(2000, 1, 1, 6, 0, 0)
        Dim strT As String
        For i = 0 To 48
            If t.Minute = 0 Then
                strT = CStr(t.Hour) + ":00"
            Else
                strT = CStr(t.Hour) + ":30"
            End If
            Dim lit As New leftItem(strT, _minuteHeight, _headlineFont, _leftItemBackColor)
            leftInnerPanel.Controls.Add(lit)
            lit.Top = i * (_minuteHeight * 30) + 1
            t = t.AddMinutes(30)
        Next
        leftInnerPanel.Height = (i * (_minuteHeight * 30))
        itemPanel.Height = leftInnerPanel.Height

        'deletes all graphic items before we make new ones
        Dim c As Control
        For Each c In itemPanel.Controls
            c.Dispose()
        Next

        For Each de As DictionaryEntry In _extendedInfos
            Dim item As New gfxItem
            item.ExtendedInfo = de.Value
            itemPanel.Controls.Add(item)
            AddHandler item.iAmMarked, AddressOf Me.EventSelected
            AddHandler item.addThis, AddressOf Me.forwardEventDoubleklicked
            If item.ExtendedInfo.IsBooked Then
                item.setPreferences((item.ExtendedInfo.AirDate.ToOADate - _startDate.ToOADate) * _dayWidth, _dayWidth, _minuteHeight, _topMaM, _programFont, Color.Gold)
            Else
                item.setPreferences((item.ExtendedInfo.AirDate.ToOADate - _startDate.ToOADate) * _dayWidth, _dayWidth, _minuteHeight, _topMaM, _programFont, _programBackColor)
            End If
        Next
        itemPanel.SendToBack()
        tabBooking.Enabled = True
        'MsgBox("Antal millisekunder för GFX Thread: " & My.Computer.Clock.TickCount - startTickCount)
    End Sub

    Public Property StartDate() As Date
        Get
            Return _startDate
        End Get
        Set(ByVal value As Date)
            _startDate = value
        End Set
    End Property

    Public Property EndDate() As Date
        Get
            Return _endDate
        End Get
        Set(ByVal value As Date)
            _endDate = value
        End Set
    End Property

    Public Property ExtendedInfos() As Trinity.cWrapper
        Get
            Return _extendedInfos
        End Get
        Set(ByVal value As Trinity.cWrapper)
            _extendedInfos = value
        End Set
    End Property

    Public Property tabThread() As System.Windows.Forms.TabPage
        Get
            Return tabBooking2
        End Get
        Set(ByVal value As System.Windows.Forms.TabPage)
            tabBooking2 = value
        End Set
    End Property

    Public Property TopMaM() As Integer
        Get
            Return _topMaM
        End Get
        Set(ByVal value As Integer)
            _topMaM = value
        End Set
    End Property

    Friend Property SelectedProg() As gfxItem
        Get
            Return _selectedProg
        End Get
        Set(ByVal value As gfxItem)
            _selectedProg = value
        End Set
    End Property

    Public Property MinuteHeights() As Single
        Get
            Return _minuteHeight
        End Get
        Set(ByVal value As Single)
            _minuteHeight = value
        End Set
    End Property

    Public Property DayWidths() As Single
        Get
            Return _dayWidth
        End Get
        Set(ByVal value As Single)
            _dayWidth = value
        End Set
    End Property

    Public Sub changeSize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        sePanel.Width = Me.Width - 70
        sePanel.Height = Me.Height - 70
        leftPanel.Height = sePanel.Height
        topPanel.Width = sePanel.Width
    End Sub

    Public Property HeadlineFont() As Font
        Get
            Return _headlineFont
        End Get
        Set(ByVal value As Font)
            _headlineFont = value
        End Set
    End Property

    Public Property HeadlineBackcolor() As Color
        Get
            Return _topItemBackColor
        End Get
        Set(ByVal value As Color)
            _topItemBackColor = value
        End Set
    End Property

    Public Property ProgramBackcolor() As Color
        Get
            Return _programBackColor
        End Get
        Set(ByVal value As Color)
            _programBackColor = value
        End Set
    End Property

    Public Property LeftsideBackcolor() As Color
        Get
            Return _leftItemBackColor
        End Get
        Set(ByVal value As Color)
            _leftItemBackColor = value
        End Set
    End Property

    Public Property ProgramFont() As Font
        Get
            Return _programFont
        End Get
        Set(ByVal value As Font)
            _programFont = value
        End Set
    End Property

    Public Sub updateGraphics(ByVal airDate As Date, ByVal MaM As Integer, ByVal progAfter As String)
        'changes the back color for the chosen one
        Dim c As Control
        Dim it As gfxItem
        For Each c In itemPanel.Controls
            it = c
            If it.ExtendedInfo.AirDate = airDate And it.ExtendedInfo.MaM = MaM And it.ExtendedInfo.ProgAfter = progAfter Then
                it.BackColor = Color.CadetBlue
            End If
        Next
    End Sub

End Class

'*********************************************************************************************************************
'Help classes
'*********************************************************************************************************************
Public Class gfxItem
    Inherits TextBox
    'en massa information
    Dim main As gfxSchedule2
    Dim _extendedInfo As Trinity.cExtendedInfo
    Public Event addThis(ByVal sender As gfxItem, ByVal e As System.EventArgs)
    Public Event iAmMarked(ByVal sender As gfxItem, ByVal e As System.EventArgs)
    Public ExtendedInfo As Trinity.cExtendedInfo
    Dim oldColor As Color

    Public Sub New()
        Me.Multiline = True
        Me.BorderStyle = Windows.Forms.BorderStyle.None
    End Sub

    Public Sub setPreferences(ByVal left As Integer, ByVal width As Integer, ByVal timeHeight As Integer, ByVal startMaM As Integer, ByVal f As Font, ByVal c As Color)
        Me.Height = (ExtendedInfo.Duration * timeHeight) - 1
        Me.Font = f
        Me.Width = width - 1
        Me.Left = left + 1
        Me.oldColor = c
        Me.BackColor = c
        Me.Text = Trinity.Helper.Mam2Tid(ExtendedInfo.MaM) & " " & ExtendedInfo.ProgAfter
        Me.Show()
        Me.BringToFront()
        Me.Top = ((ExtendedInfo.MaM - startMaM) * timeHeight) + 1
    End Sub

    Public Sub marked(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        RaiseEvent iAmMarked(Me, e)
    End Sub

    Public Sub doubleKlicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        Me.oldColor = Color.CadetBlue
        resetColor()
        RaiseEvent addThis(Me, e)
    End Sub

    Public WriteOnly Property setText()
        Set(ByVal value)
            Me.Text = value
        End Set
    End Property

    Public Sub resetColor()
        Me.BackColor = oldColor
    End Sub

End Class

Public Class topItem
    Inherits Panel

    Public Sub New()
    End Sub

    Public Sub setPreferences(ByVal str1 As String, ByVal str2 As String, ByVal left As Integer, ByVal width As Integer, ByVal f As Font, ByVal c As Color)
        Me.Height = 53
        Me.Width = width - 1
        Me.Top = 1
        Me.Left = left + 1
        Me.BackColor = c 
        Dim lblDate As New Label
        Me.Controls.Add(lblDate)
        lblDate.Text = str1
        lblDate.Font = f
        lblDate.Show()
        Dim lblDay As New Label
        Me.Controls.Add(lblDay)
        lblDay.Text = str2
        lblDay.Font = f
        lblDay.Show()
        lblDate.Top = 5
        lblDate.Left = 0
        lblDate.Width = Me.Width
        lblDate.TextAlign = ContentAlignment.MiddleCenter
        lblDay.Top = 25
        lblDay.Left = 0
        lblDay.Width = Me.Width
        lblDay.TextAlign = ContentAlignment.MiddleCenter

        Me.SendToBack()

    End Sub

End Class

Public Class leftItem
    Inherits Label

    Public Sub New(ByVal time As String, ByVal minuteHeight As Integer, ByVal f As Font, ByVal c As Color)
        Me.Text = time
        Me.Height = (30 * minuteHeight) - 1
        Me.Width = 53
        Me.Left = 1
        Me.BackColor = c
        Me.TextAlign = ContentAlignment.MiddleCenter
    End Sub

End Class

Public Class ScrollEventPanel
    Inherits Panel
    'this custom panel enables us to get what scrollbar is scrolled
    Public Event ScrollH(ByVal sender As Object)
    Public Event ScrollV(ByVal sender As Object)

    Public Shadows Event Scroll(ByVal sender As Object)
    Public Const WM_HSCROLL As Integer = &H114
    Public Const WM_VSCROLL As Integer = &H115

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_HSCROLL Then
            RaiseEvent ScrollH(Me)
        ElseIf m.Msg = WM_VSCROLL Then
            RaiseEvent ScrollV(Me)
        End If
        MyBase.WndProc(m)
    End Sub
End Class



