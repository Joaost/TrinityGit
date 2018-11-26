Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class Charts
    Inherits Control

    Private _campaigns As Dictionary(Of String, Trinity.cKampanj)
    Private _Karma As Trinity.cKarma
    Private _totalTRP As Single
    Private _chartColors As List(Of Color)
    Private _drawFrequency As Integer
    Private _borderStyle As BorderStyle

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

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim i As Integer
        Dim kv As KeyValuePair(Of String, Trinity.cKampanj)

        'Dim a As Single
        'Dim b As Single
        Dim x As Single
        Dim y As Single
        Dim LastX As Single
        Dim LastY As Single
        Dim c As Integer

        Dim ScaleLeft As Integer
        Dim ScaleRight As Integer
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Single
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single
        Dim LegendHeight As Single
        Dim col As Integer

        MyBase.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)

        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        ScaleWidth = TotalTRP
        ScaleHeight = 100
        ScaleLeft = 50
        ScaleRight = Me.Width - 125
        ScaleTop = 50
        ScaleBottom = (Me.Height - ScaleTop)
        If ScaleWidth > 0 Then
            ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
        End If
        ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight
        Dim Font = New Font("Segoe UI", 6)
        For i = 0 To 100 Step 10
            e.Graphics.DrawLine(Pens.LightGray, ScaleLeft, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
            e.Graphics.DrawString(Str(i), Font, Brushes.Black, ScaleLeft - e.Graphics.MeasureString(Str(i), Font).Width - 2, ScaleBottom - i * ScaleStepsY - (e.Graphics.MeasureString(Str(i), Font).Height / 2))
        Next
        For i = 0 To TotalTRP Step 50
            e.Graphics.DrawLine(Pens.Black, ScaleLeft + i * ScaleStepsX, ScaleBottom, ScaleLeft + i * ScaleStepsX, ScaleBottom + 2)
            e.Graphics.DrawString(Str(i), Font, Brushes.Black, ScaleLeft + i * ScaleStepsX - e.Graphics.MeasureString(Str(i), Font).Width / 2, ScaleBottom + 3)
        Next
        Font = New Font("Segoe UI", 8)
        e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)
        e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleLeft, ScaleTop)


        If _campaigns Is Nothing Then
            MyBase.Cursor = Cursors.Default
            Exit Sub
        End If
        LegendHeight = _campaigns.Count * (e.Graphics.MeasureString("A", Font).Height + 2) + 4
        i = ScaleTop + (ScaleBottom - ScaleTop) / 2 - (LegendHeight / 2) + 3
        col = 0
        'Creating the legend
        For Each kv In _campaigns
            e.Graphics.DrawString(kv.Key, Font, Brushes.Black, ScaleRight + 30, i)
            e.Graphics.DrawLine(New Pen(_chartColors(col), 2), ScaleRight + 14, i + e.Graphics.MeasureString("A", Font).Height / 2, ScaleRight + 26, i + e.Graphics.MeasureString("A", Font).Height / 2)
            i = i + e.Graphics.MeasureString("A", Font).Height + 2
            col = col + 1
        Next
        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(New Point(ScaleRight + 10, ScaleTop + (ScaleBottom - ScaleTop) / 2 - (LegendHeight / 2)), New Size(105, LegendHeight)))

        If Karma Is Nothing Then
            MyBase.Cursor = Cursors.Default
            Exit Sub
        End If
        If Karma.Channels.Count = 0 Then
            MyBase.Cursor = Cursors.Default
            Exit Sub
        End If
        If _drawFrequency < 1 Or _drawFrequency > 10 Then
            MyBase.Cursor = Cursors.Default
            Exit Sub
        End If
        For Each kv In Campaign.Campaigns
            For x = 0 To kv.Value.TotalTRP Step 0.5

                If x = 0.5 Then
                    LastX = x
                    LastY = y
                End If
                If x = 0 Then
                    y = 0
                    LastX = 0
                    LastY = 0
                Else
                    y = DirectCast(Karma.Campaigns(kv.Key), Trinity.cKarmaCampaign).ReachAtTRP(x, _drawFrequency)
                End If
                e.Graphics.DrawLine(New Pen(_chartColors(c), 2), LastX * ScaleStepsX + ScaleLeft, ScaleBottom - LastY * ScaleStepsY, x * ScaleStepsX + ScaleLeft, ScaleBottom - y * ScaleStepsY)
                LastX = x
                LastY = y
            Next
            c += 1
        Next
        'For Each kv In Campaign.Campaigns
        '    a = Karma.Campaigns.Item(kv.Key).B1(_drawFrequency)
        '    b = Karma.Campaigns.Item(kv.Key).B2(_drawFrequency)
        '    For x = 0 To kv.Value.TotalTRP Step 0.5
        '        If x <> 0 Then
        '            'Y = a * (x ^ b)
        '            y = a * x / (x + b)
        '            If x = 0.5 Then
        '                LastX = x
        '                LastY = y
        '            End If
        '            e.Graphics.DrawLine(New Pen(Color.Blue, 2), LastX * ScaleStepsX + ScaleLeft, ScaleBottom - LastY * ScaleStepsY, x * ScaleStepsX + ScaleLeft, ScaleBottom - y * ScaleStepsY)
        '            LastX = x
        '            LastY = y
        '        End If
        '    Next
        '    c = c + 1
        'Next
        MyBase.Cursor = Cursors.Default
    End Sub

    Public Property Campaigns() As Dictionary(Of String, Trinity.cKampanj)
        Get
            Return _campaigns
        End Get
        Set(ByVal value As Dictionary(Of String, Trinity.cKampanj))
            _campaigns = value
            Invalidate()
        End Set
    End Property

    Public Property Karma() As Object
        Get
            Return _karma
        End Get
        Set(ByVal value As Object)
            _Karma = value
            Invalidate()
        End Set
    End Property

    <Category("Settings"), Description("Specifies the the length of the x-axis.")> _
    Public Property TotalTRP() As Single
        Get
            Return _totalTRP
        End Get
        Set(ByVal value As Single)
            _totalTRP = value
            Invalidate()
        End Set
    End Property

    Public Sub New()
        _chartColors = New List(Of Color)
        _chartColors.Add(Color.Red)
        _chartColors.Add(Color.DarkOliveGreen)
        _chartColors.Add(Color.Green)
        _chartColors.Add(Color.Yellow)
        _chartColors.Add(Color.Gray)
        _chartColors.Add(Color.Brown)
        _chartColors.Add(Color.DarkRed)
        _chartColors.Add(Color.DarkBlue)
        _chartColors.Add(Color.DarkGreen)
        _chartColors.Add(Color.Gold)
        _chartColors.Add(Color.DarkGray)
        _chartColors.Add(Color.BlanchedAlmond)
    End Sub

    Public Property ChartColors() As List(Of Color)
        Get
            Return _chartColors
        End Get
        Set(ByVal value As List(Of Color))
            _chartColors = value
            Invalidate()
        End Set
    End Property

    <Category("Settings"), Description("Specifies on what frequency to draw the curves.")> _
  Public Property DrawFrequency() As Integer
        Get
            Return _drawFrequency
        End Get
        Set(ByVal value As Integer)
            _drawFrequency = value
            Invalidate()
        End Set
    End Property

End Class

