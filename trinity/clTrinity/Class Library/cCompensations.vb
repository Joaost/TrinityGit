Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cCompensations
        Implements IEnumerable

        Private mCol As New Collection
        Private _bookingtype As Trinity.cBookingType

        Public ReadOnly Property GetCollection()
            Get
                Return mCol
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim comp As Xml.XmlElement
            For Each TmpComp As Trinity.cCompensation In Me
                comp = xmlDoc.CreateElement("Compensation")
                TmpComp.GetXML(comp, errorMessege, xmlDoc)
                colXml.AppendChild(comp)
            Next

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving Compensations collection")
            Return False
        End Function

        Public ReadOnly Property Count() As Integer
            Get
                Return mCol.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cCompensation
            Get
                If mCol.Contains(vntIndexKey) Then
                    Return mCol(vntIndexKey)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            mCol.Remove(vntIndexKey)
        End Sub

        Public Function Add() As cCompensation
            Dim TmpComp As New cCompensation(_bookingtype, mCol)

            mCol.Add(TmpComp, TmpComp.ID)
            Return TmpComp

        End Function

        Public Sub New(ByVal Bookingtype As Trinity.cBookingType)
            _bookingtype = Bookingtype
        End Sub

        Public Function GetCompensationForDate(ByVal d As Date) As Single
            Dim TmpTRP As Single = 0

            For Each TmpComp As Trinity.cCompensation In mCol
                Dim countActualDays As Integer = 0
                'count the actual days the campaign will run in the compensation
                For Each w As Trinity.cWeek In _bookingtype.Main.Channels(1).BookingTypes(1).Weeks
                    For day As Integer = w.StartDate To w.EndDate
                        If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                            countActualDays += 1
                        End If
                    Next
                Next

                If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
                    TmpTRP += TmpComp.TRPs * (1 / countActualDays)
                End If

                'If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
                '    TmpTRP = TmpTRP + TmpComp.TRPs / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1)
                'End If
            Next
            Return TmpTRP

        End Function

        Public Function GetCompensationForDateInMainTarget(ByVal d As Date) As Single
            Dim TmpTRP As Single = 0

            For Each TmpComp As Trinity.cCompensation In mCol
                Dim countActualDays As Integer = 0
                'count the actual days the campaign will run in the compensation
                For Each w As Trinity.cWeek In _bookingtype.Main.Channels(1).BookingTypes(1).Weeks
                    For day As Integer = w.StartDate To w.EndDate
                        If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                            countActualDays += 1
                        End If
                    Next
                Next

                If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
                    TmpTRP += TmpComp.TRPMainTarget * (1 / countActualDays)
                End If

                'If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
                '    TmpTRP = TmpTRP + (TmpComp.TRPMainTarget / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1))
                'End If
            Next
            Return TmpTRP

        End Function

        Public Function GetCompensationForDateInAllAdults(ByVal d As Date) As Single
            Dim TmpTRP As Single = 0

            For Each TmpComp As Trinity.cCompensation In mCol
                Dim countActualDays As Integer = 0
                'count the actual days the campaign will run in the compensation
                For Each w As Trinity.cWeek In _bookingtype.Main.Channels(1).BookingTypes(1).Weeks
                    For day As Integer = w.StartDate To w.EndDate
                        If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                            countActualDays += 1
                        End If
                    Next
                Next

                If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
                    TmpTRP += TmpComp.TRPAllAdults * (1 / countActualDays)
                End If

                'If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
                '    TmpTRP = TmpTRP + (TmpComp.TRPAllAdults / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1))
                'End If
            Next
            Return TmpTRP

        End Function

    End Class
End Namespace