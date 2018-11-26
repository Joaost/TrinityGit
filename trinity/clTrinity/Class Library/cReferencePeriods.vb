Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cReferencePeriods
        Implements Collections.IEnumerable
        Private mCol As New Collection
        Private _base As cReachguide
        Private _main As cKampanj

        Function Add(ByVal StartDate As Date, ByVal Name As String) As cReferencePeriod

            Dim TmpPeriod As New cReferencePeriod(_base, _main)

            TmpPeriod.StartDate = StartDate
            TmpPeriod.Name = Name

            mCol.Add(TmpPeriod, Name)

            Add = mCol(Name)
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cReferencePeriod
            Get
                Item = mCol(vntIndexKey)
            End Get
        End Property

        Public Function Count() As Integer
            Count = mCol.Count
        End Function

        Public Sub Remove(ByVal vntIndexKey As Object)
            mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Function Contains(ByVal Name As String) As Boolean
            Return mCol.Contains(Name)
        End Function

        Public Sub New(ByVal Base As cReachguide, Campaign As Trinity.cKampanj)
            _main = Campaign
            _base = Base
        End Sub
    End Class
End Namespace