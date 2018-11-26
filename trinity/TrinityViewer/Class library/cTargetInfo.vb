Namespace TrinityViewer
    Public Class cTargetInfo
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

        Private _targetType As TargetTypeEnum
        Private _universe As String
        Private _targetName As String
        Private _saveTarget As String
        Private _saveSize As Integer
        Private _secondUniverse As String
        Private Main As TrinityViewer.cCampaignInfo

        Public TargetGroup As String

        Private SaveUniIndex(0 To 3)

        Public Property SecondUniverse() As String
            Get
                Return _secondUniverse
            End Get
            Set(ByVal value As String)
                _secondUniverse = value
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
                Dim TmpChan As cChannelInfo
                Dim ChanStr As String

                On Error GoTo UniSizeSec_Error

                '    If _UniSizeSec = 0 Then
                '        SecondUniverse = _SecondUniverse
                '    End If

                On Error Resume Next
                Test = TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName)
                If Test < 0 Then
                    UniSizeSec = 0
                    Exit Property
                End If
                On Error Resume Next
                Test = TrinityViewer.Helper.UniverseIndex(Main.Adedge, _universe)
                If Test < 0 Then
                    UniSizeSec = 0
                    Exit Property
                End If
                On Error GoTo UniSizeSec_Error

                If _targetName <> "" Then
                    UniSizeSec = Main.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , TrinityViewer.Helper.UniverseIndex(Main.Adedge, _universe), TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName))
                Else
                    UniSizeSec = 0
                End If
                On Error GoTo 0
                Exit Property

UniSizeSec_Error:
                If Not HasBeenErrors Then
                    PrepareAdedge()
                    For Each TmpChan In Main.Channels
                        ChanStr = ChanStr & TmpChan.AdedgeNames & ","
                    Next
                    HasBeenErrors = True
                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
                    Main.Adedge.clearTargetSelection()
                    TrinityViewer.Helper.AddTargetsToAdedge(Main.Adedge)
                    Main.Adedge.setUniverseUserDefined(Main.UniStr)
                    Main.Adedge.setArea(Main.Area)
                    Main.Adedge.setChannels(ChanStr)
                    Main.Adedge.setPeriod("-1d")
                    Main.Adedge.Run()
                    Resume
                End If

                Err.Raise(Err.Number, "cTarget: UniSizeSec", Err.Description)
            End Get

        End Property

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
                If Universes = cTargetInfo.EnumUni.uniMainTot Then
                    Dividend = UniSizeTot
                    If Dividend > 0 Then
                        SaveUniIndex(cTargetInfo.EnumUni.uniMainTot) = UniSize / Dividend
                    Else
                        SaveUniIndex(cTargetInfo.EnumUni.uniMainTot) = 0
                    End If
                ElseIf Universes = cTargetInfo.EnumUni.uniMainSec Then
                    Dividend = UniSizeSec
                    If Dividend > 0 Then
                        SaveUniIndex(Universes) = Dividend / UniSize
                    Else
                        SaveUniIndex(Universes) = 0
                    End If
                ElseIf Universes = cTargetInfo.EnumUni.uniSecTot Then
                    If UniSizeTot > 0 Then
                        SaveUniIndex(Universes) = UniSizeSec / UniSizeTot
                    Else
                        SaveUniIndex(Universes) = 0
                    End If
                ElseIf Universes = cTargetInfo.EnumUni.uniMainCmp Then
                    If Main.MainTarget.Universe = _universe Then
                        Return 1
                    Else
                        If Main.MainTarget.Universe = "" Then
                            Dividend = UniSizeTot
                        Else
                            Dividend = Main.MainTarget.UniSize
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
                Dim TmpChan As cChannelInfo
                Dim ChanStr As String

                On Error GoTo UniSizeTot_Error

                If UniSize = 0 Then
                    Universe = _universe
                End If

                On Error Resume Next
                If TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName) < 0 And Main.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, Main.Adedge.getTargetCount - 1) > 0 Then 'The getUniSampleInfo will produce an error if InternalAdedge has not been run since the adding of targets
                    PrepareAdedge()
                    Main.Adedge.clearTargetSelection()
                    TrinityViewer.Helper.AddTargetsToAdedge(Main.Adedge)
                    If TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName) < 0 Then
                        UniSizeTot = 0
                        Exit Property
                    End If
                End If
                On Error GoTo UniSizeTot_Error
                '_TargetName can't be empty
                If _targetName <> "" Then
                    UniSizeTot = Main.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName))
                Else
                    UniSizeTot = 0
                End If
                On Error GoTo 0
                Exit Property

