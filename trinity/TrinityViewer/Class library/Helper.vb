Imports System.Xml
Imports Microsoft
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D

Namespace TrinityViewer

    Public Class Helper
        Const MaxDistanceIfSame = 60
        Const MaxDistanceIfNotSame = 15
        Const StrLength = 5

        Shared XmlLog As Xml.XmlDocument

        Shared Main As TrinityViewer.cCampaignInfo

        Shared Property MainObject() As TrinityViewer.cCampaignInfo
            Get
                Return Main
            End Get
            Set(ByVal value As TrinityViewer.cCampaignInfo)
                Main = value
            End Set
        End Property

        Shared Sub WriteToLogFile(ByVal Str As String)
            If LoggingIsOn Then
                Dim TmpNode As XmlElement
                If XmlLog Is Nothing Then
                    XmlLog = New Xml.XmlDocument
                    XmlLog.AppendChild(XmlLog.CreateElement("Log"))
                End If
                TmpNode = XmlLog.CreateElement("Entry")
                TmpNode.SetAttribute("DateTime", Now)
                TmpNode.AppendChild(XmlLog.CreateTextNode(Str))
                XmlLog.ChildNodes.Item(0).AppendChild(TmpNode)
            End If
        End Sub

        Shared Sub SaveLog()
            If LoggingIsOn Then
                XmlLog.Save("C:\debug.xml")
            End If
        End Sub

        Shared Function Pathify(ByVal Path As String) As String
            If Right(Path, 1) = "\" Then
                Pathify = Path
            Else
                Pathify = Path & "\"
            End If
        End Function

        Shared Function Mam2Tid(ByVal MaM As Integer) As String
            Dim h As Integer
            Dim m As Integer
            Dim Tmpstr As String

            h = MaM \ 60
            m = MaM Mod 60

            Tmpstr = Trim(h) & ":" & Format(m, "00")
            While Len(Tmpstr) < 5
                Tmpstr = "0" & Tmpstr
            End While
            Mam2Tid = Tmpstr
        End Function

        Shared Function CreateString(ByVal Chars As Integer, ByVal ascii As Byte) As String
            Dim i As Integer
            Dim TmpStr As String

            TmpStr = ""
            For i = 1 To Chars
                TmpStr = TmpStr + Chr(ascii)
            Next
            Return TmpStr

        End Function

        Shared Function GetDaypart(ByVal MaM As Integer) As Integer
            Dim i As Integer

            For i = 0 To Main.DaypartCount - 1
                If MaM >= Main.DaypartStart(i) Then
                    If MaM <= Main.DaypartEnd(i) Then
                        Return i
                        Exit For
                    End If
                End If
            Next
        End Function

        '*******************************
        'Replaced by AddTargetsToAdedge
        '*******************************
        'Shared Function CreateTargetString(Optional ByVal IncludeBuyingTargets As Boolean = True, Optional ByVal IncludeProfile As Boolean = True) As String
        '    Dim Tmpstr As String
        '    Dim TmpChan As cChannel
        '    Dim TmpBT As cBookingType
        '    Dim TmpSex As String

        '    Tmpstr = Main.MainTarget.TargetName & "," & Main.SecondaryTarget.TargetName & "," & Main.ThirdTarget.TargetName & "," & Main.AllAdults & ","
        '    If IncludeBuyingTargets Then
        '        For Each TmpChan In Main.Channels
        '            For Each TmpBT In TmpChan.BookingTypes
        '                If TmpBT.BookIt Then
        '                    Tmpstr = Tmpstr & TmpBT.BuyingTarget.Target.TargetName & ","
        '                End If
        '            Next
        '        Next
        '    End If
        '    If IncludeProfile Then
        '        Select Case Left(Main.MainTarget.TargetNameNice, 1)
        '            Case "A" : TmpSex = ""
        '            Case "M" : TmpSex = "M"
        '            Case "W" : TmpSex = "W"
        '            Case Else : TmpSex = ""
        '        End Select
        '        Tmpstr = Tmpstr & TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "60-69," & TmpSex & "70-99,"
        '    End If
        '    Return Tmpstr
        'End Function

        'Method to add set all relevant targets in an Adedge.Breaks object
        Shared Sub AddTargetsToAdedge(ByVal Adedge As ConnectWrapper.Breaks, Optional ByVal IncludeBuyingTargets As Boolean = True, Optional ByVal IncludeProfile As Boolean = True)
            Adedge.clearTargetSelection()
            AddTarget(Adedge, Main.MainTarget)
            AddTarget(Adedge, Main.SecondaryTarget)
            AddTarget(Adedge, Main.ThirdTarget)
            AddTarget(Adedge, Main.CustomTarget)

            Adedge.setTargetMnemonic(Main.AllAdults, True)

            If IncludeBuyingTargets Then
                For Each TmpChan As cChannelInfo In Main.Channels
                    For Each TmpBT As cBookingTypeInfo In TmpChan.BookingTypes
                        If TmpBT.BookIt Then
                            AddTarget(Adedge, TmpBT.BuyingTarget)
                        End If
                    Next
                Next
            End If
            If IncludeProfile Then
                Dim TmpSex As String
                Select Case Left(Main.MainTarget.TargetNameNice, 1)
                    Case "A" : TmpSex = ""
                    Case "M" : TmpSex = "M"
                    Case "W" : TmpSex = "W"
                    Case Else : TmpSex = ""
                End Select
                Adedge.setTargetMnemonic(TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "60-69," & TmpSex & "70-99,", True)
            End If

        End Sub

        Shared Function AddTarget(ByVal Adedge As ConnectWrapper.Breaks, ByVal Target As cTargetInfo, Optional ByVal Unique As Boolean = True) As Boolean
            If Target.TargetType = cTargetInfo.TargetTypeEnum.trgMnemonicTarget Then
                Return (Adedge.setTargetMnemonic(Target.TargetName, Unique) = 1)
            ElseIf Target.TargetType = cTargetInfo.TargetTypeEnum.trgUserTarget Then
                Return (Adedge.setTargetUserDefined(Target.TargetGroup, Target.TargetName, Unique) = 1)
            End If
        End Function

        Shared Sub AddTargetsToAdedge(ByVal Adedge As ConnectWrapper.Brands, Optional ByVal IncludeBuyingTargets As Boolean = True, Optional ByVal IncludeProfile As Boolean = True)
            Adedge.clearTargetSelection()
            AddTarget(Adedge, Main.MainTarget)
            AddTarget(Adedge, Main.SecondaryTarget)
            AddTarget(Adedge, Main.ThirdTarget)
            AddTarget(Adedge, Main.CustomTarget)

            Main.Adedge.setTargetMnemonic(Main.AllAdults, True)

            If IncludeBuyingTargets Then
                For Each TmpChan As cChannelInfo In Main.Channels
                    For Each TmpBT As cBookingTypeInfo In TmpChan.BookingTypes
                        If TmpBT.BookIt Then
                            AddTarget(Adedge, TmpBT.BuyingTarget)
                        End If
                    Next
                Next
            End If
            If IncludeProfile Then
                Dim TmpSex As String
                Select Case Left(Main.MainTarget.TargetNameNice, 1)
                    Case "A" : TmpSex = ""
                    Case "M" : TmpSex = "M"
                    Case "W" : TmpSex = "W"
                    Case Else : TmpSex = ""
                End Select
                Main.Adedge.setTargetMnemonic(TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "60-69," & TmpSex & "70-99,", True)
            End If
        End Sub

        Shared Function AddTarget(ByVal Adedge As ConnectWrapper.Brands, ByVal Target As cTargetInfo, Optional ByVal Unique As Boolean = True) As Boolean
            If Target.TargetType = cTargetInfo.TargetTypeEnum.trgMnemonicTarget Then
                Adedge.setTargetMnemonic(Target.TargetName, Unique)
            ElseIf Target.TargetType = cTargetInfo.TargetTypeEnum.trgUserTarget Then
                Adedge.setTargetUserDefined(Target.TargetGroup, Target.TargetName, Unique)
            End If
            Return True
        End Function

        Declare Function SHGetSpecialFolderLocation Lib "Shell32.dll" (ByVal hwndOwner As Integer, ByVal nFolder As Integer, ByRef pidl As ITEMIDLIST) As Integer

        Declare Function SHGetPathFromIDList Lib "Shell32.dll" Alias "SHGetPathFromIDListA" (ByVal pidl As Integer, ByVal pszPath As String) As Integer

        Public Structure SH_ITEMID
            Dim cb As Integer
            Dim abID As Byte
        End Structure

        Public Structure ITEMIDLIST
            Dim mkid As SH_ITEMID
        End Structure

        Public Const MAX_PATH As Short = 260

        Public Enum CSIDLEnum
            StartMenuPrograms = 2 'C:\Documents and Settings\<USERNAME>\Start Menu\Programs
            MyDocuments = 5 'C:\Documents and Settings\<USERNAME>\My Documents
            Favorites = 6 'C:\Documents and Settings\<USERNAME>\Favorites
            Startup = 7 'C:\Documents and Settings\<USERNAME>\Start Menu\Programs\Startup
            Recent = 8 'C:\Documents and Settings\<USERNAME>\Recent
            SendTo = 9 'C:\Documents and Settings\<USERNAME>\SendTo
            StartMenu = 11 'C:\Documents and Settings\<USERNAME>\Start Menu
            Desktop = 16 'C:\Documents and Settings\<USERNAME>\Desktop
            NetHood = 19 'C:\Documents and Settings\<USERNAME>\NetHood
            Fonts = 20 'C:\WINNT\Fonts
            Templates = 21 'C:\Documents and Settings\<USERNAME>\Templates
            StartMenuAllUsers = 22 'C:\Documents and Settings\All Users\Start Menu
            StartMenuProgramsAllUsers = 23 'C:\Documents and Settings\All Users\Start Menu\Programs
            StartupAllUsers = 24 'C:\Documents and Settings\All Users\Start Menu\Programs\Startup
            DesktopAllUsers = 25 'C:\Documents and Settings\All Users\Desktop
            ApplicationData = 26 'C:\Documents and Settings\<USERNAME>\Application Data
            ProntHood = 27 'C:\Documents and Settings\<USERNAME>\PrintHood
            LocalApplicationData = 28 'C:\Documents and Settings\<USERNAME>\Local Settings\Application Data
            FavoritesAllUsers = 31 'C:\Documents and Settings\All Users\Favorites
            TemporaryInternetFiles = 32 'C:\Documents and Settings\<USERNAME>\Local Settings\Temporary Internet Files
            Cookies = 33 'C:\Documents and Settings\<USERNAME>\Cookies
            History = 34 'C:\Documents and Settings\<USERNAME>\Local Settings\History
            ApplicationDataAllUsers = 35 'C:\Documents and Settings\All Users\Application Data
            WinNT = 36 'C:\WINNT
            System32 = 37 'C:\WINNT\system32
            ProgramFiles = 38 'C:\Program Files
            MyPictures = 39 'C:\Data\My Pictures
            UserProfile = 40 'C:\Documents and Settings\<USERNAME>
            'System32 = 41            'C:\WINNT\system32
            CommonFiles = 43 'C:\Program Files\Common Files
            TemplatesAllUsers = 45 'C:\Documents and Settings\All Users\Templates
            DocumentsAllUsers = 46 'C:\Documents and Settings\All Users\Documents
            AdministrativeToolsAllUsers = 47 'C:\Documents and Settings\All Users\Start Menu\Programs\Administrative Tools
            AdministrativeTools = 48 'C:\Documents and Settings\<USERNAME>\Start Menu\Programs\Administrative Tools
            Temp = 49
        End Enum

        Shared Function MondayOfWeek(ByVal year, ByVal week) As Date
            Dim TmpDate As Date

            TmpDate = CDate(year.ToString & "-06-01")
            TmpDate = TmpDate.AddDays((week - DatePart(DateInterval.WeekOfYear, TmpDate, VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)) * 7)
            TmpDate = TmpDate.AddDays(-Weekday(TmpDate, vbMonday) + 1)
            MondayOfWeek = TmpDate

        End Function

        Shared Function DateTimeSerial(ByVal d As Date, ByVal MaM As Integer) As Double
            Dim h As Integer
            Dim m As Integer
            Dim Tmpstr As String

            h = MaM \ 60
            m = MaM Mod 60

            While h >= 24
                h = h - 24
                d = d.AddDays(1)
            End While
            Tmpstr = Trim(h) & ":" & Format(m, "00")

            Return CDate(Format(d, "Short Date") & " " & Tmpstr & ":00").ToOADate

        End Function

        Shared Function TargetIndex(ByVal Adedge As Object, ByVal Target As String) As Integer
            Dim i As Integer

            If Val(Left(Target, 1)) > 0 Then Target = "A" & Target
            If InStr(Adedge.GetType.Name, "Breaks") > 0 Then
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Breaks).getTargetCount - 1
                    If DirectCast(Adedge, ConnectWrapper.Breaks).getTargetTitle(i) = Target Then
                        TargetIndex = i
                        Exit Function
                    End If
                Next
            Else
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Brands).getTargetCount - 1
                    If DirectCast(Adedge, ConnectWrapper.Brands).getTargetTitle(i) = Target Then
                        TargetIndex = i
                        Exit Function
                    End If
                Next
            End If
            Return -1
        End Function

        Shared Function UniverseIndex(ByVal Adedge As Object, ByVal Universe As String) As Integer
            Dim i As Integer
            If Universe = "" Then Return 0
            If InStr(Adedge.GetType.Name, "Breaks") > 0 Then
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Breaks).getUniverseCount
                    If DirectCast(Adedge, ConnectWrapper.Breaks).getUniverseTitle(i) = Universe Then
                        Return i
                    End If
                Next
            Else
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Brands).getUniverseCount
                    If UCase(DirectCast(Adedge, ConnectWrapper.Brands).getUniverseTitle(i)) = UCase(Universe) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        End Function

        Private Shared Function BetterBreak(ByVal FirstBreak As Integer, ByVal SecondBreak As Integer, ByVal MaM As Integer, ByVal ProgAfter As String, ByRef Breaks As Collections.ArrayList) As Boolean
            Dim OrigDist As Integer
            Dim NewDist As Integer
            Dim OrigSameProg As Boolean
            Dim NewSameProg As Boolean

            On Error GoTo BetterBreak_Error

            If FirstBreak < 0 OrElse SecondBreak < 0 Then
                Exit Function
            End If
            If Breaks(FirstBreak).MaM > Breaks(SecondBreak).MaM Then
                Return True
                Exit Function
            End If
            OrigDist = Math.Abs(MaM - Breaks(FirstBreak).MaM)
            NewDist = Math.Abs(MaM - Breaks(SecondBreak).MaM)

            'If ProgramHit(TmpSpot.ProgBefore, Break.getAttribGroup(aBreakProgBefore, FirstBreak)) Then
            If ProgramHit(ProgAfter, Breaks(FirstBreak).ProgAfter) Then
                OrigSameProg = True
            End If
            'End If
            'If ProgramHit(TmpSpot.ProgBefore, Break.getAttribGroup(aBreakProgBefore, SecondBreak)) Then
            If ProgramHit(ProgAfter, Breaks(SecondBreak).ProgAfter) Then
                NewSameProg = True
            End If
            'End If

            If OrigSameProg AndAlso Not NewSameProg Then
                If OrigDist <= MaxDistanceIfSame Then
                    Return False
                ElseIf NewDist <= MaxDistanceIfNotSame Then
                    Return True
                ElseIf OrigDist < NewDist Then
                    Return False
                Else
                    Return True
                End If
            ElseIf NewSameProg AndAlso Not OrigSameProg Then
                If NewDist < MaxDistanceIfSame Then
                    Return True
                ElseIf OrigDist <= MaxDistanceIfNotSame Then
                    Return False
                ElseIf NewDist < OrigDist Then
                    Return True
                Else
                    Return False
                End If
            ElseIf NewSameProg AndAlso OrigSameProg Then
                If NewDist <= OrigDist Then
                    Return True
                Else
                    Return False
                End If
            Else
                If NewDist <= OrigDist Then
                    Return True
                Else
                    Return False
                End If
            End If

            On Error GoTo 0
            Exit Function

