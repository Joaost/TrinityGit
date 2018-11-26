Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cErrorCollection

        Private Structure ErrorInformation
            Public sDescription As String
            Public sHelpContext As String
            Public sHelpFile As String
            Public lNumber As Long
            Public sSource As String
        End Structure

        Private Errors() As ErrorInformation

        Public lErrors As Long

        Public Sub Clear()
            Erase Errors
            lErrors = 0
        End Sub

        Public Function IsError() As Boolean
            If Err.Number <> 0 Then
                Add(Err.Number, Err.Source, Err.Description, Err.HelpContext, Err.HelpFile)
                IsError = True
            Else
                IsError = False
            End If
        End Function

        Public Sub Add( _
        Optional ByVal lErrNumber As Long = 0, _
        Optional ByVal sSource As String = "", _
        Optional ByVal sDescription As String = "", _
        Optional ByVal sHelpContext As String = "", _
        Optional ByVal sHelpFile As String = "" _
            )

            If lErrNumber = 0 Then
                If Err.Number = 0 Then Exit Sub
                lErrNumber = Err.Number
            End If
            ReDim Preserve Errors(lErrors)
            With Errors(lErrors)
                .sDescription = sDescription
                .sHelpContext = sHelpContext
                .sHelpFile = sHelpFile
                .lNumber = lErrNumber
                .sSource = sSource
            End With
            lErrors = lErrors + 1
        End Sub


        Public Sub Remove(Optional ByVal Index As Object = Nothing)
            Dim i As Long

            If Index Is Nothing Then
                If lErrors > 0 Then
                    ReDim Preserve Errors(lErrors - 1)
                    lErrors = lErrors - 1
                End If
            ElseIf IsNumeric(Index) Then
                If Index < 0 Or Index > lErrors - 1 Then Exit Sub
                For i = Index To lErrors - 1
                    Errors(i) = Errors(i + 1)
                Next
                ReDim Preserve Errors(lErrors - 1)
                lErrors = lErrors - 1
            End If

        End Sub

        Public ReadOnly Property ErrNumber(Optional ByVal Index As Object = Nothing) As Long
            Get
                If Index Is Nothing Then
                    If lErrors > 0 Then Index = lErrors - 1
                ElseIf IsNumeric(Index) Then
                    Return Errors(Index).lNumber
                End If
            End Get
        End Property

        Public ReadOnly Property Description(Optional ByVal Index As Object = Nothing) As Long
            Get
                If Index Is Nothing Then
                    If lErrors > 0 Then Index = lErrors - 1
                ElseIf IsNumeric(Index) Then
                    Description = Errors(Index).sDescription
                End If
            End Get
        End Property

        Public ReadOnly Property HelpContext(Optional ByVal Index As Object = Nothing) As Long
            Get
                If Index Is Nothing Then
                    If lErrors > 0 Then Index = lErrors - 1
                ElseIf IsNumeric(Index) Then
                    HelpContext = Errors(Index).sHelpContext
                End If
            End Get
        End Property

        Public ReadOnly Property HelpFile(Optional ByVal Index As Object = Nothing) As Long
            Get
                If Index Is Nothing Then
                    If lErrors > 0 Then Index = lErrors - 1
                ElseIf IsNumeric(Index) Then
                    HelpFile = Errors(Index).sHelpFile
                End If
            End Get
        End Property

        Public ReadOnly Property Source(Optional ByVal Index As Object = Nothing) As Long
            Get
                If Index Is Nothing Then
                    If lErrors > 0 Then Index = lErrors - 1
                ElseIf IsNumeric(Index) Then
                    Source = Errors(Index).sSource
                End If
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            Get
                Return lErrors
            End Get
        End Property




        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Clear()
        End Sub
    End Class
End Namespace