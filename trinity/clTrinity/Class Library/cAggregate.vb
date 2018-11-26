Public Class cAggregate

    Private _value As Single
    Private _needsUpdating As Boolean = True

    Public Event UpdateRequested(ByRef e As AggregateEventArgs, ByVal sender As Object)

    Public ReadOnly Property Value() As Single
        Get
            If _needsUpdating Then
                Dim TmpAEA As New AggregateEventArgs
                RaiseEvent UpdateRequested(TmpAEA, Me)
                _value = TmpAEA.Value
                If Not _value = 0 Then
                    _needsUpdating = False
                End If
            End If
            Return _value
        End Get
    End Property

    Public Sub Invalidate()
        _needsUpdating = True
    End Sub

    Private Sub cAggregate_UpdateRequested(ByRef e As AggregateEventArgs, ByVal sender As Object) Handles Me.UpdateRequested

    End Sub
End Class



Public Class AggregateEventArgs
    Inherits EventArgs

        Public Value As Single

End Class