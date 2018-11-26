Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class GfxSchedule
    Inherits ScrollableControl

    Public Event ProgramSelected(ByVal sender As Object, ByVal e As ProgramEvent)
    Public Event ProgramDoubleClicked(ByVal sender As Object, ByVal e As ProgramEvent)

    Friend Class Day
        Private _parent As GfxSchedule

        Public Day As Date
        Private _left As Integer
        Private _programs As New SortedList(Of String, Program)
        Friend Rects As New Dictionary(Of Rectangle, Program)

        Public Property Left() As Integer
            Get
                Return _left - _parent.HorizontalScroll.Value
            End Get
            Set(ByVal value As Integer)
                _left = value
            End Set
        End Property
        Public Property Programs() As SortedList(Of String, Program)
            Get
                Return _programs
            End Get
            Set(ByVal value As SortedList(Of String, Program))
                _programs = value
            End Set
        End Property

        Public Sub Paint(ByRef g As Graphics)
            Dim Font As Font = _parent.HeadlineFont
            Dim PFont As Font = _parent.ProgramFont
            Dim Rect As New Rectangle(Left, 0, _parent.DayWidth, Font.Height * 4)
            Dim Days() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Slask"}
            Dim MaxMam As Integer

            For Each kv As KeyValuePair(Of String, Program) In _programs
                Dim ProgRect As New Rectangle(Left, _parent.MinuteHeight * (kv.Value.ExtendedInfo.MaM - _parent.TopMam) + Font.Height * 4 - _parent.VerticalScroll.Value, _parent.DayWidth, _parent.MinuteHeight * kv.Value.ExtendedInfo.Duration)
                If ProgRect.Top < _parent.Height AndAlso ProgRect.Left < _parent.Width Then
                    g.Clip = New Region(ProgRect)
                    If kv.Value.ExtendedInfo.IsBooked Then
                        g.FillRectangle(Brushes.LightBlue, ProgRect)
                    Else
                        g.FillRectangle(Brushes.White, ProgRect)
                    End If
                    g.DrawRectangle(Pens.Gray, ProgRect)
                    g.DrawString(Trinity.Helper.Mam2Tid(kv.Value.ExtendedInfo.MaM) & " " & kv.Value.ExtendedInfo.ProgAfter, PFont, Brushes.Black, Left + 2, _parent.MinuteHeight * (kv.Value.ExtendedInfo.MaM - _parent.TopMam) + 2 + Font.Height * 4 - _parent.VerticalScroll.Value)
                    kv.Value.Top = ProgRect.Top
                    kv.Value.Bottom = ProgRect.Bottom
                Else
                    'Stop
                End If
                If kv.Value.ExtendedInfo.MaM + kv.Value.ExtendedInfo.Duration > MaxMam Then
                    MaxMam = kv.Value.ExtendedInfo.MaM + kv.Value.ExtendedInfo.Duration
                End If
            Next
            If Not _parent.SelectedProg Is Nothing AndAlso _parent.SelectedProg.ExtendedInfo.AirDate = Day Then
                Dim SelRect As New Rectangle(Left, _parent.MinuteHeight * (_parent.SelectedProg.ExtendedInfo.MaM - _parent.TopMam) + Font.Height * 4 - _parent.VerticalScroll.Value, _parent.DayWidth, _parent.MinuteHeight * _parent.SelectedProg.ExtendedInfo.Duration)
                g.Clip = New Region(SelRect)
                g.FillRectangle(Brushes.Yellow, SelRect)
                g.DrawRectangle(Pens.Gray, SelRect)
                g.DrawString(Trinity.Helper.Mam2Tid(_parent.SelectedProg.ExtendedInfo.MaM) & " " & _parent.SelectedProg.ExtendedInfo.ProgAfter, PFont, Brushes.Black, Left + 2, _parent.MinuteHeight * (_parent.SelectedProg.ExtendedInfo.MaM - _parent.TopMam) + 2 + Font.Height * 4 - _parent.VerticalScroll.Value)
            End If

            g.Clip = New Region(New Rectangle(0, 0, _parent.Width, _parent.Height))
            g.DrawRectangle(Pens.Black, Left, 0, _parent.DayWidth, _parent.Height)
            g.FillRectangle(Brushes.DarkGray, Rect)
            g.DrawRectangle(Pens.Black, Rect)

            g.DrawString(Format(Day, "Short date"), Font, Brushes.Black, Left + _parent.DayWidth / 2 - g.MeasureString(Format(Day, "Short date"), Font).Width / 2, Font.Height)
            g.DrawString(Days(Weekday(Day, FirstDayOfWeek.Monday) - 1), Font, Brushes.Black, Left + _parent.DayWidth / 2 - g.MeasureString(Days(Weekday(Day, FirstDayOfWeek.Monday) - 1), Font).Width / 2, Font.Height * 2)

            _parent.AutoScrollMinSize = New Size(_parent.AutoScrollMinSize.Width, (MaxMam - _parent.TopMam) * _parent.MinuteHeight)

        End Sub

        Public Sub New(ByVal Parent As GfxSchedule)
            _parent = Parent
        End Sub
    End Class

    Public Class Program
        Public ExtendedInfo As Trinity.cExtendedInfo

        Private _top As Integer
        Private _bottom As Integer

        Public Property Top() As Integer
            Get
                Return _top
            End Get
            Set(ByVal value As Integer)
                _top = value
            End Set
        End Property

        Public Property Bottom() As Integer
            Get
                Return _bottom
            End Get
            Set(ByVal value As Integer)
                _bottom = value
            End Set
        End Property
    End Class

    Private _borderStyle As BorderStyle
    Private _minuteHeight As Single = 1
    Private _dayWidth As Single = 40
    Private _startDate As Date = Now
    Private _endDate As Date = Now
    Private _headlineFont As New Font("Segoe UI", 8, FontStyle.Regular)
    Private _headlineBackColor As Brush = Brushes.DarkGray
    Private _programFont As New Font("Segoe UI", 6, FontStyle.Regular)
    Private Days As New Collections.Generic.SortedList(Of Date, Day)
    Private _extendedInfos As New Trinity.cWrapper
    Private _selectedProg As Program
    Private _topMaM As Integer

    Public Property TopMam() As Integer
        Get
            Return _topMaM
        End Get
        Set(ByVal value As Integer)
            _topMaM = value
        End Set
    End Property

    Friend Property SelectedProg() As Program
        Get
            Return _selectedProg
        End Get
        Set(ByVal value As Program)
            _selectedProg = value
        End Set
    End Property

    <Category("Appearance"), Description("Specifies the height of a minute in pixels.")> _
    Public Property MinuteHeight() As Single
        Get
            Return _minuteHeight
        End Get
        Set(ByVal value As Single)
            _minuteHeight = value
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

    <Category("Appearance"), Description("Specifies the width of a day in pixels.")> _
    Public Property DayWidth() As Single
        Get
            Return _dayWidth
        End Get
        Set(ByVal value As Single)
            _dayWidth = value
            SetupDays()
        End Set
    End Property

    <Category("Appearance"), Description("Specifies the border style.")> _
   Public Property BorderStyle() As BorderStyle
        Get
            Return _borderStyle
        End Get
        Set(ByVal value As BorderStyle)
            _borderStyle = value
            Invalidate()
        End Set
    End Property

    Public Property StartDate() As Date
        Get
            Return _startDate
        End Get
        Set(ByVal value As Date)
            _startDate = value
            If _startDate > _endDate Then _endDate = _startDate
            SetupDays()
        End Set
    End Property

    Public Property EndDate() As Date
        Get
            Return _endDate
        End Get
        Set(ByVal value As Date)
            _endDate = value
            If _endDate < _startDate Then _endDate = _startDate
            SetupDays()
        End Set
    End Property

    Public Property HeadlineFont() As Font
        Get
            Return _headlineFont
        End Get
        Set(ByVal value As Font)
            _headlineFont = value
        End Set
    End Property

    Public Property HeadlineBackcolor() As Brush
        Get
            Return _headlineBackColor
        End Get
        Set(ByVal value As Brush)
            _headlineBackColor = value
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

    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseClick(e)
        Dim LastKV As KeyValuePair(Of Date, Day) = Nothing
        'Dim g As Graphics = Me.CreateGraphics
        If e.Button = Windows.Forms.MouseButtons.Left Then

            ''Deselect currently selected

            'If Not _selectedProg Is Nothing Then
            '    Dim ProgRect As New Rectangle(Days(_selectedProg.ExtendedInfo.AirDate).Left, MinuteHeight * (_selectedProg.ExtendedInfo.MaM - TopMam) + Font.Height * 4 - VerticalScroll.Value, _dayWidth, _minuteHeight * _selectedProg.ExtendedInfo.Duration)
            '    If ProgRect.Top < Height AndAlso ProgRect.Left < Width Then
            '        g.Clip = New Region(ProgRect)
            '        If _selectedProg.ExtendedInfo.IsBooked Then
            '            g.FillRectangle(Brushes.LightBlue, ProgRect)
            '        Else
            '            g.FillRectangle(Brushes.White, ProgRect)
            '        End If
            '        g.DrawRectangle(Pens.Gray, ProgRect)
            '        g.DrawString(Trinity.Helper.Mam2Tid(_selectedProg.ExtendedInfo.MaM) & " " & _selectedProg.ExtendedInfo.ProgAfter, _programFont, Brushes.Black, Left + 2, _minuteHeight * (_selectedProg.ExtendedInfo.MaM - _topMaM) + 2 + Font.Height * 4 - VerticalScroll.Value)
            '        _selectedProg.Top = ProgRect.Top
            '        _selectedProg.Bottom = ProgRect.Bottom
            '    End If
            'End If
            Dim MouseX As Integer = e.X
            For Each kv As KeyValuePair(Of Date, Day) In Days
                If kv.Value.Left > MouseX Then
                    Exit For
                End If
                LastKV = kv
            Next
            Dim MouseY As Integer = e.Y
            For Each kv As KeyValuePair(Of String, Program) In LastKV.Value.Programs
                If MouseY > kv.Value.Top AndAlso MouseY < kv.Value.Bottom Then
                    _selectedProg = kv.Value
                    Exit For
                End If
            Next
            Dim ev As New ProgramEvent
            ev.ExtendedInfo = _selectedProg.ExtendedInfo
            RaiseEvent ProgramSelected(Me, ev)
        End If
        'Dim SelRect As New Rectangle(Days(_selectedProg.ExtendedInfo.AirDate).Left, MinuteHeight * (SelectedProg.ExtendedInfo.MaM - TopMam) + Font.Height * 4 - VerticalScroll.Value, DayWidth, MinuteHeight * SelectedProg.ExtendedInfo.Duration)
        'g.Clip = New Region(SelRect)
        'g.FillRectangle(Brushes.Yellow, SelRect)
        'g.DrawRectangle(Pens.Gray, SelRect)
        'g.DrawString(Trinity.Helper.Mam2Tid(_selectedProg.ExtendedInfo.MaM) & " " & _selectedProg.ExtendedInfo.ProgAfter, _programFont, Brushes.Black, Left + 2, MinuteHeight * (_selectedProg.ExtendedInfo.MaM - TopMam) + 2 + Font.Height * 4 - VerticalScroll.Value)
        Invalidate()
    End Sub

    Protected Overrides Sub OnDoubleClick(ByVal e As System.EventArgs)
        MyBase.OnDoubleClick(e)
        Dim ev As New ProgramEvent
        ev.ExtendedInfo = _selectedProg.ExtendedInfo
        RaiseEvent ProgramDoubleClicked(Me, ev)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim currentContext As BufferedGraphicsContext
        Dim myBuffer As BufferedGraphics
        currentContext = BufferedGraphicsManager.Current
        myBuffer = currentContext.Allocate(e.Graphics, DisplayRectangle)

        For Each kv As KeyValuePair(Of Date, Day) In Days
            kv.Value.Paint(e.Graphics)
        Next

        e.Graphics.FillRectangle(Brushes.White, 0, 0, 25, Me.Height)
        e.Graphics.DrawRectangle(Pens.Black, 0, 0, 25, Me.Height)
        For i As Integer = 0 To Me.AutoScrollMinSize.Height / _minuteHeight Step 30
            e.Graphics.DrawLine(Pens.Black, 0, i * _minuteHeight + HeadlineFont.Height * 4 - VerticalScroll.Value, 25, i * _minuteHeight + HeadlineFont.Height * 4 - VerticalScroll.Value)
            e.Graphics.DrawString(Trinity.Helper.Mam2Tid(i + TopMam), _programFont, Brushes.Black, 12.5 - e.Graphics.MeasureString(Trinity.Helper.Mam2Tid(i + TopMam), ProgramFont).Width / 2, i * _minuteHeight + 2 + HeadlineFont.Height * 4 - VerticalScroll.Value)
        Next
        If _borderStyle = Windows.Forms.BorderStyle.FixedSingle Then
            e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If
        'myBuffer.Render(e.Graphics)
        e.Graphics.Dispose()
    End Sub

    Private Sub SetupDays()
        Days.Clear()
        For i As Integer = _startDate.ToOADate To _endDate.ToOADate
            Dim d As New Day(Me)
            d.Left = (i - _startDate.ToOADate) * _dayWidth + 25
            d.Day = Date.FromOADate(i)
            Days.Add(d.Day, d)
            Me.AutoScrollMinSize = New Size(d.Left + _dayWidth + 5, Me.AutoScrollMinSize.Height)
        Next
        For Each de As DictionaryEntry In _extendedInfos
            Dim TmpEI As Trinity.cExtendedInfo = de.Value
            Dim TmpProg As New Program
            TmpProg.ExtendedInfo = TmpEI
            If Not Days(TmpEI.AirDate).Programs.ContainsKey(Format(TmpEI.MaM, "0000") & TmpEI.ProgAfter) Then
                Days(TmpEI.AirDate).Programs.Add(Format(TmpEI.MaM, "0000") & TmpEI.ProgAfter, TmpProg)
            End If
            '_progs.Add(Format(TmpEI.AirDate.ToOADate & TmpEI.MaM, "0000") & TmpEI.ProgAfter, TmpProg)
        Next
        Invalidate()
    End Sub

    Public Sub New()
        Me.AutoScroll = True
    End Sub


    Private Sub GfxSchedule_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles Me.Scroll
        Invalidate()
    End Sub
End Class


Public Class ProgramEvent
    Inherits EventArgs

    Public ExtendedInfo As Trinity.cExtendedInfo

End Class