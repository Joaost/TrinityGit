Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cTarget
        Implements IDetectsProblems

        Public Enum EnumUni
            uniMainTot = 0
            uniMainSec = 1
            uniSecTot = 2
            uniMainCmp = 3
        End Enum

        Public Enum TargetTypeEnum
            trgMnemonicTarget = 0
            trgUserTarget = 1
            trgDynamicTarget = 2
        End Enum

        Public Enum ProblemsEnum
            UniverseIsNotUpdated = 1
        End Enum

        Private mvarTargetType As TargetTypeEnum
        Private mvarUniverse As String
        Private mvarUniSize As Long
        Private mvarTargetName As String
        Private _main As cKampanj
        Private mvarNoUniSize As Boolean
        Private mvarSecondUniverse As String
        Private mvarTargetGroup As String

        Private LastTargStr As String

        Private SaveUniIndex(0 To 3)

        Private mvarSaveSize As Single
        Private mvarSaveTarget As String
        Private mvarUniSizeRefPeriod As String = ""

        Public Event wasAltered()

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            On Error GoTo On_Error
            colXml.SetAttribute("Name", Me.TargetName)
            colXml.SetAttribute("Type", Me.TargetType)
            colXml.SetAttribute("TargetGroup", Me.TargetGroup)
            colXml.SetAttribute("Universe", Me.Universe)
            colXml.SetAttribute("SecondUniverse", Me.SecondUniverse)

            Exit Function
On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving Target " & Me.TargetName)
            Resume Next
        End Function



        Public Property TargetType() As TargetTypeEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : TargetType
            ' DateTime  : 2003-07-08 15:06
            ' Author    : joho
            ' Purpose   : Returns/sets the type of target used:
            '
            '               0 - Mnemonic Target
            '               1 - User Target     
            '               2 - Dynamic Target  (not implemented)
            '
            '---------------------------------------------------------------------------------------
            Get
                On Error GoTo TargetType_Error
                TargetType = mvarTargetType
                On Error GoTo 0
                Exit Property
TargetType_Error:
                Err.Raise(Err.Number, "cTarget: TargetType", Err.Description)
            End Get
            Set(ByVal value As TargetTypeEnum)
                On Error GoTo TargetType_Error
                mvarTargetType = value
                On Error GoTo 0
                Exit Property
TargetType_Error:
                Err.Raise(Err.Number, "cTarget: TargetType", Err.Description)
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : TargetGroup
        ' DateTime  : 2007-02-12
        ' Author    : joho
        ' Purpose   : Returns/sets the TargetGroup for a User Target. Not used on Mnemonic Targets.
        '---------------------------------------------------------------------------------------

        Public Property TargetGroup() As String
            Get
                Return mvarTargetGroup
            End Get
            Set(ByVal value As String)
                mvarTargetGroup = value
            End Set
        End Property

        Public Property Universe(Optional ByVal isPriceCheck As Boolean = False) As String
            'isPriceCheck is only used for the set property
            '---------------------------------------------------------------------------------------
            ' Procedure : Universe
            ' DateTime  : 2003-07-08 15:06
            ' Author    : joho
            ' Purpose   : Returns/sets the universe used. An error is raised if the universe
            '             does not exist.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Universe_Error
                Universe = mvarUniverse
                On Error GoTo 0
                Exit Property
Universe_Error:
                Err.Raise(Err.Number, "cTarget: Universe", Err.Description)
            End Get
            Set(ByVal value As String)
                Dim i As Integer
                On Error GoTo Universe_Error
                If value = "<National>" Then value = ""
                Return
                'Commented out since Universes are no longer supported
                'If value = mvarUniverse AndAlso _main.UniverseCollection.Contains(value) Then Exit Property

                If isPriceCheck Then Exit Property

                mvarSaveTarget = ""
                If value <> "" Then
                    Helper.WriteToLogFile("cTarget.Universe : Check if Universe(" & value & ") exists")
                    i = _main.InternalAdedge.setUniverseUserDefined(value, Nothing, False)
                    If i > 1 Then
                        mvarUniverse = value
                    End If
                    Helper.WriteToLogFile("cTarget.Universe : Reset Universes")
                    '*****Commented out when Timeshift arrived
                    'If _main.UniStr Is Nothing OrElse Not _main.UniverseCollection.Contains(value) Then
                    ' PrepareAdedge()
                    'End If
                    '_main.InternalAdedge.setUniverseUserDefined(_main.UniStr)
                    '********
                    '        If Kampanj.InternalAdedge.Validate And 2 Then
                    '            Kampanj.InternalAdedge.setChannels Kampanj.Channels(1).AdEdgeNames
                    '        End If
                    '        Kampanj.InternalAdedge.Run
                End If

                If i <> 2 And value <> "" Then
                    mvarUniverse = ""
                Else
                    mvarUniverse = value
                    If Not mvarNoUniSize Then
                        Helper.WriteToLogFile("cTarget.Universe : Get UniverseSizes")
                        GetUniSizes()
                    End If
                End If

                On Error GoTo 0
                Exit Property
