Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports Connect



Namespace Trinity
    Public Class cReferencePeriod
        'Implements ConnectWrapper.ICallBack

        Private m_dtStartDate As Date

        Private InternalAdedge As ConnectWrapper.Brands
        Private mvarSpots As cSpots
        Private m_sName As String
        Private _base As cReachguide
        Public Event Progress(ByVal ReferencePeriod As String, ByVal Progress As Long)


        Friend Sub Calculate()

            Dim TmpSpot As cSpot

            If InternalAdedge.validate > 0 Then
                InternalAdedge = New ConnectWrapper.Brands
                InternalAdedge.setArea(_base.MainObject.Area)
                InternalAdedge.setBrandType("COMMERCIAL")
                InternalAdedge.setPeriod(Format(m_dtStartDate, "ddMMyy") & "-" & Format(m_dtStartDate + (_base.EndDate - _base.StartDate), "ddMMyy"))
                Trinity.Helper.AddTimeShift(InternalAdedge)
                InternalAdedge.setChannelsArea(_base.MainObject.ChannelString, _base.MainObject.Area)
                'InternalAdedge.clearTargetSelection()
                Trinity.Helper.AddTarget(InternalAdedge, _base.Target, False)
                Trinity.Helper.AddTargetsToAdedge(InternalAdedge, False, False)
                InternalAdedge.Run(True, False, 10)
                InternalAdedge.sort("channel,date(asc),fromtime(asc)")
            End If
            InternalAdedge.clearGroup()
            For Each kv As DictionaryEntry In mvarSpots
                TmpSpot = kv.Value
                'TmpSpot = mvarSpots(i)
                InternalAdedge.addToGroup(TmpSpot.SpotIndex)
            Next
            If InternalAdedge.getGroupCount > 0 Then
                InternalAdedge.recalcRF(Connect.eSumModes.smGroup)
            End If

        End Sub

        Public ReadOnly Property Spots() As cSpots
            Get
                Spots = mvarSpots
            End Get
        End Property

        Friend Function Reach(ByVal Freq As Byte) As Single
            If InternalAdedge.getGroupCount > 0 Then
                Reach = InternalAdedge.getRF(InternalAdedge.getGroupCount - 1, , _base.MainObject.TimeShift, , Freq)
            Else
                Reach = 0
            End If
        End Function

        Friend Function TRP() As Single
            If InternalAdedge.getGroupCount > 0 Then
                Return InternalAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup)
            Else
                Return 0
            End If
        End Function

        Friend Function ProfileTRP(ByVal TargetIndex As Integer) As Single
            Return InternalAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , _base.MainObject.TimeShift, InternalAdedge.getTargetCount - 14 + TargetIndex)
        End Function

        Public ReadOnly Property Adedge() As ConnectWrapper.Brands
            Get
                Adedge = InternalAdedge
            End Get
        End Property

        Public Property StartDate() As Date
            Get
                StartDate = m_dtStartDate
            End Get
            Set(ByVal value As Date)
                m_dtStartDate = value
            End Set
        End Property

        Private Sub ICallBack_callback(ByVal p As Long)
            RaiseEvent Progress(m_sName, p)
        End Sub

        Public Property Name() As String
            Get
                Name = m_sName
            End Get
            Set(ByVal value As String)
                m_sName = value
            End Set
        End Property

        Public Sub New(ByVal Base As cReachguide, Campaign As cKampanj)
            mvarSpots = New cSpots(Base, Campaign)
            mvarSpots.Parent = Me
            InternalAdedge = New ConnectWrapper.Brands
            _base = Base
        End Sub

        'Public Sub callback(ByVal p As Integer) Implements ConnectWrapper.ICallBack.callback

        'End Sub
    End Class
End Namespace