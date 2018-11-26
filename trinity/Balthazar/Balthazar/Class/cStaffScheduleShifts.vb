Public Class cStaffScheduleShifts
    Implements ICollection

    Private _col As New Collection
    Private _day As cStaffScheduleDay

    Public Sub New(ByVal Day As cStaffScheduleDay)
        _day = Day
    End Sub

    Public Sub Add(ByVal item As cStaffScheduleShift)
        _col.Add(item, item.Shift.ID)
        '_day.Role.Location.Schedule.ShiftList.Add(item)
    End Sub

    Public Sub Clear()
        For Each TmpShift As cStaffScheduleShift In _col
            _day.Role.Location.Schedule.ShiftList.Remove(TmpShift)
        Next
        _col.Clear()
    End Sub

    Public Function Contains(ByVal item As cStaffScheduleShift) As Boolean
        Return _col.Contains(item.Shift.ID)
    End Function

    Public Function Contains(ByVal key As String) As Boolean
        Return _col.Contains(key)
    End Function

    Default ReadOnly Property Item(ByVal index As Integer) As cStaffScheduleShift
        Get
            Return _col(index)
        End Get
    End Property

    Default ReadOnly Property Item(ByVal key As String) As cStaffScheduleShift
        Get
            Return _col(key)
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Function Remove(ByVal item As cStaffScheduleShift) As Boolean
        _col.Remove(item.Shift.ID)
        _day.Role.Location.Schedule.ShiftList.Remove(item)
    End Function

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _col.GetEnumerator
    End Function

    Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo

    End Sub

    Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
        Get
            Return _col.Count
        End Get
    End Property

    Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
        Get

        End Get
    End Property

    Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
        Get

        End Get
    End Property
End Class
