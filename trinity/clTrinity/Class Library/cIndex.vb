Namespace Trinity
    Public Class cIndex
        Implements IDetectsProblems

        'the index class contains:
        'IndexOn, a numeric value for eNetCCP, eGrossCPP or cTRP
        'Name, a string containing the name
        'FromDate, a Date type for tha starting date
        'ToDate, a Date type for then End date
        'Index, the index of CPP/TRP
        'SystemGenerated, sets if the index is set by the user (false) or made by the system (true)
        'New, sets the starting variables 

        Public Enum IndexOnEnum
            eNetCPP = 0
            eGrossCPP = 1
            eTRP = 2
            eNetCPPAfterMaxDiscount = 3
            eFixedCPP = 4
        End Enum

        Public Enum ProblemsEnum
            SuspiciousIndex = 1
            IndexNotActive = 2
        End Enum

        Private mvarID As String = ""
        Private mvarIndexOn As IndexOnEnum
        Private mvarName As String
        Private mvarFromDate As Date
        Private mvarToDate As Date
        Private mvarIndex(0 To 6) As Single
        Private mvarFixedCPP As Single
        Private mvarChannel As cChannel
        Private mvarSystemGenerated As Boolean
        Private Main As cKampanj
        Private Parent As Object
        Private ParentColl As Collection
        Private mvarEnhancements As New Trinity.cEnhancements(Me)
        Private mvarUseThis As Boolean = True


        Public Property FixedCPP() As Single
            Get
                Return mvarFixedCPP
            End Get
            Set(ByVal value As Single)
                mvarFixedCPP = value
            End Set
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error


            colXml.SetAttribute("ID", Me.ID)
            colXml.SetAttribute("Name", Me.Name)

            If Me.Enhancements.Count = 0 Then
                For i As Integer = 0 To Main.Dayparts.Count - 1
                    colXml.SetAttribute("IndexDP" & i, Me.Index(i))
                Next
            Else
                Dim XMLEnhancements As Xml.XmlElement
                XMLEnhancements = xmlDoc.CreateElement("Enhancements")
                Enhancements.GetXML(XMLEnhancements, errorMessege, xmlDoc)
                colXml.AppendChild(XMLEnhancements)
            End If

            colXml.SetAttribute("IndexOn", Me.IndexOn)
            colXml.SetAttribute("SystemGenerated", Me.SystemGenerated)
            colXml.SetAttribute("FromDate", Me.FromDate)
            colXml.SetAttribute("ToDate", Me.ToDate)
            colXml.SetAttribute("UseThis", Me.UseThis)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving Index (" & Me.ID & ")")
            Return False
        End Function

        Public Property UseThis() As Boolean
            Get
                Return mvarUseThis
            End Get
            Set(ByVal value As Boolean)
                mvarUseThis = value
            End Set
        End Property

        Public Property ID() As String
            Get
                Return mvarID
            End Get
            Set(ByVal value As String)
                If ParentColl.Contains(mvarID) Then
                    ParentColl.Remove(mvarID)
                End If
                mvarID = value
                ParentColl.Add(Me, mvarID)
            End Set
        End Property

        Public Property Enhancements() As Trinity.cEnhancements
            Get
                Return mvarEnhancements
            End Get
            Set(ByVal value As Trinity.cEnhancements)
                mvarEnhancements = value
            End Set
        End Property

        Public Property IndexOn() As IndexOnEnum
            Get
                If mvarEnhancements.Count = 0 Then
                    Return mvarIndexOn
                Else
                    Return IndexOnEnum.eTRP
                End If
            End Get
            Set(ByVal value As IndexOnEnum)
                mvarIndexOn = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Name = mvarName
            End Get
            Set(ByVal value As String)
                mvarName = value
            End Set
        End Property

        Public Property FromDate() As Date
            Get
                FromDate = mvarFromDate
            End Get
            Set(ByVal value As Date)
                mvarFromDate = Date.FromOADate(Math.Floor(value.ToOADate))
            End Set
        End Property

        Public Property ToDate() As Date
            Get
                ToDate = mvarToDate
            End Get
            Set(ByVal value As Date)
                mvarToDate = Date.FromOADate(Math.Floor(value.ToOADate))
            End Set
        End Property

        Public Property Index(Optional ByVal Daypart As Integer = -1) As Single
            Get
                If mvarEnhancements.Count = 0 Then
                    If Daypart = -1 OrElse mvarIndex(Daypart) = 0 Then
                        Return mvarIndex(0)
                    Else
                        Return mvarIndex(Daypart)
                    End If
                Else
                    Dim TmpIdx As Decimal = 0
                    For Each TmpEnh As Trinity.cEnhancement In mvarEnhancements
                        TmpIdx += TmpEnh.Amount
                    Next
                    'TmpIdx = TmpIdx / (1 + TmpIdx) / mvarEnhancements.SpecificFactor
                    'Return (1 / (1 - TmpIdx)) * 100
                    Return (100 + TmpIdx)
                End If
            End Get
            Set(ByVal value As Single)
                If Not (value > 0 And value < 500) Then
                    Debug.WriteLine("Tried to set an index to " & value & ". The index name is " & Me.Name & ". Setting it to 100 instead.")
                    value = 100
                    'Exit Property
                End If
                If mvarEnhancements.Count = 0 Then
                    If Daypart = -1 Then
                        For i As Integer = 0 To Main.Dayparts.Count - 1
                            mvarIndex(i) = value
                        Next
                    Else
                        mvarIndex(Daypart) = value
                    End If
                End If
            End Set
        End Property

        '*** BEFORE GROSSINDEX REMOVAL
        'Public Property Index(Optional ByVal Daypart As Integer = -1) As Single
        '    Get
        '        If Daypart = -1 OrElse mvarIndex(Daypart) = 0 Then
        '            Return mvarIndex(0)
        '        Else
        '            Return mvarIndex(Daypart)
        '        End If
        '    End Get
        '    Set(ByVal value As Single)
        '        If Daypart = -1 Then
        '            For i As Integer = 0 To Main.DaypartCount
        '                mvarIndex(i) = value
        '            Next
        '        Else
        '            mvarIndex(Daypart) = value
        '        End If
        '    End Set
        'End Property

        Public Property SystemGenerated() As Boolean
            Get
                SystemGenerated = mvarSystemGenerated
            End Get
            Set(ByVal value As Boolean)
                mvarSystemGenerated = value
            End Set
        End Property

        Public Sub New(ByVal MainObject As cKampanj, ByVal ParentObj As Trinity.cBookingType, ByVal ParentC As Collection)
            mvarSystemGenerated = False
            Main = MainObject
            Parent = ParentObj
            ParentColl = ParentC
            If Main Is Nothing Then
                Throw New Exception("There is no main object.")
            End If
            Main.RegisterProblemDetection(Me)

        End Sub

        Public Sub New(ByVal MainObject As cKampanj, ByVal ParentC As Collection)
            mvarSystemGenerated = False
            Main = MainObject
            ParentColl = ParentC
            Main.RegisterProblemDetection(Me)

        End Sub

        Public Sub New(ByVal MainObject As cKampanj, ByVal ParentObj As Trinity.cPricelistTarget, ByVal ParentC As Collection)
            mvarSystemGenerated = False
            Main = MainObject
            Parent = ParentObj
            ParentColl = ParentC
            Main.RegisterProblemDetection(Me)

        End Sub

        Friend ReadOnly Property ParentObject() As Object
            Get
                Return Parent
            End Get
        End Property

        Protected Overrides Sub Finalize()
            mvarChannel = Nothing
            MyBase.Finalize()
        End Sub

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems
            Dim _problems As New List(Of cProblem)
            Dim ParentBT As Object

            If ParentObject IsNot Nothing Then
                Try

                    If ParentObject.GetType.FullName = "clTrinity.Trinity.cPricelistTarget" Then
                        ParentBT = DirectCast(ParentObject, cPricelistTarget).Bookingtype
                    Else
                        ParentBT = DirectCast(ParentObject, Trinity.cBookingType)
                    End If

                    If ParentBT IsNot Nothing AndAlso ParentBT.BookIt Then
                        If Me.UseThis Then

                            For DP As Integer = 0 To ParentBT.Dayparts.Count - 1
                                If Me.Index(DP) < 10 Or Me.Index(DP) > 200 Then
                                    Dim _helpText As New System.Text.StringBuilder

                                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Index may be wrong</p>")
                                    _helpText.AppendLine("<p>The index " & Me.Name & " in booking type " & ParentBT.ToString & _
                                                         " is" & Me.Index(DP) & " - this may be wrong.</p>")
                                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                                    _helpText.AppendLine("<p>This index may have been set in either Contracts or in the price list.</p>")

                                    Dim _problem As New cProblem(ProblemsEnum.SuspiciousIndex, cProblem.ProblemSeverityEnum.Warning, "Suspicious index", ParentBT.ToString & " index " & Me.Name, _helpText.ToString, Me)
                                    _problems.Add(_problem)
                                End If
                            Next
                        Else
                            If Me.FromDate <= Date.FromOADate(Main.EndDate) And Me.ToDate >= Date.FromOADate(Main.StartDate) Then
                                Dim _helpText As New System.Text.StringBuilder

                                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Index not active</p>")
                                _helpText.AppendLine("<p>The index with name " & Me.Name & " in booking type " & ParentBT.ToString & _
                                                     " covers the campaign period but is not activated.</p>")
                                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                                _helpText.AppendLine("<p>If this index is something you want counted towards your campaign, click on the green icon to the right or activate it in Setup ('Use') </p>")

                                Dim _problem As New IndexNotActiveProblem(ProblemsEnum.IndexNotActive, cProblem.ProblemSeverityEnum.Warning, "Index not active", ParentBT.ToString & " index " & Me.Name, _helpText.ToString, Me, , True)
                                _problems.Add(_problem)
                            End If
                        End If
                    End If

                    If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
                    Return _problems
                Catch ex As Exception
                    Return New List(Of cProblem)
                End Try
            End If
            Return New List(Of cProblem)
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound
    End Class

    Public Class IndexNotActiveProblem
        Inherits cProblem

        Dim Index As cIndex

        Public Overrides Function FixMe() As Boolean

            Try
                Index.UseThis = True
                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function

        Public Sub New(ByVal ProblemID As Integer, ByVal Severity As ProblemSeverityEnum, ByVal Message As String, ByVal Source As String, ByVal HelpText As String, ByVal SourceObject As IDetectsProblems, Optional ByVal SourceString As String = "", Optional ByVal AutoFixable As Boolean = False)
            MyBase.New(ProblemID, Severity, Message, Source, HelpText, SourceObject, , AutoFixable)
            Index = DirectCast(SourceObject, cIndex)
        End Sub

    End Class
End Namespace