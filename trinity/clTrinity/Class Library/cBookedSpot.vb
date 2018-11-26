Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity
    Public Class cBookedSpot
        Implements IDetectsProblems


        'A spot that is booked, the booking has not been handled by the channel yet.

        Public ID As String

        'local variable(s) to hold property value(s)
        Private mvarDatabaseID As String 'local copy
        Private mvarAirDate As Date 'local copy
        Private mvarMaM As Integer 'local copy
        Private mvarProgramme As String 'local copy
        Private mvarProgAfter As String 'local copy
        Private mvarProgBefore As String 'local copy
        Private mvarGrossPrice As Decimal 'local copy
        Private mvarNetPrice As Decimal 'local copy
        Private mvarChannelEstimate As Single 'local copy
        Private mvarMyEstimate As Single 'local copy
        Private mvarMyEstimateBuyTarget As Single
        Private mvarPlacement As PlaceEnum 'local copy
        Private mvarMatchedSpot As cPlannedSpot
        Public RecentlyBooked As Boolean
        Public MostRecentlyBooked As Boolean
        Public mvarOtherCampaign As Boolean = False
        Public Locked As Boolean = False
        Public Bid As Single = 0
        Public AgeIndex(0 To 13) As Single
        Public Chronological As Long
        Public NotFound As Boolean
        Public ChangedPrice As Boolean

        'Public BaseGrossPrice As Decimal

        Public Enum PlaceEnum
            PlaceAny = 1
            PlaceTop = 2
            PlaceTail = 4
            PlaceTopOrTail = 8
            PlaceCentreBreak = 16
            PlaceStartBreak = 32
            PlaceEndBreak = 64
            PlaceRoadBlock = 128
            PlaceRequestedBreak = 256
            PlaceSecond = 512
            PlaceSecondLast = 1024
        End Enum

        Public AddedValues As Dictionary(Of String, Trinity.cAddedValue)
        Private mvarFilmcode As String 'local copy
        Private mvarBookingtype As cBookingType
        Private mvarWeek As cWeek

        Public IsLocal As Boolean
        Public IsRB As Boolean
        Public Comments As String
        Public Matched As Byte
        Private mvarGross30Price As Decimal

        'local variable(s) to hold property value(s)
        Private mvarChannel As cChannel 'local copy
        Private Main As cKampanj

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            colXml.SetAttribute("ID", Me.ID)
            colXml.SetAttribute("AirDate", Me.AirDate)
            colXml.SetAttribute("Bookingtype", Me.Bookingtype.Name)
            colXml.SetAttribute("Channel", Me.Channel.ChannelName)
            colXml.SetAttribute("ChannelEstimate", Me.ChannelEstimate)
            colXml.SetAttribute("DatabaseID", Me.DatabaseID)
            colXml.SetAttribute("Filmcode", Me.Filmcode)
            colXml.SetAttribute("GrossPrice", Me.GrossPrice)
            colXml.SetAttribute("MaM", Me.MaM)
            colXml.SetAttribute("MyEstimate", Me.MyEstimate)
            colXml.SetAttribute("MyEstimateBuyTarget", Me.MyEstimateBuyTarget)
            colXml.SetAttribute("NetPrice", Me.NetPrice)
            colXml.SetAttribute("ProgAfter", Me.ProgAfter)
            colXml.SetAttribute("ProgBefore", Me.ProgBefore)
            colXml.SetAttribute("Programme", Me.Programme)
            colXml.SetAttribute("IsLocal", Me.IsLocal)
            colXml.SetAttribute("IsRB", Me.IsRB)
            colXml.SetAttribute("Comments", Me.Comments)

            Dim XMLAVs As XmlElement = xmlDoc.CreateElement("AddedValues")
            Dim XMLAV As XmlElement
            If Not Me.AddedValues Is Nothing Then
                For Each kv As KeyValuePair(Of String, Trinity.cAddedValue) In Me.AddedValues
                    XMLAV = xmlDoc.CreateElement("AddedValue")
                    XMLAV.SetAttribute("ID", kv.Key)
                    XMLAVs.AppendChild(XMLAV)
                Next
            End If
            colXml.AppendChild(XMLAVs)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving the Booked spot " & Me.ID)
            Return False
        End Function

        Public Sub setIndexOnFilm(ByVal index As Single)
            Dim NetPrice = mvarNetPrice / (mvarBookingtype.Weeks(1).Films(mvarFilmcode).Index / 100)
            mvarNetPrice = NetPrice * (index / 100)
        End Sub

        Function DateTimeSerial() As Single
            Return Helper.DateTimeSerial(AirDate, MaM)
        End Function

        'returns the main campaign
        Public WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Main = value
            End Set
        End Property

        'gets/sets the matchedSpot
        Public Property MatchedSpot() As cPlannedSpot
            Get
                Return mvarMatchedSpot
            End Get
            Set(ByVal value As cPlannedSpot)
                mvarMatchedSpot = value
            End Set
        End Property

        'gets/sets the Channel fro the spot
        Public Property Channel() As cChannel
            Get
                Channel = mvarChannel
            End Get
            Set(ByVal value As cChannel)
                mvarChannel = value
            End Set
        End Property

        'gets/sets the filmCode
        Public Property Filmcode() As String
            Get
                Filmcode = mvarFilmcode
            End Get
            Set(ByVal value As String)
                mvarFilmcode = value
            End Set
        End Property

        Public Property MyEstimate() As Single
            Get
                MyEstimate = mvarMyEstimate
            End Get
            Set(ByVal value As Single) '
                mvarMyEstimate = value
            End Set
        End Property

        Public Property ChannelEstimate() As Single
            Get
                ChannelEstimate = mvarChannelEstimate
            End Get
            Set(ByVal value As Single)
                mvarChannelEstimate = value
            End Set
        End Property

        Public Property NetPrice() As Decimal
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.NetPrice
            Get
                'Dim AVIndex As Single
                'Dim kv As KeyValuePair(Of String, cAddedValue)
                'Dim TmpAV As cAddedValue

                'AVIndex = 1
                'If AddedValues Is Nothing Then
                '    AddedValues = New Dictionary(Of String, cAddedValue)
                'End If
                'For Each kv In AddedValues
                '    TmpAV = kv.Value
                '    AVIndex = AVIndex * (TmpAV.IndexNet / 100)
                'Next
                'If AddedValues.Count = 0 Then
                Return mvarNetPrice
                'Else
                'Return mvarNetPrice * AVIndex
                'End If
            End Get
            Set(ByVal value As Decimal)
                If value >= 0 Then
                    mvarNetPrice = value
                End If
            End Set
        End Property

        Public Property GrossPrice() As Decimal
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.GrossPrice
            Get
                GrossPrice = mvarGrossPrice
            End Get
            Set(ByVal value As Decimal)
                mvarGrossPrice = value
                If Film() IsNot Nothing AndAlso Film.Index > 0 Then
                    mvarGross30Price = mvarGrossPrice / (Film.Index / 100)
                Else
                    mvarGross30Price = 0
                End If
            End Set
        End Property

        Public Property GrossPrice30() As Decimal
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.GrossPrice
            Get
                GrossPrice30 = mvarGross30Price
            End Get
            Set(ByVal value As Decimal)

                mvarGross30Price = value
                mvarGrossPrice = mvarGross30Price * (Me.Film.Index / 100)
            End Set
        End Property

        Public Function AddedValueIndex(Optional ByVal NetIndex As Boolean = True) As Single
            Dim AVIndex As Single
            Dim kv As KeyValuePair(Of String, cAddedValue)
            Dim TmpAV As cAddedValue
            AVIndex = 1
            If AddedValues Is Nothing Then
                AddedValues = New Dictionary(Of String, cAddedValue)
            End If
            For Each kv In AddedValues
                If Not kv.Value Is Nothing Then

                    TmpAV = kv.Value
                    If Main.MultiplyAddedValues Then
                        If NetIndex Then
                            AVIndex = AVIndex * (TmpAV.IndexNet / 100)
                        Else
                            AVIndex = AVIndex * (TmpAV.IndexGross / 100)
                        End If
                    Else
                        If NetIndex Then
                            AVIndex = AVIndex + (TmpAV.IndexNet - 100) / 100
                        Else
                            AVIndex = AVIndex + (TmpAV.IndexGross - 100) / 100
                        End If
                    End If
                End If
            Next

            Return AVIndex '+ Bid / 100
        End Function

        Public Property ProgBefore()
            'gets/sets the program that is shown before the commercial
            Get
                ProgBefore = mvarProgBefore
            End Get
            Set(ByVal value)
                mvarProgBefore = value
            End Set
        End Property

        Public Property ProgAfter()
            'gets/sets the program shown after the commercial
            Get
                ProgAfter = mvarProgAfter
            End Get
            Set(ByVal value)
                mvarProgAfter = value
            End Set
        End Property

        Public Property Programme()
            'gets/sets the program in where the commercial is shown
            Get
                Programme = mvarProgramme
            End Get
            Set(ByVal value)
                mvarProgramme = value
            End Set
        End Property


        Public Property MaM() As Integer
            'gets/sets the MaM (it is a time stamp, Minutes After Midnight)
            Get
                MaM = mvarMaM
            End Get
            Set(ByVal value As Integer)
                mvarMaM = value
            End Set
        End Property

        Public Property AirDate() As Date
            'gets/sets the air date for the commercial
            Get
                AirDate = mvarAirDate
            End Get
            Set(ByVal value As Date)
                mvarAirDate = value
            End Set
        End Property

        Public Property DatabaseID() As String
            Get
                'DatabaseID = mvarDatabaseID
                Return mvarChannel.ChannelName & mvarAirDate.ToOADate & mvarMaM
            End Get
            Set(ByVal value As String)
                mvarDatabaseID = value
            End Set
        End Property

        Public Property Bookingtype() As cBookingType
            'gets/sets the booking type for the spot
            Get
                Bookingtype = mvarBookingtype
            End Get
            Set(ByVal value As cBookingType)
                mvarBookingtype = value
            End Set
        End Property

        Public Property MyEstimateBuyTarget() As Single
            'gets/sets how much i estimate to get
            Get
                On Error GoTo MyEstimateBuyTarget_Error
                MyEstimateBuyTarget = mvarMyEstimateBuyTarget
                On Error GoTo 0
                Exit Property
