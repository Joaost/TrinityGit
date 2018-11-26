Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cCost
        Implements IDetectsProblems
        'a cost class, some costs are % of the investments others are a fixed sum. All types are handled.

        Public Enum CostTypeEnum
            CostTypeFixed = 0
            CostTypePercent = 1
            CostTypePerUnit = 2
            CostTypeOnDiscount = 3
        End Enum

        Public Enum CostOnUnitEnum
            CostOnSpots = 0
            CostOnBuyingTRP = 1
            CostOnMainTRP = 2
            CostOnWeeks = 3
        End Enum

        Public Enum CostOnPercentEnum
            CostOnMediaNet = 0
            CostOnNet = 1
            CostOnNetNet = 2
            CostOnRatecard = 3
        End Enum

        Private mvarCostName As String
        Private mvarAmount As Single
        Private mvarCountCostOn As Object
        Private mvarCostType As CostTypeEnum
        Private mvarMarathonID As String

        Public ID As String

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error


            colXml.SetAttribute("Name", Me.CostName)
            colXml.SetAttribute("ID", Me.ID)
            colXml.SetAttribute("Amount", Me.Amount)
            If Not Me.CountCostOn Is Nothing AndAlso Me.CountCostOn.GetType.FullName = "clTrinity.Trinity.cChannel" Then
                colXml.SetAttribute("CostOn", Me.CountCostOn.channelname)
            Else
                colXml.SetAttribute("CostOn", Me.CountCostOn)
            End If
            colXml.SetAttribute("CostType", Me.CostType)
            colXml.SetAttribute("MarathonID", Me.MarathonID)

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving Cost (" & Me.CostName & ")")
            Resume Next
        End Function

        Public Property CostName() As String
            'gets/sets the Cost name
            Get
                CostName = mvarCostName
            End Get
            Set(ByVal value As String)
                mvarCostName = value
            End Set
        End Property

        Public Property Amount() As Single
            'gets/sets the amount
            Get
                Amount = mvarAmount
            End Get
            Set(ByVal value As Single)
                mvarAmount = value
            End Set
        End Property

        Public Property CountCostOn() As Object
            'gets/sets on what the cost is calculated (gross net etc)
            'Changed type to Object after introduction of cost on Discount that needs to be on a cChannel. If CostType is OnDicount and CountCostOn
            'is Nothing, then all channels have the same percent
            Get
                Return mvarCountCostOn
            End Get
            Set(ByVal value As Object)
                mvarCountCostOn = value
            End Set
        End Property

        Public Property CostType() As CostTypeEnum
            'gets/sets what type of cost it is
            Get
                CostType = mvarCostType
            End Get
            Set(ByVal value As CostTypeEnum)
                mvarCostType = value
                If mvarCostType = CostTypeEnum.CostTypeOnDiscount Then
                    mvarCountCostOn = Nothing
                Else
                    mvarCountCostOn = 0
                End If
            End Set
        End Property

        Public Function FormattedAmount() As String
            If mvarCostType = CostTypeEnum.CostTypeFixed Or mvarCostType = CostTypeEnum.CostTypePerUnit Then
                FormattedAmount = Format(mvarAmount, "##,##0 kr")
            Else
                FormattedAmount = Format(mvarAmount, "##,##0.0%")
            End If
        End Function

        Public Function CostOnText() As String
            'a function for setting a string depending on what the cost is supposed to be calculated on
            '(if its a fixed/variable cost and when its goning to be debited
            If mvarCostType = CostTypeEnum.CostTypeFixed Then
                Return "-"
            ElseIf mvarCostType = CostTypeEnum.CostTypePercent Then
                Select Case mvarCountCostOn
                    Case CostOnPercentEnum.CostOnMediaNet : Return "Media Net"
                    Case CostOnPercentEnum.CostOnNet : Return "Net"
                    Case CostOnPercentEnum.CostOnNetNet : Return "Net Net"
                    Case CostOnPercentEnum.CostOnRatecard : Return "Ratecard"
                    Case Else : Return ""
                End Select
            ElseIf mvarCostType = CostTypeEnum.CostTypePerUnit Then
                Select Case mvarCountCostOn
                    Case CostOnUnitEnum.CostOnBuyingTRP : Return "Buy TRP"
                    Case CostOnUnitEnum.CostOnMainTRP : Return "Main TRP"
                    Case CostOnUnitEnum.CostOnSpots : Return "Spots"
                    Case CostOnUnitEnum.CostOnWeeks : Return "Weeks"
                    Case Else : Return ""
                End Select
            Else
                If CountCostOn Is Nothing Then
                    Return "All"
                Else
                    Return DirectCast(mvarCountCostOn, cChannel).Shortname
                End If
            End If
        End Function

        Public Property MarathonID() As String
            'gets/sets the MarathonID for the cost
            Get
                MarathonID = mvarMarathonID
            End Get
            Set(ByVal value As String)
                mvarMarathonID = value
            End Set
        End Property

        Public Sub New(ByVal Main As cKampanj)
            Main.RegisterProblemDetection(Me)
        End Sub

        Public Enum ProblemsEnum
            NoMarathonID = 1
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            Dim _helpText As New Text.StringBuilder

            If TrinitySettings.MarathonEnabled AndAlso CostType <> CostTypeEnum.CostTypePercent AndAlso MarathonID = 0 Then
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Cost does not have a Marathon ID</p>")
                _helpText.AppendLine("<p>The cost '" & CostName & "' does not have a MarathonID set. This will export it to Marathon as a Media cost. If this is the desired behaviour, then you can disregard this warning</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window and select the 'General'-tab. In the bottom most pane, enter a Marathon ID for the cost '" & CostName & "'</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoMarathonID, cProblem.ProblemSeverityEnum.Warning, "Cost does not have a Marathon ID", CostName, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound




    End Class
End Namespace