Public Class TrendChart
    Inherits Control

    Private _borderStyle As BorderStyle
    Private _extendedInfo As Trinity.cExtendedInfo

    Private _period As Trinity.cPeriod
    Public Property Period() As Trinity.cPeriod
        Get
            Return _period
        End Get
        Set(ByVal value As Trinity.cPeriod)
            _period = value
            Invalidate()
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

    Public Property ExtendedInfo() As Trinity.cExtendedInfo
        Get
            Return _extendedInfo
        End Get
        Set(ByVal value As Trinity.cExtendedInfo)
            _extendedInfo = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim ScaleLeft As Integer
        Dim ScaleRight As Integer
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Single
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single
        Dim b As Integer
        Dim FirstVal As Single
        Dim MaxVal As Single
        Dim MinVal As Single
        Dim Mean As Single

        Dim x As Single
        Dim y As Single
        Dim k As Single
        Dim m As Single

        MyBase.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)
        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        If _extendedInfo Is Nothing Then Exit Sub
        If _extendedInfo.BreakList Is Nothing Then Exit Sub
        If _extendedInfo.BreakList.Count = 0 Then Exit Sub
        For b = 1 To _extendedInfo.BreakList.Count
            If MaxVal < _period.Adedge.getUnit(Connect.eUnits.uTRP, _extendedInfo.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(_period.Adedge, Campaign.MainTarget)) Then
                MaxVal = _period.Adedge.getUnit(Connect.eUnits.uTRP, _extendedInfo.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(_period.Adedge, Campaign.MainTarget))
            End If
        Next
        MaxVal = Int(MaxVal) + 1

        ScaleWidth = _extendedInfo.BreakList.Count + 2
        ScaleHeight = MaxVal
        ScaleLeft = 5
        ScaleRight = Me.Width - 5
        ScaleTop = 5
        ScaleBottom = (Me.Height - ScaleTop)
        If ScaleWidth > 0 Then
            ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
        End If
        ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight
        Dim Font = New Font("Segoe UI", 7)

        MinVal = 99
        For b = 1 To _extendedInfo.BreakList.Count
            x = b * ScaleStepsX
            y = ScaleBottom - _period.Adedge.getUnit(Connect.eUnits.uTRP, _extendedInfo.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(_period.Adedge, Campaign.MainTarget)) * ScaleStepsY
            'e.Graphics.DrawLine(New Pen(Color.Red, 3), x, y, x + 1, y + 1)
            Dim Rect As Drawing.Rectangle = New Rectangle(x, y, 2, 2)
            e.Graphics.DrawRectangle(New Pen(Color.Red), Rect)
            e.Graphics.FillRectangle(Brushes.Red, Rect)

            If b > 2 Then
                Mean = (_period.Adedge.getUnit(Connect.eUnits.uTRP, _extendedInfo.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(_period.Adedge, Campaign.MainTarget)) + _period.Adedge.getUnit(Connect.eUnits.uTRP, _extendedInfo.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(_period.Adedge, Campaign.MainTarget))) / 2
                'picTrend.Line Step(0, 0)-(r, Mean), 0
                If MaxVal < Mean Then
                    MaxVal = Mean
                End If
                If MinVal > Mean Then
                    MinVal = Mean
                End If
            ElseIf b > 1 Then
                Mean = (_period.Adedge.getUnit(Connect.eUnits.uTRP, _extendedInfo.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(_period.Adedge, Campaign.MainTarget)) + _period.Adedge.getUnit(Connect.eUnits.uTRP, _extendedInfo.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(_period.Adedge, Campaign.MainTarget))) / 2
                'picTrend.PSet (r, Mean), 0
                FirstVal = Mean
                If MaxVal < Mean Then
                    MaxVal = Mean
                End If
                If MinVal > Mean Then
                    MinVal = Mean
                End If
            End If
        Next
        If _extendedInfo.BreakList.Count <> 2 Then
            k = (Mean - FirstVal) / (_extendedInfo.BreakList.Count - 2)
        Else
            k = 0
        End If
        y = Mean
        x = _extendedInfo.BreakList.Count
        m = y - (k * x)
        x = 1
        y = k * x + m
        e.Graphics.DrawLine(New Pen(Color.Green, 2), x * ScaleStepsX, ScaleBottom - y * ScaleStepsY, (_extendedInfo.BreakList.Count + 1) * ScaleStepsX, ScaleBottom - (k * (_extendedInfo.BreakList.Count + 1) + m) * ScaleStepsY)
        e.Graphics.DrawLine(New Pen(Color.Blue, 2), x * ScaleStepsX, ScaleBottom - y * ScaleStepsY, _extendedInfo.BreakList.Count * ScaleStepsX, ScaleBottom - (k * _extendedInfo.BreakList.Count + m) * ScaleStepsY)
        e.Graphics.DrawString(Format((k * (_extendedInfo.BreakList.Count + 1) + m), "N1"), Font, Brushes.Black, (_extendedInfo.BreakList.Count + 1) * ScaleStepsX, ScaleBottom - (k * (_extendedInfo.BreakList.Count + 1) + m) * ScaleStepsY - e.Graphics.MeasureString("A", Font).Height / 2)
        MyBase.Cursor = Cursors.Default
    End Sub

End Class

Public Class ProfileChart
    Inherits Control

    Private _borderStyle As BorderStyle
    Private _target As Trinity.cTarget
    Private _ageTRP(0 To 13) As Single
    Private _avgRating As Single
    Private _showAverageRating As Boolean = False

    Public Property AverageRating() As Single
        Get
            Return _avgRating
        End Get
        Set(ByVal value As Single)
            _avgRating = value
            Invalidate()
        End Set
    End Property

    Public Property ShowAverageRating() As Boolean
        Get
            Return _showAverageRating
        End Get
        Set(ByVal value As Boolean)
            _showAverageRating = value
            Invalidate()
        End Set
    End Property

    Public Property AgeTRP(ByVal Index As Integer) As Single
        Get
            Return _ageTRP(Index)
        End Get
        Set(ByVal value As Single)
            _ageTRP(Index) = value
            Invalidate()
        End Set
    End Property
    Public Property Target() As Trinity.cTarget
        Get
            Return _target
        End Get
        Set(ByVal value As Trinity.cTarget)
            _target = value
            Invalidate()
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

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim TmpSex As String
        Dim LowAge As Integer
        Dim HighAge As Integer
        Dim i As Integer
        Dim TmpStr As String = ""
        Dim Max As Single
        Dim LowBound As Integer
        Dim HighBound As Integer
        Dim LastLow As Integer
        Dim LastHigh As Integer

        Dim ScaleLeft As Integer
        Dim ScaleRight As Integer
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Integer
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single

        MyBase.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)
        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        If _target Is Nothing Then Exit Sub

        Select Case _target.TargetNameNice.ToString.Substring(0, 1)
            Case "A" : TmpSex = ""
            Case "M" : TmpSex = "M"
            Case "W" : TmpSex = "W"
            Case Else : TmpSex = ""
        End Select
        Dim TmpArray() As String = {TmpSex & "3-11", TmpSex & "12-14", TmpSex & "15-19", TmpSex & "20-24", TmpSex & "25-29", TmpSex & "30-34", TmpSex & "35-39", TmpSex & "40-44", TmpSex & "45-49", TmpSex & "50-54", TmpSex & "55-59", TmpSex & "60-64", TmpSex & "60-69", TmpSex & "70-99"}

        If _target.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget Then
            If InStr(_target.TargetNameNice, "+") > 0 Then
                LowAge = Mid(_target.TargetNameNice, 2, InStr(_target.TargetNameNice, "+") - 2)
                HighAge = 99
            Else
                LowAge = Mid(_target.TargetNameNice, 2, InStr(_target.TargetNameNice, "-") - 2)
                HighAge = Mid(_target.TargetNameNice, Len(_target.TargetNameNice) - (Len(_target.TargetNameNice) - 3 - Len(Trim(LowAge))))
            End If
        Else
            LowAge = 3
            HighAge = 99
        End If
        For i = 0 To 13
            If _ageTRP(i) > Max Then Max = _ageTRP(i)
            TmpStr = TmpArray(i).ToString.Substring(0, InStr(TmpArray(i), "-") - 1)
            If Val(TmpStr.ToString.Substring(0, 1)) = 0 Then TmpStr = Mid(TmpStr, 2)
            If TmpStr <= LowAge Then
                LowBound = i + 1
                LastLow = TmpStr
            End If
            TmpStr = Mid(TmpArray(i), InStr(TmpArray(i), "-") + 1)
            If TmpStr <= HighAge Then
                HighBound = i + 1
                LastHigh = TmpStr
            End If
        Next
        'Max = Max + 5
        If Max > 0 Then
            ScaleHeight = Max
        Else
            ScaleHeight = 1
        End If
        ScaleWidth = 14
        ScaleLeft = 0
        ScaleRight = Me.Width
        Dim Font = Me.Font
        ScaleTop = e.Graphics.MeasureString("A", Font).Height
        ScaleBottom = (Me.Height - e.Graphics.MeasureString("A", Font).Height - 2)
        If ScaleWidth > 0 Then
            ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
        End If
        ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight
        e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)
        For i = 1 To 13
            If e.Graphics.MeasureString("W20-44", Font).Width < ScaleStepsX OrElse i / 2 <> i \ 2 Then
                e.Graphics.DrawString(Trim(TmpArray(i - 1)), Font, Brushes.DarkGray, (i * ScaleStepsX) - e.Graphics.MeasureString(Trim(TmpArray(i - 1)), Font).Width / 2, ScaleBottom + 1)
            End If
        Next
        For i = 1 To 13
            If i < LowBound Or i > HighBound Then
                Dim Rect As New Rectangle((i - 0.4) * ScaleStepsX, ScaleBottom - (_ageTRP(i) * ScaleStepsY), 0.8 * ScaleStepsX, Int(_ageTRP(i) * ScaleStepsY))
                e.Graphics.FillRectangle(Brushes.Red, Rect)
                e.Graphics.DrawRectangle(New Pen(Color.Black, 1), Rect)
            Else
                Dim Rect As New Rectangle((i - 0.4) * ScaleStepsX, ScaleBottom - (_ageTRP(i) * ScaleStepsY), 0.8 * ScaleStepsX, _ageTRP(i) * ScaleStepsY)
                e.Graphics.FillRectangle(Brushes.Blue, Rect)
                e.Graphics.DrawRectangle(New Pen(Color.Black, 1), Rect)
            End If
        Next
        If _showAverageRating Then
            Dim TmpPen As New Drawing.Pen(Color.Black)
            TmpPen.DashStyle = Drawing2D.DashStyle.Dash
            e.Graphics.DrawLine(TmpPen, ScaleLeft, ScaleBottom - _avgRating * ScaleStepsY, ScaleRight, ScaleBottom - _avgRating * ScaleStepsY)
        End If
        MyBase.Cursor = Cursors.Default
    End Sub