Universe_Error:
                If Err.Number = 2000 Then
                    Err.Raise(9, "cTarget: Universe", "Unknown universe: " & Universe)
                Else
                    Err.Raise(Err.Number, "cTarget: Universe", Err.Description)
                End If
            End Set
        End Property

        Public ReadOnly Property UniSize(Optional HasBeenErrors = False) As String
            '---------------------------------------------------------------------------------------
            ' Procedure : UniSize
            ' DateTime  : 2003-07-08 15:06
            ' Author    : joho
            ' Purpose   : Returns the Universe size of the target.
            '---------------------------------------------------------------------------------------
            '
            Get
                Dim TmpChan As cChannel
                Dim ChanStr As String = ""
                'Dim TargIdx As Integer
                'Dim i As Integer
                'Dim Target As String
                'Target = Trim(mvarTargetName)
                'TargIdx = -1
                'For i = 0 To _main.InternalAdedge.getTargetCount - 1
                '    If _main.InternalAdedge.getTargetTitle(i) = Target Then
                '        TargIdx = i
                '        Exit For
                '    End If
                '    If _main.InternalAdedge.getTargetTitle(i) = "A" & Target Then
                '        TargIdx = i
                '        Exit For
                '    End If
                'Next
                Try
                    If _main.TargColl(mvarTargetName.ToString, _main.InternalAdedge) < 1 Then
                        Universe = mvarUniverse
                    End If
                Catch
                    Universe = mvarUniverse
                End Try
                Try
                    If _main.TargColl(mvarTargetName.ToString, _main.InternalAdedge) < 1 AndAlso Not Trinity.Helper.AddTarget(_main.InternalAdedge, Me, True) Then
                        Return 0
                    End If
                Catch
                    Return 0
                End Try
                'Try
                '    If Not _main.UniverseCollection.Contains(mvarUniverse.ToString) Then
                '        Return 0
                '    End If
                'Catch
                '    Return 0
                'End Try
                Try
                    'Make sure the UniSize is calculated from a correct period
                    Dim _refPeriod As String = ""
                    If _main.StartDate > 0 AndAlso _main.StartDate <= _main.InternalAdedge.getDataRangeFrom(Connect.eDataType.mSpot) Then
                        _refPeriod = Format(Date.FromOADate(_main.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(_main.EndDate), "ddMMyy")
                    Else
                        _refPeriod = "-1d"
                    End If
                    _main.InternalAdedge.setPeriod(_refPeriod)
                    If mvarTargetName <> "" AndAlso _main.InternalAdedge.validate = 0 Then
                        '_main.Adedge.setTargetMnemonic(_main.TargStr)
                        If ((mvarSaveTarget <> mvarTargetName OrElse mvarUniSizeRefPeriod <> _refPeriod) AndAlso _main.InternalAdedge.validate = 0) OrElse mvarSaveSize <> 0 Then
                            mvarSaveSize = _main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, Helper.TargetIndex(_main.InternalAdedge, Me))
                            mvarSaveTarget = mvarTargetName
                            mvarUniSizeRefPeriod = _refPeriod
                        End If
                        Return mvarSaveSize
                        'Stop
                    Else
                        Try
                            Return _main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, Helper.TargetIndex(_main.InternalAdedge, Me))
                        Catch ex As Exception
                            Return 0
                        End Try
                    End If
                    Exit Property

                Catch ex As COMException When ex.Message = "getUniSampleInfo: Must call Run before extraction of values"
                    Return 0
                Catch ex As Exception
                    If Not HasBeenErrors Then
                        Universe = mvarUniverse
                        For Each TmpChan In _main.Channels
                            ChanStr = ChanStr & TmpChan.AdEdgeNames & ","
                        Next
                        HasBeenErrors = True
                        '_main.Adedge.setTargetMnemonic(_main.TargStr)
                        Trinity.Helper.AddTargetsToAdedge(_main.InternalAdedge, True)
                        '_main.InternalAdedge.setTargetMnemonic(_main.TargStr)
                        '_main.InternalAdedge.setUniverseUserDefined(_main.UniStr)
                        _main.InternalAdedge.setArea(_main.Area)
                        _main.InternalAdedge.setChannelsArea(ChanStr, _main.Area)
                        _main.InternalAdedge.setPeriod("-1d")
                        _main.InternalAdedge.Run()
                        Return UniSize(True)
                    End If
                    Throw New Exception("cTarget: UniSize" & vbCrLf & ex.Message, ex)
                End Try
            End Get

        End Property

        Public Property TargetName() As String
            'gets/sets and sets the targets name
            Get
                On Error GoTo TargetName_Error
                If mvarTargetName = "" Then
                    If Not _main Is Nothing Then
                        mvarTargetName = _main.AllAdults
                    End If
                End If
                Return mvarTargetName
                On Error GoTo 0
                Exit Property
