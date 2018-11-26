Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity
    ''' <summary>
    ''' 
    ''' </summary>
    Public Class cCombinationChannel
        Implements IDetectsProblems


        Private _bookingtype As cBookingType
        Private _channelName As String
        Private _bookingtypeName As String

        Public Relation As Single
        Public ID As String = Guid.NewGuid.ToString
        Dim ParentCollection As Collection
        Dim Main As cKampanj
        Dim Combination As cCombination

        Public Property Bookingtype As cBookingType
            Set(value As cBookingType)
                If Combination.AutoUpdateRelations Then
                    If _bookingtype IsNot Nothing Then
                        _bookingtype.Combination = Nothing
                    End If
                    If value IsNot Nothing Then value.Combination = Combination
                End If
                _bookingtype = value
            End Set
            Get
                Return _bookingtype
            End Get
        End Property


        ''' <summary>
        ''' Gets or sets the name of the channel. This is only used for combinations that belong to contracts.
        ''' </summary>
        ''' <value>
        ''' The name of the channel.
        ''' </value>
        Public Property ChannelName As String
            Get
                If _bookingtype IsNot Nothing Then
                    Return _bookingtype.ParentChannel.ChannelName
                End If
                Return _channelName
            End Get
            Set(value As String)
                _channelName = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name of the booking type. This is only used for combinations that belong to contracts. 
        ''' </summary>
        ''' <value>
        ''' The name of the booking type.
        ''' </value>
        Public Property BookingTypeName As String
            Get
                If _bookingtype IsNot Nothing Then
                    Return _bookingtype.Name
                End If
                Return _bookingtypeName
            End Get
            Set(value As String)
                _bookingtypeName = value
            End Set
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            colXml.SetAttribute("ID", Me.ID)
            colXml.SetAttribute("Chan", Me.Bookingtype.ParentChannel.ChannelName)
            colXml.SetAttribute("BT", Me.Bookingtype.Name)
            colXml.SetAttribute("Relation", Me.Relation)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving CombinationChannel (" & Me.ID & ")")
            Return False
        End Function

        Public Sub New(ByVal MainObject As cKampanj, ByVal ParentCombination As cCombination, ByVal Parent As Collection)
            ParentCollection = Parent
            Main = MainObject
            Combination = ParentCombination
            Main.RegisterProblemDetection(Me)
        End Sub

        Public Function Percent(Optional IncludeCompensation As Boolean = False) As Single
            Dim Tot As Single = 0
            Dim Count As Integer = 0

            For Each TmpCC As cCombinationChannel In ParentCollection
                If Not TmpCC.Bookingtype.IsPremium AndAlso (IncludeCompensation OrElse Not TmpCC.Bookingtype.IsCompensation OrElse Combination.IsOnlyCompensation) Then
                    Tot += TmpCC.Relation
                    Count += 1
                End If
            Next

            If Tot = 0 Then Return 1 / Count
            Return Relation / Tot
        End Function



        Public Enum ProblemsEnum
            UnusedBookingType = 1
            ZeroRelation = 2
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            If Bookingtype IsNot Nothing AndAlso Not Bookingtype.BookIt Then
                Dim _helpText As New Text.StringBuilder
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>A combination uses an unused bookingtype</p>")
                _helpText.AppendLine("<p>The combination '" & Combination.Name & "' has the bookingtype '" & Bookingtype.Name & "', but that bookingtype is not in the channel list.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Either:<ul><li>Open the 'Setup'-window, choose the 'Channels'-tab and add '" & Bookingtype.Name & "' to the list of channels</li></ul>- OR - <ul><li>Open the 'Setup'-window, choose the 'Combinations'-tab, select the combination '" & Combination.Name & " and remove '" & Bookingtype.ToString & "' from the list of connected channels</p>")
                Dim _problem As New cProblem(ProblemsEnum.UnusedBookingType, cProblem.ProblemSeverityEnum.Warning, "A combination uses an unused bookingtype", Combination.Name & ": " & Bookingtype.ToString, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If Relation = 0 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Combination relation set to zero</p>")
                _helpText.AppendLine("<p>All channels on a combination must have a relation value of at least 1. On '" & Combination.Name & "', the relation value is 0 on '" & ToString() & "'</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, choose the 'Combinations'-tab, select the combination '" & Combination.Name & " and set the relation value on '" & ToString() & "' to at least 1</p>")
                Dim _problem As New cProblem(ProblemsEnum.ZeroRelation, cProblem.ProblemSeverityEnum.Warning, "Combination relation set to zero", ToString, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

        Public Overrides Function ToString() As String
            If _bookingtype IsNot Nothing Then
                Return _bookingtype.ToString
            End If
            Return _channelName & " " & _bookingtypeName
        End Function

    End Class
End Namespace