End Class

Public Class PIBChart
    Inherits Control

    Private _borderStyle As BorderStyle
    Private _first As Single
    Private _average As Single
    Private _last As Single

    Public Property Last() As Single
        Get
            Return _last
        End Get
        Set(ByVal value As Single)
            _last = value
            Invalidate()
        End Set
    End Property

    Public Property Average() As Single
        Get
            Return _average
        End Get
        Set(ByVal value As Single)
            _average = value
            Invalidate()
        End Set
    End Property

    Public Property First() As Single
        Get
            Return _first
        End Get
        Set(ByVal value As Single)
            _first = value
            Invalidate()
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

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim ScaleLeft As Single
        Dim ScaleRight As Single
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Single
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single
        Dim Max As Single
        Dim font = New Font("Segoe UI", 6)

        MyBase.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)
        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        If Max < _first Then Max = _first
        If Max < _average Then Max = _average
        If Max < _last Then Max = _last

        If Max > 0 Then
            ScaleHeight = Max
        Else
            ScaleHeight = 1
        End If

        ScaleWidth = 4
        ScaleLeft = 0
        ScaleRight = Me.Width
        ScaleTop = 11
        ScaleBottom = (Me.Height - 11)
        If ScaleWidth > 0 Then
            ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
        End If
        ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight

        Dim firstRect As New Rectangle(0.6 * ScaleStepsX, ScaleBottom - (_first * ScaleStepsY), 0.8 * ScaleStepsX, _first * ScaleStepsY)
        Dim averageRect As New Rectangle(1.6 * ScaleStepsX, ScaleBottom - (_average * ScaleStepsY), 0.8 * ScaleStepsX, _average * ScaleStepsY)
        Dim lastRect As New Rectangle(2.6 * ScaleStepsX, ScaleBottom - (_last * ScaleStepsY), 0.8 * ScaleStepsX, _last * ScaleStepsY)

        e.Graphics.FillRectangle(Brushes.Blue, firstRect)
        e.Graphics.FillRectangle(Brushes.Blue, averageRect)
        e.Graphics.FillRectangle(Brushes.Blue, lastRect)
        e.Graphics.DrawRectangle(Pens.Black, firstRect)
        e.Graphics.DrawRectangle(Pens.Black, averageRect)
        e.Graphics.DrawRectangle(Pens.Black, lastRect)

        e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)
        e.Graphics.DrawString("First", font, Brushes.Black, ScaleStepsX - e.Graphics.MeasureString("First", font).Width / 2, ScaleBottom + 1)
        e.Graphics.DrawString("Average", font, Brushes.Black, 2 * ScaleStepsX - e.Graphics.MeasureString("Average", font).Width / 2, ScaleBottom + 1)
        e.Graphics.DrawString("Last", font, Brushes.Black, 3 * ScaleStepsX - e.Graphics.MeasureString("Last", font).Width / 2, ScaleBottom + 1)

        e.Graphics.DrawString(Format(_first, "N1"), font, Brushes.Black, ScaleStepsX - e.Graphics.MeasureString(Format(_first, "N1"), font).Width / 2, ScaleBottom - (_first * ScaleStepsY) - e.Graphics.MeasureString(Format(_first, "N1"), font).Height)
        e.Graphics.DrawString(Format(_average, "N1"), font, Brushes.Black, 2 * ScaleStepsX - e.Graphics.MeasureString(Format(_average, "N1"), font).Width / 2, ScaleBottom - (_average * ScaleStepsY) - e.Graphics.MeasureString(Format(_average, "N1"), font).Height)
        e.Graphics.DrawString(Format(_last, "N1"), font, Brushes.Black, 3 * ScaleStepsX - e.Graphics.MeasureString(Format(_last, "N1"), font).Width / 2, ScaleBottom - (_last * ScaleStepsY) - e.Graphics.MeasureString(Format(_last, "N1"), font).Height)
        MyBase.Cursor = Cursors.Default

    End Sub
End Class