MyEstimateBuyTarget_Error:
                Err.Raise(Err.Number, "cBookedSpot: MyEstimateBuyTarget", Err.Description)
            End Get
            Set(ByVal value As Single)
                On Error GoTo MyEstimateBuyTarget_Error
                mvarMyEstimateBuyTarget = value
                On Error GoTo 0
                Exit Property
MyEstimateBuyTarget_Error:
                Err.Raise(Err.Number, "cBookedSpot: MyEstimateBuyTarget", Err.Description)
            End Set

        End Property

        Public Function Film() As cFilm
            'returns the film for the spot
            Film = mvarBookingtype.Weeks(1).Films(mvarFilmcode)
        End Function

        Public Property otherCampaign() As Boolean
            Get
                Return mvarOtherCampaign
            End Get
            Set(ByVal value As Boolean)
                mvarOtherCampaign = value
            End Set
        End Property
        Public Property week() As cWeek
            'gets and sets the week for the commercial 
            Get
                week = mvarWeek
            End Get
            Set(ByVal value As cWeek)
                mvarWeek = value
            End Set
        End Property

        Public Sub New(ByVal MainObject As cKampanj)
            'creates a new booked spot
            Matched = False
            AddedValues = New Dictionary(Of String, cAddedValue)
            Main = MainObject
            Main.RegisterProblemDetection(Me)
        End Sub

        Public Enum BookedSpotProblems
            OutsideCampaignPeriod = 1
            OutsideWeekPeriod = 2
            NoChannel = 3
            NoBookingtype = 4
            NoWeek = 5
            NoFilm = 6
            FilmNotAllocated = 7
            FilmDoesNotExist = 8
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems
            Dim _problems As New List(Of cProblem)


            'Check date
            If (AirDate.ToOADate < Main.StartDate OrElse AirDate.ToOADate > Main.EndDate) Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked outside of campaign period</p>")
                _helpText.AppendLine("<p>A spot has been booked on a date that is either before campaign start or after campaign end</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Booking'-window, select '" & Bookingtype.ToString & "' in the top most dropdown to the left and review the spotlist in the lower half of the window. The spot causing this warning has been booked on " & Format(AirDate, "Short date") & "</p>")

                Dim _problem As New cProblem(BookedSpotProblems.OutsideCampaignPeriod, cProblem.ProblemSeverityEnum.Warning, "A spot is booked on a date outside of the campaign period", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check week date
            If week IsNot Nothing AndAlso (AirDate.ToOADate < week.StartDate OrElse AirDate.ToOADate > week.EndDate) Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked outside of week period</p>")
                _helpText.AppendLine("<p>A spot has been booked on a date that is not included in any week on the campaign</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Booking'-window, select '" & Bookingtype.ToString & "' in the top most dropdown to the right and review the spotlist in the lower half of the window. The spot causing this warning has been booked on " & Format(AirDate, "Short date") & "</p>")

                Dim _problem As New cProblem(BookedSpotProblems.OutsideWeekPeriod, cProblem.ProblemSeverityEnum.Warning, "A spot is booked on a date not belonging to any week", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check that it has a channel
            If Channel Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked without a channel</p>")
                _helpText.AppendLine("<p>A spot in the list of booked spots does not have a channel associated to it</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Contact the system administrator</p>")

                Dim _problem As New cProblem(BookedSpotProblems.NoChannel, cProblem.ProblemSeverityEnum.Error, "A booked spot does not have a channel", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check that it has a bookingtype
            If Bookingtype Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked without a bookingtype</p>")
                _helpText.AppendLine("<p>A spot in the list of booked spots does not have a bookingtype associated to it</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Contact the system administrator</p>")

                Dim _problem As New cProblem(BookedSpotProblems.NoBookingtype, cProblem.ProblemSeverityEnum.Error, "A booked spot does not have a bookingtype", "Booked spots", _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check that it has a film
            If Film() Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked without a film</p>")
                _helpText.AppendLine("<p>A spot in the list of booked spots does not have a film associated to it</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Booking'-window, select '" & Bookingtype.ToString & "' in the top most dropdown to the right and find the spot with <unknown> film on " & Format(AirDate, "Short date") & " and click the film icon to change to another film.</p>")

                Dim _problem As New cProblem(BookedSpotProblems.NoFilm, cProblem.ProblemSeverityEnum.Error, "A booked spot does not have a film", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check that it has a week
            If week Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked without a week</p>")
                _helpText.AppendLine("<p>A spot in the list of booked spots does not have a week associated to it</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Contact the system administrator</p>")

                Dim _problem As New cProblem(BookedSpotProblems.NoWeek, cProblem.ProblemSeverityEnum.Error, "A booked spot does not have a week", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check film exists
            If week IsNot Nothing AndAlso Film() IsNot Nothing AndAlso week.Films(Film.Name) Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked with a film that no longer exists</p>")
                _helpText.AppendLine("<p>A spot has been allocated a film that is no longer on the campaign.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Either:<ul><li>Open the 'Booking'-window, select '" & Bookingtype.ToString & "' in the top most dropdown to the right and find the spot with the film '" & Film.Name & "' and click the film icon to change to another film.</li></ul>")
                _helpText.AppendLine("- or -<ul><li>Open the 'Setup'-window, select the 'Films' tab and add a film with the name '" & Film.Name & "'</li><ul>")

                Dim _problem As New cProblem(BookedSpotProblems.FilmDoesNotExist, cProblem.ProblemSeverityEnum.Warning, "A spot is booked with a film no longer exists", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check film is allocated
            If week IsNot Nothing AndAlso Film() IsNot Nothing AndAlso week.Films(Film.Name) IsNot Nothing AndAlso week.Films(Film.Name).Share = 0 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot booked with a film that it is not allocated for week</p>")
                _helpText.AppendLine("<p>A spot has been allocated the film '" & Film.Name & "' that should not used in week '" & week.Name & "' according to user input in the 'Allocate'-window</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Either:<ul><li>Open the 'Booking'-window, select '" & Bookingtype.ToString & "' in the top most dropdown to the right and find the spot with the film '" & Film.Name & "' on " & Format(AirDate, "Short date") & " and click the film icon to change to another film.</li></ul>")
                _helpText.AppendLine("- or -<ul><li>Open the 'Allocate'-window, select '" & Bookingtype.ToString & "' in the pane in the top right corner ('Films') and set the share on '" & Film.Name & "' to at least 1% in week '" & week.Name & "'</li><ul>")

                Dim _problem As New cProblem(BookedSpotProblems.FilmNotAllocated, cProblem.ProblemSeverityEnum.Warning, "A spot is booked with a film that it is not allocated for week", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If


            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound



    End Class
End Namespace