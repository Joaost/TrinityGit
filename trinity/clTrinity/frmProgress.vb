
Public Enum cProgressBarType
    SingleBar
    DoubleBar
End Enum
Public Class frmProgress

    Private _totalOffset As Integer
    Private _barType As cProgressBarType = cProgressBarType.SingleBar
    Public Property Status() As String
        Get
            Return lblStatus.Text
        End Get
        Set(ByVal value As String)
            lblStatus.Text = value
        End Set
    End Property

    Public Property Progress() As Integer
        Get
            Return pbProgress.Value
        End Get
        Set(ByVal value As Integer)
            pbProgress.Value = value
            Windows.Forms.Application.DoEvents()
        End Set
    End Property

    Public Property MaxValue As Integer
        Get
            Return pbProgress.Maximum
        End Get
        Set(value As Integer)
            pbProgress.Maximum = value
        End Set
    End Property

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        MyBase.OnLoad(e)

        Me.BarType = cProgressBarType.SingleBar
    End Sub

    Public Property BarType As cProgressBarType
        Get
            Return Me._barType
        End Get
        Set(value As cProgressBarType)
            Me._barType = value

            If value = cProgressBarType.SingleBar Then
                Me.pbOverall.Hide()
                Me.lblTotalStatus.Hide()

                Me.Height = 110
                Windows.Forms.Application.DoEvents()
            ElseIf value = cProgressBarType.DoubleBar Then
                Me.pbOverall.Show()
                Me.lblTotalStatus.Show()

                Me.Height = 147
                Windows.Forms.Application.DoEvents()
            End If
        End Set
    End Property

    Public Property OverallStatus() As String
        Get
            Return lblTotalStatus.Text
        End Get
        Set(ByVal value As String)
            lblTotalStatus.Text = value
        End Set
    End Property

    Public Property OverallProgress() As Integer
        Get
            Return pbOverall.Value
        End Get
        Set(ByVal value As Integer)
            pbOverall.Value = value + Me._totalOffset
            Windows.Forms.Application.DoEvents()
        End Set
    End Property

    Public Property OverallMaxValue As Integer
        Get
            Return pbOverall.Maximum
        End Get
        Set(value As Integer)
            pbOverall.Maximum = value
        End Set
    End Property

    Public WriteOnly Property OverallOffset As Integer
        Set(value As Integer)
            Me._totalOffset = value
        End Set
    End Property

    Public Sub OverallAddOffset(ByVal i As Integer)
        Me._totalOffset += i
    End Sub

End Class