BetterBreak_Error:

            'Err.Raise Err.Number, "frmSchedule: BetterBreak", Err.Description
            MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")
            Exit Function
            Resume
        End Function


        Private Shared Function ProgramHit(ByVal FirstProg As String, ByVal SecondProg As String) As Boolean
            On Error GoTo ProgramHit_Error

            FirstProg = UCase(FirstProg)
            SecondProg = UCase(SecondProg)
            If FirstProg = "" Then
                Return True
            Else
                If InStr(FirstProg, Left(SecondProg, StrLength)) > 0 Then
                    Return True
                End If
                If InStr(SecondProg, Left(FirstProg, StrLength)) > 0 Then
                    Return True
                End If
            End If

            On Error GoTo 0
            Exit Function

ProgramHit_Error:

            'Err.Raise Err.Number, "frmSchedule: ProgramHit", Err.Description
            MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")


        End Function


        Shared Function Estimate(ByVal EstDate As Date, ByVal EstTime As Integer, ByVal NameAfter As String, ByVal Channel As String, ByRef Breaks As Collections.ArrayList) As Collections.ArrayList

            Dim b As Long
            Dim LastDate As Date
            Dim TmpBreakList As New Collections.ArrayList

            On Error GoTo Estimate_Error

            '    If Datum = "" Then
            '        TmpSpot.AirDate = 0
            '    Else
            '        TmpSpot.AirDate = Datum
            '    End If
            '    While Len(Tid) < 4
            '        Tid = "0" & Tid
            '    Wend
            '    TmpSpot.MaM = Left(Tid, 2) * 60 + Right(Tid, 2)
            '    TmpSpot.Estimation = EstNotOk
            '    TmpSpot.ProgBefore = NameBefore
            '    TmpSpot.ProgAfter = NameAfter
            For b = 1 To Breaks.Count - 1 'For b = 2 To Breaks.Count
                If Weekday(Breaks(b).AirDate, vbMonday) = Weekday(EstDate, vbMonday) OrElse EstDate.ToOADate = 0 Then
                    If Breaks(b).Channel.ChannelName = Channel Then
                        While BetterBreak(b - 1, b, EstTime, NameAfter, Breaks) AndAlso b > -1
                            b += 1
                            If b > Breaks.Count - 1 Then
                                b = -1
                            End If
                        End While
                        b = b - 1
                        If b > -1 Then
                            TmpBreakList.Add(Breaks(b))
                            If Breaks(b).AirDate = Breaks(Breaks.Count - 1).AirDate Then
                                Exit For
                            End If
                            LastDate = Breaks(b).AirDate
                            While Breaks(b).AirDate = LastDate
                                If b + 100 < Breaks.Count - 1 Then
                                    b += 100 'Steps of 200 to make the code faster
                                Else
                                    b += 1
                                End If
                            End While
                            While Breaks(b).airdate <> LastDate
                                b -= 1
                            End While
                            b += 1
                        Else
                            Exit For
                        End If
                    End If
                End If
            Next
            Return TmpBreakList

            On Error GoTo 0
            Exit Function

Estimate_Error:

            'Err.Raise Err.Number, "frmSchedule: Estimate", Err.Description
            MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")
            Exit Function
            Resume

        End Function

        Shared Function Adedge2Channel(ByVal AdedgeChannel As String) As cChannelInfo
            Dim TmpChan As cChannelInfo

            For Each TmpChan In Main.Channels
                If InStr(UCase(TmpChan.AdEdgeNames.Trim) & ",", UCase(AdedgeChannel) & ",") > 0 Then
                    Return TmpChan
                    Exit For
                End If
            Next
            Return Nothing
        End Function

        Public Sub New(ByVal mainObject As TrinityViewer.cCampaignInfo)
            Main = mainObject
        End Sub
    End Class

End Namespace