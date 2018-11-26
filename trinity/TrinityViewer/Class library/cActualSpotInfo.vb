Namespace TrinityViewer
    Public Class cActualSpotInfo
        Public Enum EnumBreakType
            btBlock = 0
            btBreak = 1
        End Enum

        Public Enum ActualTargetEnum
            ateMainTarget = 0
            ateSecondTarget = 1
            ateThirdTarget = 2
            ateAllAdults = 3
            ateBuyingTarget = 4
            ateCustomTarget = 5
        End Enum

        Public AirDate As Long
        Public MaM As Integer
        Public Second As Integer
        Public Filmcode As String
        Public PosInBreak As Integer
        Public SpotsInBreak As Integer
        Public SpotLength As Byte
        Public Week As TrinityViewer.cWeekInfo
        Public AdEdgeChannel As String
        Public ID As String
        Private mvarGroupIdx As Integer
        Private mvarBreakType As EnumBreakType
        Private Main As TrinityViewer.cCampaignInfo
        Private mvarBookingtype As TrinityViewer.cBookingTypeInfo
        Private mvarChannel As TrinityViewer.cChannelInfo

        '---------------------------------------------------------------------------------------
        ' Procedure : Channel
        ' DateTime  : 2003-06-12 16:05
        ' Author    : joho
        ' Purpose   : Pointer to an object of the cChannel class that contains data about
        '             the channel where the spot was aired
        '---------------------------------------------------------------------------------------
        '
        Public Property Channel() As TrinityViewer.cChannelInfo

            Get
                On Error GoTo Channel_Error
                Channel = mvarChannel
                On Error GoTo 0
                Exit Property
Channel_Error:
                Err.Raise(Err.Number, "cActualSpot: Channel", Err.Description)
            End Get
            Set(ByVal value As TrinityViewer.cChannelInfo)
                On Error GoTo Channel_Error
                mvarChannel = value
                If value Is Nothing Then
                    AdEdgeChannel = ""
                Else
                    If InStr(value.AdedgeNames, ",") = 0 Then
                        AdEdgeChannel = Channel.AdedgeNames
                    Else
                        AdEdgeChannel = Left(value.AdedgeNames, InStr(value.AdedgeNames, ",") - 1)
                    End If
                End If
                On Error GoTo 0
                Exit Property
Channel_Error:
                Err.Raise(Err.Number, "cActualSpot: Channel", Err.Description)
            End Set

        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : BookingType
        ' DateTime  : 2003-07-15 12:51
        ' Author    : joho
        ' Purpose   : Pointer to a cBookingType representing the Booking Type this spot
        '             is a part of
        '---------------------------------------------------------------------------------------
        '
        Public Property Bookingtype() As TrinityViewer.cBookingTypeInfo
            Get
                On Error GoTo BookingType_Error
                Bookingtype = mvarBookingtype
                On Error GoTo 0
                Exit Property
BookingType_Error:
                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
            End Get
            Set(ByVal value As TrinityViewer.cBookingTypeInfo)
                On Error GoTo BookingType_Error
                mvarBookingtype = value
                On Error GoTo 0
                Exit Property
BookingType_Error:
                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
            End Set
        End Property

        Public Property BreakType() As EnumBreakType
            Get
                On Error GoTo BreakType_Error
                BreakType = mvarBreakType
                On Error GoTo 0
                Exit Property
BreakType_Error:
                Err.Raise(Err.Number, "cActualSpot: BreakType", Err.Description)
            End Get
            Set(ByVal value As EnumBreakType)
                On Error GoTo BreakType_Error
                mvarBreakType = value
                On Error GoTo 0
                Exit Property
BreakType_Error:
                Err.Raise(Err.Number, "cActualSpot: BreakType", Err.Description)
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : GroupIdx
        ' DateTime  : 2003-09-04 13:53
        ' Author    : joho
        ' Purpose   : Returns/sets the index used to access this spot in Adedge
        '---------------------------------------------------------------------------------------
        '
        Public Property GroupIdx() As Integer
            Get
                On Error GoTo GroupIdx_Error
                GroupIdx = mvarGroupIdx
                On Error GoTo 0
                Exit Property
GroupIdx_Error:
                Err.Raise(Err.Number, "cActualSpot: GroupIdx", Err.Description)
            End Get
            Set(ByVal value As Integer)
                On Error GoTo GroupIdx_Error
                mvarGroupIdx = value
                On Error GoTo 0
                Exit Property