TargetName_Error:
                Err.Raise(Err.Number, "cTarget: TargetName", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo TargetName_Error
                If _main.TargetCollection.Contains(Me) Then _main.TargetCollection.Remove(Me)
                If Right(value, 3) = "-99" Then
                    value = Left(value, Len(value) - 3) & "+"
                End If
                If value = "" Then
                    If Not _main Is Nothing Then
                        mvarTargetName = _main.AllAdults
                    End If
                Else
                    mvarTargetName = value
                End If
                'If _main.InternalAdedge.setTargetMnemonic(mvarTargetName) = 0 Then
                '    Err.Raise(13, Me.ToString, "Target " & mvarTargetName & " is illegal")
                'End If
                If Not mvarNoUniSize Then
                    GetUniSizes()
                End If
                mvarSaveTarget = ""
                On Error GoTo 0

                'this event will trigger the delegate events when main,secondary or third target is changed
                RaiseEvent wasAltered()

                Exit Property
TargetName_Error:
                Err.Raise(Err.Number, "cTarget: TargetName", Err.Description)
            End Set
        End Property

        Friend WriteOnly Property MainObject()
            Set(ByVal value)
                _main = value
                If _main IsNot Nothing Then
                    mvarSecondUniverse = TrinitySettings.DefaultSecondUniverse(_main.Area)
                End If
            End Set
        End Property

        Public ReadOnly Property UniSizeTot() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : UniSizeTot
            ' DateTime  : 2003-07-08 15:56
            ' Author    : joho
            ' Purpose   : Returns the _national_ Universe size of the target
            '---------------------------------------------------------------------------------------
            ' 
            Get
                Dim Test As Integer
                Dim HasBeenErrors As Boolean
                Dim TmpChan As cChannel
                Dim ChanStr As String

                On Error GoTo UniSizeTot_Error

                If mvarUniSize = 0 Then
                    Universe = mvarUniverse
                End If

                On Error Resume Next
                If _main.TargColl(Trim(mvarTargetName), _main.InternalAdedge) < 0 And _main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, _main.InternalAdedge.getTargetCount - 1) > 0 Then 'The getUniSampleInfo will produce an error if InternalAdedge has not been run since the adding of targets
                    PrepareAdedge()
                    _main.InternalAdedge.clearTargetSelection()
                    For Each TmpTarget As Trinity.cTarget In _main.TargetCollection
                        Trinity.Helper.AddTarget(_main.InternalAdedge, TmpTarget)
                    Next
                    If _main.TargColl(Trim(mvarTargetName), _main.InternalAdedge) < 0 Then
                        UniSizeTot = 0
                        Exit Property
                    End If
                End If
                On Error GoTo UniSizeTot_Error
                'mvarTargetName can't be empty
                If mvarTargetName <> "" Then
                    'Try
                    'UniSizeTot = _main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, _main.TargColl(mvarTargetName, _main.InternalAdedge) - 1)
                    
                    Dim t1 = _main.InternalAdedge.debug()
                    UniSizeTot = _main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, _main.TargColl(mvarTargetName, _main.InternalAdedge) - 1)

                    'Catch ex As Exception
                    'Debug.Print("Error getting the Uni size for target " & mvarTargetName)
                    'End Try

                Else
                    UniSizeTot = 0
                End If
                On Error GoTo 0
                Exit Property