Public Class MonitorChart
    Inherits Control
    Private _borderStyle As BorderStyle
    Private _dimensions As Integer
    Private _dimensionLabel(0 To 255) As String
    Private _planned(0 To 255) As Single
    Private _booked(0 To 255) As Single
    Private _confirmed(0 To 255) As Single
    Private _actual(0 To 255) As Single
    Private _headline As String
    Private _headlineFont As Font

    Private Enum RectColor
        Red = 0
        Yellow = 1
        Blue = 2
        Green = 3
    End Enum

    Public Property HeadlineFont() As Font
        Get
            Return _headlineFont
        End Get
        Set(ByVal value As Font)
            _headlineFont = value
            Invalidate()
        End Set
    End Property

    Public Property Headline() As String
        Get
            Return _headline
        End Get
        Set(ByVal value As String)
            _headline = value
            Invalidate()
        End Set
    End Property

    Public Property Actual(ByVal Index As Integer) As Single
        Get
            Return _actual(Index)
        End Get
        Set(ByVal value As Single)
            _actual(Index) = value
        End Set
    End Property

    Public Property Confirmed(ByVal Index As Integer) As Single
        Get
            Return _confirmed(Index)
        End Get
        Set(ByVal value As Single)
            _confirmed(Index) = value
        End Set
    End Property

    Public Property Booked(ByVal Index As Integer) As Single
        Get
            Return _booked(Index)
        End Get
        Set(ByVal value As Single)
            _booked(Index) = value
        End Set
    End Property

    Public Property Planned(ByVal Index As Integer) As Single
        Get
            Return _planned(Index)
        End Get
        Set(ByVal value As Single)
            If Trinity.Helper.IsIndefinite(value) Then
                value = 0
            End If
            _planned(Index) = value
        End Set
    End Property

    Private Sub DrawStack(ByVal gr As Graphics, ByVal rect As Rectangle, ByVal printColor As RectColor, ByVal border_style As BorderStyle, ByVal Filled As Boolean, Optional ByVal Text As String = Nothing)
        If rect.Height = 0 Then Exit Sub

        Select Case border_style

            'just a single black border
            Case BorderStyle.FixedSingle
                If Filled Then
                    'Draw border
                    gr.DrawRectangle(Pens.Black, rect)

                    'fill it
                    rect.X += 1
                    rect.Y += 1
                    rect.Width -= 1
                    rect.Height -= 1
                    Dim b As Brush
                    Select Case printColor
                        Case Is = RectColor.Red
                            b = Brushes.Red
                            gr.FillRectangle(b, rect)
                        Case Is = RectColor.Yellow
                            b = Brushes.Yellow
                            gr.FillRectangle(b, rect)
                        Case Is = RectColor.Blue
                            b = Brushes.Blue
                            gr.FillRectangle(b, rect)
                        Case Is = RectColor.Green
                            b = Brushes.Green
                            gr.FillRectangle(b, rect)
                    End Select
                Else
                    gr.DrawRectangle(Pens.Black, rect)
                End If

                'if we have a text we print it on the rectangle
                If Not Text Is Nothing AndAlso gr.MeasureString("A", Font).Height < rect.Height Then
                    If gr.MeasureString(Text, Font).Width > rect.Width Then

                        'try to remove the decimals if it is a number to make it fit
                        Text = Text.Substring(0, Text.IndexOf(","))

                        Dim tmpFont As Font = Font
                        Dim i As Single = Font.Size
                        While gr.MeasureString(Text, tmpFont).Width > rect.Width
                            i -= 1
                            tmpFont = New Font(Font.FontFamily, i, FontStyle.Regular)
                            If tmpFont.Size < 5 Then
                                Text = ""
                                Exit While
                            End If
                        End While

                        'print the shortened and deacreased font text
                        If printColor = RectColor.Blue OrElse printColor = RectColor.Green Then
                            gr.DrawString(Text, tmpFont, Brushes.White, CSng((rect.Width - gr.MeasureString(Text, tmpFont).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y - (gr.MeasureString(Text, Font).Height / 2)))
                        Else
                            gr.DrawString(Text, tmpFont, Brushes.Black, CSng((rect.Width - gr.MeasureString(Text, tmpFont).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y - (gr.MeasureString(Text, Font).Height / 2)))
                        End If

                    Else
                        'print it
                        If printColor = RectColor.Blue OrElse printColor = RectColor.Green Then
                            gr.DrawString(Text, Font, Brushes.White, CSng((rect.Width - gr.MeasureString(Text, Font).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y - (gr.MeasureString(Text, Font).Height / 2)))
                        Else
                            gr.DrawString(Text, Font, Brushes.Black, CSng((rect.Width - gr.MeasureString(Text, Font).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y) - (gr.MeasureString(Text, Font).Height / 2))
                        End If
                    End If
                End If

                '3d border style
            Case BorderStyle.Fixed3D
                'Dim colors() As Color

                Dim b As Brush = Nothing
                Dim lightColor As Color
                Dim darkColor As Color
                Select Case printColor
                    Case Is = RectColor.Red
                        b = Brushes.Red
                        lightColor = Color.Pink
                        darkColor = Color.DarkRed
                    Case Is = RectColor.Yellow
                        b = Brushes.Yellow
                        lightColor = Color.White
                        darkColor = Color.DarkGoldenrod
                    Case Is = RectColor.Blue
                        b = Brushes.Blue
                        lightColor = Color.CornflowerBlue
                        darkColor = Color.DarkBlue
                    Case Is = RectColor.Green
                        b = Brushes.Green
                        lightColor = Color.YellowGreen
                        darkColor = Color.DarkGreen
                End Select

                'draws the border
                gr.DrawRectangle(Pens.Black, rect)

                Dim p As Pen
                p = New Pen(lightColor)

                'paint the bright border (top and left)
                gr.DrawLine(p, rect.X + 1, rect.Y + 1, rect.X + rect.Width - 1, rect.Y + 1)
                gr.DrawLine(p, rect.X + 1, rect.Y + 1, rect.X + 1, rect.Y + rect.Height - 1)

                p = New Pen(darkColor)

                'paint the dark border (bottom and right
                gr.DrawLine(p, rect.X + rect.Width - 1, rect.Y + rect.Height - 1, rect.X + rect.Width - 1, rect.Y + 1)
                gr.DrawLine(p, rect.X + rect.Width - 1, rect.Y + rect.Height - 1, rect.X + 1, rect.Y + rect.Height - 1)

                rect.Width -= 3
                rect.Height -= 3
                rect.X += 2
                rect.Y += 2
                gr.FillRectangle(b, rect)


                'if we have a text we print it on the rectangle
                If Not Text Is Nothing AndAlso gr.MeasureString("A", Font).Height < rect.Height Then
                    If gr.MeasureString(Text, Font).Width > rect.Width Then

                        'try to remove the decimals if it is a number to make it fit
                        Text = Text.Substring(0, Text.Replace(".", ",").IndexOf(","))

                        Dim tmpFont As Font = Font
                        Dim i As Single = Font.Size
                        While gr.MeasureString(Text, tmpFont).Width > rect.Width
                            i -= 1
                            tmpFont = New Font(Font.FontFamily, i, FontStyle.Regular)
                            If tmpFont.Size < 6 Then
                                'less than 6 is not readable
                                Text = ""
                                Exit While
                            End If
                        End While

                        'print the shortened and deacreased font text,
                        If printColor = RectColor.Blue OrElse printColor = RectColor.Green Then
                            gr.DrawString(Text, tmpFont, Brushes.White, CSng((rect.Width - gr.MeasureString(Text, tmpFont).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y - (gr.MeasureString(Text, Font).Height / 2)))
                        Else
                            gr.DrawString(Text, tmpFont, Brushes.Black, CSng((rect.Width - gr.MeasureString(Text, tmpFont).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y - (gr.MeasureString(Text, Font).Height / 2)))
                        End If

                    Else
                        'print it
                        If printColor = RectColor.Blue OrElse printColor = RectColor.Green Then
                            gr.DrawString(Text, Font, Brushes.White, CSng((rect.Width - gr.MeasureString(Text, Font).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y - (gr.MeasureString(Text, Font).Height / 2)))
                        Else
                            gr.DrawString(Text, Font, Brushes.Black, CSng((rect.Width - gr.MeasureString(Text, Font).Width) / 2 + rect.X), CSng((rect.Height / 2) + rect.Y - (gr.MeasureString(Text, Font).Height / 2)))
                        End If
                    End If
                End If
        End Select
    End Sub

    Public Sub CopyToClipboard()
        Dim TmpBMP As New Bitmap(Me.Width, Me.Height)
        Using gfx As Graphics = Graphics.FromImage(TmpBMP)
            gfx.Clear(Color.White)
            DrawChart(gfx)
        End Using
        Clipboard.SetImage(TmpBMP)
    End Sub

    Public Property Dimensions() As Integer
        Get
            Return _dimensions
        End Get
        Set(ByVal value As Integer)
            Dim i As Integer
            _dimensions = value
            For i = value To 255
                _planned(i) = 0
                _actual(i) = 0
                _booked(i) = 0
                _confirmed(i) = 0
            Next
            If _dimensions = 0 Then
                Invalidate()
            End If
        End Set
    End Property

    Public Property DimensionLabel(ByVal Index As Integer) As String
        Get
            Return _dimensionLabel(Index)
        End Get
        Set(ByVal value As String)
            _dimensionLabel(Index) = value
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

    Public Function speacialRound(ByVal value As Double) As Double
        If value.ToString.Replace(",", ".").IndexOf(".") = -1 Then
            Return value
        Else
            Return value.ToString.Substring(0, value.ToString.Replace(",", ".").IndexOf(".") + 2)
        End If

    End Function


    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

        Me.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)

        DrawChart(e.Graphics)

        Me.Cursor = Cursors.Default

    End Sub

    Sub DrawChart(ByVal gfx As Graphics)
        Dim ScaleLeft As Single
        Dim ScaleRight As Single
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Single
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single
        Dim LegendTop As Single
        Dim LegendWidth As Single
        Dim LegendHeight As Single
        Dim LegendLeft As Single

        Dim ElementLabels() As String = {"Planned", "Booked", "Confirmed", "Actual"}
        Dim ElementColors() As Brush = {Brushes.Red, Brushes.Yellow, Brushes.Blue, Brushes.Green}

        Dim MaxValue As Single
        Dim i As Integer
        Dim y As Integer

        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            gfx.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        For i = 0 To _dimensions - 1
            If _booked(i) > MaxValue Then
                MaxValue = _booked(i)
            End If
            If _confirmed(i) + _actual(i) > MaxValue Then
                MaxValue = _confirmed(i) + _actual(i)
            End If
            If _planned(i) > MaxValue Then
                MaxValue = _planned(i)
            End If
        Next
        MaxValue = (MaxValue \ 10 + 1) * 10
        ScaleHeight = MaxValue

        ScaleTop = CInt(gfx.MeasureString("A", _headlineFont).Height + 10)
        ScaleBottom = CInt(Me.Height - ScaleTop - gfx.MeasureString("A", Me.Font).Height - 4)
        'ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight 'the old line replaced by the one beneath
        ScaleStepsY = speacialRound(((Me.Height - (ScaleTop + (Me.Height - ScaleBottom))) / ScaleHeight))

        If ScaleStepsY = 0 Then
            ScaleStepsY = 0.5
        End If

        LegendHeight = (gfx.MeasureString("A", Me.Font).Height + 4) * 4
        LegendTop = ScaleTop + (ScaleHeight * ScaleStepsY) / 2 - LegendHeight / 2
        LegendWidth = gfx.MeasureString("Confirmed", Me.Font).Width + gfx.MeasureString("A", Me.Font).Height + 6
        LegendLeft = Me.Width - LegendWidth - 10

        'create the legend box
        Dim LegendBorder As New Drawing.Rectangle(LegendLeft, LegendTop, LegendWidth, LegendHeight)
        gfx.DrawRectangle(Pens.Black, LegendBorder)
        For i = 0 To 3
            y = LegendTop + (gfx.MeasureString("A", Me.Font).Height + 4) * i + 2
            gfx.FillRectangle(ElementColors(i), LegendLeft + 2, y, gfx.MeasureString("A", Me.Font).Height, gfx.MeasureString("A", Me.Font).Height)
            gfx.DrawRectangle(Pens.Black, LegendLeft + 2, y, gfx.MeasureString("A", Me.Font).Height, gfx.MeasureString("A", Me.Font).Height)
            gfx.DrawString(ElementLabels(i), Me.Font, Brushes.Black, LegendLeft + 2 + gfx.MeasureString("A", Me.Font).Height + 2, y + 1)
        Next

        ScaleWidth = _dimensions
        ScaleLeft = CInt(gfx.MeasureString("999999", Me.Font).Width + 5)
        ScaleRight = CInt(LegendLeft - 10)
        If ScaleWidth > 0 Then
            ScaleStepsX = speacialRound((Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth)
        End If
        gfx.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleLeft, ScaleTop)
        gfx.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)

        'we have different scaling on the left side depending on the max value
        Select Case MaxValue
            Case Is < 200
                For i = 0 To MaxValue Step 10
                    gfx.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
                    gfx.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - gfx.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - gfx.MeasureString("A", Font).Height / 2)
                Next
            Case Is < 500
                For i = 0 To MaxValue Step 20
                    gfx.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
                    gfx.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - gfx.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - gfx.MeasureString("A", Font).Height / 2)
                Next
            Case Is < 1000
                For i = 0 To MaxValue Step 50
                    gfx.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
                    gfx.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - gfx.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - gfx.MeasureString("A", Font).Height / 2)
                Next
            Case Is < 2000
                For i = 0 To MaxValue Step 100
                    gfx.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
                    gfx.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - gfx.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - gfx.MeasureString("A", Font).Height / 2)
                Next
            Case Is >= 2000
                For i = 0 To MaxValue Step 200
                    gfx.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
                    gfx.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - gfx.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - gfx.MeasureString("A", Font).Height / 2)
                Next
        End Select


        'print the headline for the diagram
        gfx.DrawString(_headline, _headlineFont, Brushes.Black, ScaleLeft + (ScaleWidth / 2) * ScaleStepsX - gfx.MeasureString(_headline, _headlineFont).Width / 2, ScaleTop / 2 - gfx.MeasureString(_headline, _headlineFont).Height / 2)


        For i = 1 To _dimensions
            If _booked(i - 1) > 0 Then

                Dim PlannedRect As New Rectangle(ScaleLeft + ((i - 0.9) * ScaleStepsX), ScaleBottom - CInt(_planned(i - 1) * ScaleStepsY), (0.18 * ScaleStepsX), CInt(_planned(i - 1) * ScaleStepsY))
                'gfx.FillRectangle(ElementColors(0), PlannedRect)
                'gfx.DrawRectangle(Pens.Black, PlannedRect)
                'If gfx.MeasureString("A", Font).Height < _planned(i - 1) * ScaleStepsY Then
                '    gfx.DrawString(Format(_planned(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.8) * ScaleStepsX - gfx.MeasureString(Format(_planned(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_planned(i - 1) * ScaleStepsY) / 2 - gfx.MeasureString("A", Font).Height / 2)
                'End If
                DrawStack(gfx, PlannedRect, RectColor.Red, Windows.Forms.BorderStyle.Fixed3D, True, Format(_planned(i - 1), "N1"))

                Dim BookedRect As New Rectangle(ScaleLeft + ((i - 0.7) * ScaleStepsX), ScaleBottom - CInt(_booked(i - 1) * ScaleStepsY), (0.18 * ScaleStepsX), CInt(_booked(i - 1) * ScaleStepsY))
                'gfx.FillRectangle(ElementColors(1), BookedRect)
                'gfx.DrawRectangle(Pens.Black, BookedRect)
                'If gfx.MeasureString("A", Font).Height < _booked(i - 1) * ScaleStepsY Then
                '    gfx.DrawString(Format(_booked(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.6) * ScaleStepsX - gfx.MeasureString(Format(_booked(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_booked(i - 1) * ScaleStepsY) / 2 - gfx.MeasureString("A", Font).Height / 2)
                'End If

                DrawStack(gfx, BookedRect, RectColor.Yellow, Windows.Forms.BorderStyle.Fixed3D, True, Format(_booked(i - 1), "N1"))
            Else
                Dim PlannedRect As New Rectangle(ScaleLeft + ((i - 0.9) * ScaleStepsX), ScaleBottom - CInt(_planned(i - 1) * ScaleStepsY), (0.36 * ScaleStepsX), CInt(_planned(i - 1) * ScaleStepsY))
                'gfx.FillRectangle(ElementColors(0), PlannedRect)
                'gfx.DrawRectangle(Pens.Black, PlannedRect)
                'If gfx.MeasureString("A", Font).Height < _planned(i - 1) * ScaleStepsY Then
                '    gfx.DrawString(Format(_planned(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.7) * ScaleStepsX - gfx.MeasureString(Format(_planned(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_planned(i - 1) * ScaleStepsY) / 2 - gfx.MeasureString("A", Font).Height / 2)
                'End If

                DrawStack(gfx, PlannedRect, RectColor.Red, Windows.Forms.BorderStyle.Fixed3D, True, Format(_planned(i - 1), "N1"))
            End If

            Dim ConfirmedRect As New Rectangle(ScaleLeft + ((i - 0.48) * ScaleStepsX), ScaleBottom - ((CInt(_confirmed(i - 1)) + CInt(_actual(i - 1))) * ScaleStepsY), (0.36 * ScaleStepsX), CInt(_confirmed(i - 1)) * ScaleStepsY)
            'gfx.FillRectangle(ElementColors(2), ConfirmedRect)
            'gfx.DrawRectangle(Pens.Black, ConfirmedRect)
            'If gfx.MeasureString("A", Font).Height < _confirmed(i - 1) * ScaleStepsY Then
            '    gfx.DrawString(Format(_confirmed(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.29) * ScaleStepsX - gfx.MeasureString(Format(_confirmed(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_confirmed(i - 1) / 2) * ScaleStepsY - _actual(i - 1) * ScaleStepsY - gfx.MeasureString("A", Font).Height / 2)
            'End If

            DrawStack(gfx, ConfirmedRect, RectColor.Blue, Windows.Forms.BorderStyle.Fixed3D, True, Format(_confirmed(i - 1), "N1"))

            Dim ActualRect As New Rectangle(ScaleLeft + ((i - 0.48) * ScaleStepsX), ScaleBottom - CInt(_actual(i - 1)) * ScaleStepsY, (0.36 * ScaleStepsX), CInt(_actual(i - 1)) * ScaleStepsY)

            'gfx.FillRectangle(ElementColors(3), ActualRect)
            'gfx.DrawRectangle(Pens.Black, ActualRect)
            'If gfx.MeasureString("A", Font).Height < _actual(i - 1) * ScaleStepsY Then
            '    gfx.DrawString(Format(_actual(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.29) * ScaleStepsX - gfx.MeasureString(Format(_actual(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_actual(i - 1) * ScaleStepsY) / 2)
            'End If

            DrawStack(gfx, ActualRect, RectColor.Green, Windows.Forms.BorderStyle.Fixed3D, True, Format(_actual(i - 1), "N1"))

            'the labels beneath the stacks
            gfx.DrawString(_dimensionLabel(i - 1), Font, Brushes.Black, ScaleLeft + (i - 0.5) * ScaleStepsX - gfx.MeasureString(_dimensionLabel(i - 1), Font).Width / 2, ScaleBottom + 2)

        Next
    End Sub
    'Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    '    Dim ScaleLeft As Single
    '    Dim ScaleRight As Single
    '    Dim ScaleHeight As Single
    '    Dim ScaleWidth As Single
    '    Dim ScaleTop As Single
    '    Dim ScaleBottom As Single
    '    Dim ScaleStepsX As Single
    '    Dim ScaleStepsY As Single
    '    Dim LegendTop As Single
    '    Dim LegendWidth As Single
    '    Dim LegendHeight As Single
    '    Dim LegendLeft As Single

    '    Dim ElementLabels() As String = {"Planned", "Booked", "Confirmed", "Actual"}
    '    Dim ElementColors() As Brush = {Brushes.Red, Brushes.Yellow, Brushes.Blue, Brushes.Green}

    '    Dim MaxValue As Single
    '    Dim i As Integer
    '    Dim y As Integer

    '    Me.Cursor = Cursors.WaitCursor
    '    MyBase.OnPaint(e)

    '    If _borderStyle <> Windows.Forms.BorderStyle.None Then
    '        e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
    '    End If

    '    For i = 0 To _dimensions - 1
    '        If _booked(i) > MaxValue Then
    '            MaxValue = _booked(i)
    '        End If
    '        If _confirmed(i) + _actual(i) > MaxValue Then
    '            MaxValue = _confirmed(i) + _actual(i)
    '        End If
    '        If _planned(i) > MaxValue Then
    '            MaxValue = _planned(i)
    '        End If
    '    Next
    '    MaxValue = (MaxValue \ 10 + 1) * 10
    '    ScaleHeight = MaxValue
    '    ScaleTop = e.Graphics.MeasureString("A", _headlineFont).Height + 10
    '    ScaleBottom = (Me.Height - ScaleTop - e.Graphics.MeasureString("A", Me.Font).Height - 4)
    '    'ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight
    '    ScaleStepsY = (Me.Height - (ScaleTop + (Me.Height - ScaleBottom))) / ScaleHeight

    '    LegendHeight = (e.Graphics.MeasureString("A", Me.Font).Height + 4) * 4
    '    LegendTop = ScaleTop + (ScaleHeight * ScaleStepsY) / 2 - LegendHeight / 2
    '    LegendWidth = e.Graphics.MeasureString("Confirmed", Me.Font).Width + e.Graphics.MeasureString("A", Me.Font).Height + 6
    '    LegendLeft = Me.Width - LegendWidth - 10
    '    Dim LegendBorder As New Drawing.Rectangle(LegendLeft, LegendTop, LegendWidth, LegendHeight)
    '    e.Graphics.DrawRectangle(Pens.Black, LegendBorder)
    '    For i = 0 To 3
    '        y = LegendTop + (e.Graphics.MeasureString("A", Me.Font).Height + 4) * i + 2
    '        e.Graphics.FillRectangle(ElementColors(i), LegendLeft + 2, y, e.Graphics.MeasureString("A", Me.Font).Height, e.Graphics.MeasureString("A", Me.Font).Height)
    '        e.Graphics.DrawRectangle(Pens.Black, LegendLeft + 2, y, e.Graphics.MeasureString("A", Me.Font).Height, e.Graphics.MeasureString("A", Me.Font).Height)
    '        e.Graphics.DrawString(ElementLabels(i), Me.Font, Brushes.Black, LegendLeft + 2 + e.Graphics.MeasureString("A", Me.Font).Height + 2, y + 1)
    '    Next

    '    ScaleWidth = _dimensions
    '    ScaleLeft = e.Graphics.MeasureString("999999", Me.Font).Width + 5
    '    ScaleRight = LegendLeft - 10
    '    If ScaleWidth > 0 Then
    '        ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
    '    End If
    '    e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleLeft, ScaleTop)
    '    e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)

    '    'we have different scaling on the left side depending on the max value
    '    Select Case MaxValue
    '        Case Is < 200
    '            For i = 0 To MaxValue Step 10
    '                e.Graphics.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
    '                e.Graphics.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - e.Graphics.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - e.Graphics.MeasureString("A", Font).Height / 2)
    '            Next
    '        Case Is < 500
    '            For i = 0 To MaxValue Step 20
    '                e.Graphics.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
    '                e.Graphics.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - e.Graphics.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - e.Graphics.MeasureString("A", Font).Height / 2)
    '            Next
    '        Case Is < 1000
    '            For i = 0 To MaxValue Step 50
    '                e.Graphics.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
    '                e.Graphics.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - e.Graphics.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - e.Graphics.MeasureString("A", Font).Height / 2)
    '            Next
    '        Case Is < 2000
    '            For i = 0 To MaxValue Step 100
    '                e.Graphics.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
    '                e.Graphics.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - e.Graphics.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - e.Graphics.MeasureString("A", Font).Height / 2)
    '            Next
    '        Case Is >= 2000
    '            For i = 0 To MaxValue Step 200
    '                e.Graphics.DrawLine(Pens.Black, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
    '                e.Graphics.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - e.Graphics.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - e.Graphics.MeasureString("A", Font).Height / 2)
    '            Next
    '    End Select


    '    e.Graphics.DrawString(_headline, _headlineFont, Brushes.Black, ScaleLeft + (ScaleWidth / 2) * ScaleStepsX - e.Graphics.MeasureString(_headline, _headlineFont).Width / 2, ScaleTop / 2 - e.Graphics.MeasureString(_headline, _headlineFont).Height / 2)

    '    For i = 1 To _dimensions
    '        If _booked(i - 1) > 0 Then
    '            Dim PlannedRect As New Rectangle(ScaleLeft + ((i - 0.9) * ScaleStepsX), ScaleBottom - _planned(i - 1) * ScaleStepsY, (0.18 * ScaleStepsX), Int(_planned(i - 1) * ScaleStepsY))
    '            e.Graphics.FillRectangle(ElementColors(0), PlannedRect)
    '            e.Graphics.DrawRectangle(Pens.Black, PlannedRect)
    '            If e.Graphics.MeasureString("A", Font).Height < _planned(i - 1) * ScaleStepsY Then
    '                e.Graphics.DrawString(Format(_planned(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.8) * ScaleStepsX - e.Graphics.MeasureString(Format(_planned(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_planned(i - 1) * ScaleStepsY) / 2 - e.Graphics.MeasureString("A", Font).Height / 2)
    '            End If

    '            Dim BookedRect As New Rectangle(ScaleLeft + ((i - 0.7) * ScaleStepsX), ScaleBottom - _booked(i - 1) * ScaleStepsY, (0.18 * ScaleStepsX), Int(_booked(i - 1) * ScaleStepsY))
    '            e.Graphics.FillRectangle(ElementColors(1), BookedRect)
    '            e.Graphics.DrawRectangle(Pens.Black, BookedRect)
    '            If e.Graphics.MeasureString("A", Font).Height < _booked(i - 1) * ScaleStepsY Then
    '                e.Graphics.DrawString(Format(_booked(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.6) * ScaleStepsX - e.Graphics.MeasureString(Format(_booked(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_booked(i - 1) * ScaleStepsY) / 2 - e.Graphics.MeasureString("A", Font).Height / 2)
    '            End If
    '        Else
    '            Dim PlannedRect As New Rectangle(ScaleLeft + ((i - 0.9) * ScaleStepsX), ScaleBottom - _planned(i - 1) * ScaleStepsY, (0.36 * ScaleStepsX), Int(_planned(i - 1) * ScaleStepsY))
    '            e.Graphics.FillRectangle(ElementColors(0), PlannedRect)
    '            e.Graphics.DrawRectangle(Pens.Black, PlannedRect)
    '            If e.Graphics.MeasureString("A", Font).Height < _planned(i - 1) * ScaleStepsY Then
    '                e.Graphics.DrawString(Format(_planned(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.7) * ScaleStepsX - e.Graphics.MeasureString(Format(_planned(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_planned(i - 1) * ScaleStepsY) / 2 - e.Graphics.MeasureString("A", Font).Height / 2)
    '            End If
    '        End If

    '        Dim ConfirmedRect As New Rectangle(ScaleLeft + ((i - 0.48) * ScaleStepsX), ScaleBottom - (_confirmed(i - 1) + _actual(i - 1)) * ScaleStepsY, (0.36 * ScaleStepsX), Int(_confirmed(i - 1) * ScaleStepsY))
    '        e.Graphics.FillRectangle(ElementColors(2), ConfirmedRect)
    '        e.Graphics.DrawRectangle(Pens.Black, ConfirmedRect)
    '        If e.Graphics.MeasureString("A", Font).Height < _confirmed(i - 1) * ScaleStepsY Then
    '            e.Graphics.DrawString(Format(_confirmed(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.29) * ScaleStepsX - e.Graphics.MeasureString(Format(_confirmed(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_confirmed(i - 1) / 2) * ScaleStepsY - _actual(i - 1) * ScaleStepsY - e.Graphics.MeasureString("A", Font).Height / 2)
    '        End If

    '        Dim ActualRect As New Rectangle(ScaleLeft + ((i - 0.48) * ScaleStepsX), ScaleBottom - _actual(i - 1) * ScaleStepsY, (0.36 * ScaleStepsX), Int(_actual(i - 1) * ScaleStepsY))
    '        e.Graphics.FillRectangle(ElementColors(3), ActualRect)
    '        e.Graphics.DrawRectangle(Pens.Black, ActualRect)
    '        If e.Graphics.MeasureString("A", Font).Height < _actual(i - 1) * ScaleStepsY Then
    '            e.Graphics.DrawString(Format(_actual(i - 1), "N1"), Font, Brushes.Black, ScaleLeft + (i - 0.29) * ScaleStepsX - e.Graphics.MeasureString(Format(_actual(i - 1), "N1"), Font).Width / 2, ScaleBottom - (_actual(i - 1) * ScaleStepsY) / 2)
    '        End If

    '        e.Graphics.DrawString(_dimensionLabel(i - 1), Font, Brushes.Black, ScaleLeft + (i - 0.5) * ScaleStepsX - e.Graphics.MeasureString(_dimensionLabel(i - 1), Font).Width / 2, ScaleBottom + 2)

    '    Next

    '    Me.Cursor = Cursors.Default
    'End Sub

    Public Sub New()
        _headlineFont = New Font("Segoe UI", 8)
    End Sub

    Private Sub MonitorChart_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Invalidate()
    End Sub

    Sub UpdateChart()
        Invalidate()
    End Sub
