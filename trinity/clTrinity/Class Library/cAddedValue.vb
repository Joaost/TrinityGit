Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cAddedValue
        Implements IDetectsProblems

        Public Enum ShowInEnum
            siBoth = 0
            siAllocate = 1
            siBooking = 2
        End Enum

        Public Name As String
        Public IndexNet As Single
        Public IndexGross As Single
        Private mvarID As String = ""
        Public ShowIn As ShowInEnum = ShowInEnum.siBoth
        Private mvarAmount(0 To 255) As Decimal
        Private Parent As cBookingType
        Private ParColl As Collection
        Private mvarUseThis As Boolean = True

        Public Property ID() As String
            Get
                Return mvarID
            End Get
            Set(ByVal value As String)
                If ParColl.Contains(mvarID) Then
                    ParColl.Remove(mvarID)
                    ParColl.Add(Me, value)
                End If
                mvarID = value
            End Set
        End Property

        Public Property Amount(ByVal week As Integer) As Decimal
            Get
                Amount = mvarAmount(week)
            End Get
            Set(ByVal value As Decimal)
                mvarAmount(week) = value
                If Parent.Weeks(week).TRPControl Then
                    Parent.Weeks(week).TRP = Parent.Weeks(week).TRP
                Else
                    Parent.Weeks(week).NetBudget = Parent.Weeks(week).NetBudget
                End If
            End Set
        End Property

        Friend Property Bookingtype() As cBookingType
            Get
                Return Parent
            End Get
            Set(ByVal value As cBookingType)
                Parent = value
            End Set
        End Property

        Public Sub New(ByVal MainObject As cKampanj, ByVal ParentColl As Collection)
            ParColl = ParentColl
            MainObject.RegisterProblemDetection(Me)
        End Sub

        Public Property UseThis() As Boolean
            Get
                Return mvarUseThis
            End Get
            Set(ByVal value As Boolean)
                mvarUseThis = value
            End Set
        End Property

        Public Enum ProblemsEnum
            LowNetIndex = 1
            LowGrossIndex = 2
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            If IndexNet < 10 Then
                Dim _helpText As New Text.StringBuilder
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Net index too low?</p>")
                _helpText.AppendLine("<p>The Net index on the Added Value '" & Name & "' on '" & Parent.ToString & "' is " & IndexNet & ", which seems very low.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, select the 'Index and Added values'-tab, select '" & Parent.ToString & "' and review the Net index on '" & Name & "'</p>")
                Dim _problem As New cProblem(ProblemsEnum.LowNetIndex, cProblem.ProblemSeverityEnum.Warning, "Net index too low?", Bookingtype.ToString & ": " & Name, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            If IndexGross < 10 Then
                Dim _helpText As New Text.StringBuilder
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Gross index too low?</p>")
                _helpText.AppendLine("<p>The Gross index on the Added Value '" & Name & "' on '" & Parent.ToString & "' is " & IndexGross & ", which seems very low.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, select the 'Index and Added values'-tab, select '" & Parent.ToString & "' and review the Gross index on '" & Name & "'</p>")
                Dim _problem As New cProblem(ProblemsEnum.LowNetIndex, cProblem.ProblemSeverityEnum.Warning, "Gross index too low?", Bookingtype.ToString & ": " & Name, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

    End Class
End Namespace