UniSizeTot_Error:
                If Not HasBeenErrors Then
                    PrepareAdedge()
                    For Each TmpChan In _main.Channels
                        ChanStr = ChanStr & TmpChan.AdEdgeNames & ","
                    Next
                    HasBeenErrors = True
                    '_main.Adedge.setTargetMnemonic(_main.TargStr)
                    _main.InternalAdedge.clearTargetSelection()
                    For Each TmpTarget As Trinity.cTarget In _main.TargetCollection
                        Trinity.Helper.AddTarget(_main.InternalAdedge, TmpTarget)
                    Next
                    '_main.InternalAdedge.setUniverseUserDefined(_main.UniStr)
                    _main.InternalAdedge.setArea(_main.Area)
                    Trinity.Helper.AddTimeShift(_main.InternalAdedge)
                    '_main.InternalAdedge.setChannelsArea(ChanStr, _main.Area)
                    _main.InternalAdedge.setChannelsAll()
                    _main.InternalAdedge.setPeriod("-1d")

                    ''If _main.InternalAdedge.validate() <> 0 then
                    ''    MsgBox(_main.InternalAdedge.validate())
                    ''end if
                    
                    Dim t2 =_main.InternalAdedge.debug()
                    _main.InternalAdedge.Run()
                    Resume
                End If

                Err.Raise(Err.Number, "cTarget: UniSizeTot", Err.Description)
            End Get

        End Property

        Private Sub GetUniSizes()
            '---------------------------------------------------------------------------------------
            ' Procedure : GetUniSizes
            ' DateTime  : 2003-07-09 13:13
            ' Author    : joho
            ' Purpose   : Gets the universe sizes for this Target
            '---------------------------------------------------------------------------------------
            '

            Dim i As Integer
            Dim UniColl As New Collection
            Dim TmpDate As Date

            On Error GoTo GetUniSizes_Error

            If Not PrepareAdedge() Then
                Exit Sub
            End If

            If mvarTargetType = 0 And Not _main Is Nothing Then

                _main.InternalAdedge.setArea(_main.Area)
                _main.InternalAdedge.setPeriod("-14d")
                _main.InternalAdedge.clearList()
                'TODO: Replace call below with a better one
                If _main.Channels.Count > 0 Then
                    _main.InternalAdedge.clearList()
                    _main.InternalAdedge.setChannelsArea(_main.Channels(1).AdEdgeNames, _main.Area)
                    If _main.InternalAdedge.validate = 0 Then
                        TmpDate = Date.FromOADate(_main.InternalAdedge.getDataRangeTo(Connect.eDataType.mSpot))
                    Else
                        TmpDate = Now.AddDays(-14)
                    End If
                    _main.InternalAdedge.addBrand(Format(TmpDate, "ddMMyy"), "12:00", _main.Channels(1).AdEdgeNames, _main.Area)
                End If
                _main.InternalAdedge.clearTargetSelection()
                For Each TmpTarget As Trinity.cTarget In _main.TargetCollection
                    Trinity.Helper.AddTarget(_main.InternalAdedge, TmpTarget)
                Next
                i = _main.InternalAdedge.Run(False, False, 0, false)
                '        If i > 0 And Kampanj.InternalAdedge.Validate <> 4 Then
                '            If mvarUniverse = "" Then
                '                mvarUniSize = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , 0)
                '                mvarUniSizeTot = mvarUniSize
                '            Else
                '                mvarUniSize = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , UniColl(mvarUniverse))
                '                mvarUniSizeTot = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , 0)
                '            End If
                '            If mvarSecondUniverse = mvarUniverse Then
                '                mvarUniSizeSec = mvarUniSize
                '            Else
                '                mvarUniSizeSec = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , Kampanj.UniColl(mvarSecondUniverse))
                '            End If
                '        Else
                '            mvarUniSize = 0
                '            mvarUniSizeTot = 0
                '            mvarUniSizeSec = 0
                '        End If
            ElseIf mvarTargetType = TargetTypeEnum.trgUserTarget And Not _main Is Nothing Then

                _main.InternalAdedge.setArea(_main.Area)
                _main.InternalAdedge.setPeriod("-14d")
                _main.InternalAdedge.clearList()
                'TODO: Replace call below with a better one
                If _main.Channels.Count > 0 Then
                    _main.InternalAdedge.clearList()
                    _main.InternalAdedge.setChannelsArea(_main.Channels(1).AdEdgeNames, _main.Area)
                    TmpDate = Date.FromOADate(_main.InternalAdedge.getDataRangeTo(Connect.eDataType.mSpot))
                    _main.InternalAdedge.addBrand(Format(TmpDate, "ddMMyy"), "12:00", _main.Channels(1).AdEdgeNames, _main.Area)
                End If
                _main.InternalAdedge.clearTargetSelection()
                For Each TmpTarget As Trinity.cTarget In _main.TargetCollection
                    Trinity.Helper.AddTarget(_main.InternalAdedge, TmpTarget)
                Next
                i = _main.InternalAdedge.Run(False, False, -1)
            End If
            SaveUniIndex(0) = 0
            SaveUniIndex(1) = 0
            SaveUniIndex(2) = 0
            SaveUniIndex(3) = 0

            UniColl = Nothing
            On Error GoTo 0
            Exit Sub