UniSizeTot_Error:
                If Not HasBeenErrors Then
                    PrepareAdedge()
                    For Each TmpChan In Main.Channels
                        ChanStr = ChanStr & TmpChan.AdedgeNames & ","
                    Next
                    HasBeenErrors = True
                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
                    Main.Adedge.clearTargetSelection()
                    TrinityViewer.Helper.AddTargetsToAdedge(Main.Adedge)
                    Main.Adedge.setUniverseUserDefined(Main.UniStr)
                    Main.Adedge.setArea(Main.Area)
                    Main.Adedge.setChannels(ChanStr)
                    Main.Adedge.setPeriod("-1d")
                    Main.Adedge.Run()
                    Resume
                End If

                Err.Raise(Err.Number, "cTarget: UniSizeTot", Err.Description)
            End Get

        End Property

        Public ReadOnly Property UniSize() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : UniSize
            ' DateTime  : 2003-07-08 15:06
            ' Author    : joho
            ' Purpose   : Returns the Universe size of the target.
            '---------------------------------------------------------------------------------------
            '
            Get
                Dim Test As Integer
                Dim HasBeenErrors As Boolean
                Dim TmpChan As cChannelInfo
                Dim ChanStr As String
                'Dim TargIdx As Integer
                'Dim i As Integer
                'Dim Target As String
                On Error GoTo UniSize_Error

                'Target = Trim(mvarTargetName)
                'TargIdx = -1
                'For i = 0 To Main.InternalAdedge.getTargetCount - 1
                '    If Main.InternalAdedge.getTargetTitle(i) = Target Then
                '        TargIdx = i
                '        Exit For
                '    End If
                '    If Main.InternalAdedge.getTargetTitle(i) = "A" & Target Then
                '        TargIdx = i
                '        Exit For
                '    End If
                'Next
                On Error Resume Next
                If TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName) < 0 Then
                    Universe = _universe
                End If
                If TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName) < 0 Then
                    UniSize = 0
                    Exit Property
                End If
                On Error Resume Next
                If TrinityViewer.Helper.UniverseIndex(Main.Adedge, _universe.ToString) < 0 Then
                    UniSize = 0
                    Exit Property
                End If
                On Error GoTo UniSize_Error

                If _targetName <> "" Then
                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
                    If _saveTarget <> _targetName Then
                        _saveSize = Main.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , TrinityViewer.Helper.UniverseIndex(Main.Adedge, _universe.ToString), TrinityViewer.Helper.TargetIndex(Main.Adedge, _targetName.ToString))
                        _saveTarget = _targetName
                    End If
                    UniSize = _saveSize
                    'Stop
                Else
                    UniSize = 0
                End If

                On Error GoTo 0
                Exit Property

UniSize_Error:
                If Not HasBeenErrors Then
                    PrepareAdedge()
                    Universe = _universe
                    For Each TmpChan In Main.Channels
                        ChanStr = ChanStr & TmpChan.AdedgeNames & ","
                    Next
                    HasBeenErrors = True
                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
                    TrinityViewer.Helper.AddTargetsToAdedge(Main.Adedge, True)
                    'Main.InternalAdedge.setTargetMnemonic(Main.TargStr)
                    Main.Adedge.setUniverseUserDefined(Main.UniStr)
                    Main.Adedge.setArea(Main.Area)
                    Main.Adedge.setChannels(ChanStr)
                    Main.Adedge.setPeriod("-1d")
                    Main.Adedge.Run()
                    Resume
                End If
                If Err.GetException.GetType.Name = "COMException" AndAlso Err.GetException.Message = "getUniSampleInfo: Must call Run before extraction of values" Then
                    Return 0
                End If
                Err.Raise(Err.Number, "cTarget: UniSize", Err.Description)
            End Get

        End Property

        Public Property Universe() As String
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
                Universe = _universe
                On Error GoTo 0
                Exit Property
