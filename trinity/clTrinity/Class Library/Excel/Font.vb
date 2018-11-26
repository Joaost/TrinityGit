Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class Font

        Dim _font As Excel.Font
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Font As Excel.Font)
            _font = Font
        End Sub

        Public Property Size As Single
            Get
                Try
                    Return _font.GetType.InvokeMember("Size", Reflection.BindingFlags.GetProperty, Nothing, _font, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Single)
                Try
                    _font.GetType.InvokeMember("Size", Reflection.BindingFlags.SetProperty, Nothing, _font, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Property Name As String
            Get
                Try
                    Return _font.GetType.InvokeMember("Name", Reflection.BindingFlags.GetProperty, Nothing, _font, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As String)
                Try
                    _font.GetType.InvokeMember("Name", Reflection.BindingFlags.SetProperty, Nothing, _font, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property Bold As Boolean
            Set(value As Boolean)
                Try
                    _font.GetType.InvokeMember("Bold", Reflection.BindingFlags.SetProperty, Nothing, _font, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property Italic As Boolean
            Set(value As Boolean)
                Try
                    _font.GetType.InvokeMember("Italic", Reflection.BindingFlags.SetProperty, Nothing, _font, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Property Color As Integer
            Get
                Try
                    Return _font.GetType.InvokeMember("Color", Reflection.BindingFlags.GetProperty, Nothing, _font, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Integer)
                Try
                    _font.GetType.InvokeMember("Color", Reflection.BindingFlags.SetProperty, Nothing, _font, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property ColorIndex As Integer
            Set(value As Integer)
                Try
                    _font.GetType.InvokeMember("ColorIndex", Reflection.BindingFlags.SetProperty, Nothing, _font, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property
    End Class
End Namespace