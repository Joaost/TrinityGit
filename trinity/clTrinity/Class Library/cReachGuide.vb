Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cReachguide

        Private mvarReferencePeriods As cReferencePeriods
        Private mvarSpots As cSpots
        Public StartDate As Date
        Public EndDate As Date
        Public Target As Trinity.cTarget
        Private _main As cKampanj

        Public ReadOnly Property MainObject() As cKampanj
            Get
                Return _main
            End Get
        End Property

        Public ReadOnly Property ReferencePeriods() As cReferencePeriods
            Get
                ReferencePeriods = mvarReferencePeriods
            End Get
        End Property

        Friend ReadOnly Property Spots() As cSpots
            Get
                Spots = mvarSpots
            End Get
        End Property

        Public Sub Calculate()

            Dim TmpPeriod As cReferencePeriod

            For Each TmpPeriod In mvarReferencePeriods
                TmpPeriod.Calculate()
            Next

        End Sub

        Public Function Reach(ByVal Freq As Byte) As Single

            Dim TotReach As Single
            Dim i As Integer

            For i = 1 To mvarReferencePeriods.Count
                TotReach = TotReach + mvarReferencePeriods(i).Reach(Freq)
            Next
            If mvarReferencePeriods.Count > 0 Then
                Reach = TotReach / mvarReferencePeriods.Count
            Else
                Reach = 0
            End If
        End Function

        Function TRP() As Single
            Dim TotTRP As Single
            Dim i As Integer

            For i = 1 To mvarReferencePeriods.Count
                TotTRP = TotTRP + mvarReferencePeriods(i).TRP
            Next
            If mvarReferencePeriods.Count > 0 Then
                Return TotTRP / mvarReferencePeriods.Count
            Else
                Return 0
            End If
        End Function

        Public Function Solus(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Channel As String, Optional ByVal Freq As Byte = 1, Optional ByVal Placement As Integer = 0) As Single
            Dim ID As String
            Dim ReachBefore As Single
            Dim ReachAfter As Single

            ID = CreateGUID()
            Calculate()
            ReachBefore = Reach(CByte(Freq))
            mvarSpots.Add(AirDate, MaM, Channel, ID, Placement)
            Calculate()
            ReachAfter = Reach(CByte(Freq))
            mvarSpots.Remove(ID)
            Calculate()
            Solus = ReachAfter - ReachBefore

        End Function

        Public Function TRP(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Channel As String, Optional ByVal Freq As Byte = 1) As Single
            Dim ID As String
            Dim TRPBefore As Single
            Dim TRPAfter As Single

            ID = CreateGUID()
            Calculate()
            TRPBefore = TRP()
            mvarSpots.Add(AirDate, MaM, Channel, ID)
            Calculate()
            TRPAfter = TRP()
            mvarSpots.Remove(ID)
            Calculate()
            Return TRPAfter - TRPBefore
        End Function

        Public Sub New(ByVal Main As cKampanj)
            mvarSpots = New cSpots(Me, Main)
            mvarSpots.Parent = Me
            mvarReferencePeriods = New cReferencePeriods(Me, Main)
            _main = Main
        End Sub
    End Class
End Namespace