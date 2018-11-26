Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cSpots
        Implements IEnumerable

        Private mCol As New cWrapper
        Private mvarParent As Object
        Private Main As cKampanj
        Private _base As cReachguide

        Public Function Add(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Channel As String, ByVal ID As String, Optional ByVal Placement As Integer = 0)

            Dim TmpSpot As New cSpot
            Dim i As Integer

            If mvarParent.StartDate = _base.StartDate Then
                TmpSpot.AirDate = AirDate
                TmpSpot.MaM = MaM
                TmpSpot.Channel = Channel
                For i = 1 To _base.ReferencePeriods.Count
                    _base.ReferencePeriods.Item(i).Spots.Add(AirDate - (_base.StartDate - _base.ReferencePeriods.Item(i).StartDate), MaM, Channel, ID, Placement)
                Next
            Else
                TmpSpot = FindBestSpot(AirDate, MaM, Channel, Placement)
            End If
            TmpSpot.ID = ID
            On Error Resume Next
            Add = mCol.Add(TmpSpot, ID)
            On Error GoTo 0
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cSpot
            Get
                Item = mCol(vntIndexKey)
            End Get
        End Property

        Friend WriteOnly Property Parent()
            Set(ByVal value)
                mvarParent = value
            End Set
        End Property

        Private Function FindBestSpot(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Channel As String, Optional ByVal Placement As Integer = 0) As cSpot

            Dim i As Long
            Dim LastDistance As Long
            Dim TmpSpot As cSpot
            Dim TmpPeriod As Trinity.cReferencePeriod = mvarParent

            LastDistance = 999999
            i = 0
            While Channel <> Trinity.Helper.Adedge2Channel(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aChannel, i)).ChannelName
                i = i + TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
            End While
            While Format(Date.FromOADate(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aDate, i)), "yyyy-MM-dd") < Format(AirDate, "yyyy-MM-dd")
                i = i + TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
            End While

            Try
                While Math.Abs(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60 - MaM) <= LastDistance
                    LastDistance = Math.Abs(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60 - MaM)
                    i = i + TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
                End While
            Catch
                'might not have a good match so we check the day after as well
                'mam is decreased by 24 hours (24*60)

                'if we are at hte last day we cant check any further ahead
                If AirDate = TmpPeriod.StartDate.AddDays(Main.EndDate - Main.StartDate) Then Exit Try

                TmpSpot = FindBestSpot(AirDate.AddDays(1), MaM - 1440, Channel, Placement)

                If LastDistance > Math.Abs(MaM - TmpSpot.MaM) Then
                    FindBestSpot = TmpSpot
                    Exit Function
                End If
            End Try

            


            i = i - TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, i - 1)
            If Placement = 0 Then
                i = i + Int(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i) / 2)
            ElseIf Placement = -1 Then
                i = i
            ElseIf Placement = 1 Then
                i = i + Int(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)) - 1
            End If
            TmpSpot = New cSpot
            TmpSpot.AirDate = AirDate
            TmpSpot.Channel = Channel
            TmpSpot.MaM = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60
            TmpSpot.SpotIndex = i
            TmpSpot.TRP = TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, i)
            TmpSpot.Programme = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, i)

            FindBestSpot = TmpSpot
            '    Stop
        End Function

        Public Function Count() As Integer
            Count = mCol.Count
        End Function

        Public Sub Remove(ByVal vntIndexKey As Object)

            Dim i As Integer

            On Error Resume Next
            If mCol.Exists(vntIndexKey) Then mCol.Remove(vntIndexKey)
            On Error GoTo ErrHandle

            If mvarParent.StartDate = _base.StartDate Then
                For i = 1 To _base.ReferencePeriods.Count
                    _base.ReferencePeriods.Item(i).Spots.Remove(vntIndexKey)
                Next
            End If
            Exit Sub

ErrHandle:
            Err.Raise(Err.Number)
        End Sub

        Public Sub Clear()
            Dim i As Integer

            mCol.RemoveAll()
            If mvarParent.StartDate = _base.StartDate Then
                For i = 1 To _base.ReferencePeriods.Count
                    _base.ReferencePeriods.Item(i).Spots.Clear()
                Next
            End If

        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Sub New(ByVal base As cReachguide, Campaign As cKampanj)
            Main = Campaign
            _base = base
        End Sub
    End Class
End Namespace