GetUniSizes_Error:
            Err.Raise(Err.Number, "cTarget: GetUniSizes", Err.Description)


        End Sub

        Public Function UniIndex(Optional ByVal Universes As EnumUni = 0) As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : UniIndex
            ' DateTime  : 2003-07-09 13:15
            ' Author    : joho
            ' Purpose   : Function to return the index between the different universes
            '---------------------------------------------------------------------------------------
            '
            Dim Dividend As Long

            On Error GoTo UniIndex_Error

            If SaveUniIndex(Universes) = 0 Then
                If Universes = cTarget.EnumUni.uniMainTot Then
                    Dividend = UniSizeTot
                    If Dividend > 0 Then
                        SaveUniIndex(cTarget.EnumUni.uniMainTot) = UniSize / Dividend
                    Else
                        SaveUniIndex(cTarget.EnumUni.uniMainTot) = 0
                    End If
                ElseIf Universes = cTarget.EnumUni.uniMainSec Then
                    Dividend = UniSizeSec
                    If Dividend > 0 Then
                        SaveUniIndex(Universes) = Dividend / UniSize
                    Else
                        SaveUniIndex(Universes) = 0
                    End If
                ElseIf Universes = cTarget.EnumUni.uniSecTot Then
                    If UniSizeTot > 0 Then
                        SaveUniIndex(Universes) = UniSizeSec / UniSizeTot
                    Else
                        SaveUniIndex(Universes) = 0
                    End If
                ElseIf Universes = cTarget.EnumUni.uniMainCmp Then
                    If _main.MainTarget.Universe = mvarUniverse Then
                        Return 1
                    Else
                        If _main.MainTarget.Universe = "" Then
                            Dividend = UniSizeTot
                        Else
                            Dividend = _main.MainTarget.UniSize
                        End If
                        If Dividend > 0 Then
                            SaveUniIndex(Universes) = UniSize / Dividend
                        Else
                            SaveUniIndex(Universes) = 0
                        End If
                    End If
                End If
            End If
            UniIndex = SaveUniIndex(Universes)

            On Error GoTo 0
            Exit Function

UniIndex_Error:

            Err.Raise(Err.Number, "cTarget: UniIndex", Err.Description)

        End Function

        Public Property NoUniverseSize() As Boolean
            Get
                NoUniverseSize = mvarNoUniSize
            End Get
            Set(ByVal value As Boolean)
                mvarNoUniSize = value
            End Set
        End Property

        Public ReadOnly Property TargetNameNice() As String
            'make sure teh name is of correct formating
            'if you just add a number its automatically veiwed as all, therefor we att a A to the name
            'if there is a small letter in the beginning (for example a) it is made into upper case
            Get
                Dim Tmpstr As String

                If mvarTargetName = "" Then
                    Return ""
                Else
                    If Val(Left(mvarTargetName, 1)) <> 0 Then
                        Tmpstr = "A" & mvarTargetName
                    Else
                        Tmpstr = mvarTargetName
                    End If
                    Mid(Tmpstr, 1, 1) = UCase(Mid(Tmpstr, 1, 1))
                    Return Tmpstr
                End If
            End Get
        End Property

        Public Property SecondUniverse() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : SecondUniverse
            ' DateTime  : 2003-09-23 08:53
            ' Author    : joho
            ' Purpose   : Returns/sets the secondary Universe for Target
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo SecondUniverse_Error

                SecondUniverse = mvarSecondUniverse

                On Error GoTo 0
                Exit Property