End Class

Public Class ReachChart
    Inherits Control

    Private _borderStyle As BorderStyle
    Private _days As Integer
    Private _reachGoal(10) As Single
    Private _actualReach(365, 10) As Single
    Private _showReach(10) As Boolean
    Private _showIntersect As Boolean
    Private _showBuildUp As Boolean
    Private _headline As String
    Private _headlineFont As Font

    Public Property HeadlineFont() As Font
        Get
            Return _headlineFont
        End Get
        Set(ByVal value As Font)
            _headlineFont = value
            Invalidate()
        End Set
    End Property

    Public Property Headline() As String
        Get
            Return _headline
        End Get
        Set(ByVal value As String)
            _headline = value
            Invalidate()
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

    Public Property Days() As Integer
        Get
            Return _days
        End Get
        Set(ByVal value As Integer)
            _days = value
        End Set
    End Property

    Public Property ReachGoal(ByVal Freq As Integer) As Single
        Get
            Return _reachGoal(Freq)
        End Get
        Set(ByVal value As Single)
            _reachGoal(Freq) = value
        End Set
    End Property

    Public Property ActualReach(ByVal Day As Integer, ByVal Freq As Integer) As Single
        Get
            Return _actualReach(Day, Freq)
        End Get
        Set(ByVal value As Single)
            _actualReach(Day, Freq) = value
        End Set
    End Property

    Public Property ShowReach(ByVal Freq As Integer) As Single
        Get
            Return _showReach(Freq)
        End Get
        Set(ByVal value As Single)
            _showReach(Freq) = value
            Invalidate()
        End Set
    End Property

    Public Property ShowIntersect() As Boolean
        Get
            Return _showIntersect
        End Get
        Set(ByVal value As Boolean)
            _showIntersect = value
            Invalidate()
        End Set
    End Property

    Public Property ShowBuildup() As Boolean
        Get
            Return _showBuildUp
        End Get
        Set(ByVal value As Boolean)
            _showBuildUp = value
            Invalidate()
        End Set
    End Property

    Public Sub CopyToClipboard()
        Dim TmpBMP As New Bitmap(Me.Width, Me.Height)
        Using gfx As Graphics = Graphics.FromImage(TmpBMP)
            gfx.Clear(Color.White)
            DrawChart(gfx)
        End Using
        Clipboard.SetImage(TmpBMP)
    End Sub

    Sub DrawChart(ByVal gfx As Graphics)
        Dim ScaleLeft As Single
        Dim ScaleRight As Single
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Single
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single
        Dim i As Integer
        Dim d As Integer
        Dim LastX As Integer
        Dim LastY As Integer

        Dim Colors() As Drawing.Color = {Color.Green, Color.Blue, Color.Red, Color.Yellow, Color.Turquoise, Color.Black, Color.Beige, Color.Purple, Color.Pink, Color.DarkGray}

        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            gfx.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        ScaleHeight = 100
        ScaleTop = gfx.MeasureString("A", _headlineFont).Height + 10
        ScaleBottom = (Me.Height - ScaleTop - gfx.MeasureString("A", Me.Font).Height - 4)
        'ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight
        ScaleStepsY = (Me.Height - (ScaleTop + (Me.Height - ScaleBottom))) / ScaleHeight

        ScaleWidth = _days
        ScaleLeft = gfx.MeasureString("999999", Me.Font).Width + 5
        ScaleRight = Me.Width - ScaleLeft
        If ScaleWidth > 0 Then
            ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
        End If
        gfx.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleLeft, ScaleTop)
        gfx.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)

        For i = 0 To 100 Step 10
            gfx.DrawLine(Pens.Gray, ScaleLeft - 3, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
            gfx.DrawString(i, Font, Brushes.Black, ScaleLeft - 4 - gfx.MeasureString(i, Font).Width, ScaleBottom - i * ScaleStepsY - gfx.MeasureString("A", Font).Height / 2)
        Next

        For i = 1 To 10
            If _showReach(i) Then
                If _showIntersect Then
                    Dim Pen As New Pen(Colors(i - 1), 1)
                    Pen.DashStyle = Drawing2D.DashStyle.Dash
                    gfx.DrawLine(Pen, ScaleLeft, ScaleBottom - _reachGoal(i) * ScaleStepsY, ScaleRight, ScaleBottom - _reachGoal(i) * ScaleStepsY)
                End If
                If _showBuildUp Then
                    Dim Pen As New Pen(Colors(i - 1), 1)
                    Pen.DashStyle = Drawing2D.DashStyle.Dash
                    gfx.DrawLine(Pen, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom - _reachGoal(i) * ScaleStepsY)
                End If
                LastX = ScaleLeft
                LastY = ScaleBottom
                For d = 1 To _days
                    If _actualReach(d, i) > 0 Then
                        gfx.DrawLine(New Pen(Colors(i - 1), 2), LastX, LastY, d * ScaleStepsX + ScaleLeft, ScaleBottom - ScaleStepsY * _actualReach(d, i))
                    End If
                    LastX = d * ScaleStepsX + ScaleLeft
                    LastY = ScaleBottom - ScaleStepsY * _actualReach(d, i)
                Next
            End If
        Next
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Me.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)

        DrawChart(e.Graphics)

        Me.Cursor = Cursors.Default
    End Sub

    Public Sub UpdateChart()
        Invalidate()
    End Sub

    Public Sub New()
        _headlineFont = New Font("Segoe UI", 8)
        _showReach(1) = True
    End Sub

    Private Sub ReachChart_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Invalidate()
    End Sub
