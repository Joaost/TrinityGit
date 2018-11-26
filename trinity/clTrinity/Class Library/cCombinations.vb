Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Namespace Trinity
    Public Class cCombinations
        Implements IEnumerable

        Private MainObject As cKampanj
        Private mCol As Collection
        Private _autoUpdateRelation As Boolean

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim xmlComb As Xml.XmlElement
            For Each TmpComb As Trinity.cCombination In Me
                xmlComb = xmlDoc.CreateElement("Combo")
                TmpComb.GetXML(xmlComb, errorMessege, xmlDoc)
                colXml.AppendChild(xmlComb)
            Next

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving Combinations collection")
            Return False
        End Function


        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Sub New(ByVal Main As cKampanj, AutoUpdateRelation As Boolean)
            mCol = New Collection
            MainObject = Main
            _autoUpdateRelation = AutoUpdateRelation
        End Sub

        Public Function Count() As Integer
            Return mCol.Count
        End Function

        Public Function Add() As cCombination
            Dim TmpCombo As New cCombination(MainObject, mCol, _autoUpdateRelation)
            mCol.Add(TmpCombo, TmpCombo.ID)
            Return TmpCombo
        End Function

        Public Sub Remove(ByVal ID As String)
            For Each _cc As Trinity.cCombinationChannel In DirectCast(mCol(ID), cCombination).Relations
                _cc.Bookingtype.Combination = Nothing
            Next
            mCol.Remove(ID)
        End Sub

        Public Function ContainsName(Name As String) As Boolean
            Return (From _c As cCombination In mCol Select _c Where _c.Name = Name).Count > 0
        End Function

        Public Sub Remove(ByVal Index As Integer)
            For Each _cc As Trinity.cCombinationChannel In DirectCast(mCol(Index), cCombination).Relations
                _cc.Bookingtype.Combination = Nothing
            Next
            mCol.Remove(Index)
        End Sub

        Public Sub Clear()
            mCol.Clear()
        End Sub

        Public Sub Remove(ByVal Combination As cCombination)
            For Each _cc As Trinity.cCombinationChannel In Combination.Relations
                If _cc.Bookingtype IsNot Nothing Then _cc.Bookingtype.Combination = Nothing
            Next
            mCol.Remove(Combination.ID)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cCombination
            Get
                Return mCol(vntIndexKey)
            End Get
        End Property
    End Class
End Namespace
