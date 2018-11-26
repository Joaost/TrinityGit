Public Class cScheduleSheet
    Public Property Cells As String(,)

    Private _rows As Integer
    Public ReadOnly Property Rows As Integer
        Get
            Return _rows
        End Get
    End Property

    Private _columns As Integer
    Public ReadOnly Property Columns As Integer
        Get
            Return _columns
        End Get
    End Property

    Public Sub New(Rows As Integer, Columns As Integer)
        _rows = Rows
        _columns = Columns
        ReDim _Cells(Rows, Columns)
    End Sub
End Class