End Class

Public Class GenderChart
    Inherits Control

    Private _borderStyle As BorderStyle
    Private _men As Decimal
    Private _women As Decimal

    Public Property Men() As Decimal
        Get
            Return _men
        End Get
        Set(ByVal value As Decimal)
            _men = value
            Invalidate()
        End Set
    End Property

    Public Property Women() As Decimal
        Get
            Return _women
        End Get
        Set(ByVal value As Decimal)
            _women = value
            Invalidate()
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

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim ScaleLeft As Single
        Dim ScaleRight As Single
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Single
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single
        Dim Max As Single
        Dim font = New Font("Segoe UI", 6)

        MyBase.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)
        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        If Max < _men Then Max = _men
        If Max < _women Then Max = _women

        If Max > 0 Then
            ScaleHeight = Max
        Else
            ScaleHeight = 1
        End If

        ScaleWidth = 3
        ScaleLeft = 0
        ScaleRight = Me.Width
        ScaleTop = 11
        ScaleBottom = (Me.Height - 11)
        If ScaleWidth > 0 Then
            ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
        End If
        ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight

        Dim firstRect As New Rectangle(0.6 * ScaleStepsX, ScaleBottom - (_men * ScaleStepsY), 0.8 * ScaleStepsX, _men * ScaleStepsY)
        Dim lastRect As New Rectangle(1.6 * ScaleStepsX, ScaleBottom - (_women * ScaleStepsY), 0.8 * ScaleStepsX, _women * ScaleStepsY)

        e.Graphics.FillRectangle(Brushes.Blue, firstRect)
        e.Graphics.FillRectangle(Brushes.Blue, lastRect)
        e.Graphics.DrawRectangle(Pens.Black, firstRect)
        e.Graphics.DrawRectangle(Pens.Black, lastRect)

        e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)
        e.Graphics.DrawString("Men", font, Brushes.Black, ScaleStepsX - e.Graphics.MeasureString("Men", font).Width / 2, ScaleBottom + 1)
        e.Graphics.DrawString("Women", font, Brushes.Black, 2 * ScaleStepsX - e.Graphics.MeasureString("Women", font).Width / 2, ScaleBottom + 1)

        e.Graphics.DrawString(Format(_men, "N1"), font, Brushes.Black, ScaleStepsX - e.Graphics.MeasureString(Format(_men, "N1"), font).Width / 2, ScaleBottom - (_men * ScaleStepsY) - e.Graphics.MeasureString(Format(_men, "N1"), font).Height)
        e.Graphics.DrawString(Format(_women, "N1"), font, Brushes.Black, 2 * ScaleStepsX - e.Graphics.MeasureString(Format(_women, "N1"), font).Width / 2, ScaleBottom - (_women * ScaleStepsY) - e.Graphics.MeasureString(Format(_women, "N1"), font).Height)
        MyBase.Cursor = Cursors.Default

    End Sub

