Namespace Trinity
    Public Class cKarma
        Implements Connect.ICallBack

        'Local advantedge object for Karma
        Private Adedge As New ConnectWrapper.Brands

        Private _referencePeriod As String
        Private mvarCampaigns As New cKarmaCampaigns(Me)
        Public Channels As New List(Of String)
        Public Spots As New List(Of cKarmaSpot)

        Private DateDiff As Integer = -1

        Private SourceCount As Long

        Public Event PopulateProgress(ByVal p As Integer)

        Private _main As cKampanj

        Private _weeks As Integer
        Public Property Weeks() As Integer
            Get
                Return _weeks
            End Get
            Set(ByVal value As Integer)
                _weeks = value
            End Set
        End Property

        Private _tag As Object
        Public Property Tag() As Object
            Get
                Return _tag
            End Get
            Set(ByVal value As Object)
                _tag = value
            End Set
        End Property

        Public Property ReferencePeriod() As String
            Get
                Return _referencePeriod
            End Get
            Set(ByVal value As String)
                _referencePeriod = value
            End Set
        End Property

        Friend ReadOnly Property KarmaAdedge() As ConnectWrapper.Brands
            Get
                KarmaAdedge = Adedge
            End Get
        End Property

        Public Sub Populate(Optional ByVal UseSponsorship As Boolean = False, Optional ByVal UseCommercial As Boolean = False)
            Dim _chanStr As String = ""

            Dim _adedgeToChannel As New Dictionary(Of String, String)

            'Create channel list for AdvantEdge
            For Each _chan As String In Channels
                _chanStr &= _main.Channels(_chan).AdEdgeNames & ","
                For Each _adedgeChan As String In _main.Channels(_chan).AdEdgeNames.Split(",")
                    If Not _adedgeToChannel.ContainsKey(_adedgeChan.ToUpper) Then _adedgeToChannel.Add(_adedgeChan.ToUpper, _chan)
                Next
            Next

            'Setup Advantedge
            Adedge.setArea(_main.Area)
            Adedge.clearTargetSelection()
            Trinity.Helper.AddTarget(Adedge, _main.MainTarget)
            Trinity.Helper.AddTarget(Adedge, _main.SecondaryTarget, False)
            If UseSponsorship Then
                If UseCommercial And UseSponsorship Then
                    Adedge.setBrandType("SPONSOR, COMMERCIAL")
                ElseIf TrinitySettings.ExcludeCommercialsInSponsorship Then
                    Adedge.setBrandType("SPONSOR")
                Else
                    Adedge.setBrandType("SPONSOR, COMMERCIAL")
                End If
            Else
                Adedge.setBrandType("COMMERCIAL")
            End If
            Adedge.setChannelsArea(_chanStr, _main.Area)
            Adedge.setPeriod(ReferencePeriod)
            'Adedge.setUniverseUserDefined(_main.UniStr)
            
            'For some reason in a Spons-campaign  timeShift function is not possible, thats why there is a If-clause


            'If UseCommercial = False And UseSponsorship = False
            '    Trinity.Helper.AddTimeShift(Adedge)
            'End If
            
            'Trinity.Helper.AddTimeShift(Adedge)

            'Register this class to get the callback from AdvantEdge
            Adedge.registerCallback(Me)
            If UseSponsorship Then
                'If sponsorships will be used we will need the titles
                SourceCount = Adedge.Run(True, False, 10)
            Else
                'If no sponsorships are used, skip titles for performance
                SourceCount = Adedge.Run(False, False, 10)
            End If
            'Unregister the callback
            Adedge.unregisterCallback()
            
            'Windows.Forms.MessageBox.Show((Adedge.getRf(SourceCount -1, , , ,3)))


            If SourceCount > 0 Then
                DateDiff = _main.StartDate - Adedge.getAttrib(Connect.eAttribs.aDate, 0)
            End If

            Spots.Clear()
            For s As Integer = 0 To SourceCount - 1
                RaiseEvent PopulateProgress((s / SourceCount) * 100)
                Dim _spot As New cKarmaSpot
                _spot.Week = GetWeekNumber(Date.FromOADate(Adedge.getAttrib(Connect.eAttribs.aDate, s)))
                If _weeks = 1 Then
                    _spot.Week = 1
                End If
                If _spot.Week > 0 Then
                    _spot.Channel = _adedgeToChannel(Adedge.getAttrib(Connect.eAttribs.aChannel, s).ToString.ToUpper)
                    _spot.AirDate = Date.FromOADate(Adedge.getAttrib(Connect.eAttribs.aDate, s))
                    _spot.Mam = Adedge.getAttrib(Connect.eAttribs.aFromTime, s) \ 60
                    _spot.HasBeenPicked = False
                    _spot.ListIndex = s
                    If UseSponsorship AndAlso Adedge.getAttrib(Connect.eAttribs.aBrandSpotClass, s) <> "Commercial" Then
                        _spot.Type = cKarmaSpot.KarmaSpotType.Sponsorship
                        _spot.ProgAfter = Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, s)
                        _spot.ProgBefore = Adedge.getAttrib(Connect.eAttribs.aBrandProgBefore, s)
                        _spot.Product = Adedge.getAttrib(Connect.eAttribs.aBrandProduct, s)
                    Else
                        _spot.Type = cKarmaSpot.KarmaSpotType.Spot
                        If UseSponsorship Then
                            _spot.ProgAfter = Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, s)
                            _spot.ProgBefore = Adedge.getAttrib(Connect.eAttribs.aBrandProgBefore, s)
                            _spot.Product = Adedge.getAttrib(Connect.eAttribs.aBrandProduct, s)
                        End If
                        _spot.SpotInBreak = Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, s)
                        _spot.SpotCount = Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, s)
                    End If
                    Spots.Add(_spot)
                Else
                    Debug.Print("Spot week was less than or equal to 0")
                End If
            Next

        End Sub

        Private Function GetWeekNumber(ByVal [Date] As Date) As Integer
            ' Return WeekOfYear for a certain Date
            For i As Integer = 1 To _main.Channels(1).BookingTypes(1).Weeks.Count
                Dim adjustedStartDate As Date = Date.FromOADate(_main.Channels(1).BookingTypes(1).Weeks(i).StartDate).AddDays(-DateDiff)
                If adjustedStartDate <= [Date] Then
                    Dim adjustedEndDate As Date = Date.FromOADate(_main.Channels(1).BookingTypes(1).Weeks(i).EndDate).AddDays(-DateDiff)
                    If adjustedEndDate >= [Date] Then
                        Return i
                    End If
                End If
            Next
            Return 0
        End Function


        Public ReadOnly Property Campaigns() As cKarmaCampaigns
            Get
                Return mvarCampaigns
            End Get
        End Property

        Public Sub New(Main As cKampanj)
            _main = Main
            KarmaAdedge.setArea(_main.Area)
        End Sub

        Public Sub callback(ByVal p As Integer) Implements Connect.ICallBack.callback
            RaiseEvent PopulateProgress(p)
        End Sub
    End Class
End Namespace