SecondUniverse_Error:

                Err.Raise(Err.Number, "cTarget: SecondUniverse", Err.Description)
            End Get
            Set(ByVal value As String)
                Dim i As Integer

                On Error GoTo SecondUniverse_Error

                If value <> "" Then
                    i = _main.InternalAdedge.setUniverseUserDefined(value, "", true)
                    'If _main.UniStr Is Nothing Then _main.UniStr = ""
                    '_main.InternalAdedge.setUniverseUserDefined(_main.UniStr)
                    '        Kampanj.InternalAdedge.Run
                End If

                If i <> 2 And Universe <> "" Then
                    mvarSecondUniverse = ""
                Else
                    mvarSecondUniverse = value
                    If Not mvarNoUniSize Then
                        GetUniSizes()
                    End If
                End If

                On Error GoTo 0
                Exit Property

SecondUniverse_Error:

                Err.Raise(Err.Number, "cTarget: SecondUniverse", Err.Description)


            End Set
        End Property

        Public ReadOnly Property UniSizeSec() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : UniSizeSec
            ' DateTime  : 2003-09-23 08:54
            ' Author    : joho
            ' Purpose   : Returns the Universe size of the secondary Universe
            '---------------------------------------------------------------------------------------
            '
            Get
                Dim Test As Integer
                Dim HasBeenErrors As Boolean
                Dim TmpChan As cChannel
                Dim ChanStr As String

                On Error GoTo UniSizeSec_Error

                '    If mvarUniSizeSec = 0 Then
                '        SecondUniverse = mvarSecondUniverse
                '    End If

                On Error Resume Next
                Test = _main.TargColl(mvarTargetName, _main.InternalAdedge)
                If Err.Number <> 0 OrElse Test < 0 Then
                    UniSizeSec = 0
                    Exit Property
                End If
                On Error Resume Next
                'Test = _main.UniColl(mvarSecondUniverse, _main.InternalAdedge)
                If Err.Number <> 0 OrElse Test < 0 Then
                    UniSizeSec = 0
                    Exit Property
                End If
                On Error GoTo UniSizeSec_Error

                If mvarTargetName <> "" Then
                    UniSizeSec = _main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , _main.TimeShift, _main.TargColl(mvarTargetName, _main.InternalAdedge) - 1)
                Else
                    UniSizeSec = 0
                End If
                On Error GoTo 0
                Exit Property