Universe_Error:
                Err.Raise(Err.Number, "cTarget: Universe", Err.Description)
            End Get
            Set(ByVal value As String)
                Dim i As Integer
                On Error GoTo Universe_Error
                If value = "<National>" Then value = ""
                If value = _universe Then Exit Property
                _saveTarget = ""
                If value <> "" Then
                    TrinityViewer.Helper.WriteToLogFile("cTarget.Universe : Check if Universe(" & value & ") exists")
                    i = Main.Adedge.setUniverseUserDefined(value)
                    TrinityViewer.Helper.WriteToLogFile("cTarget.Universe : Reset Universes")
                    Main.Adedge.setUniverseUserDefined(Main.UniStr)
                    '        If Kampanj.InternalAdedge.Validate And 2 Then
                    '            Kampanj.InternalAdedge.setChannels Kampanj.Channels(1).AdEdgeNames
                    '        End If
                    '        Kampanj.InternalAdedge.Run
                End If

                If i <> 2 And value <> "" Then
                    _universe = ""
                Else
                    _universe = value
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
                TargetType = _targetType
                On Error GoTo 0
                Exit Property
TargetType_Error:
                Err.Raise(Err.Number, "cTarget: TargetType", Err.Description)
            End Get
            Set(ByVal value As TargetTypeEnum)
                On Error GoTo TargetType_Error
                _targetType = value
                On Error GoTo 0
                Exit Property
TargetType_Error:
                Err.Raise(Err.Number, "cTarget: TargetType", Err.Description)
            End Set
        End Property

        Public Property TargetName() As String
            'gets/sets and sets the targets name
            Get
                On Error GoTo TargetName_Error
                If _targetName = "" Then
                    If Not Main Is Nothing Then
                        _targetName = "3+"
                    End If
                End If
                Return _targetName
                On Error GoTo 0
                Exit Property
TargetName_Error:
                Err.Raise(Err.Number, "cTarget: TargetName", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo TargetName_Error

                If Right(value, 3) = "-99" Then
                    value = Left(value, Len(value) - 3) & "+"
                End If
                If value = "" Then
                    If Not Main Is Nothing Then
                        _targetName = "3+"
                    End If
                Else
                    _targetName = value
                End If
                'If Main.InternalAdedge.setTargetMnemonic(_TargetName) = 0 Then
                '    Err.Raise(13, Me.ToString, "Target " & _TargetName & " is illegal")
                'End If
                On Error GoTo 0
                Exit Property
TargetName_Error:
                Err.Raise(Err.Number, "cTarget: TargetName", Err.Description)
            End Set
        End Property

        Public Sub New(ByVal MainObject As TrinityViewer.cCampaignInfo)
            'returns the main campaign
            Main = MainObject
            _targetType = TargetTypeEnum.trgMnemonicTarget
            _universe = ""
            _targetName = "3+"
        End Sub

        Public ReadOnly Property TargetNameNice() As String
            'make sure teh name is of correct formating
            'if you just add a number its automatically veiwed as all, therefor we att a A to the name
            'if there is a small letter in the beginning (for example a) it is made into upper case
            Get
                Dim Tmpstr As String

                If _targetName = "" Then
                    Return ""
                Else
                    If Val(Left(_targetName, 1)) <> 0 Then
                        Tmpstr = "A" & _targetName
                    Else
                        Tmpstr = _targetName
                    End If
                    Mid(Tmpstr, 1, 1) = UCase(Mid(Tmpstr, 1, 1))
                    Return Tmpstr
                End If
            End Get
        End Property

        Private Function PrepareAdedge() As Boolean
            'if we have no target name then we cant prepare Adedge
            If _targetName = "" Then Exit Function
            PrepareAdedge = False

            'if the string contains ",TargetName,"
            If TrinityViewer.Helper.TargetIndex(Main.Adedge, Me.TargetName) = 0 Then
                TrinityViewer.Helper.AddTarget(Main.Adedge, Me)
                PrepareAdedge = True
            End If

            'if the string contains "Universe,"
            If InStr(UCase(Main.UniStr), UCase(_universe) & ",") = 0 Then
                If Helper.UniverseIndex(Main.Adedge, _universe) < 0 Then
                    Main.Adedge.setUniverseUserDefined(Main.UniStr & "," & _universe)
                End If
                Main.Adedge.setUniverseUserDefined(Main.UniStr)
                PrepareAdedge = True
            End If

            'if the string contains "'Universe,"
            If InStr(UCase(Main.UniStr), UCase(_SecondUniverse) & ",") = 0 Then
                If Helper.UniverseIndex(Main.Adedge, _secondUniverse) < 0 Then
                    Main.Adedge.setUniverseUserDefined(Main.UniStr & "," & _secondUniverse)
                End If
                Main.Adedge.setUniverseUserDefined(Main.UniStr)
                PrepareAdedge = True
            End If
        End Function

    End Class
End Namespace