End Class

Public Class PrimePeakChart
    Inherits Control

    Private _borderStyle As BorderStyle
    Private _peak As Decimal
    Private _offpeak As Decimal

    Public Property Peak() As Decimal
        Get
            Return _peak
        End Get
        Set(ByVal value As Decimal)
            _peak = value
            Invalidate()
        End Set
    End Property

    Public Property OffPeak() As Decimal
        Get
            Return _offpeak
        End Get
        Set(ByVal value As Decimal)
            _offpeak = value
            Invalidate()
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

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim ScaleLeft As Single
        Dim ScaleRight As Single
        Dim ScaleHeight As Single
        Dim ScaleWidth As Single
        Dim ScaleTop As Single
        Dim ScaleBottom As Single
        Dim ScaleStepsX As Single
        Dim ScaleStepsY As Single
        Dim Max As Single
        Dim font = New Font("Segoe UI", 6)

        MyBase.Cursor = Cursors.WaitCursor
        MyBase.OnPaint(e)
        If _borderStyle <> Windows.Forms.BorderStyle.None Then
            e.Graphics.DrawRectangle(Drawing.Pens.Black, New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1)))
        End If

        If Max < _peak Then Max = _peak
        If Max < _offpeak Then Max = _offpeak

        If Max > 0 Then
            ScaleHeight = Max
        Else
            ScaleHeight = 1
        End If

        ScaleWidth = 3
        ScaleLeft = 0
        ScaleRight = Me.Width
        ScaleTop = 11
        ScaleBottom = (Me.Height - 11)
        If ScaleWidth > 0 Then
            ScaleStepsX = (Me.Width - (ScaleLeft + (Me.Width - ScaleRight))) / ScaleWidth
        End If
        ScaleStepsY = (Me.Height - ScaleTop * 2) / ScaleHeight

        Dim firstRect As New Rectangle(0.6 * ScaleStepsX, ScaleBottom - (_peak * ScaleStepsY), 0.8 * ScaleStepsX, _peak * ScaleStepsY)
        Dim lastRect As New Rectangle(1.6 * ScaleStepsX, ScaleBottom - (_offpeak * ScaleStepsY), 0.8 * ScaleStepsX, _offpeak * ScaleStepsY)

        e.Graphics.FillRectangle(Brushes.Blue, firstRect)
        e.Graphics.FillRectangle(Brushes.Blue, lastRect)
        e.Graphics.DrawRectangle(Pens.Black, firstRect)
        e.Graphics.DrawRectangle(Pens.Black, lastRect)

        e.Graphics.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)
        e.Graphics.DrawString("Peak", font, Brushes.Black, ScaleStepsX - e.Graphics.MeasureString("Men", font).Width / 2, ScaleBottom + 1)
        e.Graphics.DrawString("offPeak", font, Brushes.Black, 2 * ScaleStepsX - e.Graphics.MeasureString("Women", font).Width / 2, ScaleBottom + 1)

        e.Graphics.DrawString(Format(_peak, "N1"), font, Brushes.Black, ScaleStepsX - e.Graphics.MeasureString(Format(_peak, "N1"), font).Width / 2, ScaleBottom - (_peak * ScaleStepsY) - e.Graphics.MeasureString(Format(_peak, "N1"), font).Height)
        e.Graphics.DrawString(Format(_offpeak, "N1"), font, Brushes.Black, 2 * ScaleStepsX - e.Graphics.MeasureString(Format(_offpeak, "N1"), font).Width / 2, ScaleBottom - (_offpeak * ScaleStepsY) - e.Graphics.MeasureString(Format(_offpeak, "N1"), font).Height)
        MyBase.Cursor = Cursors.Default

    End Sub

