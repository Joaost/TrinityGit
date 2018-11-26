Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class Border

        Private _border As Excel.Border
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Border As Excel.Border)
            _border = Border
        End Sub

        Public WriteOnly Property Weight As Excel.XlBorderWeight
            Set(value As Excel.XlBorderWeight)
                Try
                    _border.GetType.InvokeMember("Weight", Reflection.BindingFlags.SetProperty, Nothing, _border, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property LineStyle As Excel.XlLineStyle
            Set(value As Excel.XlLineStyle)
                Try
                    _border.GetType.InvokeMember("LineStyle", Reflection.BindingFlags.SetProperty, Nothing, _border, New Object() {value}, _ci)
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
                    Return _border.GetType.InvokeMember("Color", Reflection.BindingFlags.GetProperty, Nothing, _border, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Integer)
                Try
                    _border.GetType.InvokeMember("Color", Reflection.BindingFlags.SetProperty, Nothing, _border, New Object() {value}, _ci)
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
                    _border.GetType.InvokeMember("ColorIndex", Reflection.BindingFlags.SetProperty, Nothing, _border, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property
    End Class
End Namespace
