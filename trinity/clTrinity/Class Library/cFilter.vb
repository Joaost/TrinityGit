Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cFilter
        Private mCol As New Hashtable

        Public Property Data(ByVal Headline As String, ByVal SubItem As String, Optional ByVal DefaultValue As Boolean = True) As Boolean
            Get
                If mCol.ContainsKey(Headline) Then
                    Dim ItemCol As Hashtable = mCol(Headline)
                    If ItemCol.ContainsKey(SubItem) Then
                        Return ItemCol(SubItem)
                    Else
                        Return DefaultValue
                    End If
                Else
                    Return DefaultValue
                End If
            End Get
            Set(ByVal value As Boolean)
                Dim ItemCol As Hashtable
                If mCol.ContainsKey(Headline) Then
                    ItemCol = mCol(Headline)
                Else
                    ItemCol = New Hashtable
                End If
                If ItemCol.ContainsKey(SubItem) Then
                    ItemCol(SubItem) = value
                Else
                    ItemCol.Add(SubItem, value)
                End If
                If Not mCol.ContainsKey(Headline) Then
                    mCol.Add(Headline, ItemCol)
                End If
            End Set
        End Property

        Public Function Count() As Integer
            Return mCol.Count
        End Function

        Public Function Table() As Hashtable
            Return mCol
        End Function

        Public Sub AllTrue(ByVal Headline As String)
            If mCol.ContainsKey(Headline) Then
                mCol.Item(Headline).clear()
            End If
        End Sub

    End Class
End Namespace