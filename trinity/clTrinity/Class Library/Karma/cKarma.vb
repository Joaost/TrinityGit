Namespace Trinity
    Class _cKarma
        Implements Connect.ICallBack

        Private Adedge As New ConnectWrapper.Brands
        Public ReferencePeriod As String
        Public Channels As New cKarmaChannels
        Private mvarCampaigns As New _cKarmaCampaigns

        Private DateDiff As Integer = -1

        Private SourceCount As Long

        Public Event PopulateProgress(ByVal p As Integer)

        Private _tag As Object
        Public Property Tag() As Object
            Get
                Return _tag
            End Get
            Set(ByVal value As Object)
                _tag = value
            End Set
        End Property

        Friend ReadOnly Property KarmaAdedge() As ConnectWrapper.Brands
            Get
                KarmaAdedge = Adedge
            End Get
        End Property

        'Function to find available spots in the reference period
        Public Sub Populate(Optional ByVal UseSponsorship As Boolean = False)
            Dim NameToAdedge As New Collection 'Collection to get AdvantEdge channel from a Trinity channel name
            Dim i As Integer
            Dim s As Long
            Dim ChanStr As String
            Dim TmpChan As cKarmaChannel
            Dim TmpStr As String

            DateDiff = -1

            ChanStr = ""
            For Each TmpChan In Channels
                TmpStr = Campaign.Channels(TmpChan.Name).AdEdgeNames
                If TmpStr.IndexOf(",") > -1 Then
                    TmpStr = TmpStr.Substring(0, TmpStr.IndexOf(",")) 'Only get the first channel if the AdedgeChannel is a channel list
                End If
                ChanStr = ChanStr & TmpStr & "," 'Add Channel to Channel list
                NameToAdedge.Add(TmpChan.Name, TmpStr) ' Add channel to collection of channels to get AdedgeChannel from Name
                For w As Integer = 1 To TmpChan.Weeks.Count
                    For i = 1 To TmpChan.Weeks(w).Dayparts.Count
                        TmpChan.Weeks(w).Dayparts(i).Spots.Clear()
                    Next
                Next
            Next

            'Setup Adedge object
            Adedge.setArea(Campaign.Area)
            Adedge.clearTargetSelection()
            Trinity.Helper.AddTarget(Adedge, Campaign.MainTarget)
            Trinity.Helper.AddTarget(Adedge, Campaign.SecondaryTarget, False)
            If UseSponsorship Then
                Adedge.setBrandType("COMMERCIAL,SPONSOR")
            Else
                Adedge.setBrandType("COMMERCIAL")
            End If
            Adedge.setChannelsArea(ChanStr, Campaign.Area)
            Adedge.setPeriod(ReferencePeriod)
            Adedge.setUniverseUserDefined(Campaign.UniStr)
            Adedge.registerCallback(Me)
            If UseSponsorship Then
                SourceCount = Adedge.Run(True, False, 10)
            Else
                SourceCount = Adedge.Run(False, False, 10)
            End If
            Adedge.unregisterCallback()

            If SourceCount > 0 Then
                DateDiff = Campaign.StartDate - Adedge.getAttrib(Connect.eAttribs.aDate, 0)
            End If

            For s = 0 To SourceCount - 1
                RaiseEvent PopulateProgress((s / SourceCount) * 100)
                'Build a list of spots and a list of sponsorships. Each Channel, Week and Daypart has its own list of available spots and sponsorsjips
                'The function below finds the Channel, week and daypart for the spot/sponsorship and adds it to the appropriate list
                Dim WeekNumber As Integer = GetWeekNumber(Date.FromOADate(Adedge.getAttrib(Connect.eAttribs.aDate, s)))
                If Channels(NameToAdedge(Adedge.getAttrib(Connect.eAttribs.aChannel, s))).Weeks.Count = 1 Then
                    WeekNumber = 1
                End If
                If WeekNumber > 0 Then
                    If UseSponsorship AndAlso Adedge.getAttrib(Connect.eAttribs.aBrandSpotClass, s) <> "Commercial" Then
                        'TODO: **DAYPART** Måste Karma göras om?
                        Channels(NameToAdedge(Adedge.getAttrib(Connect.eAttribs.aChannel, s))).Weeks(WeekNumber).Dayparts(Campaign.Dayparts.GetDaypartIndexForMam(Adedge.getAttrib(Connect.eAttribs.aFromTime, s) \ 60) + 1).Sponsorships.Add(s)
                    Else
                        Channels(NameToAdedge(Adedge.getAttrib(Connect.eAttribs.aChannel, s))).Weeks(WeekNumber).Dayparts(Campaign.Dayparts.GetDaypartIndexForMam(Adedge.getAttrib(Connect.eAttribs.aFromTime, s) \ 60) + 1).Spots.Add(s)
                    End If
                End If
            Next
        End Sub

        Private Function GetWeekNumber(ByVal [Date] As Date) As Integer
            ' Return WeekOfYear for a certain Date
            For i As Integer = 1 To Campaign.Channels(1).BookingTypes(1).Weeks.Count
                If Campaign.Channels(1).BookingTypes(1).Weeks(i).StartDate - DateDiff <= [Date].ToOADate AndAlso Campaign.Channels(1).BookingTypes(1).Weeks(i).EndDate - DateDiff >= [Date].ToOADate Then
                    Return i
                End If
            Next
            Return 0
        End Function


        Public ReadOnly Property Campaigns() As _cKarmaCampaigns
            Get
                mvarCampaigns.Karma = Me
                Campaigns = mvarCampaigns
            End Get
        End Property

        Public Sub New()
            KarmaAdedge.setArea(Campaign.Area)
        End Sub

        Public Sub callback(ByVal p As Integer) Implements Connect.ICallBack.callback
            RaiseEvent PopulateProgress(p)
        End Sub
    End Class
End Namespace