UniSizeSec_Error:
                If Not HasBeenErrors Then
                    PrepareAdedge()
                    For Each TmpChan In _main.Channels
                        ChanStr = ChanStr & TmpChan.AdEdgeNames & ","
                    Next
                    HasBeenErrors = True
                    '_main.Adedge.setTargetMnemonic(_main.TargStr)
                    _main.InternalAdedge.clearTargetSelection()
                    For Each TmpTarget As Trinity.cTarget In _main.TargetCollection
                        Trinity.Helper.AddTarget(_main.InternalAdedge, TmpTarget)
                    Next
                    '_main.InternalAdedge.setUniverseUserDefined(_main.UniStr)
                    Trinity.Helper.AddTimeShift(_main.InternalAdedge)
                    _main.InternalAdedge.setArea(_main.Area)
                    _main.InternalAdedge.setChannelsArea(ChanStr, _main.Area)
                    _main.InternalAdedge.setPeriod("-1d")
                    _main.InternalAdedge.Run()
                    Resume
                End If

                Err.Raise(Err.Number, "cTarget: UniSizeSec", Err.Description)
            End Get

        End Property

        Private Function PrepareAdedge() As Boolean
            Dim Y As Integer
            'if we have no target name then we cant prepare Adedge
            If mvarTargetName = "" Then Exit Function
            PrepareAdedge = False

            'if the string contains ",TargetName,"
            If Not _main.TargetCollection.Contains(Me) Then
                If Trinity.Helper.AddTarget(_main.InternalAdedge, Me) Then
                    _main.TargetCollection.Add(Me)
                End If
                PrepareAdedge = True
            End If

            'if the string contains "Universe,"
            'If InStr(UCase(_main.UniStr), UCase(mvarUniverse) & ",") = 0 Then
            '    If Not _main.UniverseCollection.Contains("") Then
            '        _main.UniverseCollection.Add("")
            '        _main.UniverseCollection.Add(Nothing)
            '    End If
            '    If _main.UniStr = "" Then _main.UniStr = ","
            '    If Not _main.UniverseCollection.Contains(mvarUniverse) Then
            '        Y = _main.UniverseCollection.Count
            '        _main.UniverseCollection.Add(mvarUniverse)
            '        _main.UniStr = _main.UniStr + mvarUniverse + ","
            '        Y = Y + 1
            '    ElseIf InStr(UCase(_main.UniStr), UCase(mvarUniverse) & ",") = 0 Then
            '        _main.UniverseCollection.Clear()
            '        _main.UniverseCollection.Add("")
            '        _main.UniverseCollection.Add(Nothing)
            '        If _main.UniStr = "" Then _main.UniStr = ","
            '        Y = _main.UniverseCollection.Count
            '        _main.UniverseCollection.Add(mvarUniverse)
            '        _main.UniStr = _main.UniStr + mvarUniverse + ","
            '        Y = Y + 1
            '    End If
            '    _main.InternalAdedge.setUniverseUserDefined(_main.UniStr)
            '    PrepareAdedge = True
            'End If

            'if the string contains "'Universe,"
            'If InStr(UCase(_main.UniStr), UCase(mvarSecondUniverse) & ",") = 0 Then
            '    If Not _main.UniverseCollection.Contains("") Then
            '        _main.UniverseCollection.Add("")
            '        _main.UniverseCollection.Add(Nothing)
            '    End If
            '    If _main.UniStr = "" Then _main.UniStr = ","
            '    Y = _main.UniverseCollection.Count
            '    If Not _main.UniverseCollection.Contains(mvarSecondUniverse) Then
            '        _main.UniverseCollection.Add(mvarSecondUniverse)
            '        _main.UniStr = _main.UniStr + mvarSecondUniverse + ","
            '        Y = Y + 1
            '    ElseIf InStr(UCase(_main.UniStr), UCase(mvarSecondUniverse) & ",") = 0 Then
            '        _main.UniverseCollection.Clear()
            '        _main.UniverseCollection.Add("")
            '        _main.UniverseCollection.Add(Nothing)
            '        If _main.UniStr = "" Then _main.UniStr = ","
            '        Y = _main.UniverseCollection.Count
            '        _main.UniverseCollection.Add(mvarSecondUniverse)
            '        _main.UniStr = _main.UniStr + mvarSecondUniverse + ","
            '        Y = Y + 1
            '    End If
            '    _main.InternalAdedge.setUniverseUserDefined(_main.UniStr)
            '    PrepareAdedge = True
            'End If
            Trinity.Helper.AddTimeShift(_main.InternalAdedge)
        End Function

        Public Sub New(ByVal Main As cKampanj)
            'returns the main campaign
            MainObject = Main
            mvarTargetType = 0
            mvarUniverse = ""
            mvarTargetName = "3+"
            _main.RegisterProblemDetection(Me)
            AddHandler _main.TimeShiftChanged, AddressOf TimeShiftChanged
        End Sub

        Private Sub TimeShiftChanged(newTimeShift As cKampanj.TimeShiftEnum)
            'this event will trigger the delegate events when main,secondary or third target is changed
            RaiseEvent wasAltered()
        End Sub

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            'If mvarUniSize <> Me.GetUniSizes Then

            '    Dim _helpText As New Text.StringBuilder

            '    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Universe size is 0</p>")
            '    _helpText.AppendLine("<p>The target " & Me.TargetName & " in the booking type " & "</p>")
            '    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
            '    _helpText.AppendLine("<p>You can fix this by going into Edit Pricelist and clicking the button Get Universe from AdvantEdge on pricerows with this target.</p>")

            '    Dim _problem As New cProblem(ProblemsEnum.UniverseIsZero, cProblem.ProblemSeverityEnum.Error, "Universe is 0 or less", Me.TargetName, _helpText.ToString, Me)
            '    _problems.Add(_problem)

            'End If

            'If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

    End Class

End Namespace