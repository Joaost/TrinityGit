Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity
    Public Class cChannels
        'the cChannels is a collection of cChannel objects. It is fairly simple as it is only used as a container.
        Implements Collections.IEnumerable

        'local variable to hold Collection
        Private mCol As Collection
        Private Main As cKampanj
        Private mvarDefaultFilmIndex(700) As Single

        ' Event and delegate to raise when the TRp might have been changed
        Public Event TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)

        Public Sub _trpChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
            RaiseEvent TRPChanged(sender, e)
        End Sub

        Public Event FilmChanged(Film As cFilm)
        Public Event WeekChanged(Week As cWeek)
        Public Event BookingtypeChanged(Bookingtype As cBookingType)
        Public Event ChannelChanged(Channel As cChannel)
        Public Event DaypartDefinitionsChanged()

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
        End Sub

        Private Sub _weekChanged(Week As cWeek)
            RaiseEvent WeekChanged(Week)
        End Sub

        Private Sub _bookingtypeChanged(Bookingtype As cBookingType)
            RaiseEvent BookingtypeChanged(Bookingtype)
        End Sub

        Private Sub _channelChanged(Channel As cChannel)
            RaiseEvent ChannelChanged(Channel)
        End Sub

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLChannel As Xml.XmlElement

            For Each TmpChannel As Trinity.cChannel In Me
                XMLChannel = xmlDoc.CreateElement("Channel")
                TmpChannel.GetXML(XMLChannel, errorMessege, xmlDoc)
                colXml.AppendChild(XMLChannel)
            Next

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving the channels collection")
            Resume Next
        End Function

        Public Function saveChannels(ByVal strFile As String) As Boolean

            'If its stored in database
            If (TrinitySettings.ConnectionStringCommon <> "") Then

                Trinity.Helper.WriteToLogFile("Saveing channel definitions to database")

                For Each TmpChan As Trinity.cChannel In Me
                    If TmpChan.fileName = strFile Then
                        TmpChan.saveChannelToDatabase(True)
                    End If
                Next

            Else

                If strFile = "Default" Then
                    strFile = "Channels.xml"
                Else
                    strFile = strFile & ".xml"
                End If

                Trinity.Helper.WriteToLogFile("Save channel definitions")

                Dim XmlDoc As New XmlDocument
                Dim XmlRoot As XmlElement
                Dim XmlChannels As XmlElement
                Dim XmlChannel As XmlElement
                Dim XmlBTs As XmlElement
                Dim XmlBT As XmlElement
                Dim XmlIndexes As XmlElement
                Dim XmlIndex As XmlElement

                Dim Node As XmlNode

                Trinity.Helper.WriteToLogFile("Start creating document")
                XmlDoc.PreserveWhitespace = True
                Node = XmlDoc.CreateProcessingInstruction("xml", "version='1.0'")
                XmlDoc.AppendChild(Node)

                Node = XmlDoc.CreateComment("Trinity channel definition file.")
                XmlDoc.AppendChild(Node)
                Node = XmlDoc.CreateComment("Saved by user " & TrinitySettings.UserName & " at " & Now)
                XmlDoc.AppendChild(Node)

                XmlRoot = XmlDoc.CreateElement("Data")

                Dim TmpXml As XmlElement = XmlDoc.CreateElement("DefaultSpotIndex")
                For i As Integer = 1 To 500
                    If Main.Channels.DefaultFilmIndex(i) > 0 Then
                        XmlIndex = XmlDoc.CreateElement("Index")
                        XmlIndex.SetAttribute("Length", i)
                        XmlIndex.SetAttribute("Idx", Main.Channels.DefaultFilmIndex(i))
                        TmpXml.AppendChild(XmlIndex)
                    End If
                Next
                XmlRoot.AppendChild(TmpXml)

                XmlChannels = XmlDoc.CreateElement("Channels")

                XmlRoot.AppendChild(XmlChannels)

                For Each TmpChan As Trinity.cChannel In Main.Channels
                    If TmpChan.fileName = strFile Then
                        XmlChannel = XmlDoc.CreateElement("Channel")
                        XmlChannel.SetAttribute("Name", TmpChan.ChannelName)
                        XmlChannel.SetAttribute("Shortname", TmpChan.Shortname)
                        XmlChannel.SetAttribute("Marathon", TmpChan.MarathonName)
                        XmlChannel.SetAttribute("BuyingUniverse", TmpChan.BuyingUniverse)
                        XmlChannel.SetAttribute("AdEdgeNames", TmpChan.AdEdgeNames)
                        XmlChannel.SetAttribute("MatrixName", TmpChan.MatrixName)
                        XmlChannel.SetAttribute("AgencyCommission", TmpChan.AgencyCommission)
                        XmlChannel.SetAttribute("ListNumber", TmpChan.ListNumber)
                        XmlChannel.SetAttribute("ConnectedChannel", TmpChan.ConnectedChannel)
                        XmlChannel.SetAttribute("Penalty", TmpChan.Penalty)
                        XmlChannel.SetAttribute("Logo", TmpChan.LogoPath)
                        XmlChannel.SetAttribute("AdtooxID", TmpChan.AdTooxChannelID)
                        XmlChannel.SetAttribute("UseBid", TmpChan.UseBid)
                        XmlChannel.SetAttribute("ChannelGroup", TmpChan.ChannelGroup)

                        XmlIndexes = XmlDoc.CreateElement("SpotIndex")
                        For i As Integer = 1 To 200
                            If TmpChan.BookingTypes.Count > 0 AndAlso TmpChan.BookingTypes(1).FilmIndex(i) <> 0 Then
                                XmlIndex = XmlDoc.CreateElement("Index")
                                XmlIndex.SetAttribute("Length", i)
                                XmlIndex.SetAttribute("Idx", TmpChan.BookingTypes(1).FilmIndex(i))
                                XmlIndexes.AppendChild(XmlIndex)
                            End If
                        Next
                        XmlChannel.AppendChild(XmlIndexes)
                        XmlChannels.AppendChild(XmlChannel)
                        XmlBTs = XmlDoc.CreateElement("Bookingtypes")
                        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                            XmlBT = XmlDoc.CreateElement("Bookingtype")
                            XmlBT.SetAttribute("Name", TmpBT.Name)
                            XmlBT.SetAttribute("Shortname", TmpBT.Shortname)
                            XmlBT.SetAttribute("IsRBS", TmpBT.IsRBS)
                            XmlBT.SetAttribute("IsSpecific", TmpBT.IsSpecific)
                            XmlBT.SetAttribute("IsPremium", TmpBT.IsPremium)
                            XmlBT.SetAttribute("IsCompensation", TmpBT.IsCompensation)
                            XmlBT.SetAttribute("IsSponsorship", TmpBT.IsSponsorship)
                            XmlBT.SetAttribute("SpecificsFactor", TmpBT.EnhancementFactor)
                            XmlBT.SetAttribute("PrintBookingCode", TmpBT.PrintBookingCode)
                            XmlBT.SetAttribute("PrintDayparts", TmpBT.PrintDayparts)
                            XmlBT.SetAttribute("Pricelist", TmpBT.PricelistName)
                            XmlBTs.AppendChild(XmlBT)
                        Next
                        XmlChannel.AppendChild(XmlBTs)

                        'if there are addresses available we save those aswell
                        If TmpChan.DeliveryAddress <> "" OrElse TmpChan.VHSAddress <> "" Then
                            'save the channel adresses
                            Dim XmlDocA As XmlDocument = New XmlDocument

                            XmlDocA.PreserveWhitespace = True
                            Dim NodeA As XmlNode = XmlDocA.CreateProcessingInstruction("xml", "version='1.0'")
                            XmlDocA.AppendChild(NodeA)

                            Node = XmlDoc.CreateComment("Trinity channel definition file.")
                            XmlDocA.AppendChild(NodeA)

                            Dim XmlRoota As XmlElement = XmlDocA.CreateElement("Data")
                            Dim XmlAddress As Xml.XmlElement = XmlDocA.CreateElement("Address")
                            XmlAddress.InnerText = TmpChan.DeliveryAddress
                            XmlRoota.AppendChild(XmlAddress)

                            XmlAddress = XmlDocA.CreateElement("VHSAddress")
                            XmlAddress.InnerText = TmpChan.VHSAddress
                            XmlRoota.AppendChild(XmlAddress)

                            XmlDocA.AppendChild(XmlRoota)
                            XmlDocA.Normalize()
                            XmlDocA.Save(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Channel Info\" & TmpChan.ChannelName & ".xml")
                        End If

                    End If
                Next

                'XmlBTs = XmlDoc.CreateElement("Bookingtypes")
                'For Each TmpBT As Trinity.cBookingType In DirectCast(cmbChannel.SelectedItem, Trinity.cChannel).BookingTypes
                '    XmlBT = XmlDoc.CreateElement("Bookingtype")
                '    XmlBT.SetAttribute("Name", TmpBT.Name)
                '    XmlBT.SetAttribute("Shortname", TmpBT.Shortname)
                '    XmlBT.SetAttribute("IsRBS", TmpBT.IsRBS)
                '    XmlBT.SetAttribute("IsSpecific", TmpBT.IsSpecific)
                '    XmlBT.SetAttribute("IsPremium", TmpBT.IsPremium)
                '    XmlBT.SetAttribute("PrintBookingCode", TmpBT.PrintBookingCode)
                '    XmlBT.SetAttribute("PrintDayparts", TmpBT.PrintDayparts)
                '    XmlBTs.AppendChild(XmlBT)
                'Next
                'XmlRoot.AppendChild(XmlBTs)
                XmlDoc.AppendChild(XmlRoot)
                XmlDoc.Normalize()
                XmlDoc.Save(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\" & strFile)
            End If
        End Function

        Public Property DefaultFilmIndex(ByVal Length As Integer) As Single
            Get
                Try
                    DefaultFilmIndex = mvarDefaultFilmIndex(Length)
                Catch

                End Try
            End Get
            Set(ByVal value As Single)
                mvarDefaultFilmIndex(Length) = value
            End Set
        End Property

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Dim TmpChannel As cChannel

                Main = value
                For Each TmpChannel In mCol
                    TmpChannel.MainObject = value
                Next
            End Set
        End Property

        Friend ReadOnly Property RawCollection As Collection
            Get
                Return mCol
            End Get
        End Property

        Public Function Add(ByVal Name As String, ByVal filename As String, Optional ByVal Area As String = "", Optional ByVal DBID As String = "NOT SAVED") As cChannel
            'creates a new object
            Dim objNewMember As cChannel
            Dim TmpChannel As New cChannel(Main, mCol)
            Dim i As Integer

            On Error GoTo Add_Error

            objNewMember = New cChannel(Main, mCol)

            'Make sure to remove all extra spaces
            Name = Trim(Name)
            Area = Trim(Area)

            'set the properties passed into the method
            objNewMember.MainObject = Main
            objNewMember.ChannelName = Name
            objNewMember.databaseID = DBID
            'objNewMember.DBID = ID

            'set the filename
            If filename = "" Then
                objNewMember.fileName = "Channels.xml"
            ElseIf IsNumeric(filename) Then
                objNewMember.fileName = filename
            Else
                If filename = "" Then
                    objNewMember.fileName = "Channels.xml"
                Else
                    If filename.LastIndexOf(".") > 0 Then
                        objNewMember.fileName = filename
                    Else
                        objNewMember.fileName = filename & ".xml"
                    End If
                End If
            End If

            If objNewMember.databaseID = "NOT SAVED" Then
                objNewMember.ReadDefaultProperties(Area)
            End If

            AddHandler objNewMember.TRPChanged, AddressOf _trpChanged
            AddHandler objNewMember.ChannelChanged, AddressOf _channelChanged
            AddHandler objNewMember.BookingtypeChanged, AddressOf _bookingtypeChanged
            AddHandler objNewMember.WeekChanged, AddressOf _weekChanged
            AddHandler objNewMember.FilmChanged, AddressOf _filmChanged

            'Find channel before or after
            TmpChannel = Nothing
            If mCol.Count > 0 And objNewMember.ListNumber > 0 Then
                i = 1
                TmpChannel = mCol(1)
                While TmpChannel.ListNumber < objNewMember.ListNumber And i <= mCol.Count
                    TmpChannel = mCol(i)
                    i = i + 1
                End While
                If i >= mCol.Count And TmpChannel.ListNumber < objNewMember.ListNumber Then
                    TmpChannel = Nothing
                End If
            End If

            On Error Resume Next

            If TmpChannel Is Nothing OrElse TmpChannel.ChannelName = "" Then
                mCol.Add(objNewMember, Name)
            ElseIf TmpChannel.ListNumber > objNewMember.ListNumber Then
                mCol.Add(objNewMember, Name, TmpChannel.ChannelName)
            Else
                mCol.Add(objNewMember, Name, , TmpChannel.ChannelName)
            End If

            On Error GoTo Add_Error

            'return the object created
            Add = mCol(Name)
            objNewMember = Nothing


            On Error GoTo 0
            Exit Function

Add_Error:

            Err.Raise(Err.Number, "cChannels: Add", Err.Description)


        End Function

        Private Sub SetDefaultIndexes()
            Dim XMLDoc As New XmlDocument
            Dim XMLTmpNode As XmlElement

            XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & TrinitySettings.DefaultArea & "\Channels.xml")

            Dim TmpXml As XmlElement = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("DefaultSpotIndex")
            If Not TmpXml Is Nothing Then
                XMLTmpNode = TmpXml.ChildNodes.Item(0)
                While Not XMLTmpNode Is Nothing
                    Me.DefaultFilmIndex(XMLTmpNode.GetAttribute("Length")) = XMLTmpNode.GetAttribute("Idx")
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
            End If
        End Sub

        ''' <summary>
        ''' Returns true if the campaign contains the specified ChannelName
        ''' </summary>
        ''' <param name="ChannelName"></param>
        ''' <value>A string</value>
        ''' <returns>A boolean</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Contains(ByVal ChannelName As String) As Boolean
            Get
                If ChannelName Is Nothing Then Return False
                Contains = (From Chans As cChannel In mCol Where Chans.ChannelName = ChannelName).Count > 0
                'For Each Chan As cChannel In mCol
                '    If Chan.ChannelName = ChannelName Then Return True
                'Next
                'Return False
            End Get
        End Property
        Default Public ReadOnly Property Item(ByVal Key As String) As cChannel
            'used when referencing an element in the Collection
            'vntIndexKey contains either the Index or Key to the Collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                Try
                    If mCol.Contains(Key) Then
                        Return mCol(Key)
                    Else
                        Return Nothing
                    End If
                Catch
                    Return Nothing
                End Try
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As cChannel
            'used when referencing an element in the Collection
            'vntIndexKey contains either the Index or Key to the Collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                Try
                    If Index <= mCol.Count Then
                        Return mCol(Index)
                    Else
                        Return Nothing
                    End If
                Catch
                    Return Nothing
                End Try
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            'used when retrieving the number of elements in the
            'Collection. Syntax: Debug.Print x.Count
            Get
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the Collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)
            mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cKampanj)
            mCol = New Collection
            Main = MainObject
            SetDefaultIndexes()
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub
        
        Dim _progress As frmProgress
        Public Function updateAllChannels(Optional ByVal KeepCommissions As Boolean = False) As Boolean
            Dim _daypartsChanged As Boolean = False
            Try
                Me._progress = New frmProgress
                Me._progress.MaxValue = Main.Channels.Count
                Me._progress.Show()
                Me._progress.BarType = cProgressBarType.SingleBar

                '(TrinitySettings.ConnectionStringCommon <> "")
                If (TrinitySettings.ConnectionStringCommon <> "") Then
                    'Update all channel information
                    For Each TmpChan As Trinity.cChannel In Main.Channels
                        If Not TmpChan.updateChannelFromDatabase(True) Then
                            MsgBox("FAILED TO UPDATE CHANNEL " & TmpChan.ChannelName, MsgBoxStyle.Critical)
                        End If
                    Next

                    'check for new channels
                    DBReader.checkForNewChannels(Main)

                Else
                    Dim chanList As New Dictionary(Of String, Single)
                    For Each tmpChan As cChannel In Main.Channels
                        chanList.Add(tmpChan.ChannelName, tmpChan.AgencyCommission)
                    Next

                    For Each TmpChan As Trinity.cChannel In Main.Channels
                        Me._progress.Progress += 1
                        Me._progress.Status = "Updating channel " & TmpChan.ChannelName

                        Dim tmpChanNew As New Trinity.cChannel(Main, mCol)
                        tmpChanNew.ChannelName = TmpChan.ChannelName
                        tmpChanNew.MainObject = Campaign
                        tmpChanNew.fileName = TmpChan.fileName

                        If tmpChanNew.ReadDefaultProperties("") Then
                            tmpChanNew.readDefaultBookingTypes()
                            TmpChan.Shortname = tmpChanNew.Shortname
                            TmpChan.BuyingUniverse = tmpChanNew.BuyingUniverse
                            TmpChan.AdEdgeNames = tmpChanNew.AdEdgeNames
                            TmpChan.MatrixName = tmpChanNew.MatrixName
                            TmpChan.MarathonName = tmpChanNew.MarathonName
                            TmpChan.ConnectedChannel = tmpChanNew.ConnectedChannel
                            TmpChan.Penalty = tmpChanNew.Penalty
                            TmpChan.ChannelGroup = tmpChanNew.ChannelGroup

                            If Not KeepCommissions Then
                                TmpChan.AgencyCommission = tmpChanNew.AgencyCommission
                            Else
                                TmpChan.AgencyCommission = chanList(TmpChan.ChannelName)
                            End If

                            TmpChan.ListNumber = tmpChanNew.ListNumber
                            TmpChan.LogoPath = tmpChanNew.LogoPath
                            TmpChan.DeliveryAddress = tmpChanNew.DeliveryAddress
                            TmpChan.AdTooxChannelID = tmpChanNew.AdTooxChannelID
                            TmpChan.UseBid = tmpChanNew.UseBid

                            'update the BT that exists
                            For Each bt As Trinity.cBookingType In TmpChan.BookingTypes
                                If Not tmpChanNew.BookingTypes(bt.Name) Is Nothing Then
                                    bt.Shortname = tmpChanNew.BookingTypes(bt.Name).Shortname
                                    bt.IsRBS = tmpChanNew.BookingTypes(bt.Name).IsRBS
                                    bt.IsSpecific = tmpChanNew.BookingTypes(bt.Name).IsSpecific
                                    bt.IsSponsorship = tmpChanNew.BookingTypes(bt.Name).IsSponsorship
                                    bt.IsPremium = tmpChanNew.BookingTypes(bt.Name).IsPremium
                                    bt.EnhancementFactor = tmpChanNew.BookingTypes(bt.Name).EnhancementFactor
                                    bt.PrintDayparts = tmpChanNew.BookingTypes(bt.Name).PrintDayparts
                                    bt.PrintBookingCode = tmpChanNew.BookingTypes(bt.Name).PrintBookingCode
                                    bt.PricelistName = tmpChanNew.BookingTypes(bt.Name).PricelistName

                                    For i As Integer = 1 To 500
                                        If tmpChanNew.BookingTypes(bt.Name).FilmIndex(i) > 0 Then
                                            bt.FilmIndex(i) = tmpChanNew.BookingTypes(bt.Name).FilmIndex(i)
                                        End If
                                    Next


                                    tmpChanNew.BookingTypes(bt.Name).ReadDefaultDayparts()

                                    Dim _changed As Boolean = False
                                    If tmpChanNew.BookingTypes(bt.Name).Dayparts.Count > 0 Then
                                        For Each _dp As cDaypart In tmpChanNew.BookingTypes(bt.Name).Dayparts
                                            If Not bt.Dayparts.Contains(_dp.Name) Then
                                                _dp.Parent = bt.Dayparts
                                                bt.Dayparts.Add(New cDaypart() With {.Name = _dp.Name})
                                                If bt.BookIt Then _daypartsChanged = True
                                                _changed = True
                                            End If
                                            With bt.Dayparts(_dp.Name)
                                                .StartMaM = _dp.StartMaM
                                                .EndMaM = _dp.EndMaM
                                                .IsPrime = _dp.IsPrime
                                                .MyIndex = _dp.MyIndex
                                            End With
                                        Next
                                        Dim _removeList As New List(Of cDaypart)
                                        For Each _dp As cDaypart In bt.Dayparts
                                            If Not tmpChanNew.BookingTypes(bt.Name).Dayparts.Contains(_dp.Name) Then
                                                _removeList.Add(_dp)
                                            End If
                                        Next
                                        For Each _dp As cDaypart In _removeList
                                            bt.Dayparts.Remove(_dp)
                                            If bt.BookIt Then _daypartsChanged = True
                                            _changed = True
                                        Next
                                    End If

                                    Dim _needSorting As Boolean = True
                                    While _needSorting
                                        Dim i As Integer = 0
                                        _needSorting = False
                                        While i < bt.Dayparts.Count - 1
                                            If bt.Dayparts(i + 1).MyIndex - bt.Dayparts(i).MyIndex < 1 Then
                                                _needSorting = True
                                                bt.Dayparts.SwitchPosition(bt.Dayparts(i), bt.Dayparts(i + 1))
                                                _changed = True
                                                Exit While
                                            End If
                                            i += 1
                                        End While
                                    End While
                                End If
                            Next

                            'add BT that does not exists
                            For Each bt As Trinity.cBookingType In tmpChanNew.BookingTypes
                                If TmpChan.BookingTypes(bt.Name) Is Nothing Then
                                    TmpChan.BookingTypes.Add(bt.Name, True)
                                    TmpChan.BookingTypes(bt.Name).Shortname = tmpChanNew.BookingTypes(bt.Name).Shortname
                                    TmpChan.BookingTypes(bt.Name).IsRBS = tmpChanNew.BookingTypes(bt.Name).IsRBS
                                    TmpChan.BookingTypes(bt.Name).IsSpecific = tmpChanNew.BookingTypes(bt.Name).IsSpecific
                                    TmpChan.BookingTypes(bt.Name).PrintDayparts = tmpChanNew.BookingTypes(bt.Name).PrintDayparts
                                    TmpChan.BookingTypes(bt.Name).PrintBookingCode = tmpChanNew.BookingTypes(bt.Name).PrintBookingCode

                                    tmpChanNew.BookingTypes(bt.Name).ReadDefaultDayparts()

                                    For Each _dp As cDaypart In tmpChanNew.BookingTypes(bt.Name).Dayparts
                                        If Not bt.Dayparts.Contains(_dp.Name) Then
                                            _dp.Parent = bt.Dayparts
                                            bt.Dayparts.Add(_dp)
                                        Else
                                            With bt.Dayparts(_dp.Name)
                                                .StartMaM = _dp.StartMaM
                                                .EndMaM = _dp.EndMaM
                                                .IsPrime = _dp.IsPrime
                                                .MyIndex = _dp.MyIndex
                                            End With
                                        End If
                                    Next

                                    For Each TmpWeek As Trinity.cWeek In TmpChan.BookingTypes(1).Weeks
                                        With TmpChan.BookingTypes(bt.Name).Weeks.Add(TmpWeek.Name)
                                            .StartDate = TmpWeek.StartDate
                                            .EndDate = TmpWeek.EndDate
                                            For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                                With .Films.Add(TmpFilm.Name)
                                                    .Description = TmpFilm.Description
                                                    .Filmcode = TmpFilm.Filmcode
                                                    .FilmLength = TmpFilm.FilmLength
                                                    .GrossIndex = TmpFilm.GrossIndex
                                                    .Index = TmpFilm.Index
                                                End With
                                            Next
                                        End With
                                    Next
                                End If
                            Next
                        Else
                            If Windows.Forms.MessageBox.Show("The channel " & tmpChanNew.ChannelName & " is no longer in the channel file." & vbCrLf & vbCrLf & "Do you want to delete it from the campaign?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                Main.Channels.Remove(tmpChanNew.ChannelName)
                            End If
                        End If
                    Next

                    Dim TmpCampaign As New Trinity.cKampanj(False)
                    TmpCampaign.Area = Campaign.Area
                    TmpCampaign.CreateChannels()

                    For Each TmpChan As Trinity.cChannel In TmpCampaign.Channels
                        If Main.Channels(TmpChan.ChannelName) Is Nothing Then
                            Main.Channels.Add(TmpChan.ChannelName, "Channels.xml")
                            Main.Channels(TmpChan.ChannelName).ReadDefaultProperties()
                            Main.Channels(TmpChan.ChannelName).readDefaultBookingTypes()
                            For Each TmpBT As Trinity.cBookingType In Main.Channels(TmpChan.ChannelName).BookingTypes
                                For Each TmpWeek As Trinity.cWeek In Main.Channels(1).BookingTypes(1).Weeks
                                    With TmpBT.Weeks.Add(TmpWeek.Name)
                                        .StartDate = TmpWeek.StartDate
                                        .EndDate = TmpWeek.EndDate
                                        .IsLocked = False
                                        .IsVisible = True
                                        .Bookingtype = TmpBT
                                        For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                            With .Films.Add(TmpFilm.Name)
                                                .Bookingtype = TmpBT
                                                .Description = TmpFilm.Description
                                                .FilmLength = TmpFilm.FilmLength
                                                .Filmcode = TmpFilm.Filmcode
                                            End With
                                        Next
                                    End With
                                Next
                                TmpBT.Dayparts = TmpChan.BookingTypes(TmpBT.Name).Dayparts
                            Next
                        End If
                    Next
                End If
                If _daypartsChanged Then
                    RaiseEvent DaypartDefinitionsChanged()
                End If
                Me._progress.Hide()
                Me._progress.Dispose()
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Me._progress.Hide()
            Me._progress.Dispose()
        End Function
    End Class
End Namespace