Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class Window
        Private _window As Excel.Window
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Window As Excel.Window)
            _window = Window
        End Sub

        Public WriteOnly Property View As Excel.XlWindowView
            Set(value As Excel.XlWindowView)
                Try
                    _window.GetType.InvokeMember("View", Reflection.BindingFlags.SetProperty, Nothing, _window, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property Zoom As Integer
            Set(value As Integer)
                Try
                    _window.GetType.InvokeMember("Zoom", Reflection.BindingFlags.SetProperty, Nothing, _window, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property
    End Class
End Namespace