Public Class frmTweak


    Private _normalCurve(99) As Single
    Public Sub SetNormalCurve(ByVal Curve As Object)
        _normalCurve = Curve
    End Sub

    Private _orignalTRP(99) As Single
    Public Sub SetOriginalTRP(ByVal TRPs As Object)
        _orignalTRP = TRPs
    End Sub

    Private _lowAge As Integer
    Public Property LowAge() As Integer
        Get
            Return _lowAge
        End Get
        Set(ByVal value As Integer)
            _lowAge = value
        End Set
    End Property

    Private _highAge As Integer
    Public Property HighAge() As Integer
        Get
            Return _highAge
        End Get
        Set(ByVal value As Integer)
            _highAge = value
        End Set
    End Property

    Public Enum GenderEnum
        A = 0
        M = 1
        W = 2
    End Enum

    Private _gender As GenderEnum
    Public Property Gender() As GenderEnum
        Get
            Return _gender
        End Get
        Set(ByVal value As GenderEnum)
            _gender = value
        End Set
    End Property

    Private _baseRating As Single
    Public Property BaseRating() As Single
        Get
            Return _baseRating
        End Get
        Set(ByVal value As Single)
            _baseRating = value
        End Set
    End Property

    Private _correctionFactor As Single = 1
    Public Property CorrectionFactor() As Single
        Get
            Return _correctionFactor
        End Get
        Set(ByVal value As Single)
            _correctionFactor = value
        End Set
    End Property


    Private Sub TweakChart1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TweakChart1.Click

    End Sub

    Private Sub TweakChart1_DotMoveFinished() Handles TweakChart1.DotMoveFinished
        Const MAX = 100
        Const MULTIPLY = 1
        Dim IndexCurve(99) As Single
        Dim Rating(99) As Single
        Dim TotRating As Single
        For i As Integer = LowAge To HighAge
            Dim MyRating As Single = MAX - (((-51 + i) * MULTIPLY - (TweakChart1.AgeFactor * 100)) / 10) ^ 2
            IndexCurve(i) = MyRating / _normalCurve(i)
            Rating(i) = _baseRating * (_orignalTRP(i) / _baseRating) * IndexCurve(i)
            TotRating += Rating(i)
        Next
        TotRating /= (HighAge - LowAge) + 1
        Select Case _gender
            Case GenderEnum.M
                TotRating = (TotRating * 2) * (TweakChart1.GenderFactor + 0.5)
            Case GenderEnum.W
                TotRating = -(TotRating * 2) * (TweakChart1.GenderFactor - 0.5)
        End Select
        TotRating *= _correctionFactor
        lblEstimate.Text = Format(TotRating, "N1")
    End Sub

    Private Sub TweakChart1_DotMoving() Handles TweakChart1.DotMoving
        Const MAX = 100
        Const MULTIPLY = 1
        Dim IndexCurve(99) As Single
        Dim Rating(99) As Single
        Dim TotRating As Single
        For i As Integer = LowAge To HighAge
            Dim MyRating As Single = MAX - (((-51 + i) * MULTIPLY - (TweakChart1.AgeFactor * 100)) / 10) ^ 2
            IndexCurve(i) = MyRating / _normalCurve(i)
            Rating(i) = _baseRating * (_orignalTRP(i) / _baseRating) * IndexCurve(i)
            TotRating += Rating(i)
        Next
        TotRating /= (HighAge - LowAge) + 1
        Select Case _gender
            Case GenderEnum.M
                TotRating = (TotRating * 2) * (TweakChart1.GenderFactor + 0.5)
            Case GenderEnum.W
                TotRating = -(TotRating * 2) * (TweakChart1.GenderFactor - 0.5)
        End Select
        TotRating *= _correctionFactor
        lblEstimate.Text = Format(TotRating, "N1")
    End Sub
End Class