GroupIdx_Error:
                Err.Raise(Err.Number, "cActualSpot: GroupIdx", Err.Description)
            End Set
        End Property

        Public ReadOnly Property Rating(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
            Get
                Dim t As String
                Dim u As String


                'if target is = 0
                If Target = ActualTargetEnum.ateMainTarget Then
                    'set the main target age and universe
                    t = Main.MainTarget.TargetName
                    u = Main.MainTarget.Universe

                    'if target is = 1
                ElseIf Target = ActualTargetEnum.ateSecondTarget Then
                    'set the secondary target age and universe
                    t = Main.SecondaryTarget.TargetName
                    u = Main.SecondaryTarget.Universe

                    'if target is = 2
                ElseIf Target = ActualTargetEnum.ateThirdTarget Then
                    'set the third target age and universe
                    t = Main.ThirdTarget.TargetName
                    u = Main.ThirdTarget.Universe

                    'if target is = 4
                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then
                    'set the booking type and booking universe for the campaign
                    t = mvarBookingtype.BuyingTarget.TargetName
                    u = mvarBookingtype.BuyingTarget.Universe

                    'if target is = 3
                ElseIf Target = ActualTargetEnum.ateAllAdults Then
                    'sets teh target to maximum
                    t = "3+"
                    u = ""

                    'if target is = 5
                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
                    'set a custom target and universe
                    t = CustomTarget
                    u = Customuniverse
                    If u Is Nothing Then u = ""
                Else
                    'if no one was applyable we set them empty
                    t = ""
                    u = ""
                End If
                Try
                    Rating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Helper.UniverseIndex(Main.Adedge, u), Helper.TargetIndex(Main.Adedge, t))
                Catch
                    If Main.Adedge.getGroupCount > 0 Then
                        Helper.AddTargetsToAdedge(Main.Adedge)
                        Main.Adedge.Run(True, False, 10)
                        Rating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Helper.UniverseIndex(Main.Adedge, u), Helper.TargetIndex(Main.Adedge, t))
                    End If
                End Try
            End Get
        End Property

        Public ReadOnly Property Rating000(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
            Get
                Dim t As String
                Dim u As String


                'if target is = 0
                If Target = ActualTargetEnum.ateMainTarget Then
                    'set the main target age and universe
                    t = Main.MainTarget.TargetName
                    u = Main.MainTarget.Universe

                    'if target is = 1
                ElseIf Target = ActualTargetEnum.ateSecondTarget Then
                    'set the secondary target age and universe
                    t = Main.SecondaryTarget.TargetName
                    u = Main.SecondaryTarget.Universe

                    'if target is = 2
                ElseIf Target = ActualTargetEnum.ateThirdTarget Then
                    'set the third target age and universe
                    t = Main.ThirdTarget.TargetName
                    u = Main.ThirdTarget.Universe

                    'if target is = 4
                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then
                    'set the booking type and booking universe for the campaign
                    t = mvarBookingtype.BuyingTarget.TargetName
                    u = mvarBookingtype.BuyingTarget.Universe

                    'if target is = 3
                ElseIf Target = ActualTargetEnum.ateAllAdults Then
                    'sets teh target to maximum
                    t = "3+"
                    u = ""

                    'if target is = 5
                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
                    'set a custom target and universe
                    t = CustomTarget
                    u = Customuniverse
                    If u Is Nothing Then u = ""
                Else
                    'if no one was applyable we set them empty
                    t = ""
                    u = ""
                End If
                Try
                    Return Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Helper.UniverseIndex(Main.Adedge, u), Helper.TargetIndex(Main.Adedge, t)) + 0.5)
                Catch
                    If Main.Adedge.getGroupCount > 0 Then
                        Helper.AddTargetsToAdedge(Main.Adedge)
                        Main.Adedge.Run(True, False, 10)
                        Return (Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Helper.UniverseIndex(Main.Adedge, u), Helper.TargetIndex(Main.Adedge, t)) + 0.5)
                    End If
                End Try
            End Get
        End Property

        Public ReadOnly Property SpotIndex() As Decimal
            Get
                If Rating = 0 Then
                    Return 1
                End If
                Return Rating30 / Rating
            End Get
        End Property

        Public ReadOnly Property Rating30(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
            Get
                Dim t As String
                Dim u As String

                If Target = ActualTargetEnum.ateMainTarget Then
                    t = Main.MainTarget.TargetName
                    u = Main.MainTarget.Universe
                ElseIf Target = ActualTargetEnum.ateSecondTarget Then
                    t = Main.SecondaryTarget.TargetName
                    u = Main.SecondaryTarget.Universe
                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then
                    t = mvarBookingtype.BuyingTarget.TargetName
                    u = mvarBookingtype.BuyingTarget.Universe
                ElseIf Target = ActualTargetEnum.ateAllAdults Then
                    t = "3+"
                    u = ""
                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
                    t = CustomTarget
                    u = Customuniverse
                Else
                    t = ""
                    u = ""
                End If
                Rating30 = Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Helper.UniverseIndex(Main.Adedge, u), Helper.TargetIndex(Main.Adedge, t))
            End Get
        End Property

        Public Sub New(ByVal MainObject As TrinityViewer.cCampaignInfo)
            'returns the main campaign
            Main = MainObject
            ID = CreateGUID()
        End Sub
    End Class
End Namespace
