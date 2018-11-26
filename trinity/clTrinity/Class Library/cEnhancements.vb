Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cEnhancements
        Implements IEnumerable

        Private mCol As New Collection
        Private _index As Trinity.cIndex
        'Private _specificFactor As Single = 1

        ''Factor used in calculations in Denmark
        'Public Property SpecificFactor() As Single
        '    Get
        '        Return _specificFactor
        '    End Get
        '    Set(ByVal value As Single)
        '        _specificFactor = value
        '    End Set
        'End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            On Error GoTo On_Error

            For Each TmpEn As Trinity.cEnhancement In Me
                Dim XMLEnhancements As Xml.XmlElement = xmlDoc.CreateElement("Enhancements")
                TmpEn.GetXML(XMLEnhancements, errorMessege, xmlDoc)
                colXml.AppendChild(XMLEnhancements)
            Next

On_Error:
            errorMessege.Add("Error saving Enhancement collection")
            Return False
        End Function

        Public ReadOnly Property Count() As Integer
            Get
                Return mCol.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Key As String) As cEnhancement
            Get
                If mCol.Contains(Key) Then
                    Return mCol(Key)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As cEnhancement
            Get
                If Index > 0 AndAlso Index <= mCol.Count Then
                    Return mCol(Index)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            mCol.Remove(vntIndexKey)
        End Sub

        Public Function Add() As cEnhancement
            Dim TmpComp As New cEnhancement(_index, mCol)

            mCol.Add(TmpComp, TmpComp.ID)
            Return TmpComp

        End Function

        Public ReadOnly Property FactoredIndex() As Single
            Get
                Dim TmpIdx As Single = 0
                For Each TmpEnh As cEnhancement In mCol
                    If TmpEnh.UseThis Then
                        TmpIdx += TmpEnh.Amount
                    End If
                Next
                TmpIdx /= 100
                TmpIdx = TmpIdx / (1 + TmpIdx) / (_index.ParentObject.EnhancementFactor / 100)
                TmpIdx = (1 / (1 - TmpIdx))
                Return TmpIdx * 100
            End Get
        End Property

        Public Sub New(ByVal Index As Trinity.cIndex)
            _index = Index
        End Sub

        Public Sub Clear()
            mCol.Clear()
        End Sub
    End Class
End Namespace