End Class

Public Class TweakChart
    Inherits Control

    Private _originalAgeFactor As Double = 0
    Public Property OriginalAgeFactor() As Double
        Get
            Return _originalAgeFactor
        End Get
        Set(ByVal value As Double)
            _originalAgeFactor = value
        End Set
    End Property

    Private _originalGenderFactor As Double = 0
    Public Property OriginalGenderFactor() As Double
        Get
            Return _originalGenderFactor
        End Get
        Set(ByVal value As Double)
            _originalGenderFactor = value
        End Set
    End Property

    Private _ageFactor As Double = 0
    Public Property AgeFactor() As Double
        Get
            Return _ageFactor
        End Get
        Set(ByVal value As Double)
            _ageFactor = value
        End Set
    End Property

    Private _genderFactor As Double = 0
    Public Property GenderFactor() As Double
        Get
            Return _genderFactor
        End Get
        Set(ByVal value As Double)
            _genderFactor = value
        End Set
    End Property

    Private _myDot As Rectangle
    Private _originalDot As Rectangle
    Private _myMargin As Single
    Private _moving As Boolean = False

    Public Event DotMoveFinished()
    Public Event DotMoving()

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

        Using fnt As New Font("Segoe UI", 8, FontStyle.Regular)
            Using gfx As Graphics = e.Graphics

                _myMargin = gfx.MeasureString("99", fnt).Height * 2
                If _myMargin < gfx.MeasureString("W", fnt).Width * 2 Then
                    _myMargin = gfx.MeasureString("W", fnt).Width * 2
                End If

                gfx.FillRectangle(Brushes.White, 0, 0, Me.Width, Me.Height)
                gfx.DrawString("99", fnt, Brushes.DarkGray, Me.Width / 2 - gfx.MeasureString("99", fnt).Width / 2, _myMargin / 2 - gfx.MeasureString("99", fnt).Height / 2)
                gfx.DrawString("3", fnt, Brushes.DarkGray, Me.Width / 2 - gfx.MeasureString("3", fnt).Width / 2, Me.Height - _myMargin / 2 - gfx.MeasureString("3", fnt).Height / 2)
                gfx.DrawString("W", fnt, Brushes.DarkGray, _myMargin / 2 - gfx.MeasureString("W", fnt).Width / 2, Me.Height / 2 - gfx.MeasureString("W", fnt).Height / 2)
                gfx.DrawString("M", fnt, Brushes.DarkGray, Me.Width - _myMargin / 2 - gfx.MeasureString("W", fnt).Width / 2, Me.Height / 2 - gfx.MeasureString("W", fnt).Height / 2)
                gfx.DrawLine(Pens.Black, _myMargin, CSng(Me.Height / 2), CSng(Me.Width - _myMargin), CSng(Me.Height / 2))
                gfx.DrawLine(Pens.Black, CSng(Me.Width / 2), _myMargin, CSng(Me.Width / 2), CSng(Me.Height - _myMargin))

                _originalDot.X = Me.Width / 2 + (Me.Width / 2 - _myMargin) * _originalGenderFactor / 0.5 - _originalDot.Width / 2
                _originalDot.Y = Me.Height / 2 - (Me.Height / 2 - _myMargin) * _originalAgeFactor / 0.5 - _originalDot.Height / 2
                Using dashPen As New Pen(Color.Black, 1)
                    dashPen.DashStyle = Drawing2D.DashStyle.Dash
                    gfx.DrawRectangle(dashPen, _originalDot)
                End Using

                _myDot.X = Me.Width / 2 + (Me.Width / 2 - _myMargin) * _genderFactor / 0.5 - _myDot.Width / 2
                _myDot.Y = Me.Height / 2 - (Me.Height / 2 - _myMargin) * _ageFactor / 0.5 - _myDot.Height / 2
                gfx.FillRectangle(Brushes.Blue, _myDot)
                gfx.DrawRectangle(Pens.Black, _myDot)
            End Using
            MyBase.OnPaint(e)
        End Using
    End Sub

    Public Sub New()
        _myDot = New Rectangle
        With _myDot
            .Size = New Size(8, 8)
        End With
        _originalDot = New Rectangle
        With _originalDot
            .Size = New Size(8, 8)
        End With
    End Sub

    Public Sub DotMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim rect As New Rectangle(e.X, e.Y, 1, 1)
        If _myDot.IntersectsWith(rect) Then
            _moving = True
        End If
    End Sub

    Public Sub DotMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim rect As New Rectangle(e.X, e.Y, 1, 1)
        If _myDot.IntersectsWith(rect) Then
            Cursor = Cursors.SizeAll
        Else
            Cursor = Cursors.Default
        End If
        If _moving Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                _genderFactor = (e.X - Me.Width / 2) / (Me.Width / 2 - _myMargin) * 0.5
                _ageFactor = -(e.Y - Me.Height / 2) / (Me.Height / 2 - _myMargin) * 0.5
                If _genderFactor < -0.5 Then _genderFactor = -0.5
                If _genderFactor > 0.5 Then _genderFactor = 0.5
                If _ageFactor < -0.5 Then _ageFactor = -0.5
                If _ageFactor > 0.5 Then _ageFactor = 0.5
                Invalidate()
                RaiseEvent DotMoving()
            End If
        End If
    End Sub

    Public Sub DotMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        If _moving Then
            RaiseEvent DotMoveFinished()
        End If
        _moving = False
    End Sub
End Class