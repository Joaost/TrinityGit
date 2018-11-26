Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cExtendedInfo

        'a class that holds extended information for the schedules in the database
        Private mvarNetPrice As Decimal
        Private mvarEstimatedRating As Single
        Private mvarBreakList As ArrayList
        Private mvarEstimatedRatingBuyingTarget As Single
        Private mvarEstimatedRatingSecondTarget As Single
        Public EstimatedReachRating As Single
        Public IsBooked As Boolean
        Public Estimation As Single
        Public AirDate As Date
        Public MaM As Integer
        Public EstimationPeriod As String
        Public EstimatedOnPeriod As String
        Public EstimationTarget As String
        Public ProgAfter As String
        Public Channel As String
        Private mvarGrossPrice30 As Decimal
        Public ChannelEstimate As Single
        Public Remark As String
        Public Solus As Single
        Public SolusFirst As Single
        Public Duration As Integer
        Public ID As String
        Public Bookingtype As String
        Public Addition As Integer
        Public IsAvail As Boolean
        Public Chronological As Long
        Private Main As Trinity.cKampanj

        Public Property GrossPrice30(ByVal IncludeIndex As Boolean, Optional ByVal Bid As Single = 0) As Decimal
            Get
                If IncludeIndex Then
                    Return (mvarGrossPrice30 + (mvarGrossPrice30 / (1 + (Addition / 100))) * (Bid / 100)) * (Main.Channels(Channel).BookingTypes(Bookingtype.Substring(Channel.Length + 1)).Indexes.GetIndexForDate(AirDate, cIndex.IndexOnEnum.eGrossCPP) / 100)
                Else
                    Return mvarGrossPrice30 + (mvarGrossPrice30 / (1 + (Addition / 100))) * (Bid / 100)
                End If
            End Get
            Set(ByVal value As Decimal)
                If IncludeIndex Then
                    mvarGrossPrice30 = value / (Main.Channels(Channel).BookingTypes(Bookingtype.Substring(Channel.Length + 1)).Indexes.GetIndexForDate(AirDate, cIndex.IndexOnEnum.eGrossCPP) / 100)
                Else
                    mvarGrossPrice30 = value
                End If
            End Set
        End Property

        Public ReadOnly Property CostPerSolus(ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType) As Decimal
            Get
                If Solus <> 0 Then
                    Return NetPrice(Film, Bookingtype) / Solus
                Else
                    Return 0
                End If
            End Get
        End Property

        Public ReadOnly Property IndexSecondVsChannelEstimate() As Single
            Get
                Return (EstimatedRatingSecondTarget / ChannelEstimate) * 100
            End Get
        End Property

        Public ReadOnly Property IndexVsChannelEstimate() As Single
            Get
                Return (EstimatedRating / ChannelEstimate) * 100
            End Get
        End Property

        Public ReadOnly Property IndexVsBuyingEstimate() As Single
            Get
                Return (EstimatedRating / EstimatedRatingBuyingTarget) * 100
            End Get
        End Property

        Public ReadOnly Property NetPrice(ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType, Optional ByVal Bid As Single = 0) As Decimal
            Get
                If Bookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                    Return ChannelEstimate * Bookingtype.BuyingTarget.NetCPP * (Film.Index / 100) * (Bookingtype.Indexes.GetIndexForDate(AirDate, Trinity.cIndex.IndexOnEnum.eNetCPP) / 100)
                Else
                    If Film IsNot Nothing Then
                        Return GrossPrice30(True, Bid) * (Film.Index / 100) * (1 - Bookingtype.BuyingTarget.Discount) * (Bookingtype.Indexes.GetIndexForDate(AirDate, Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) '* (Bookingtype.BuyingTarget.PricelistPeriods.GetCPPForDate(AirDate.ToOADate))
                    Else
                    Return 0
                    End If
                End If
            End Get
        End Property

        Public ReadOnly Property GrossPrice(ByVal Film As Trinity.cFilm, Optional ByVal Bid As Single = 0) As Decimal
            Get
                Return GrossPrice30(True, Bid) * (Film.Index / 100)
            End Get
        End Property

        Public ReadOnly Property GrossCPP30() As Single
            Get
                If EstimatedRating = 0 Then
                    Return 0
                Else
                    Return GrossPrice30(True) / EstimatedRating
                End If
            End Get
        End Property

        Public ReadOnly Property NetCPP(ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType) As Single
            Get
                If EstimatedRating = 0 Then
                    Return 0
                Else
                    Return NetPrice(Film, Bookingtype) / EstimatedRating
                End If
            End Get
        End Property

        Public ReadOnly Property NetCPT(ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType) As Single
            Get
                If EstimatedRating = 0 Then
                    Return 0
                Else
                    Return NetPrice(Film, Bookingtype) / ((EstimatedRating / 100) * Main.MainTarget.UniSize)
                End If
            End Get
        End Property

        Public ReadOnly Property NetCPPSecond(ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType) As Single
            Get
                If EstimatedRatingSecondTarget = 0 Then
                    Return 0
                Else
                    Return NetPrice(Film, Bookingtype) / EstimatedRatingSecondTarget
                End If
            End Get
        End Property

        Public ReadOnly Property GrossCPPChannel() As Single
            Get
                If ChannelEstimate = 0 Then
                    Return 0
                Else
                    Return GrossPrice30(True) / ChannelEstimate
                End If
            End Get
        End Property

        Public ReadOnly Property NetCPPChannel(ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType) As Single
            Get
                If ChannelEstimate = 0 Then
                    Return 0
                Else
                    Return NetPrice(Film, Bookingtype) / ChannelEstimate
                End If
            End Get
        End Property

        Public Property EstimatedRating() As Single
            Get
                Return mvarEstimatedRating
            End Get
            Set(ByVal value As Single)
                mvarEstimatedRating = value
            End Set
        End Property

        Public Property EstimatedRatingSecondTarget() As Single
            Get
                Return mvarEstimatedRatingSecondTarget
            End Get
            Set(ByVal value As Single)
                mvarEstimatedRatingSecondTarget = value
            End Set
        End Property

        Public Property EstimatedRatingBuyingTarget() As Single
            Get
                Return mvarEstimatedRatingBuyingTarget
            End Get
            Set(ByVal value As Single)
                mvarEstimatedRatingBuyingTarget = value
            End Set
        End Property


        Public Property BreakList() As ArrayList
            Get
                BreakList = mvarBreakList
            End Get
            Set(ByVal value As ArrayList)
                mvarBreakList = value
            End Set
        End Property

        Public Sub New(ByVal MainObject As Trinity.cKampanj)
            Main = MainObject
            IsBooked = False
        End Sub

        Public Sub New(ByVal MainObject As Trinity.cKampanj, dr As DataRow, BookingType As cBookingType, byref NoEstimationTargetColumn As Boolean)
            Me.New(MainObject)

    Me.AirDate = Date.FromOADate(dr.Item("Date"))
            Me.MaM = dr.Item("StartMam")
            If IsDBNull(dr.Item("EstimationPeriod")) OrElse dr.Item("EstimationPeriod") Is Nothing Then
                Me.EstimationPeriod = "-4fw"
            Else
                Me.EstimationPeriod = dr.Item("EstimationPeriod")
            End If
            Me.ProgAfter = dr.Item("Name")

            Me.Channel = dr.Item("Channel")
            Me.Bookingtype = BookingType.ToString()

            If Not NoEstimationTargetColumn Then
                Try
                    If IsDBNull(dr.Item("EstimationTarget")) Then dr.Item("EstimationTarget") = ""
                    Me.EstimationTarget = Trim(dr.Item("EstimationTarget"))
                    If IsDBNull(dr.Item("price")) Then dr.Item("price") = 0
                    If dr.Item("UseCPP") Then
                        Me.Addition = dr.Item("Addition")
                        If Not BookingType.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))) Is Nothing Then
                            Me.GrossPrice30(False) = dr.Item("ChanEst") * (1 + (dr.Item("Addition") / 100)) * (BookingType.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))).GetCPPForDate(Me.AirDate.ToOADate))
                        ElseIf Not BookingType.Pricelist.Targets("A" & Trim(dr.Item("EstimationTarget"))) Is Nothing Then
                            Me.GrossPrice30(False) = dr.Item("ChanEst") * (1 + (dr.Item("Addition") / 100)) * (BookingType.Pricelist.Targets("A" & Trim(dr.Item("EstimationTarget"))).GetCPPForDate(Me.AirDate.ToOADate))
                        End If

                    Else
                        Me.GrossPrice30(False) = dr.Item("price")
                    End If
                Catch
                    Me.GrossPrice30(False) = dr.Item("price")
                    Me.EstimationTarget = BookingType.BuyingTarget.Target.TargetName
                    NoEstimationTargetColumn = True
                End Try
            Else
                Me.GrossPrice30(False) = dr.Item("price")
                Me.EstimationTarget = BookingType.BuyingTarget.Target.TargetName
            End If
            Me.ChannelEstimate = dr.Item("ChanEst").ToString.Replace(".", ",")
            Me.Remark = ""
            Dim whatisit As Object = dr.Item("Islocal")

            If IsDBNull(dr.Item("IsLocal")) Then dr.Item("IsLocal") = False
            If dr.Item("IsLocal") Then
                Me.Remark = Me.Remark & "L"
            End If
            If IsDBNull(dr.Item("IsRB")) Then dr.Item("IsRB") = False
            If dr.Item("IsRB") Then
                Me.Remark = Me.Remark & "R"
            End If
            If IsDBNull(dr.Item("Comment")) Then
                dr.Item("Comment") = ""
            End If
            'Comment field is '0' if there is no comment, not empty
            If dr.Item("Comment") <> "0" AndAlso Not String.IsNullOrEmpty(dr.Item("Comment")) Then
                Me.Remark = dr.Item("Comment")
            End If
            Me.Duration = dr.Item("Duration")
            Me.ID = dr.Item("Channel") & dr.Item("Date") & dr.Item("StartMam")

            For Each TmpSpot As cBookedSpot In Campaign.BookedSpots
                If TmpSpot.DatabaseID = ID Then
                    Me.IsBooked = True
                    Exit For
                End If
            Next
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            mvarBreakList = Nothing
        End Sub
    End Class
End Namespace