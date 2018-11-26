Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cBookedSpots
        Implements Collections.IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Private Main As cKampanj

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Main = value
            End Set
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLSpot As Xml.XmlElement

            For Each TmpSpot As Trinity.cBookedSpot In Me
                XMLSpot = xmlDoc.CreateElement("Spot")
                TmpSpot.GetXML(XMLSpot, errorMessege, xmlDoc)
                colXml.AppendChild(XMLSpot)
            Next

            colXml.SetAttribute("TotalTRP", Me.TotalTRP)
            colXml.SetAttribute("TotalNet", Me.TotalNetBudget)
            colXml.SetAttribute("TotalGross", Me.TotalGrossBudget)
            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving the Booked spots collection")
            Return False
        End Function

        Public Function crossCheckSpotsWithDatabase(ByVal changePrices As Boolean) As Boolean
            'returns true if there are bad spots

            Dim dtResults As DataTable
            Dim drSaved As DataRow = Nothing

            Dim badSpots As String = ""

            For Each TmpSpot As Trinity.cBookedSpot In Me

                'make a search, we allow 5 minute time difference but no difference in date
                dtResults = DBReader.getEventsInInterval(TmpSpot.AirDate, TmpSpot.Bookingtype.ParentChannel.ChannelName, TmpSpot.MaM - 10, TmpSpot.MaM + 10)

                For Each dr As DataRow In dtResults.Rows
                    'we are looking for a spot with the same program
                    If TmpSpot.Programme = dr.Item("Name") Then
                        If changePrices Then
                            TmpSpot.GrossPrice30 = dr.Item("Price")
                        Else
                            If Not TmpSpot.GrossPrice = dr.Item("Price") Then
                                TmpSpot.ChangedPrice = True
                                crossCheckSpotsWithDatabase = True
                            End If
                        End If
                        GoTo Found
                    End If
                Next

                'here we deal with non found spots
                TmpSpot.NotFound = True
                crossCheckSpotsWithDatabase = True
Found:
                dtResults.Dispose()
            Next


        End Function

        Public Function Add(ByVal ID As String, ByVal DatabaseID As String, ByVal Channel As String, ByVal AirDate As Date, ByVal MaM As Integer, ByVal Programme As String, ByVal ProgAfter As String, ByVal ProgBefore As String, ByVal GrossPrice As Decimal, ByVal NetPrice As Decimal, ByVal ChannelEstimate As Single, ByVal MyEstimate As Single, ByVal MyEstimateChannelTarget As Single, ByVal Filmcode As String, ByVal Bookingtype As String, ByVal IsLocal As Boolean, ByVal IsRB As Boolean, ByVal Bid As Single) As cBookedSpot

            Try

           
            'create a new object
            Dim objNewMember As cBookedSpot
            objNewMember = New cBookedSpot(Main)


            'set the properties passed into the method
            objNewMember.ID = ID
            objNewMember.DatabaseID = DatabaseID
            objNewMember.AirDate = AirDate
            objNewMember.MaM = MaM
            objNewMember.Programme = Programme
            objNewMember.ProgAfter = ProgAfter
            objNewMember.ProgBefore = ProgBefore
            objNewMember.Filmcode = Filmcode
            objNewMember.Channel = Main.Channels(Channel)
            objNewMember.Bookingtype = objNewMember.Channel.BookingTypes(Bookingtype)
            objNewMember.week = objNewMember.Bookingtype.GetWeek(AirDate)
            objNewMember.GrossPrice = GrossPrice
            objNewMember.NetPrice = NetPrice
            objNewMember.ChannelEstimate = ChannelEstimate
            objNewMember.MyEstimate = MyEstimate
            objNewMember.IsLocal = IsLocal
            objNewMember.IsRB = IsRB
            objNewMember.Bid = Bid
            objNewMember.AddedValues = New Dictionary(Of String, Trinity.cAddedValue)
            'Main.ExtendedInfos(DatabaseID).IsBooked = True
            Main.RFEstimation.Spots.Add(AirDate, MaM, Channel, ID)


            'If Not objNewMember.Bookingtype.Weeks(1).Films(Filmcode) Is Nothing Then
            '    objNewMember.GrossPrice30 = GrossPrice / (objNewMember.Bookingtype.Weeks(1).Films(Filmcode).Index / 100)
            'End If
            objNewMember.MyEstimateBuyTarget = MyEstimateChannelTarget
            mCol.Add(objNewMember, ID)


            'return the object created
            Add = objNewMember
                objNewMember = Nothing

            Catch ex As Exception
                Return Nothing
            End Try


        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cBookedSpot
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                If vntIndexKey Is Nothing OrElse vntIndexKey.GetType.ToString = "System.String" Then
                    If vntIndexKey IsNot Nothing AndAlso mCol.Contains(vntIndexKey) Then
                        Return mCol(vntIndexKey)
                    Else
                        Return Nothing
                    End If
                Else
                    Return mCol(vntIndexKey)
                End If
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Get
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)
            If Main.ExtendedInfos.Exists(mCol(vntIndexKey).DatabaseID) Then
                Main.ExtendedInfos(mCol(vntIndexKey).DatabaseID).IsBooked = False
            End If
            Main.RFEstimation.Spots.Remove(vntIndexKey)
            mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cKampanj)
            mCol = New Collection
            Main = MainObject
            If Not Main.RFEstimation Is Nothing Then
                Main.RFEstimation.Spots.Clear()
            End If
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub


        Public Function TotalTRP() As Single
            Dim TmpTRP As Single = 0

            For Each TmpSpot As cBookedSpot In mCol
                TmpTRP += TmpSpot.MyEstimate
            Next
            Return TmpTRP
        End Function

        Public Function TotalNetBudget() As Single
            Dim TmpBudget As Single = 0

            For Each TmpSpot As cBookedSpot In mCol
                TmpBudget += TmpSpot.NetPrice
            Next
            Return TmpBudget
        End Function

        Public Function TotalGrossBudget() As Single
            Dim TmpBudget As Single = 0

            For Each TmpSpot As cBookedSpot In mCol
                TmpBudget += TmpSpot.GrossPrice
            Next
            Return TmpBudget
        End Function

        Private Function CheckFilms() As List(Of String)
            Dim outOfDateSpots As List(Of cBookedSpot) = From bSpot As cBookedSpot In mCol Select bSpot Where bSpot.AirDate < Date.FromOADate(Main.StartDate) Or bSpot.AirDate > Date.FromOADate(Main.EndDate)


        End Function
        Public Function FindBookedSpotsProblems() As List(Of String)

            CheckFilms()

        End Function

    End Class



End Namespace