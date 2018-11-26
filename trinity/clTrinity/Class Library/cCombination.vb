Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity

    Public Class cCombination
        Implements IDetectsProblems

        Public Enum CombinationOnEnum
            coBudget = 0
            coTRP = 1
        End Enum

        Public Enum IndexStatusEnum
            Unknown
            Unchanged
            NaturalDelivery
            EnteredByUser
        End Enum

        Public Name As String
        Public CombinationOn As CombinationOnEnum = CombinationOnEnum.coBudget
        Private mvarRelations As cCombinationChannels
        Private mvarID As String = Guid.NewGuid.ToString
        Private _MarathonIDCombination As String
        Private _sendAsOneUnitToMarathon As Boolean = False
        Private Parent As Collection
        Private mvarManualIndexes = False
        Private mvarIndexMainTargetStatus As IndexStatusEnum = IndexStatusEnum.Unchanged
        Private mvarIndexSecondTargetStatus As IndexStatusEnum = IndexStatusEnum.Unchanged
        Private mvarIndexAllAdultsStatus As IndexStatusEnum = IndexStatusEnum.Unchanged
        Private Main As cKampanj

        Property PrintAsOne As Boolean = False

        Public ReadOnly Property HasRelations() As Boolean
            Get
                Dim totRelations As Integer = 0
                For Each TmpCC As cCombinationChannel In Relations
                    totRelations += TmpCC.Relation
                Next
                If totRelations = 0 Then
                    Return False
                End If
                Return True
            End Get

        End Property

        Public ReadOnly Property IsOnlyCompensation As Boolean
            Get
                For Each TmpCC As cCombinationChannel In Relations
                    If Not TmpCC.Bookingtype.IsCompensation Then
                        Return False
                    End If
                Next
                Return True
            End Get
        End Property
        Public Property IndexMainTarget() As Double
            Get
                Dim _index As Single = 0
                For Each cc As Trinity.cCombinationChannel In Relations
                    If Not cc.Bookingtype.IsCompensation OrElse IsOnlyCompensation Then
                        _index += cc.Bookingtype.IndexMainTarget * cc.Percent
                    End If
                Next
                Return _index
            End Get
            Set(ByVal value As Double)
                For Each cc As Trinity.cCombinationChannel In Relations
                    If Not cc.Bookingtype.IsCompensation OrElse IsOnlyCompensation Then
                        cc.Bookingtype.IndexMainTarget = value
                    End If
                Next
            End Set
        End Property

        Public Property IndexSecondTarget() As Double
            Get
                Dim _index As Single = 0
                For Each cc As Trinity.cCombinationChannel In Relations
                    _index += cc.Bookingtype.IndexSecondTarget * cc.Percent
                Next
                Return _index
            End Get
            Set(ByVal value As Double)
                For Each cc As Trinity.cCombinationChannel In Relations
                    cc.Bookingtype.IndexSecondTarget = value
                Next
            End Set
        End Property

        Public Property IndexAllAdults() As Double
            Get
                Dim _index As Single = 0
                For Each cc As Trinity.cCombinationChannel In Relations
                    _index += cc.Bookingtype.IndexAllAdults * cc.Percent
                Next
                Return _index
            End Get
            Set(ByVal value As Double)
                For Each cc As Trinity.cCombinationChannel In Relations
                    cc.Bookingtype.IndexAllAdults = value
                Next
            End Set
        End Property

        Public Property IndexMainTargetStatus() As IndexStatusEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexMainTargetStatus
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets how the property IndexMainTarget was set
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexMainTarget_Error

                Return mvarIndexMainTargetStatus

                On Error GoTo 0
                Exit Property

IndexMainTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)
            End Get
            Set(ByVal value As IndexStatusEnum)
                On Error GoTo IndexMainTarget_Error

                mvarIndexMainTargetStatus = value

                On Error GoTo 0
                Exit Property

IndexMainTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)

            End Set
        End Property

        Public Property IndexSecondTargetStatus() As IndexStatusEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexSecondTarget
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets how the property IndexSecondTarget was set
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexSecondTarget_Error

                Return mvarIndexSecondTargetStatus

                On Error GoTo 0
                Exit Property

IndexSecondTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

            End Get
            Set(ByVal value As IndexStatusEnum)
                On Error GoTo IndexSecondTarget_Error

                mvarIndexSecondTargetStatus = value

                On Error GoTo 0
                Exit Property

IndexSecondTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

            End Set
        End Property

        Public Property IndexAllAdultsStatus() As IndexStatusEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexAllAdults
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets how the property IndexAllAdults was set
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexAllAdults_Error

                Return mvarIndexAllAdultsStatus

                On Error GoTo 0
                Exit Property

IndexAllAdults_Error:

                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)
            End Get
            Set(ByVal value As IndexStatusEnum)
                On Error GoTo IndexAllAdults_Error

                mvarIndexAllAdultsStatus = value

                On Error GoTo 0
                Exit Property

IndexAllAdults_Error:

                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)

            End Set
        End Property

        Private bolShowAsOne As Boolean

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLComboChannel As XmlElement
            For Each TmpCC As Trinity.cCombinationChannel In Me.Relations
                XMLComboChannel = xmlDoc.CreateElement("Channel")
                TmpCC.GetXML(XMLComboChannel, errorMessege, xmlDoc)
                colXml.AppendChild(XMLComboChannel)
            Next

            colXml.SetAttribute("ID", Me.ID)
            colXml.SetAttribute("Name", Me.Name)
            colXml.SetAttribute("CombinationOn", Me.CombinationOn)
            colXml.SetAttribute("ShowAsOne", Me.ShowAsOne)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving Combination (" & Me.Name & ")")
            Return False
        End Function


        Public Function BuyingTarget() As String
            Dim target As String = Relations(1).Bookingtype.BuyingTarget.TargetName

            For Each cc As Trinity.cCombinationChannel In Relations
                If Not target = cc.Bookingtype.BuyingTarget.TargetName Then
                    Return "-"
                    Exit Function
                End If
            Next

            Return target
        End Function

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Property ShowAsOne() As Boolean
            Get
                Return bolShowAsOne
            End Get
            Set(ByVal value As Boolean)
                bolShowAsOne = value
            End Set
        End Property

        Public Property ManualIndexes() As Boolean
            Get
                Return mvarManualIndexes
            End Get
            Set(ByVal value As Boolean)
                mvarManualIndexes = value
            End Set
        End Property

        Public Property ID() As String
            Get
                Return mvarID
            End Get
            Set(ByVal value As String)
                If Parent.Contains(mvarID) Then
                    Parent.Remove(mvarID)
                End If
                If Not Parent.Contains(value) Then
                    Parent.Add(Me, value)
                End If
                mvarID = value
            End Set
        End Property
        Public Property MarathonIDCombination() As String
            Get
                Return _MarathonIDCombination
            End Get
            Set(ByVal value As String)
                _MarathonIDCombination = value
            End Set
        End Property
        
        Public Property sendAsOneUnitTOMarathon() As Boolean
            Get
                Return _sendAsOneUnitToMarathon
            End Get
            Set(ByVal value As Boolean)
                _sendAsOneUnitToMarathon = value
            End Set
        End Property


        Public ReadOnly Property Relations() As cCombinationChannels
            Get
                Return mvarRelations
            End Get
        End Property

        Public Function IncludesBookingtype(ByVal BT As Trinity.cBookingType) As Boolean
            For Each TmpCC As cCombinationChannel In mvarRelations
                If TmpCC.Bookingtype Is BT Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private _autoUpdateRelations As Boolean
        ReadOnly Property AutoUpdateRelations As Boolean
            Get
                Return _autoUpdateRelations
            End Get
        End Property

        Public Sub New(ByVal MainObject As cKampanj, ByVal ParentColl As Collection, AutoUpdateRelations As Boolean)
            Parent = ParentColl
            Main = MainObject
            Main.RegisterProblemDetection(Me)
            mvarRelations = New cCombinationChannels(Main, Me)
            _autoUpdateRelations = AutoUpdateRelations
        End Sub



        Public Enum ProblemsEnum
            NoChannels = 1
            OnlyOneChannel = 2
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            If Relations.count = 0 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>A combination does not contain any channels</p>")
                _helpText.AppendLine("<p>The combination '" & Name & "' does not have any channels added to it.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, choose the 'Combinations'-tab, select the combination '" & Name & "' and add at least two channels to it</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoChannels, cProblem.ProblemSeverityEnum.Warning, "A combination does not contain any channels", Name, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If Relations.count = 1 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>A combination contains only one channel</p>")
                _helpText.AppendLine("<p>The combination '" & Name & "' only has one channel added to it</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, choose the 'Combinations'-tab, select the combination '" & Name & "' and add at least one more channel to it</p>")
                Dim _problem As New cProblem(ProblemsEnum.OnlyOneChannel, cProblem.ProblemSeverityEnum.Warning, "A combination contains only one channel", Name, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

    End Class

End Namespace
